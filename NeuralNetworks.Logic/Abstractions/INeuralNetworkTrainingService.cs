using NeuralNetworks.DTO;
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
        /// Train Neural Network (N.B Do not train for just 1 example, always randomize training data)
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="trainingData">Training Data</param>
        /// <param name="epochs">Number of Training Epochs</param>
        /// <param name="activationFn">Activation Function</param>
        /// <param name="learningRate">Learning Rate</param>
        /// <returns>Results Array</returns>
        double[][] Train(NeuralNetwork neuralNetwork, TrainingDataDTO trainingData, int epochs, IActivationFunction activationFn, double learningRate);
    }
}
