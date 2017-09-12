using System;
using NeuralNetworks.Common;
using NeuralNetworks.Entities;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Neuron Factory
    /// </summary>
    public class NeuronFactory
    {
        /// <summary>
        /// Neuron Factory
        /// </summary>
        /// <param name="numberOfInputs">Number Of Inputs</param>
        /// <param name="layerBias">Layer Bias</param>
        /// <param name="weights">Weights</param>
        /// <returns>A Neuron</returns>
        public static Neuron Create(int numberOfInputs, double layerBias, double[] weights = null)
        {
            Neuron neuron = new Neuron();

            neuron.Inputs = new double[numberOfInputs];
            neuron.Weights = new double[numberOfInputs];
            neuron.Bias = layerBias;

            if (weights != null)
            {
                neuron.Weights = weights;
            }
            else
            {
                // Randomise Weights & Bias
                RandomiseWeightsAndBias(neuron);
            }

            return neuron;
        }

        /// <summary>
        /// Randomise Weights And Bias
        /// </summary>
        /// <param name="neuron">The Neuron</param>
        private static void RandomiseWeightsAndBias(Neuron neuron)
        {
            neuron.Weights = ArrayHelper.CreateRandomArray(neuron.Weights.Length, 1000);

            Random r = new Random(1000);
            neuron.Bias = r.NextDouble();
        }
    }
}
