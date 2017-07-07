using System.Collections.Generic;
using NeuralNetworkExample.Entities.Enums;
using NeuralNetworkExample.Entities.Implementation;

namespace NeuralNetworkExample.Entities
{
    /// <summary>
    /// INeuralNetwork Interface
    /// </summary>
    public interface INeuralNetwork
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the activation function type
        /// </summary>
        ActivationFunctionType ActivationFunction { get; set; }

        /// <summary>
        /// Gets or sets Processing Layers
        /// </summary>
        IList<ProcessingLayer> ProcessingLayers { get; set; }

        /// <summary>
        /// Gets or sets Output Layer
        /// </summary>
        OutputLayer OutputLayer { get; set; }
    }
}
