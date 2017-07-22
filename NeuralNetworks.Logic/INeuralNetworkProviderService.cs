using NeuralNetworks.Entities;
using NeuralNetworks.Entities.DTO;
using System.Collections.Generic;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Neural Network Provider Service Interface
    /// </summary>
    public interface INeuralNetworkProviderService
    {
        /// <summary>
        /// Get Neural Network
        /// </summary>
        /// <param name="id">Neural Network Id</param>
        /// <returns>Neural Network</returns>
        INeuralNetwork Get(int id);

        /// <summary>
        /// Create Neural Network
        /// </summary>
        /// <param name="numberOfInitialInputs">Number Of Initial Inputs</param>
        /// <param name="networkOutputs">Network Outputs</param>
        /// <param name="learningRate">Learning Rate</param>
        /// <param name="initialInputWeights">Initial Input Weights</param>
        /// <param name="initialHiddenLayerWeights">Initial Hidden Layer Weights</param>
        /// <returns>A New Neural Network</returns>
        INeuralNetwork Create(int numberOfInitialInputs, IList<NetworkOutput> networkOutputs, double learningRate, double[] initialInputWeights = null, double[] initialHiddenLayerWeights = null);
    }
}
