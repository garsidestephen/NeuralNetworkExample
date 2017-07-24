using NeuralNetworks.Entities.Implementation;
using NeuralNetworks.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NeuralNetworks.Entities.Implementation
{
    /// <summary>
    /// Neural Network
    /// Inputs should be between 0.01 to 0.99 or -1 to +1
    /// Link weights should be small, avoid 0, perhaps 1 to -1
    /// </summary>
    public class NeuralNetwork : INeuralNetwork
    {
        /// <summary>
        /// Initialises a new instance of the NeuralNetwork class
        /// </summary>
        public NeuralNetwork()
        {
            InputLayer = new List<IWorkerNeuron>();
            HiddenLayer = new List<IWorkerNeuron>();
            OutputLayer = new List<IOutputNeuron>();
            TraceLog = new List<string>();
        }

        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Learning Rate
        /// </summary>
        public double LearningRate { get; set; }

        /// <summary>
        /// Initial Inputs
        /// </summary>
        public double[] InitialInputs { get; set; }

        /// <summary>
        /// Gets or sets the activation function type
        /// </summary>
        public ActivationFunctionType ActivationFunction { get; set; }

        /// <summary>
        /// Gets or sets the Input Layer
        /// </summary>
        public IList<IWorkerNeuron> InputLayer { get; internal set; }

        /// <summary>
        /// Gets or sets the Hidden Layer
        /// </summary>
        public IList<IWorkerNeuron> HiddenLayer{ get; internal set; }

        /// <summary>
        /// Gets or sets the Output Layer
        /// </summary>
        public IList<IOutputNeuron> OutputLayer { get; internal set; }

        /// <summary>
        /// Gets or sets the TraceLog
        /// </summary>
        public IList<string> TraceLog { get; internal set; }

        /// <summary>
        /// Write Current State To Trace Log
        /// </summary>
        public void WriteCurrentStateToTraceLog()
        {
            if (InputLayer != null && HiddenLayer != null && OutputLayer != null)
            {
                // Output the inputs
                TraceLog.Add("Inputs:");
                for (int i = 0; i < InputLayer.Count; i++)
                {
                    var inputNeuron = (WorkerNeuron)InputLayer[i];

                    TraceLog.Add(string.Format("Neuron {0} | Val: {1}", i, inputNeuron.Input));
                }

                TraceLog.Add(string.Empty);

                // Run > Outputs
                TraceLog.Add("Outputs:");
                for (int i = 0; i < OutputLayer.Count; i++)
                {
                    var outputNeuron = (OutputNeuron)OutputLayer[i];

                    TraceLog.Add(string.Format("Neuron {0} ({1}) : {2} : err {3}", i, outputNeuron.Description, outputNeuron.Input, outputNeuron.Error));
                }

                TraceLog.Add(string.Empty);

                // Write out Weight Info
                TraceLog.Add("Input Layer Weights");
                TraceLog.Add("Initial:");
                //WriteArrayToConsole(neuralNetwork.);
                TraceLog.Add("Trained:");
                foreach (IWorkerNeuron neuron in InputLayer)
                {
                    WriteArrayToTraceLog(neuron.Weights, false);
                }

                TraceLog.Add(string.Empty);

                TraceLog.Add("Hidden Layer Weights");
                TraceLog.Add("Initial:");
                //WriteArrayToConsole(GetInitialHiddenLayerWeights());
                TraceLog.Add("Trained:");
                foreach (IWorkerNeuron neuron in HiddenLayer)
                {
                    WriteArrayToTraceLog(neuron.Weights, false);
                }
            }
        }

        /// <summary>
        /// Write Array To Trace Log
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="addNewLineAtEndOfOutput">Add New Line to end of output</param>
        private void WriteArrayToTraceLog(double[] array, bool addNewLineAtEndOfOutput = true)
        {
            for (int i = 0; i < array.Length; i++)
            {
                double d = array[i];
                TraceLog.Add(string.Format("{0}, ", d.ToString("F12")));
            }

            if (addNewLineAtEndOfOutput)
            {
                TraceLog.Add(string.Empty);
            }
        }
    }
}
