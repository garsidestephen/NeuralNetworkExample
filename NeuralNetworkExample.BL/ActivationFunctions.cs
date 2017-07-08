using System;

namespace NeuralNetworkExample.BL
{
    /// <summary>
    /// Activation Functions
    /// </summary>
    public class ActivationFunctions
    {
        /// <summary>
        /// Sigmoid Constant
        /// </summary>
        private const double SigmoidConst = 2.71828;

        /// <summary>
        /// Sigmoid Func 
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Sigmoid Value</returns>
        public static double Sigmoid(double value)
        {
            return 1 / (1 + Math.Pow(SigmoidConst, -value));
        }
    }
}
