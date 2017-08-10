using System;
using System.Linq;

namespace NeuralNetworks.Logic.Functions
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

        /// <summary>
        /// Rectified Linear Units Func
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>ReLU Value</returns>
        public static double ReLU(double value)
        {
            return Math.Max(value, 0);
        }

        /// <summary>
        /// Softmax Func - Converts nums in array to percentage of 1 of total array sum
        /// </summary>
        /// <param name="values">Values</param>
        /// <returns>SoftMaxed Values</returns>
        public static double[] SoftMax(double[] values)
        {
            if (values == null)
            {
                return null;
            }

            double[] result = new double[values.Length];
            double arrayTotal = values.Sum();

            // Loop through each member of original array and calc softmaxed value
            for(int i = 0; i < values.Length; i++)
            {
                result[i] = values[i] / arrayTotal;
            }

            return result;
        }
    }
}
