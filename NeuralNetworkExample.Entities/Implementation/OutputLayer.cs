using System.Collections.Generic;

namespace NeuralNetworkExample.Entities.Implementation
{
    /// <summary>
    /// Neural Network Output Layer
    /// </summary>
    public class OutputLayer : BaseLayer
    {
        /// <summary>
        /// Initialises a new instance of an Output Layer
        /// </summary>
        public OutputLayer() 
        {
            OutputNeurons = new List<OutputNeuron>();
        }

        /// <summary>
        /// Output Neurons
        /// </summary>
        public IList<OutputNeuron> OutputNeurons { get; internal set; }
    }
}
