using NeuralNetworks.Entities.Implementation;

namespace NeuralNetworks.Entities.Implementation
{
    /// <summary>
    /// Neural Network Layer
    /// </summary>
    public class ProcessingLayer : BaseLayer
    {
        /// <summary>
        /// Gets or sets Layer weights
        /// </summary>
        public double[,] Weights { get; set; }

        /// <summary>
        /// Gets or sets Layer BackPropogationErrors
        /// </summary>
        public double[] BackPropogationErrors { get; set; }
    }
}
