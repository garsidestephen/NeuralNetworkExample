using NeuralNetworkExample.Entities.Enums;
using System.Collections.Generic;

namespace NeuralNetworkExample.Entities.Implementation
{
    /// <summary>
    /// Neural Network
    /// </summary>
    public class NeuralNetwork : INeuralNetwork
    {
        /// <summary>
        /// Initialises a new instance of the NeuralNetwork class
        /// </summary>
        public NeuralNetwork()
        {
            ProcessingLayers = new List<ProcessingLayer>();
        }

        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the activation function type
        /// </summary>
        public ActivationFunctionType ActivationFunction { get; set; }

        /// <summary>
        /// Gets or sets Layers
        /// </summary>
        public IList<ProcessingLayer> ProcessingLayers { get; set; }

        /// <summary>
        /// Gets or sets Output Layer
        /// </summary>
        public OutputLayer OutputLayer { get; set; }
    }
}
