using System.Collections.Generic;
using NeuralNetworks.Common;
using NeuralNetworks.Entities;
using NeuralNetworks.Logic.Abstractions;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Neural Network Factory
    /// </summary>
    public class NeuralNetworkFactory : INeuralNetworkFactory
    {
        /// <summary>
        /// Create Feed Forward Neural Network
        /// </summary>
        /// <param name="numberOfInputs">Number Of Inputs</param>
        /// <param name="expectedResults">Expected Results</param>
        /// <param name="learningRate">Learning Rate</param>
        /// <param name="numberOfNeuronsInHiddenLayer">Number Of Neurons In Hidden Layer</param>
        /// <param name="initialHiddenLayerWeights">Initial Hidden Layer Weights</param>
        /// <param name="initialOutputLayerWeights">Initial Output Layer Weights</param>
        /// <param name="hiddenLayerBias">Hidden Layer Bias</param>
        /// <param name="outputLayerBias">Output Layer Bias</param>
        /// <returns>Neural Network</returns>
        public NeuralNetwork CreateFeedForwardNetwork(int numberOfInputs, double[] expectedResults, double learningRate, int numberOfNeuronsInHiddenLayer = 0, double[] initialHiddenLayerWeights = null, double[] initialOutputLayerWeights = null, double[] hiddenLayerBias = null, double[] outputLayerBias = null)
        {
            NeuralNetwork nn = new NeuralNetwork();

            nn.Inputs = new double[numberOfInputs];
            nn.LearningRate = learningRate;
            nn.ExpectedResults = expectedResults;

            // If no param provided then calculate how many neurons we need in the hidden layer (mean of num inputs + num outputs)
            if (numberOfNeuronsInHiddenLayer == 0)
            {
                numberOfNeuronsInHiddenLayer = (numberOfInputs + expectedResults.Length) / 2;
                numberOfNeuronsInHiddenLayer = numberOfNeuronsInHiddenLayer == 1 ? 2 : numberOfNeuronsInHiddenLayer; // Never have just 1 neuron in hidden layer
            }

            nn.HiddenLayer = CreateLayer(numberOfNeuronsInHiddenLayer, numberOfInputs, initialHiddenLayerWeights, hiddenLayerBias);
            nn.OutputLayer = CreateLayer(expectedResults.Length, numberOfNeuronsInHiddenLayer, initialOutputLayerWeights, outputLayerBias);

            return nn;
        }

        /// <summary>
        /// Create a network Layer
        /// </summary>
        /// <param name="numberOfNeurons">Number Of Neurons</param>
        /// <param name="numberOfInputsFromPreviousLayer">Number Of Inputs From Previous Layer</param>
        /// <param name="weights">Weights</param>
        /// <param name="bias">Layer Bias</param>
        /// <returns>Network Layer</returns>
        private static IList<Neuron> CreateLayer(int numberOfNeurons, int numberOfInputsFromPreviousLayer, double[] weights = null, double[] bias = null)
        {
            IList<Neuron> layer = new List<Neuron>();

            if (weights == null)
            {
                // Create some random weights
                weights = ArrayHelper.CreateRandomArray(numberOfInputsFromPreviousLayer, 1000);
            }

            if (bias == null)
            {
                // Create some random bias
                bias = ArrayHelper.CreateRandomArray(numberOfNeurons, 1000);
            }

            // Populate Weights and Bias
            int startPos = 0;

            for (int i = 0; i < numberOfNeurons; i++)
            {
                Neuron neuron = NeuronFactory.Create(numberOfInputsFromPreviousLayer, bias[i], weights.GetSlice(startPos, startPos + numberOfInputsFromPreviousLayer));

                layer.Add(neuron);

                startPos += numberOfInputsFromPreviousLayer;
            }

            return layer;
        }
    }
}
