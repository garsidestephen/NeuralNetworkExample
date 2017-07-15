using NeuralNetworks.Entities;
using NeuralNetworks.Entities.DTO;
using System;
using System.Collections.Generic;

namespace NeuralNetworks.Logic
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
        /// Create Neural Network
        /// </summary>
        /// <param name="numberOfInitialInputs">Number Of Initial Inputs</param>
        /// <param name="networkOutputs">Network Outputs</param>
        /// <param name="initialInputWeights">Initial Input Weights</param>
        /// <param name="initialHiddenLayerWeights">Initial Hidden Layer Weights</param>
        /// <returns></returns>
        INeuralNetwork Create(int numberOfInitialInputs, IList<NetworkOutput> networkOutputs, double[] initialInputWeights = null, double[] initialHiddenLayerWeights = null);

        /// <summary>
        /// Back Propogate
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        void BackPropogate(INeuralNetwork neuralNetwork);

        /// <summary>
        /// Process a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="inputs">Inputs</param>
        /// <param name="activationFunction">Activation Function</param>
        void Process(INeuralNetwork neuralNetwork, double[] inputs, Func<double, double> activationFunction);
    }
}
