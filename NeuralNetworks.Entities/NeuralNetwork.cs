using System.Collections.Generic;

namespace NeuralNetworks.Entities
{
    /// <summary>
    /// Neural Network
    /// </summary>
    public class NeuralNetwork
    {
        /// <summary>
        /// Gets and sets Inputs
        /// </summary>
        public double[] Inputs { get; set; }

        /// <summary>
        /// Gets and sets Hidden Layer
        /// </summary>
        public IList<Neuron> HiddenLayer { get; set; }

        /// <summary>
        /// Gets and sets Output Layer
        /// </summary>
        public IList<Neuron> OutputLayer { get; set; }

        /// <summary>
        /// Gets and sets Learning Rate
        /// </summary>
        public double LearningRate { get; set; }

        /// <summary>
        /// Gets and sets Expected Results
        /// </summary>
        public double[] ExpectedResults { get; set; }

        /// <summary>
        /// Gets and sets Globa lError
        /// </summary>
        public double GlobalError { get; set; }
    }
}
