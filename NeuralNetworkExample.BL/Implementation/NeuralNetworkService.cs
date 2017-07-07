using NeuralNetworkExample.BL.Helpers;
using NeuralNetworkExample.DAL;
using NeuralNetworkExample.DAL.Implementation;
using NeuralNetworkExample.Entities;
using NeuralNetworkExample.Entities.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkExample.BL.Implementation
{
    /// <summary>
    /// Neural Network Service
    /// </summary>
    public class NeuralNetworkService : INeuralNetworkService
    {
        /// <summary>
        /// Sigmoid Constant
        /// </summary>
        private const double SigmoidConst = 2.71828;

        /// <summary>
        /// Private neural Network Repo
        /// </summary>
        private INeuralNetworkRepo _neuralNetworkRepo;

        /// <summary>
        /// Initializes a new instance of the NeuralNetworkService class
        /// </summary>
        public NeuralNetworkService()
        {
            // ToDo: Implement DI
            _neuralNetworkRepo = new NeuralNetworkRepo();
        }

        /// <summary>
        /// Get Neural Network
        /// </summary>
        /// <param name="id">Neural Network Id</param>
        /// <returns>Neural Network</returns>
        public INeuralNetwork Get(int id)
        {
            return _neuralNetworkRepo.Get(id);
        }

        /// <summary>
        /// Create a neural network
        /// </summary>
        /// <param name="numberOfInitialInputs">Number Of Initial Inputs</param>
        /// <param name="numberOfProcessingLayers">Number Of Processing Layers</param>
        /// <param name="orderedOutputLayerNeuronDescriptions">Ordered Output Layer Neuron Descriptions</param>
        /// <param name="allInitialDelimitedWeights">Optional All Initial Delimited Weights</param>
        /// <returns>A Neural Network</returns>
        public INeuralNetwork Create(int numberOfInitialInputs, int numberOfProcessingLayers, string[] orderedOutputLayerNeuronDescriptions, string allInitialDelimitedWeights = null)
        {
            var neuralNetwork = new NeuralNetwork();

            CreateProcessingLayers(neuralNetwork, numberOfInitialInputs, numberOfProcessingLayers, allInitialDelimitedWeights);

            CreateOutputLayer(neuralNetwork, orderedOutputLayerNeuronDescriptions);

            return neuralNetwork;
        }

        /// <summary>
        /// Process a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="inputs">Inputs</param>
        public void Process(INeuralNetwork neuralNetwork, double[] inputs)
        {
            double[] outputs = null;

            // Push data through network
            for (int currentLayerNumber = 0; currentLayerNumber < neuralNetwork.ProcessingLayers.Count; currentLayerNumber++)
            {
                outputs = CalculateNeuronOutputs(inputs, neuralNetwork.ProcessingLayers[currentLayerNumber].Weights, SigmoidActivationFunction);

                inputs = outputs;
            }

            PopulateOutputNeurons(neuralNetwork, outputs);
        }

        /// <summary>
        /// Calculate Neuron Outputs
        /// </summary>
        /// <param name="neuronInputs"></param>
        /// <param name="weights"></param>
        /// <param name="activationFunction"></param>
        /// <returns>Neuron Outputs</returns>
        private static double[] CalculateNeuronOutputs(double[] neuronInputs, double[,] weights, Func<double, double> activationFunction)
        {
            double[] outputs = new double[neuronInputs.Length];

            // Multiply Inputs * Weights
            for (int i = 0; i < neuronInputs.Length; i++)
            {
                for (int j = 0; j < neuronInputs.Length; j++)
                {
                    var input = neuronInputs[j];
                    var weight = weights[i, j];

                    // 0.9  0.3  0.4   0.9      (0.9x0.9) + (0.3*0.1) + (0.4*0.8)
                    // 0.2  0.8  0.2 X 0.1  SO  (0.2x0.9) + (0.8*0.1) + (0.2*0.8)
                    // 0.1  0.5  0.6   0.8      (0.1x0.9) + (0.5*0.1) + (0.6*0.8)

                    outputs[i] = (input * weight) + outputs[i];
                }
            }

            // Apply Activation Function to outputs
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i] = activationFunction(outputs[i]);
            }

            return outputs;
        }

        /// <summary>
        /// Create Processing Layers
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="numberOfInitialInputs">Number Of Initial Inputs</param>
        /// <param name="numberOfProcessingLayers">Number Of Processing Layers</param>
        /// <param name="allInitialDelimitedWeights">All Initial Delimited Weights</param>
        private static void CreateProcessingLayers(INeuralNetwork neuralNetwork, int numberOfInitialInputs, int numberOfProcessingLayers, string allInitialDelimitedWeights = null)
        {
            double[,] allInitialWeights;

            // Do we have an initial delimited weights string?
            if (allInitialDelimitedWeights != null)
            {
                // Yep, so convert it to a double 2d array
                allInitialWeights = ArrayHelper.ConvertStringArrayTo2dDoubleArray(allInitialDelimitedWeights, numberOfInitialInputs);
            }
            else
            {
                // Nope, lets create an array of random doubles
                // Example 2d Weights Array "0.9,0.3,0.4, 0.2,0.8,0.2, 0.1,0.5,0.6, 0.3,0.7,0.5, 0.6,0.5,0.2, 0.8,0.1,0.9";
                allInitialWeights = ArrayHelper.CreateRandomArray(numberOfInitialInputs, numberOfProcessingLayers);
            }

            // Create layers and apportion allInitialWeights accross them
            int counter = 0;

            for (int i = 0; i < numberOfProcessingLayers; i++)
            {
                var layer = new ProcessingLayer() { Number = i + 1, Weights = ArrayHelper.Get2dArraySlice(allInitialWeights, counter, counter + (numberOfInitialInputs - 1)) };
                neuralNetwork.ProcessingLayers.Add(layer);

                counter += numberOfInitialInputs;
            }
        }

        /// <summary>
        /// Create Output Layer
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="orderedOutputLayerNeuronDescriptions">Ordered Output Layer Neuron Descriptions</param>
        private static void CreateOutputLayer(INeuralNetwork neuralNetwork, string[] orderedOutputLayerNeuronDescriptions)
        {
            neuralNetwork.OutputLayer = new OutputLayer();

            for (int i = 0; i < orderedOutputLayerNeuronDescriptions.Length; i++)
            {
                neuralNetwork.OutputLayer.OutputNeurons.Add(new OutputNeuron() { Number = i + 1, Description = orderedOutputLayerNeuronDescriptions[i] });
            }
        }

        /// <summary>
        /// Populate Output Neurons
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="orderedOutputs">Ordered Outputs</param>
        private static void PopulateOutputNeurons(INeuralNetwork neuralNetwork, double[] orderedOutputs)
        {
            if (neuralNetwork.OutputLayer != null && orderedOutputs.Length > 0)
            {
                for (int i = 0; i < neuralNetwork.OutputLayer.OutputNeurons.Count; i++)
                {
                    var outputNeuron = neuralNetwork.OutputLayer.OutputNeurons[i];

                    outputNeuron.ActualOutput = orderedOutputs[i];
                }
            }
        }

        /// <summary>
        /// Sigmoid Func 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static double SigmoidActivationFunction(double value)
        {
            return 1 / (1 + Math.Pow(SigmoidConst, -value));
        }
    }
}
