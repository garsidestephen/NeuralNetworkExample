using NeuralNetworks.Entities;
using NeuralNetworks.Entities.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworks.Logic.Implementation
{
    /// <summary>
    /// Neural Network Training Service
    /// </summary>
    public class NeuralNetworkTrainingService : INeuralNetworkTrainingService
    {
        /// <summary>
        /// Neural Network Processing Service
        /// </summary>
        private static INeuralNetworkProcessingService _neuralNetworkProcessingService;

        /// <summary>
        /// 
        /// </summary>
        public NeuralNetworkTrainingService()
        {
            _neuralNetworkProcessingService = new NeuralNetworkProcessingService();
        }

        /// <summary>
        /// Train a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="trainingProfile">Training Profile</param>
        /// <param name="traceActivity">Trace Activity</param>
        public void Train(INeuralNetwork neuralNetwork, TrainingProfile trainingProfile, bool traceActivity = false)
        {
            if (neuralNetwork != null && trainingProfile != null && trainingProfile.Data != null)
            {
                // Iterate through training data and perform runs
                for (int i = 0; i < trainingProfile.Data.Count; i++)
                {
                    // Set expected outputs for this training run
                    for (int j = 0; j < neuralNetwork.OutputLayer.Count; j++)
                    {
                        neuralNetwork.OutputLayer[j].ExpectedOutput = trainingProfile.Data[i].Outputs[j];
                    }

                    // Start training run
                    _neuralNetworkProcessingService.Process(neuralNetwork, trainingProfile.Data[i].Inputs, trainingProfile.ActivationFn);

                    // Back propagate any errors
                    BackPropogate(neuralNetwork, trainingProfile.BackPropagationErrorCalculationFn, trainingProfile.WeightCalculationFn);

                    if (traceActivity)
                    {
                        _neuralNetworkProcessingService.WriteCurrentStateToTraceLog(neuralNetwork);
                        neuralNetwork.TraceLog.Add(string.Format("---------- End of Iteration {0} ----------", i));
                    }
                }

                if (traceActivity)
                {
                    neuralNetwork.TraceLog.Add("----End of Training -----");
                }
            }
        }

        /// <summary>
        /// Back Propogate Errors
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="backPropagationErrorCalculationFn">Back Propagation Error Calculation Function</param>
        /// <param name="weightCalculationFn">Weight Calculation Function</param>
        private static void BackPropogate(INeuralNetwork neuralNetwork, Func<double, double, double[], double> backPropagationErrorCalculationFn, Func<double, double, double, double, double, double> weightCalculationFn)
        {
            PrepareNetworkForBackPropagation(neuralNetwork);

            BackPropogateLayer(neuralNetwork.HiddenLayer, neuralNetwork.OutputLayer, backPropagationErrorCalculationFn);
            BackPropogateLayer(neuralNetwork.InputLayer, neuralNetwork.HiddenLayer, backPropagationErrorCalculationFn);

            RecalculateWeights(neuralNetwork.LearningRate, neuralNetwork.HiddenLayer, neuralNetwork.OutputLayer, weightCalculationFn);
            RecalculateWeights(neuralNetwork.LearningRate, neuralNetwork.InputLayer, neuralNetwork.HiddenLayer, weightCalculationFn);
        }

        /// <summary>
        /// Prepare Network For Back Propagation
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        private static void PrepareNetworkForBackPropagation(INeuralNetwork neuralNetwork)
        {
            CreateWeightErrorArrays(neuralNetwork.InputLayer);
            CreateWeightErrorArrays(neuralNetwork.HiddenLayer);
        }

        /// <summary>
        /// Create Weight Error Arrays
        /// </summary>
        /// <param name="workerNeuronList">Worker Neuron List</param>
        private static void CreateWeightErrorArrays(IList<IWorkerNeuron> workerNeuronList)
        {
            for (int i = 0; i < workerNeuronList.Count; i++)
            {
                var neuron = workerNeuronList[i];
                neuron.WeightErrors = new double[neuron.Weights.Length];
            }
        }

        /// <summary>
        /// Back Propogate Layer
        /// </summary>
        /// <param name="currentLayer">Current Layer</param>
        /// <param name="forwardLayer">Forward Layer</param>
        /// <param name="backPropagationErrorCalculationFunction">Back Propagation Error Calculation Function</param>
        private static void BackPropogateLayer(IEnumerable<IWorkerNeuron> currentLayer, IEnumerable<INeuron> forwardLayer, Func<double, double, double[], double> backPropagationErrorCalculationFunction)
        {
            int currentLayerNeuronsCount = currentLayer.Count();
            int forwardLayerNeuronsCount = forwardLayer.Count();

            for (int i = 0; i < forwardLayerNeuronsCount; i++)
            {
                var currentForwardNeuron = forwardLayer.ElementAt(i);

                double[] currentLayerWeightsForCurrentForwardNeuron = new double[currentLayerNeuronsCount];

                for (int j = 0; j < currentLayerNeuronsCount; j++)
                {
                    currentLayerWeightsForCurrentForwardNeuron[j] = currentLayer.ElementAt(j).Weights[i];
                }

                // Calculate error weights for each weight going into forward neuron
                double[] errorWeights = new double[currentLayerNeuronsCount];

                for (int j = 0; j < currentLayerNeuronsCount; j++)
                {
                    errorWeights[j] = backPropagationErrorCalculationFunction(currentForwardNeuron.Error, currentLayerWeightsForCurrentForwardNeuron[j], currentLayerWeightsForCurrentForwardNeuron);
                }

                // Add error weights to relevant neuron in current layer
                for (int j = 0; j < currentLayerNeuronsCount; j++)
                {
                    currentLayer.ElementAt(j).WeightErrors[i] = errorWeights[j];
                }
            }

            // Calculate combined Error of current neuron
            for (int i = 0; i < currentLayerNeuronsCount; i++)
            {
                var currentLayerNeuron = currentLayer.ElementAt(i);
                currentLayerNeuron.Error = currentLayerNeuron.WeightErrors.Sum();
            }
        }

        /// <summary>
        /// Recalculate Weights
        /// </summary>
        /// <param name="learningRate">Learning Rate</param>
        /// <param name="currentLayer">Current Layer</param>
        /// <param name="forwardLayer">Forward Layer</param>
        /// <param name="weightCalculationFn">Weight Calculation Fn</param>
        private static void RecalculateWeights(double learningRate, IEnumerable<IWorkerNeuron> currentLayer, IEnumerable<INeuron> forwardLayer, Func<double, double, double, double, double, double> weightCalculationFn)
        {
            int currentLayerNeuronsCount = currentLayer.Count();
            int forwardLayerNeuronsCount = forwardLayer.Count();

            for (int i = 0; i < forwardLayerNeuronsCount; i++)
            {
                var currentForwardNeuron = forwardLayer.ElementAt(i);

                for (int j = 0; j < currentLayerNeuronsCount; j++)
                {
                    var currentLayerNeuron = currentLayer.ElementAt(j);
                    double currentWeight = currentLayerNeuron.Weights[i];
                    double weightedOutput = currentLayerNeuron.WeightedOutputs[i];

                    currentLayerNeuron.Weights[i] = weightCalculationFn(currentWeight, currentForwardNeuron.Error, currentForwardNeuron.Input, weightedOutput, learningRate);
                }
            }
        }
    }
}
