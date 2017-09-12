using System;
using NeuralNetworks.Entities;
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
            // Weights
            double[] hiddenLayerWeights = new double[] { 0.38286575972236, 0.441739972420847, 0.00519391335788831, 0.152334728814817 };
            double[] hiddenLayerBias = new double[] { 0.79060369394282, 0.25095684418965 };
            double[] outputLayerWeights = new double[] { 0.100294556049767, 0.24866028653861 };
            double[] outputLayerBias = new double[] { 0.97103805186741 };

            //ToDo: Inject
            INeuralNetworkFactory neuralNetworkFactory = new NeuralNetworkFactory();
            var neuralNetwork = neuralNetworkFactory.Create(2, 1, 2, hiddenLayerWeights, outputLayerWeights, hiddenLayerBias, outputLayerBias);

            // Train the Network
            double[] data1 = { 0, 0 };
            double[] data2 = { 0, 1 };
            double[] data3 = { 1, 0 };
            double[] data4 = { 1, 1 };

            double[][] trainingData = { data1, data2, data3, data4 };

            double[] result1 = { 0 };
            double[] result2 = { 1 };
            double[] result3 = { 1 };
            double[] result4 = { 0 };
            double[][] expectedResults = { result1, result2, result3, result4 };

            //ToDo: Inject
            INeuralNetworkTrainingService trainingService = new NeuralNetworkTrainingService(new NeuralNetworkProcessingService());
            trainingService.Train(neuralNetwork, trainingData, expectedResults, 2000, new Sigmoid(), 1);

            Console.ReadLine();
        }
    }
}

