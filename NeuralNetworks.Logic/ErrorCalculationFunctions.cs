using System.Linq;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Error Calculation Functions
    /// </summary>
    public class ErrorCalculationFunctions
    {
        /// <summary>
        /// Calculate Back Propagation Error
        /// </summary>
        /// <param name="error">Error</param>
        /// <param name="weight">Weight</param>
        /// <param name="allNeuronInputWeights">All Neuron Input Weights</param>
        /// <returns>Back Propagation Error</returns>
        public static double CalculateProportionateBackPropagationError(double error, double weight, double[] allNeuronInputWeights)
        {
            return error * (weight / allNeuronInputWeights.Sum());
        }
    }
}
