using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkExample.Entities.Implementation
{
    /// <summary>
    /// Output Neuron
    /// </summary>
    public class OutputNeuron
    {
        public int Number { get; set; }

        public string Description { get; set; }

        public double ActualOutput { get; set; }

        public double ExpectedOutput { get; set; }

        public double Error 
        {
            get
            {
                return ExpectedOutput - ActualOutput;
            }
        }
    }
}
