using NeuralNetworks.Data.Abstractions;
using NeuralNetworks.Entities;

namespace NeuralNetworks.Data
{
    /// <summary>
    /// Neural Network Repo
    /// </summary>
    public class NeuralNetworkRepo : INeuralNetworkRepo
    {
        /// <summary>
        /// Get Neural Network
        /// </summary>
        /// <param name="id">Neural Network Id</param>
        /// <returns>Neural Network</returns>
        public NeuralNetwork Get(int id)
        {
            // ToDo: Implement EF
            var neuralNetwork = new NeuralNetwork();

            return neuralNetwork;
        }
    }
}
