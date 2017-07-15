using NeuralNetworks.Entities;
using System;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Neural Network Training Service Interface
    /// </summary>
    public interface INeuralNetworkTrainingService
    {
        /// <summary>
        /// Back Propogate
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="backPropagationErrorCalculationFunction">Back Propagation Error Calculation Function</param>
        void BackPropogate(INeuralNetwork neuralNetwork, Func<double, double, double[], double> backPropagationErrorCalculationFunction);
    }
}
