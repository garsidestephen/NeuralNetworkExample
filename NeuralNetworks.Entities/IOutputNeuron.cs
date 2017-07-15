namespace NeuralNetworks.Entities
{
    /// <summary>
    /// Output Neuron Interface
    /// </summary>
    public interface IOutputNeuron : INeuron
    {
        /// <summary>
        /// Gets or sets description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the expected output
        /// </summary>
        double ExpectedOutput { get; set; }
    }
}
