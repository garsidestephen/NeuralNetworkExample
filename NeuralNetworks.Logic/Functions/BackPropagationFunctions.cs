//ToDo: Pass activation Fn into RecalculateWeight

using System;
using System.Linq;

namespace NeuralNetworks.Logic.Functions
{
    /// <summary>
    /// Back Propagation Functions
    /// </summary>
    public class BackPropagationFunctions
    {
        /// <summary>
        /// Distribute Error Proportioned To Weight
        /// </summary>
        /// <param name="backPropagatedError">Back PropagatedError</param>
        /// <param name="weight">Weight whose error you want to calc</param>
        /// <param name="allInboundWeights">All Inbound Weights</param>
        /// <returns>Back Propagted error for weight</returns>
        public static double DistributeErrorProportionedToWeight(double backPropagatedError, double weight, double[] allInboundWeights)
        {
            return backPropagatedError * (weight / allInboundWeights.Sum());
        }

        /// <summary>
        /// Recalculate Weight Using Sigmoid
        /// </summary>
        /// <param name="currentWeight">Current Weight</param>
        /// <param name="forwardNeuronError">Forward Neuron Error</param>
        /// <param name="forwardNeuronInput">Forward Neuron Input</param>
        /// <param name="currentNeuronWeightedOutput">Current Neuron Weighted Output</param>
        /// <param name="learningRate">Learning Rate</param>
        /// <returns>Recalculated Weight</returns>
        public static double RecalculateWeight(double currentWeight, double forwardNeuronError, double forwardNeuronInput, double currentNeuronWeightedOutput, double learningRate)
        {
            double forwardNeuronInputAfterActivationFn = ActivationFunctions.Sigmoid(forwardNeuronInput);
            double ActivatedForwardNeuronInput = forwardNeuronInputAfterActivationFn * (1 - forwardNeuronInputAfterActivationFn);
            double intialWeightDiff = -forwardNeuronError * ActivatedForwardNeuronInput * currentNeuronWeightedOutput;
            double finalWeightDiff = -(learningRate * intialWeightDiff);

            return currentWeight + finalWeightDiff;
        }
    }
}
