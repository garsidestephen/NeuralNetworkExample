using System;
using NeuralNetworks.Logic.ActivationFunctions.Abstractions;

namespace NeuralNetworks.Logic.ActivationFunctions
{
    /// <summary>
    /// Hyperbolic Tangent Activation Function (preferred to sigmoid)
    /// </summary>
    public class HyperbolicTangent : IActivationFunction
    {
        /// <summary>
        /// Activation Output
        /// </summary>
        /// <param name="x">The Value</param>
        /// <returns>Activated Value</returns>
        public double output(double x)
        {
            return Math.Tanh(x);
        }

        /// <summary>
        /// The Activation Derivative
        /// </summary>
        /// <param name="x">The Value</param>
        /// <returns>Activation Derivative</returns>
        public double derivative(double x)
        {
            double tanh = Math.Tanh(x);
            return 1 - tanh * tanh;
        }
    }
}
