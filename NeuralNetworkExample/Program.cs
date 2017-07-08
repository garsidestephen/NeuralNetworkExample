using System;
using NeuralNetworkExample.BL;
using NeuralNetworkExample.BL.Implementation;
using NeuralNetworkExample.Entities;
using System.Collections.Generic;
using NeuralNetworkExample.Entities.DTO;

namespace NeuralNetworkExample
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
        private static INeuralNetworkService _neuralNetworkService;

        /// <summary>
        /// Main Prog Entry Point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            // ToDo: Inject via DI
            _neuralNetworkService = new NeuralNetworkService();

            var neuralNetwork = _neuralNetworkService.Create(2, 2, GetNetworkOutputs(), GetInitialWeights());

            _neuralNetworkService.Process(neuralNetwork, GetInitialInputs(), ActivationFunctions.Sigmoid);

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
                for (int i = 0; i < neuralNetwork.OutputLayer.OutputNeurons.Count; i++)
                {
                    var outputNeuron = neuralNetwork.OutputLayer.OutputNeurons[i];

                    WriteToConsole(string.Format("{0} : {1} : err {2}", outputNeuron.Description, outputNeuron.ActualOutput, outputNeuron.Error));
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
        private static string GetInitialWeights()
        {
            return "0.7,0.5,0.2,0.3, 0.4,0.6,0.3,0.2";
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
