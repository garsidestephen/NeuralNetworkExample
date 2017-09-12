using NeuralNetworks.Entities;

namespace NeuralNetworks.Logic.Abstractions
{
    /// <summary>
    /// Neural Network Factory Interface
    /// </summary>
    public interface INeuralNetworkFactory
    {
        /// <summary>
        /// Create a Feed Forward Neural Network
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
        NeuralNetwork CreateFeedForwardNetwork(int numberOfInputs, double[] expectedResults, double learningRate, int numberOfNeuronsInHiddenLayer = 0, double[] initialHiddenLayerWeights = null, double[] initialOutputLayerWeights = null, double[] hiddenLayerBias = null, double[] outputLayerBias = null);
    }
}
