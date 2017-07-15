using System;
using System.Linq;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Back Propagation Functions
    /// </summary>
    public class BackPropagationFunctions
    {
        /// <summary>
        /// Distribute Error Propotioned To Weight
        /// </summary>
        /// <param name="backPropagatedError">Back PropagatedError</param>
        /// <param name="weight">Weight whose error you want to calc</param>
        /// <param name="allInboundWeights">All Inbound Weights</param>
        /// <returns>Back Propagted error for weight</returns>
        public static double DistributeErrorPropotionedToWeight(double backPropagatedError, double weight, double[] allInboundWeights)
        {
            return backPropagatedError * (weight / allInboundWeights.Sum());
        }
    }
}
