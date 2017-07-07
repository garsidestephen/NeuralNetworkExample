using NeuralNetworkExample.Entities;

namespace NeuralNetworkExample.BL
{
    public interface INeuralNetworkService
    {
        /// <summary>
        /// Get Neural Network
        /// </summary>
        /// <param name="id">Neural Network Id</param>
        /// <returns>Neural Network</returns>
        INeuralNetwork Get(int id);

        /// <summary>
        /// Create a Neural Network
        /// </summary>
        /// <param name="numberOfInitialInputs">Number Of Initial Inputs</param>
        /// <param name="numberOfProcessingLayers">Number Of Processing Layers</param>
        /// <param name="orderedOutputLayerNeuronDescriptions">Ordered Output Layer Neuron Descriptions</param>
        /// <param name="allInitialDelimitedWeights">Optional All Initial Delimited Weights</param>
        /// <returns>A Neural Network</returns>
        INeuralNetwork Create(int numberOfInitialInputs, int numberOfProcessingLayers, string[] orderedOutputLayerNeuronDescriptions, string allInitialDelimitedWeights = null);

        /// <summary>
        /// Process a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="inputs">Inputs</param>
        void Process(INeuralNetwork neuralNetwork, double[] inputs);
    }
}
