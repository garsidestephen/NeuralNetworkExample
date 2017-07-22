namespace NeuralNetworks.Entities
{
    /// <summary>
    /// Worker Neuron Interface
    /// </summary>
    public interface IWorkerNeuron : INeuron
    {
        /// <summary>
        /// Gets or sets Output
        /// </summary>
        double Output { get; set; }

        /// <summary>
        /// Gets or sets Weights
        /// </summary>
        double[] Weights { get; set; }

        /// <summary>
        /// Gets or sets Weighted Outputs
        /// </summary>
        double[] WeightedOutputs { get; set; }

        /// <summary>
        /// Gets or sets Weight Errors
        /// </summary>
        double[] WeightErrors { get; set; }
    }
}
