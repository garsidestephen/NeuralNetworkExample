using NeuralNetworkExample.Entities;
using NeuralNetworkExample.Entities.DTO;
using System;
using System.Collections.Generic;

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
        /// Create a neural network
        /// </summary>
        /// <param name="numberOfInitialInputs">Number Of Initial Inputs</param>
        /// <param name="numberOfProcessingLayers">Number Of Processing Layers</param>
        /// <param name="networkOutputs">Network Outputs</param>
        /// <param name="allInitialDelimitedWeights">Optional All Initial Delimited Weights</param>
        /// <returns>A Neural Network</returns>
        INeuralNetwork Create(int numberOfInitialInputs, int numberOfProcessingLayers, IList<NetworkOutput> networkOutputs, string allInitialDelimitedWeights = null);

        /// <summary>
        /// Process a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="inputs">Inputs</param>
        /// <param name="activationFunction">Activation Function</param>
        void Process(INeuralNetwork neuralNetwork, double[] inputs, Func<double, double> activationFunction);

        void BackPropogate(INeuralNetwork neuralNetwork);
    }
}
