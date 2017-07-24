// Consider passing training data into Train (via DTO) - will this be the same shape every time i.e. double[] 's

using NeuralNetworks.Entities;
using NeuralNetworks.Entities.DTO;
using NeuralNetworks.Entities.Implementation;
using NeuralNetworks.Logic;
using NeuralNetworks.Logic.Functions;
using NeuralNetworks.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworks
{
    /// <summary>
    /// C# Neural Network Example
    /// </summary>
    class Program
    {
         /// <summary>
        /// If in test mode then changes to a 6, 4, 2 network
        /// </summary>
        private static bool _inTestMode = false;

        /// <summary>
        /// Main Prog Entry Point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var neuralNetworkService = new NeuralNetworkProviderService();
            var neuralNetworkTrainingService = new NeuralNetworkTrainingService();
            var neuralNetworkProcessingService = new NeuralNetworkProcessingService();

            int numInitialInputs = _inTestMode ? 6 : 1;

            // Create a Neural Network
            var neuralNetwork = neuralNetworkService.Create(numInitialInputs, GetNetworkOutputs(), 0.2, GetInitialInputLayerWeights(), GetInitialHiddenLayerWeights());

            // Create a Training Profile to train our Network
            var trainingProfile = new TrainingProfile()
            {
                ActivationFn = ActivationFunctions.Sigmoid,
                BackPropagationErrorCalculationFn = BackPropagationFunctions.DistributeErrorProportionedToWeight,
                WeightCalculationFn = BackPropagationFunctions.RecalculateWeight,
                Data = GetTrainingData()
            };

            // Train network
            neuralNetworkTrainingService.Train(neuralNetwork, trainingProfile, true);

            // Test Trained network
            neuralNetworkProcessingService.Process(neuralNetwork, new double[1] { 1 }, ActivationFunctions.Sigmoid, true);
            neuralNetworkProcessingService.Process(neuralNetwork, new double[1] { -1 }, ActivationFunctions.Sigmoid, true);

            // Output network trace to console
            WriteNetworkInfoToConsole(neuralNetwork);

            Console.ReadLine();
        }

        /// <summary>
        /// Write Neuron Output To Console
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        private static void WriteNetworkInfoToConsole(INeuralNetwork neuralNetwork)
        {
            if (neuralNetwork != null)
            {
                for (int i = 0; i < neuralNetwork.TraceLog.Count; i++)
                {
                    Console.WriteLine(neuralNetwork.TraceLog[i]);
                }
            }
        }

        /// <summary>
        /// Get Training Data
        /// </summary>
        /// <returns>List of Training data</returns>
        private static IList<TrainingData> GetTrainingData()
        {
            IList<TrainingData> trainingData = new List<TrainingData>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (i == 0)
                    {
                        trainingData.Add(new TrainingData() { Inputs = new double[1] { 1 }, Outputs = new double[2] { 0.99, 0.01 } });
                    }
                    else
                    {
                        trainingData.Add(new TrainingData() { Inputs = new double[1] { -1 }, Outputs = new double[2] { 0.01, 0.99 } });
                    }
                }
            }

            return trainingData;
        }

        /// <summary>
        /// Gets Initial Weights
        /// </summary>
        /// <returns>Initial Weights</returns>
        private static double[] GetInitialInputLayerWeights()
        {
            if (_inTestMode)
            {
                return new double[] { 0.7, 0.2, 0.5, 0.3, 0.6, 0.26, 0.45, 0.3, 0.7, 0.21, 0.5, 0.3, 0.76, 0.28, 0.53, 0.3, 0.733, 0.23, 0.544, 0.33, 0.78, 0.22, 0.51, 0.33 };
            }

            //return new double[] { 0.7, 0.2, 0.5, 0.3 };
            return new double[] { 0.7, 0.2 };
        }

        /// <summary>
        /// Gets Initial Weights
        /// </summary>
        /// <returns>Initial Weights</returns>
        private static double[] GetInitialHiddenLayerWeights()
        {
            if (_inTestMode)
            {
                return new double[] { 0.4, 0.3, 0.6, 0.2, 0.4, 0.3, 0.6, 0.2 };
            }

            //return new double[] { 0.4, 0.3, 0.6, 0.2 };
            return new double[] { 0.4, 0.3, 0.3, 0.4 };
        }

        /// <summary>
        /// Get Network Outputs
        /// </summary>
        /// <returns>Network Output List</returns>
        private static List<NetworkOutput> GetNetworkOutputs()
        {
            var networkOutputs = new List<NetworkOutput>();

            networkOutputs.Add(new NetworkOutput() { Number = 1, Description = "On" });
            networkOutputs.Add(new NetworkOutput() { Number = 2, Description = "Off" });

            return networkOutputs;
        }
    }
}
