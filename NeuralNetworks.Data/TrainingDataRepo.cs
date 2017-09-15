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

        /// <summary>
        /// Get Training Data AND
        /// </summary>
        /// <returns>Training Data AND</returns>
        public TrainingDataDTO GetTrainingDataAND()
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
            double[] result2 = { 0 };
            double[] result3 = { 0 };
            double[] result4 = { 1 };

            trainingDataDTO.ExpectedResults = new double[][] { result1, result2, result3, result4 };
            trainingDataDTO.ExpectedResultsTitles = new string[1] { "Res1" };

            return trainingDataDTO;
        }

        /// <summary>
        /// Get Training Data Garden Design
        /// </summary>
        /// <returns>Training Data Garden Design</returns>
        public TrainingDataDTO GetTrainingDataGardenDesign()
        {
            var trainingDataDTO = new TrainingDataDTO();

            // Training Data
            double[] data1 = { 0, 0, 0 };
            double[] data2 = { 0, 0, 1 };
            double[] data3 = { 0, 1, 1 };
            double[] data4 = { 1, 1, 1 };
            double[] data5 = { 0, 1, 0 };
            double[] data6 = { 1, 0, 0 };
            double[] data7 = { 1, 0, 1 };
            double[] data8 = { 1, 1, 0 };

            trainingDataDTO.Inputs = new double[][] { data1, data2, data3, data4, data5, data6, data7, data8 };

            trainingDataDTO.InputTitles = new string[8] { "Inp1", "Inp2", "Inp3", "Inp4", "Inp5", "Inp6", "Inp7", "Inp8" };

            // Expected Results
            double[] result1 = { 1, 0, 0, 0, 0 };
            double[] result2 = { 0, 0, 0, 1, 0 };
            double[] result3 = { 0, 0, 0, 0, 1 };
            double[] result4 = { 0, 0, 0, 0, 1 };
            double[] result5 = { 0, 1, 0, 0, 0 };
            double[] result6 = { 0, 0, 1, 0, 0 };
            double[] result7 = { 0, 0, 0, 0, 1 };
            double[] result8 = { 0, 0, 0, 0, 1 };

            trainingDataDTO.ExpectedResults = new double[][] { result1, result2, result3, result4, result5, result6, result7, result8 };
            trainingDataDTO.ExpectedResultsTitles = new string[8] { "LowMaint", "Child", "Gen", "Gen", "Ent", "Gard", "Gen", "Gen" };

            return trainingDataDTO;
        }
    }
}
