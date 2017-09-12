using NeuralNetworks.Entities;
using NeuralNetworks.Logic.ActivationFunctions.Abstractions;

namespace NeuralNetworks.Logic.Abstractions
{
    /// <summary>
    /// Neural Network Processing Service
    /// </summary>
    public interface INeuralNetworkProcessingService
    {
        /// <summary>
        /// Process Feed Forward Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="networkInputs">Inputs</param>
        void ProcessFeedForwardNeuralNetwork(NeuralNetwork neuralNetwork, double[] networkInputs, IActivationFunction activationFn);
    }
}
