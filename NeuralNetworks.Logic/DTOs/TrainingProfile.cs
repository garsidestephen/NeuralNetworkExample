using System;
using System.Collections.Generic;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Training Profile DTO
    /// </summary>
    public class TrainingProfile
    {       
        /// <summary>
        /// Gets or sets Training Data
        /// </summary>
        public IList<TrainingData> Data { get; set; }

        /// <summary>
        /// Gets or sets ActivationFn
        /// </summary>
        public Func<double, double> ActivationFn { get; set; }

        /// <summary>
        /// Gets or sets BackPropagationErrorCalculationFn
        /// </summary>
        public Func<double, double, double[], double> BackPropagationErrorCalculationFn { get; set; }

        /// <summary>
        /// Gets or sets WeightCalculationFn
        /// </summary>
        public Func<double, double, double, double, double, double> WeightCalculationFn { get; set; }
    }
}
