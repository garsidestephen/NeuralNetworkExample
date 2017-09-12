using NeuralNetworks.Entities;
using NeuralNetworks.Logic.ActivationFunctions.Abstractions;

namespace NeuralNetworks.Logic.Abstractions
{
    /// <summary>
    /// Neural Network Training Service Interface
    /// </summary>
    public interface INeuralNetworkTrainingService
    {
        /// <summary>
        /// Train Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="inputs">Network Inputs</param>
        /// <param name="expectedResults">Expected Results</param>
        /// <param name="epochs">Number of Training Epochs</param>
        /// <param name="activationFn">Activation Function</param>
        /// <param name="learningRate">Learning Rate</param>
        void Train(NeuralNetwork neuralNetwork, double[][] inputs, double[][] expectedResults, int epochs, IActivationFunction activationFn, double learningRate);
    }
}
