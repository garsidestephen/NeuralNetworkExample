using NeuralNetworkExample.Entities.Implementation;
using NeuralNetworkExample.Entities.Enums;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkExample.Entities.Implementation
{
    /// <summary>
    /// Neural Network
    /// Inputs should be between 0.01 to 0.99 or -1 to +1
    /// Link weights should be small, avoid 0, perhaps 1 to -1
    /// </summary>
    public class NeuralNetwork : INeuralNetwork
    {
        /// <summary>
        /// Initialises a new instance of the NeuralNetwork class
        /// </summary>
        public NeuralNetwork()
        {
            ProcessingLayers = new List<ProcessingLayer>();
            OutputLayer = new OutputLayer();
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

        /// <summary>
        /// Gets
        /// </summary>
        public bool IsTraining 
        {
            get
            {
                return OutputLayer.OutputNeurons.Any(x => x.ExpectedOutput > 0);
            }        
        }
    }
}
