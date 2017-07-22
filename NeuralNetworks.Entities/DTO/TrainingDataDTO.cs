using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Entities.DTO
{
    public class TrainingDataDTO
    {
        public double[] Inputs { get; set; }

        public double[] Outputs { get; set; }
    }
}
