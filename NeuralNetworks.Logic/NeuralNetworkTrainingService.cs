using NeuralNetworks.DTO;
using NeuralNetworks.Entities;
using NeuralNetworks.Logic.Abstractions;
using NeuralNetworks.Logic.ActivationFunctions;
using NeuralNetworks.Logic.ActivationFunctions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Neural Network Training Service
    /// </summary>
    public class NeuralNetworkTrainingService : INeuralNetworkTrainingService
    {
        /// <summary>
        /// Private Neural Network Processing Service
        /// </summary>
        private readonly INeuralNetworkProcessingService _neuralNetworkProcessingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuralNetworkProcessingService"></param>
        public NeuralNetworkTrainingService(INeuralNetworkProcessingService neuralNetworkProcessingService)
        {
            _neuralNetworkProcessingService = neuralNetworkProcessingService;
        }

        /// <summary>
        /// Train Neural Network (N.B Do not train for just 1 example, always randomize training data)
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="trainingData">Training Data</param>
        /// <param name="epochs">Number of Training Epochs</param>
        /// <param name="activationFn">Activation Function</param>
        /// <param name="learningRate">Learning Rate</param>
        /// <returns>Results Array</returns>
        public double[][] Train(NeuralNetwork neuralNetwork, TrainingDataDTO trainingData, int epochs, IActivationFunction activationFn, double learningRate)
        {
            // Get / create some inital vars for results obj
            int arrayWidth = trainingData.Inputs.GetLength(0) + trainingData.ExpectedResults.GetLength(0);
            double[][] results = new double[epochs * trainingData.Inputs.Length][];
            int counter = 0;

            for (int i = 0; i < epochs; i++)
            {
                for (int j = 0; j < trainingData.Inputs.Length; j++)
                {
                    double[] inputs = trainingData.Inputs[j];
                    double[] expectedResults = trainingData.ExpectedResults[j];
                    _neuralNetworkProcessingService.ProcessFeedForwardNeuralNetwork(neuralNetwork, inputs, activationFn);

                    BackPropagate(neuralNetwork, expectedResults, activationFn, learningRate);

                    // Populate results obj
                    results[counter] = 
                        inputs.Concat(expectedResults) // Inputs and Expected Results
                        .Concat(neuralNetwork.OutputLayer.Select(x => x.Output))  // Actual Outputs
                        .Concat(new double[1] { neuralNetwork.GlobalError }).ToArray(); // Overall Global Network Error

                    counter++;
                }
            }

            return results;
        }

        /// <summary>
        /// Back Propagate
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="expectedResults">Expected Results</param>
        /// <param name="activationFn">Activation Function</param>
        /// <param name="learningRate">Learning Rate (Not yet implemented)</param>
        private static void BackPropagate(NeuralNetwork neuralNetwork, double[] expectedResults, IActivationFunction activationFn, double learningRate)
        {
            // Calculate the error of each output neuron vs target
            for (int i = 0; i < neuralNetwork.OutputLayer.Count; i++)
            {
                neuralNetwork.OutputLayer[i].Error = activationFn.derivative(neuralNetwork.OutputLayer[i].Output) * (expectedResults[i] - neuralNetwork.OutputLayer[i].Output);
            }

            // Calc the global network error
            neuralNetwork.GlobalError = neuralNetwork.OutputLayer.Sum(x => x.Error);

            // Re-calc Output Layer weights
            for (int i = 0; i < neuralNetwork.OutputLayer.Count; i++)
            {
                AdjustNeuronWeights(neuralNetwork.OutputLayer[i], neuralNetwork.GlobalError);
            }

            // Recalc Hidden Layer weights
            // Calc Hidden Layer Errors
            for (int i = 0; i < neuralNetwork.HiddenLayer.Count; i++)
            {
                double totalError = 0;

                for (int j = 0; j < neuralNetwork.OutputLayer.Count; j++)
                {
                    totalError += activationFn.derivative(neuralNetwork.HiddenLayer[i].Output) * neuralNetwork.GlobalError * neuralNetwork.OutputLayer[j].Weights[i];
                }

                neuralNetwork.HiddenLayer[i].Error = totalError;
            }

            // then adjusts the hidden neurons' weights, based on their errors
            for (int i = 0; i < neuralNetwork.HiddenLayer.Count; i++)
            {
                AdjustNeuronWeights(neuralNetwork.HiddenLayer[i]);
            }
        }
        /// <summary>
        /// Adjust Neuron Weights
        /// </summary>
        /// <param name="neuron">The Neuron</param>
        /// <param name="error">The Error</param>
        private static void AdjustNeuronWeights(Neuron neuron, double error)
        {
            for (int i = 0; i < neuron.Weights.Length; i++)
            {
                neuron.Weights[i] += error * neuron.Inputs[i];
            }

            neuron.Bias += error;
        }

        /// <summary>
        /// Adjust Neuron Weights
        /// </summary>
        /// <param name="neuron">The Neuron</param>
        private static void AdjustNeuronWeights(Neuron neuron)
        {
            for (int i = 0; i < neuron.Weights.Length; i++)
            {
                neuron.Weights[i] += neuron.Error * neuron.Inputs[i];
            }

            neuron.Bias += neuron.Error;
        }
    }
}
