using NeuralNetworks.DTO;

namespace NeuralNetworks.Data.Abstractions
{
    /// <summary>
    /// Neural Network Training Repo Interface
    /// </summary>
    public interface ITrainingDataRepo
    {
        /// <summary>
        /// Get Training Data XOR
        /// </summary>
        /// <returns>Training Data XOR</returns>
        TrainingDataDTO GetTrainingDataXOR();
    }
}
