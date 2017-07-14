namespace NeuralNetworks.Entities.Implementation
{
    /// <summary>
    /// Worker Neuron
    /// </summary>
    public class WorkerNeuron : Neuron, INeuron, IWorkerNeuron
    {
        /// <summary>
        /// Gets or sets Back Propagated Error
        /// </summary>
        public double BackPropagatedError { get; set; }

        /// <summary>
        /// Gets or sets Output
        /// </summary>
        public double Output { get; set; }

        /// <summary>
        /// Gets or sets Weights
        /// </summary>
        public double[] weights { get; set; }
    }
}
