﻿using NeuralNetworks.Entities.Enums;
using NeuralNetworks.Entities.Implementation;
using System.Collections.Generic;

namespace NeuralNetworks.Entities
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
        /// Learning Rate
        /// </summary>
        double LearningRate { get; set; }

        /// <summary>
        /// Initial Inputs
        /// </summary>
        double[] InitialInputs { get; set; }

        /// <summary>
        /// Gets or sets the activation function type
        /// </summary>
        ActivationFunctionType ActivationFunction { get; set; }

        /// <summary>
        /// Gets or sets the Input Layer
        /// </summary>
        IList<IWorkerNeuron> InputLayer { get; }

        /// <summary>
        /// Gets or sets the Hidden Layer
        /// </summary>
        IList<IWorkerNeuron> HiddenLayer { get; }

        /// <summary>
        /// Gets or sets the Output Layer
        /// </summary>
        IList<IOutputNeuron> OutputLayer { get; }
    }
}
