using NeuralNetworks.Data.Abstractions;
using NeuralNetworks.DTO;

namespace NeuralNetworks.Data
{
    /// <summary>
    /// Neural Network Training Data Repo
    /// </summary>
    public class TrainingDataRepo : ITrainingDataRepo
    {
        /// <summary>
        /// Get Training Data XOR
        /// </summary>
        /// <returns>Training Data XOR</returns>
        public TrainingDataDTO GetTrainingDataXOR()
        {
            var trainingDataDTO = new TrainingDataDTO();

            // Training Data
            double[] data1 = { 0, 0 };
            double[] data2 = { 0, 1 };
            double[] data3 = { 1, 0 };
            double[] data4 = { 1, 1 };

            trainingDataDTO.Inputs = new double[][] { data1, data2, data3, data4 };

            trainingDataDTO.InputTitles = new string[2] { "Inp1", "Inp2" };

            // Expected Results
            double[] result1 = { 0 };
            double[] result2 = { 1 };
            double[] result3 = { 1 };
            double[] result4 = { 0 };

            trainingDataDTO.ExpectedResults = new double[][] { result1, result2, result3, result4 };
            trainingDataDTO.ExpectedResultsTitles = new string[1] { "Res1" };

            return trainingDataDTO;
        }
    }
}
