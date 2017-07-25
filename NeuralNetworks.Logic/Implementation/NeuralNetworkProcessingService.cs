using NeuralNetworks.Logic.Helpers;
using NeuralNetworks.Data;
using NeuralNetworks.Data.Implementation;
using NeuralNetworks.Entities;
using NeuralNetworks.Entities.DTO;
using NeuralNetworks.Entities.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetworks.Logic.Implementation
{
    /// <summary>
    /// Neural Network Processing Service
    /// </summary>
    public class NeuralNetworkProcessingService : INeuralNetworkProcessingService
    {
        /// <summary>
        /// Process a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="inputs">Inputs</param>
        /// <param name="activationFn">Activation Function</param>
        /// <param name="traceActivity">Trace Activity</param>
        public void Process(INeuralNetwork neuralNetwork, double[] inputs, Func<double, double> activationFn, bool traceActivity = false)
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

            if (traceActivity)
            {
                WriteCurrentStateToTraceLog(neuralNetwork);
            }
        }

        /// <summary>
        /// Write Current State To Trace Log
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        public void WriteCurrentStateToTraceLog(INeuralNetwork neuralNetwork)
        {
            if (neuralNetwork.InputLayer != null && neuralNetwork.HiddenLayer != null && neuralNetwork.OutputLayer != null)
            {
                // Output the inputs
                neuralNetwork.TraceLog.Add("Inputs:");
                for (int i = 0; i < neuralNetwork.InputLayer.Count; i++)
                {
                    var inputNeuron = (WorkerNeuron)neuralNetwork.InputLayer[i];

                    neuralNetwork.TraceLog.Add(string.Format("Neuron {0} | Val: {1}", i, inputNeuron.Input));
                }

                neuralNetwork.TraceLog.Add(string.Empty);

                // Run > Outputs
                neuralNetwork.TraceLog.Add("Outputs:");
                for (int i = 0; i < neuralNetwork.OutputLayer.Count; i++)
                {
                    var outputNeuron = (OutputNeuron)neuralNetwork.OutputLayer[i];

                    neuralNetwork.TraceLog.Add(string.Format("Neuron {0} ({1}) : {2} : err {3}", i, outputNeuron.Description, outputNeuron.Input, outputNeuron.Error));
                }

                neuralNetwork.TraceLog.Add(string.Empty);

                // Write out Weight Info
                neuralNetwork.TraceLog.Add("Input Layer Weights");
                neuralNetwork.TraceLog.Add("Initial:");
                WriteArrayToTraceLog(neuralNetwork, neuralNetwork.InitialInputWeights);
                neuralNetwork.TraceLog.Add("Trained:");
                foreach (IWorkerNeuron neuron in neuralNetwork.InputLayer)
                {
                    WriteArrayToTraceLog(neuralNetwork, neuron.Weights, false);
                }

                neuralNetwork.TraceLog.Add(string.Empty);

                neuralNetwork.TraceLog.Add("Hidden Layer Weights");
                neuralNetwork.TraceLog.Add("Initial:");
                WriteArrayToTraceLog(neuralNetwork, neuralNetwork.InitialHiddenWeights);
                neuralNetwork.TraceLog.Add("Trained:");
                foreach (IWorkerNeuron neuron in neuralNetwork.HiddenLayer)
                {
                    WriteArrayToTraceLog(neuralNetwork, neuron.Weights, false);
                }
            }
        }

        /// <summary>
        /// Write Array To Trace Log
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="array">Array</param>
        /// <param name="addNewLineAtEndOfOutput">Add New Line to end of output</param>
        private static void WriteArrayToTraceLog(INeuralNetwork neuralNetwork, double[] array, bool addNewLineAtEndOfOutput = true)
        {
            if (array != null)
            {
                var sb = new StringBuilder();

                for (int i = 0; i < array.Length; i++)
                {
                    double d = array[i];
                    sb.AppendFormat(string.Format("{0}, ", d.ToString("F12")));
                }

                neuralNetwork.TraceLog.Add(sb.ToString());

                if (addNewLineAtEndOfOutput)
                {
                    neuralNetwork.TraceLog.Add(string.Empty);
                }
            }
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

            for (int i = 0; i < forwardNeurons.Count(); i++)
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
