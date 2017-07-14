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

        INeuralNetwork Create(int numberOfInitialInputs, IList<NetworkOutput> networkOutputs, double[] initialInputWeights = null, double[] initialHiddenLayerWeights = null);

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
