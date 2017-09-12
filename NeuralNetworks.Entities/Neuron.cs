namespace NeuralNetworks.Entities
{
    /// <summary>
    /// A Neuron
    /// </summary>
    public class Neuron
    {
        /// <summary>
        /// Gets and sets Inputs
        /// </summary>
        public double[] Inputs { get; set; }

        /// <summary>
        /// Gets and sets Weights
        /// </summary>
        public double[] Weights { get; set; }

        /// <summary>
        /// Gets and sets Error
        /// </summary>
        public double Error { get; set; }

        /// <summary>
        /// Gets and sets Bias
        /// </summary>
        public double Bias { get; set; }

        /// <summary>
        /// Gets and sets Output
        /// </summary>
        public double Output { get; set; }
    }
}
