using NeuralNetworks.Entities;
using NeuralNetworks.Logic.Abstractions;
using NeuralNetworks.Logic.ActivationFunctions.Abstractions;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Neural Network Processing Service
    /// </summary>
    public class NeuralNetworkProcessingService : INeuralNetworkProcessingService
    {
        /// <summary>
        /// Process Feed Forward Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="networkInputs">Inputs</param>
        public void ProcessFeedForwardNeuralNetwork(NeuralNetwork neuralNetwork, double[] networkInputs, IActivationFunction activationFn)
        {
            double[] hiddenLayerOutputs = new double[neuralNetwork.HiddenLayer.Count];

            for (int i = 0; i < neuralNetwork.HiddenLayer.Count; i++)
            {
                var neuron = neuralNetwork.HiddenLayer[i];

                neuron.Inputs = networkInputs;
                CalculateNeuronOutput(neuron, activationFn);

                hiddenLayerOutputs[i] = neuron.Output;
            }

            foreach (Neuron neuron in neuralNetwork.OutputLayer)
            {
                neuron.Inputs = hiddenLayerOutputs;
                CalculateNeuronOutput(neuron, activationFn);
            }
        }

        /// <summary>
        /// Calculate Neuron Output
        /// </summary>
        /// <param name="neuron">The Neuron</param>
        /// <param name="activationFn">Activation Fn</param>
        private static void CalculateNeuronOutput(Neuron neuron, IActivationFunction activationFn)
        {
            double totalInput = 0;

            for (int i = 0; i < neuron.Inputs.Length; i++)
            {
                totalInput += neuron.Weights[i] * neuron.Inputs[i];
            }

            totalInput += neuron.Bias;

            neuron.Output = activationFn.output(totalInput);
        }
    }
}
