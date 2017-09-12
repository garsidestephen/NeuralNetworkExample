using System.Linq;

namespace NeuralNetworks.Logic.ActivationFunctions
{
    /// <summary>
    /// Softmax Activation Function
    /// </summary>
    public class Softmax
    {
        /// <summary>
        /// Softmax Func - Converts nums in array to percentage of 1 of total array sum
        /// </summary>
        /// <param name="values">Values</param>
        /// <returns>SoftMaxed Values</returns>
        public double[] SoftMax(double[] values)
        {
            if (values == null)
            {
                return null;
            }

            double[] result = new double[values.Length];
            double arrayTotal = values.Sum();

            // Loop through each member of original array and calc softmaxed value
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = values[i] / arrayTotal;
            }

            return result;
        }
    }
}
