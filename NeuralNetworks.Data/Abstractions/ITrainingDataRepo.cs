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

        /// <summary>
        /// Get Training Data AND
        /// </summary>
        /// <returns>Training Data AND</returns>
        TrainingDataDTO GetTrainingDataAND();

        /// <summary>
        /// Get Training Data Garden Design
        /// </summary>
        /// <returns>Training Data Garden Design</returns>
        TrainingDataDTO GetTrainingDataGardenDesign();
    }
}
