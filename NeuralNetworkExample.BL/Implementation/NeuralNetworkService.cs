using NeuralNetworkExample.BL.Helpers;
using NeuralNetworkExample.DAL;
using NeuralNetworkExample.DAL.Implementation;
using NeuralNetworkExample.Entities;
using NeuralNetworkExample.Entities.DTO;
using NeuralNetworkExample.Entities.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkExample.BL.Implementation
{
    /// <summary>
    /// Neural Network Service
    /// </summary>
    public class NeuralNetworkService : INeuralNetworkService
    {
        /// <summary>
        /// Private neural Network Repo
        /// </summary>
        private INeuralNetworkRepo _neuralNetworkRepo;

        /// <summary>
        /// Initializes a new instance of the NeuralNetworkService class
        /// </summary>
        public NeuralNetworkService()
        {
            // ToDo: Implement DI
            _neuralNetworkRepo = new NeuralNetworkRepo();
        }

        /// <summary>
        /// Get Neural Network
        /// </summary>
        /// <param name="id">Neural Network Id</param>
        /// <returns>Neural Network</returns>
        public INeuralNetwork Get(int id)
        {
            return _neuralNetworkRepo.Get(id);
        }

        /// <summary>
        /// Create Neural Network
        /// </summary>
        /// <param name="numberOfInitialInputs">Number Of Initial Inputs</param>
        /// <param name="networkOutputs">Network Outputs</param>
        /// <param name="initialInputWeights">Initial Input Weights</param>
        /// <param name="initialHiddenLayerWeights">Initial Hidden Layer Weights</param>
        /// <returns></returns>
        public INeuralNetwork Create(int numberOfInitialInputs, IList<NetworkOutput> networkOutputs, double[] initialInputWeights = null, double[] initialHiddenLayerWeights = null)
        {
            var neuralNetwork = new NeuralNetwork();

            // Calculate how many neurons we need in the hidden layer (mean of num inputs + num outputs)
            int numberOfNeuronsInHiddenLayer = (numberOfInitialInputs + networkOutputs.Count) / 2;

            // Create Input Layer
            CreateProcessingLayer(neuralNetwork.InputLayer, numberOfInitialInputs, numberOfNeuronsInHiddenLayer, initialInputWeights);

            // Create Hidden Layer
            CreateProcessingLayer(neuralNetwork.HiddenLayer, numberOfNeuronsInHiddenLayer, networkOutputs.Count, initialHiddenLayerWeights);

            // Create Ouput Layer
            CreateOutputLayer(neuralNetwork, networkOutputs);

            return neuralNetwork;
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
        }

        public void BackPropogate(INeuralNetwork neuralNetwork)
        {
            //double[] previousLayerErrors = neuralNetwork.OutputLayer.OutputNeurons.Select(x => x.Error).ToArray();

            //for (int i = neuralNetwork.ProcessingLayers.Count; i > 0; i--)
            //{
            //    ProcessingLayer currentProcessingLayer = neuralNetwork.ProcessingLayers[i];

            //    for (int j = 0; j < currentProcessingLayer.Weights.Length; j++)
            //    {
            //        // Consider adding a property to p[rocessing layer to indicate how many neurons there are rather than using initial inputs.
            //    }
            //}
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
                    var input =  neuronsInCurrentLayer.ElementAt(j).Input;
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
        /// Creates a Processing Layer (i.e. Input, Hidden)
        /// </summary>
        /// <param name="layer">The Layer</param>
        /// <param name="numberOfNeuronsReqd">Number Of Neurons</param>
        /// <param name="numberOfNeuronsReqdInForwardLayer">Number Of Neurons Reqd In Forward Layer</param>
        /// <param name="weights">Weights (Optional)</param>
        private static void CreateProcessingLayer(IList<IWorkerNeuron> layer, int numberOfNeuronsReqd, int numberOfNeuronsReqdInForwardLayer, double[] weights = null)
        {
            if (layer == null)
            {
                layer = new List<IWorkerNeuron>();
            }
            else if (layer.Count > 0)
            {
                // Clear any existing neurons in the input layer
                layer.Clear();
            }

            // Create input layer neurons
            for (int i = 0; i < numberOfNeuronsReqd; i++)
            {
                layer.Add(new WorkerNeuron() { Number = i });
            }

            // Do we have any Input weights?
            if (weights == null)
            {
                // Nope, so create some
                CreateRandomWeightsArray(layer.Count, numberOfNeuronsReqdInForwardLayer);
            }

            PopulateNeuronWeights(layer, weights, numberOfNeuronsReqdInForwardLayer);
        }

        /// <summary>
        /// Populate Neuron Weights
        /// </summary>
        /// <param name="neuronList">Neuron List</param>
        /// <param name="allLayerWeights">All Layer Weights</param>
        /// <param name="numberOfNeuronsInNextLayer">Number Of Neurons In Next Layer</param>
        private static void PopulateNeuronWeights(IList<IWorkerNeuron> neuronList, double[] allLayerWeights, int numberOfNeuronsInNextLayer)
        {
            int counter = 0;

            for (int i = 0; i < neuronList.Count; i++)
            {
                neuronList[i].weights = allLayerWeights.GetSlice(counter, counter + numberOfNeuronsInNextLayer);
                counter += numberOfNeuronsInNextLayer;
            }
        }

        /// <summary>
        /// Populate Output Layer
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="outputs">Outputs</param>
        private static void CreateOutputLayer(INeuralNetwork neuralNetwork, IList<NetworkOutput> outputs)
        {
            //ToDo: Implement Automapper
            foreach (var expectedOutput in outputs)
            {
                neuralNetwork.OutputLayer.Add(new OutputNeuron() { Number = expectedOutput.Number, Description = expectedOutput.Description, ExpectedOutput = expectedOutput.ExpectedOutput });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuronCount"></param>
        /// <param name="forwardLayerNeuronCount"></param>
        /// <returns></returns>
        private static double[] CreateRandomWeightsArray(int neuronCount, int forwardLayerNeuronCount)
        {
            // ToDo: Implement
            return new double[] { };
        }
    }
}
