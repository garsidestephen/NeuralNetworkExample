namespace NeuralNetworkExample.Entities
{
    /// <summary>
    /// Base Neuron Interface
    /// </summary>
    public interface INeuron
    {
        /// <summary>
        /// Gets or sets the number of the neuron in the list of neurons (i.e. its order)
        /// </summary>
        int Number { get; set; }

        /// <summary>
        /// Gets or sets the Neurons input
        /// </summary>
        double Input { get; set; }
    }
}
