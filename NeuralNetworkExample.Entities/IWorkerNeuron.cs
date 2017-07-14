namespace NeuralNetworks.Entities
{
    /// <summary>
    /// Worker Neuron Interface
    /// </summary>
    public interface IWorkerNeuron : INeuron
    {
        /// <summary>
        /// Gets or sets Back Propagated Error
        /// </summary>
        double BackPropagatedError { get; set; }

        /// <summary>
        /// Gets or sets Output
        /// </summary>
        double Output { get; set; }

        /// <summary>
        /// Gets or sets Weights
        /// </summary>
        double[] weights { get; set; }
    }
}
