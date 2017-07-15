using NeuralNetworks.Entities;
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
        /// Back Propogate
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="backPropagationErrorCalculationFunction">Back Propagation Error Calculation Function</param>
        public void BackPropogate(INeuralNetwork neuralNetwork, Func<double, double, double[], double> backPropagationErrorCalculationFunction)
        {
            PrepareNetworkForBackPropagation(neuralNetwork);

            BackPropogateLayer(neuralNetwork.HiddenLayer, neuralNetwork.OutputLayer, backPropagationErrorCalculationFunction);
            BackPropogateLayer(neuralNetwork.InputLayer, neuralNetwork.HiddenLayer, backPropagationErrorCalculationFunction);
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
                neuron.weightErrors = new double[neuron.weights.Length];
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
                    currentLayerWeightsForCurrentForwardNeuron[j] = currentLayer.ElementAt(j).weights[i];
                }

                // Calculate error weights for each weight going into forward neuron
                double[] errorWeights = new double[currentLayerNeuronsCount];

                //for (int j = 0; j < currentLayerNeuronsCount; j++)
                //{
                //    errorWeights[j] = currentForwardNeuron.Error * (currentLayerWeightsForCurrentForwardNeuron[j] / currentLayerWeightsForCurrentForwardNeuron.Sum());
                //}

                for (int j = 0; j < currentLayerNeuronsCount; j++)
                {
                    errorWeights[j] =  backPropagationErrorCalculationFunction(currentForwardNeuron.Error, currentLayerWeightsForCurrentForwardNeuron[j], currentLayerWeightsForCurrentForwardNeuron);
                }

                // Add error weights to relevant neuron in current layer
                for (int j = 0; j < currentLayerNeuronsCount; j++)
                {
                    currentLayer.ElementAt(j).weightErrors[i] = errorWeights[j];
                }

            }

            // Calculate combined Error of current neuron
            for (int i = 0; i < currentLayerNeuronsCount; i++)
            {
                var currentLayerNeuron = currentLayer.ElementAt(i);
                currentLayerNeuron.Error = currentLayerNeuron.weightErrors.Sum();
            }
        }
    }
}
