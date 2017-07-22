namespace NeuralNetworks.Entities.Implementation
{
    /// <summary>
    /// Worker Neuron
    /// </summary>
    public class WorkerNeuron : Neuron, INeuron, IWorkerNeuron
    {
        /// <summary>
        /// Gets or sets Output
        /// </summary>
        public double Output { get; set; }

        /// <summary>
        /// Gets or sets Weights
        /// </summary>
        public double[] Weights { get; set; }

        /// <summary>
        /// Gets or sets Weighted Outputs
        /// </summary>
        public double[] WeightedOutputs { get; set; }

        /// <summary>
        /// Gets or sets Weight Errors
        /// </summary>
        public double[] WeightErrors { get; set; }
    }
}
