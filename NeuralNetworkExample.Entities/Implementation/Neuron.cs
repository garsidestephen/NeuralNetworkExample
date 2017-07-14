namespace NeuralNetworkExample.Entities.Implementation
{
    /// <summary>
    /// A Base Neuron
    /// </summary>
    public abstract class Neuron : INeuron
    {
        /// <summary>
        /// Gets or sets the number of the neuron in the list of neurons (i.e. its order)
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the Neurons input
        /// </summary>
        public double Input { get; set; }
    }
}
