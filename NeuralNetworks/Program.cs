using System;
using NeuralNetworks.Data;
using NeuralNetworks.Data.Abstractions;
using NeuralNetworks.Logic;
using NeuralNetworks.Logic.Abstractions;
using NeuralNetworks.Logic.ActivationFunctions;

namespace NeuralNetworks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the Network
            // Initiate Weights
            double[] hiddenLayerWeights = new double[] { 0.38286575972236, 0.441739972420847, 0.00519391335788831, 0.152334728814817 };
            double[] hiddenLayerBias = new double[] { 0.79060369394282, 0.25095684418965 };
            double[] outputLayerWeights = new double[] { 0.100294556049767, 0.24866028653861 };
            double[] outputLayerBias = new double[] { 0.97103805186741 };

            //ToDo: Inject
            INeuralNetworkFactory neuralNetworkFactory = new NeuralNetworkFactory();
            var neuralNetwork = neuralNetworkFactory.Create(2, 1, 2, hiddenLayerWeights, outputLayerWeights, hiddenLayerBias, outputLayerBias);

            // Train the Network
            //ToDo: Inject
            ITrainingDataRepo trainingDataRepo = new TrainingDataRepo();
            var trainingData = trainingDataRepo.GetTrainingDataXOR();

            //ToDo: Inject
            INeuralNetworkTrainingService trainingService = new NeuralNetworkTrainingService(new NeuralNetworkProcessingService());
            double[][] trainingResults = trainingService.Train(neuralNetwork, trainingData, 4000, new Sigmoid(), 1);

            OutputResultsToConsole(trainingResults);
            Console.WriteLine("Training Complete ---------------");

            // Test the Trained Network
            //ToDo: Inject
            INeuralNetworkProcessingService processingService = new NeuralNetworkProcessingService();
            double[] testInputs1 = { 0, 0 };
            double[][] testResults1 = { testInputs1, processingService.ProcessFeedForwardNeuralNetwork(neuralNetwork, testInputs1, new Sigmoid()) };
            Console.WriteLine("Test1 Results (0 XOR 0) ---------------");
            OutputResultsToConsole(testResults1);

            double[] testInputs2 = { 1, 0 };
            double[][] testResults2 = { testInputs2, processingService.ProcessFeedForwardNeuralNetwork(neuralNetwork, testInputs2, new Sigmoid()) };
            Console.WriteLine("Test2 Results (1 XOR 0) ---------------");
            OutputResultsToConsole(testResults2);

            double[] testInputs3 = { 0, 1 };
            double[][] testResults3 = { testInputs3, processingService.ProcessFeedForwardNeuralNetwork(neuralNetwork, testInputs3, new Sigmoid()) };
            Console.WriteLine("Test3 Results (0 XOR 1) ---------------");
            OutputResultsToConsole(testResults3);

            double[] testInputs4 = { 1, 1 };
            double[][] testResults4 = { testInputs4, processingService.ProcessFeedForwardNeuralNetwork(neuralNetwork, testInputs4, new Sigmoid()) };
            Console.WriteLine("Test4 Results (1 XOR 1) ---------------");
            OutputResultsToConsole(testResults4);

            Console.ReadLine();
        }

        /// <summary>
        /// Output Results To Console
        /// </summary>
        /// <param name="results">Results</param>
        private static void OutputResultsToConsole(double[][] results)
        {
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine(string.Join(", ", results[i]));
            }
        }
    }
}

