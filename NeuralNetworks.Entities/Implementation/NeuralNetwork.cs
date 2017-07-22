using NeuralNetworks.Entities.Implementation;
using NeuralNetworks.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NeuralNetworks.Entities.Implementation
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
            InputLayer = new List<IWorkerNeuron>();
            HiddenLayer = new List<IWorkerNeuron>();
            OutputLayer = new List<IOutputNeuron>();
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
        /// Learning Rate
        /// </summary>
        public double LearningRate { get; set; }

        /// <summary>
        /// Initial Inputs
        /// </summary>
        public double[] InitialInputs { get; set; }

        /// <summary>
        /// Gets or sets the activation function type
        /// </summary>
        public ActivationFunctionType ActivationFunction { get; set; }

        /// <summary>
        /// Gets or sets the Input Layer
        /// </summary>
        public IList<IWorkerNeuron> InputLayer { get; internal set; }

        /// <summary>
        /// Gets or sets the Hidden Layer
        /// </summary>
        public IList<IWorkerNeuron> HiddenLayer{ get; internal set; }

        /// <summary>
        /// Gets or sets the Output Layer
        /// </summary>
        public IList<IOutputNeuron> OutputLayer { get; internal set; }
    }
}
