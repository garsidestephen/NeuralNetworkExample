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
        /// <param name="activationFn">Activation Function</param>
        /// <param name="traceActivity">Trace Activity</param>
        void Process(INeuralNetwork neuralNetwork, double[] inputs, Func<double, double> activationFn, bool traceActivity = false);
    }
}
