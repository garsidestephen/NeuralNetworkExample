using NeuralNetworks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Logic
{
    public class TrainingProfile
    {       
        public double[] Inputs { get; set; }
        
        public double[] ExpectedOutputs { get; set; }

        public Func<double, double> ActivationFn { get; set; }

        public Func<double, double, double[], double> BackPropagationErrorCalculationFn { get; set; }

        public Func<double, double, double, double, double, double> WeightCalculationFn { get; set; }
    }
}
