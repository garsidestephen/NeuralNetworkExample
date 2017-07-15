using NeuralNetworks.Data;
using NeuralNetworks.Data.Implementation;
using NeuralNetworks.Entities;
using NeuralNetworks.Entities.DTO;
using NeuralNetworks.Entities.Implementation;
using System.Collections.Generic;

namespace NeuralNetworks.Logic.Implementation
{
    /// <summary>
    /// Neural Network Provider Service
    /// </summary>
    public class NeuralNetworkProviderService : INeuralNetworkProviderService
    {
        /// <summary>
        /// Private neural Network Repo
        /// </summary>
        private INeuralNetworkRepo _neuralNetworkRepo;

        /// <summary>
        /// Initializes a new instance of the NeuralNetworkService class
        /// </summary>
        public NeuralNetworkProviderService()
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
                var neuron = neuronList[i];
                neuron.weights = allLayerWeights.GetSlice(counter, counter + numberOfNeuronsInNextLayer);

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
