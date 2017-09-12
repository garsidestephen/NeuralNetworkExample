using System;
using NeuralNetworks.Logic.ActivationFunctions.Abstractions;

namespace NeuralNetworks.Logic.ActivationFunctions
{
    /// <summary>
    /// Sigmoid Activation Function
    /// </summary>
    public class Sigmoid : IActivationFunction
    {
        /// <summary>
        /// Activation Output
        /// </summary>
        /// <param name="x">The Value</param>
        /// <returns>Activated Value</returns>
        public double output(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        /// <summary>
        /// The Activation Derivative
        /// </summary>
        /// <param name="x">The Value</param>
        /// <returns>Activation Derivative</returns>
        public double derivative(double x)
        {
            return x * (1 - x);
        }
    }
}
