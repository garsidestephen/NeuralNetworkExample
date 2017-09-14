namespace NeuralNetworks.DTO
{
    /// <summary>
    /// Training Data DTO
    /// </summary>
    public class TrainingDataDTO
    {
        /// <summary>
        /// Gets or sets Inputs
        /// </summary>
        public double[][] Inputs { get; set; }

        /// <summary>
        /// Gets or sets Input Titles
        /// </summary>
        public string[] InputTitles { get; set; }

        /// <summary>
        /// Gets or sets Expected Results
        /// </summary>
        public double[][] ExpectedResults { get; set; }

        /// <summary>
        /// Gets or sets Expected Results Titles
        /// </summary>
        public string[] ExpectedResultsTitles { get; set; }
    }
}
