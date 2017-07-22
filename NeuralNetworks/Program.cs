using NeuralNetworks.Entities;
using NeuralNetworks.Entities.DTO;
using NeuralNetworks.Entities.Implementation;
using NeuralNetworks.Logic;
using NeuralNetworks.Logic.Functions;
using NeuralNetworks.Logic.Implementation;
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
        /// Training Data
        /// </summary>
        private static IList<TrainingDataDTO> _trainingData;

        /// <summary>
        /// Number of Training Iterations
        /// </summary>
        private static int _overallTrainingIterations = 2000; 

        /// <summary>
        /// Training Iteration Midway Point
        /// </summary>
        private static int _trainingIterationMidwayPoint = 1000;

        /// <summary>
        /// 
        /// </summary>
        private static bool _outputCalculations = false;

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
            // ToDo: Inject via DI
            _neuralNetworkService = new NeuralNetworkProviderService();
            _neuralNetworkProcessingService = new NeuralNetworkProcessingService();
            _neuralNetworkTrainingService = new NeuralNetworkTrainingService();

            int numInitialInputs = _inTestMode ? 6 : 1;

            var neuralNetwork = _neuralNetworkService.Create(numInitialInputs, GetNetworkOutputs(), 0.2, GetInitialInputLayerWeights(), GetInitialHiddenLayerWeights());

            var trainingProfile = new TrainingProfile() 
            { 
                ActivationFn = ActivationFunctions.Sigmoid,  
                BackPropagationErrorCalculationFn = BackPropagationFunctions.DistributeErrorProportionedToWeight, 
                WeightCalculationFn = ErrorCalculationFunctions.RecalculateWeight
            };

            for (int trainingIterations = 1; trainingIterations < _overallTrainingIterations; trainingIterations++)
            {
                var trainingData = GetTrainingData(trainingIterations);
                trainingProfile.Inputs = trainingData.Inputs;
                trainingProfile.ExpectedOutputs = trainingData.Outputs;

                _neuralNetworkTrainingService.Train(neuralNetwork, trainingProfile);

                if (_outputCalculations)
                {
                    WriteNetworkInfoToConsole(neuralNetwork);

                    Console.WriteLine(); Console.WriteLine();
                    WriteToConsole(string.Format("---------- End of Iteration {0} ----------", trainingIterations));
                    Console.WriteLine();
                }
            }

            Console.WriteLine("----End of Training -----");
            _neuralNetworkProcessingService.Process(neuralNetwork, new double[1] { 1 }, ActivationFunctions.Sigmoid);
            WriteNetworkInfoToConsole(neuralNetwork);
            _neuralNetworkProcessingService.Process(neuralNetwork, new double[1] { -1 }, ActivationFunctions.Sigmoid);
            WriteNetworkInfoToConsole(neuralNetwork);

            Console.ReadLine();
        }

        /// <summary>
        /// Write Neuron Output To Console
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        private static void WriteNetworkInfoToConsole(INeuralNetwork neuralNetwork)
        {
            if (neuralNetwork != null && neuralNetwork.InputLayer != null && neuralNetwork.HiddenLayer != null && neuralNetwork.OutputLayer != null)
            {
                // Output the inputs
                WriteToConsole("Inputs:");
                for (int i = 0; i < neuralNetwork.InputLayer.Count; i++)
                {
                    var inputNeuron = (WorkerNeuron)neuralNetwork.InputLayer[i];

                    WriteToConsole(string.Format("Neuron {0} | Val: {1}", i, inputNeuron.Input));
                }

                Console.WriteLine();

                // Run > Outputs
                WriteToConsole("Outputs:");
                for (int i = 0; i < neuralNetwork.OutputLayer.Count; i++)
                {
                    var outputNeuron = (OutputNeuron)neuralNetwork.OutputLayer[i];

                    WriteToConsole(string.Format("Neuron {0} ({1}) : {2} : err {3}", i, outputNeuron.Description, outputNeuron.Input, outputNeuron.Error));
                }

                Console.WriteLine();

                // Write out Weight Info
                WriteToConsole("Input Layer Weights");
                WriteToConsole("Initial:");
                WriteArrayToConsole(GetInitialInputLayerWeights());
                WriteToConsole("Trained:");
                foreach (IWorkerNeuron neuron in neuralNetwork.InputLayer)
                {
                    WriteArrayToConsole(neuron.Weights, false);
                }

                Console.WriteLine(); Console.WriteLine();

                WriteToConsole("Hidden Layer Weights");
                WriteToConsole("Initial:");
                WriteArrayToConsole(GetInitialHiddenLayerWeights());
                WriteToConsole("Trained:");
                foreach (IWorkerNeuron neuron in neuralNetwork.HiddenLayer)
                {
                    WriteArrayToConsole(neuron.Weights, false);
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

        private static TrainingDataDTO GetTrainingData(int index)
        {
            if (_trainingData == null)
            {
                _trainingData = new List<TrainingDataDTO>();

                _trainingData.Add(new TrainingDataDTO() { Inputs = new double[1] { 1 }, Outputs = new double[2] { 0.99, 0.01 } });
                _trainingData.Add(new TrainingDataDTO() { Inputs = new double[1] { -1 }, Outputs = new double[2] { 0.01, 0.99 } });
            }

            if (index < _trainingIterationMidwayPoint)
            {
                return _trainingData[0];
            }

            return _trainingData[1];
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

        private static void WriteArrayToConsole(double[] matrix, bool addNewLine = true)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                double d = matrix[i];
                Console.Write(string.Format("{0}, ", d.ToString("F12")));
            }

            if (addNewLine)
            {
                Console.WriteLine();
            }
        }
    }
}
