// Create new branch
// Test with more input layers (6)

// Update weights


using NeuralNetworks.Logic;
using NeuralNetworks.Logic.Implementation;
using NeuralNetworks.Entities;
using NeuralNetworks.Entities.DTO;
using NeuralNetworks.Entities.Implementation;
using System;
using System.Collections.Generic;

namespace NeuralNetworks
{
    /// <summary>
    /// C# Neural Network Example
    /// </summary>
    class Program
    {
        /// <summary>
        /// Logging Enabled
        /// </summary>
        private static bool _loggingEnabled = true;

        /// <summary>
        /// Neural Network Service
        /// </summary>
        private static INeuralNetworkProviderService _neuralNetworkService;

        /// <summary>
        /// Neural Network Processing Service
        /// </summary>
        private static INeuralNetworkProcessingService _neuralNetworkProcessingService;

        /// <summary>
        /// Neural Network Training Service
        /// </summary>
        private static INeuralNetworkTrainingService _neuralNetworkTrainingService;

        /// <summary>
        /// Main Prog Entry Point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // ToDo: Inject via DI
            _neuralNetworkService = new NeuralNetworkProviderService();
            _neuralNetworkProcessingService = new NeuralNetworkProcessingService();
            _neuralNetworkTrainingService = new NeuralNetworkTrainingService();

            var neuralNetwork = _neuralNetworkService.Create(2, GetNetworkOutputs(), GetInitialInputWeights(), GetInitialHiddenLayerWeights());
            _neuralNetworkProcessingService.Process(neuralNetwork, GetInitialInputs(), ActivationFunctions.Sigmoid);
            _neuralNetworkTrainingService.BackPropogate(neuralNetwork, BackPropagationFunctions.DistributeErrorPropotionedToWeight);

            WriteNeuronOutputsToConsole(neuralNetwork);

            Console.ReadLine();
        }

        /// <summary>
        /// Write Neuron Output To Console
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        private static void WriteNeuronOutputsToConsole(INeuralNetwork neuralNetwork)
        {
            if (neuralNetwork.OutputLayer != null)
            {
                for (int i = 0; i < neuralNetwork.OutputLayer.Count; i++)
                {
                    var outputNeuron = (OutputNeuron)neuralNetwork.OutputLayer[i];

                    WriteToConsole(string.Format("{0} : {1} : err {2}", outputNeuron.Description, outputNeuron.Input, outputNeuron.Error));
                }
            }

            if (neuralNetwork.InputLayer != null)
            {
                for (int i = 0; i < neuralNetwork.InputLayer.Count; i++)
                {
                    var inputNeuron = (WorkerNeuron)neuralNetwork.InputLayer[i];

                    WriteToConsole(string.Format("Input Neuron {0} : err {1}", i, inputNeuron.Error));
                }
            }
        }

        /// <summary>
        /// Write String to Console
        /// </summary>
        /// <param name="message">The Message</param>
        private static void WriteToConsole(string message)
        {
            if (_loggingEnabled)
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Gets Initial Inputs
        /// </summary>
        /// <returns>Initial Inputs</returns>
        private static double[] GetInitialInputs()
        {
            // ToDo: Get from DB
            return new double[] { 0.99, 0.01, };
        }

        /// <summary>
        /// Gets Initial Weights
        /// </summary>
        /// <returns>Initial Weights</returns>
        private static double[] GetInitialInputWeights()
        {
            return new double[] { 0.7,0.2,0.5,0.3 };
        }

        /// <summary>
        /// Gets Initial Weights
        /// </summary>
        /// <returns>Initial Weights</returns>
        private static double[] GetInitialHiddenLayerWeights()
        {
            return new double[] { 0.4,0.3,0.6,0.2 };
        }

        /// <summary>
        /// Get Network Outputs
        /// </summary>
        /// <returns>Network Output List</returns>
        private static List<NetworkOutput> GetNetworkOutputs()
        {
            var networkOutputs = new List<NetworkOutput>();

            networkOutputs.Add(new NetworkOutput() { Number = 1, Description = "On", ExpectedOutput = 0.99 });
            networkOutputs.Add(new NetworkOutput() { Number = 2, Description = "Off", ExpectedOutput = 0.01 });

            return networkOutputs;
        }
    }
}
