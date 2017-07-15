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
        public double[] weights { get; set; }

        /// <summary>
        /// Gets or sets Weight Errors
        /// </summary>
        public double[] weightErrors { get; set; }
    }
}
