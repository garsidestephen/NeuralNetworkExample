using NeuralNetworks.Entities;
using System;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Neural Network Processing Service Interface
    /// </summary>
    public interface INeuralNetworkProcessingService
    {
        /// <summary>
        /// Process a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="inputs">Inputs</param>
        /// <param name="activationFunction">Activation Function</param>
        void Process(INeuralNetwork neuralNetwork, double[] inputs, Func<double, double> activationFunction);
    }
}
