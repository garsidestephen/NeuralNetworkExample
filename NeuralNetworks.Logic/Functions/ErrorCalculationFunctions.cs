using System.Linq;

namespace NeuralNetworks.Logic.Functions
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

        /// <summary>
        /// Recalculate Weight
        /// </summary>
        /// <param name="error"></param>
        /// <param name="input"></param>
        /// <param name="weight"></param>
        /// <param name="learningRate"></param>
        /// <returns></returns>
        public static double RecalculateWeight(double currentWeight, double forwardNeuronError, double forwardNeuronInput, double currentNeuronWeightedOutput, double learningRate)
        {
            double forwardNeuronInputSigmoid = ActivationFunctions.Sigmoid(forwardNeuronInput);
            double sigmoidedForwardNeuronInput = forwardNeuronInputSigmoid * (1 - forwardNeuronInputSigmoid);
            double intialWeightDiff = -forwardNeuronError * sigmoidedForwardNeuronInput * currentNeuronWeightedOutput;
            double finalWeightDiff = -(learningRate * intialWeightDiff);

            return currentWeight + finalWeightDiff;
        }
    }
}
