namespace NeuralNetworkExample.Entities.Implementation
{
    /// <summary>
    /// Output Neuron
    /// </summary>
    public class OutputNeuron : Neuron, INeuron, IOutputNeuron
    {
        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the actual output
        /// </summary>
        public double ActualOutput { get; set; }

        /// <summary>
        /// Gets or sets the expected output
        /// </summary>
        public double ExpectedOutput { get; set; }

        /// <summary>
        /// Gets or sets the error
        /// </summary>
        public double Error 
        {
            get
            {
                return ExpectedOutput - Input;
            }
        }
    }
}
