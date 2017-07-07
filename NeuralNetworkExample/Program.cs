using System;
using NeuralNetworkExample.BL;
using NeuralNetworkExample.BL.Implementation;
using NeuralNetworkExample.Entities;

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

            var neuralNetwork = _neuralNetworkService.Create(3, 2, new string[] { "Yes", "No", "Not Sure" });
            _neuralNetworkService.Process(neuralNetwork, GetInitialInputs());

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

                    WriteToConsole(string.Format("{0} : {1}", outputNeuron.Description, outputNeuron.ActualOutput));
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
            return new double[] { 0.9, 0.1, 0.8 };
        }
    }
}
