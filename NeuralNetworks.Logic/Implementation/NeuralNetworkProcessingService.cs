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
        /// <param name="activationFn">Activation Function</param>
        public void Process(INeuralNetwork neuralNetwork, double[] inputs, Func<double, double> activationFn)
        {
            neuralNetwork.InitialInputs = inputs;

            for (int i = 0; i < inputs.Length; i++)
            {
                neuralNetwork.InputLayer[i].Input = inputs[i];
            }

            // Push data through network
            CalculateNeuronOutputs(neuralNetwork.InputLayer, neuralNetwork.HiddenLayer, activationFn);
            CalculateNeuronOutputs(neuralNetwork.HiddenLayer, neuralNetwork.OutputLayer, activationFn);
            CalculateOutputLayerError(neuralNetwork.OutputLayer);
        }

        /// <summary>
        /// Calculate Neuron Outputs
        /// </summary>
        /// <param name="neuronsInCurrentLayer">Neurons in current Layer</param>
        /// <param name="forwardNeurons">Neurons in Forward layer</param>
        /// <param name="activationFn">Activation Function</param>
        private static void CalculateNeuronOutputs(IEnumerable<IWorkerNeuron> neuronsInCurrentLayer, IEnumerable<INeuron> forwardNeurons, Func<double, double> activationFn)
        {
            double[] outputs = new double[forwardNeurons.Count()];

            for (int i = 0; i < forwardNeurons.Count(); i++ )
            {
                for (int j = 0; j < neuronsInCurrentLayer.Count(); j++)
                {
                    var currentNeuron = neuronsInCurrentLayer.ElementAt(j);
                    double weight = neuronsInCurrentLayer.ElementAt(j).Weights[i];
                    double currentNeuronInputxWeight = (currentNeuron.Input * weight);

                    // Stash Weighted Output for Back Prop
                    currentNeuron.WeightedOutputs[i] = currentNeuronInputxWeight;

                    outputs[i] = currentNeuronInputxWeight + outputs[i]; 
                }
            }

            // Apply Activation Function to outputs and make them the inputs for next Neurons
            for (int i = 0; i < outputs.Length; i++)
            {
                forwardNeurons.ElementAt(i).Input = activationFn(outputs[i]);
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
