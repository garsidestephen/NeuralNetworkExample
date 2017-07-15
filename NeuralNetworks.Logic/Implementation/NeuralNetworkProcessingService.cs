using NeuralNetworks.Logic.Helpers;
using NeuralNetworks.Data;
using NeuralNetworks.Data.Implementation;
using NeuralNetworks.Entities;
using NeuralNetworks.Entities.DTO;
using NeuralNetworks.Entities.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworks.Logic.Implementation
{
    /// <summary>
    /// Neural Network Processing Service
    /// </summary>
    public class NeuralNetworkProcessingService : INeuralNetworkProcessingService
    {
        /// <summary>
        /// Initializes a new instance of the NeuralNetworkProcessingService class
        /// </summary>
        public NeuralNetworkProcessingService()
        {
            // ToDo: Implement DI
        }

        /// <summary>
        /// Process a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="inputs">Inputs</param>
        /// <param name="activationFunction">Activation Function</param>
        public void Process(INeuralNetwork neuralNetwork, double[] inputs, Func<double, double> activationFunction)
        {
            neuralNetwork.InitialInputs = inputs;

            for (int i = 0; i < inputs.Length; i++)
            {
                neuralNetwork.InputLayer[i].Input = inputs[i];
            }

            // Push data through network
            CalculateNeuronOutputs(neuralNetwork.InputLayer, neuralNetwork.HiddenLayer, activationFunction);
            CalculateNeuronOutputs(neuralNetwork.HiddenLayer, neuralNetwork.OutputLayer, activationFunction);
            CalculateOutputLayerError(neuralNetwork.OutputLayer);
        }

        /// <summary>
        /// Calculate Neuron Outputs
        /// </summary>
        /// <param name="neuronsInCurrentLayer">Neurons in current Layer</param>
        /// <param name="forwardNeurons">Neurons in Forward layer</param>
        /// <param name="activationFunction">Activation Function</param>
        private static void CalculateNeuronOutputs(IEnumerable<IWorkerNeuron> neuronsInCurrentLayer, IEnumerable<INeuron> forwardNeurons, Func<double, double> activationFunction)
        {
            double[] outputs = new double[forwardNeurons.Count()];

            // Multiply Inputs * Weights
            for (int i = 0; i < neuronsInCurrentLayer.Count(); i++)
            {
                for (int j = 0; j < forwardNeurons.Count(); j++)
                {
                    var input = neuronsInCurrentLayer.ElementAt(j).Input;
                    var weight = neuronsInCurrentLayer.ElementAt(j).weights[i];

                    // 0.9  0.3  0.4   0.9      (0.9x0.9) + (0.3*0.1) + (0.4*0.8)
                    // 0.2  0.8  0.2 X 0.1  SO  (0.2x0.9) + (0.8*0.1) + (0.2*0.8)
                    // 0.1  0.5  0.6   0.8      (0.1x0.9) + (0.5*0.1) + (0.6*0.8)

                    outputs[i] = (input * weight) + outputs[i];
                }
            }

            // Apply Activation Function to outputs and make them the inputs for next Neurons
            for (int i = 0; i < outputs.Length; i++)
            {
                forwardNeurons.ElementAt(i).Input = activationFunction(outputs[i]);
            }
        }

        /// <summary>
        /// Calculate Output Layer Error
        /// </summary>
        /// <param name="outputNeurons">Output Neurons</param>
        private static void CalculateOutputLayerError(IList<IOutputNeuron> outputNeurons)
        {
            foreach (IOutputNeuron outputNeuron in outputNeurons)
            {
                outputNeuron.Error = outputNeuron.ExpectedOutput - outputNeuron.Input;
            }
        }
    }
}
