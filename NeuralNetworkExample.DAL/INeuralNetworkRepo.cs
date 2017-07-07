using NeuralNetworkExample.Entities;

namespace NeuralNetworkExample.DAL
{
    /// <summary>
    /// Neural Network Repo Interface
    /// </summary>
    public interface INeuralNetworkRepo
    {
        /// <summary>
        /// Get Neural Network
        /// </summary>
        /// <param name="id">Neural Network Id</param>
        /// <returns>Neural Network</returns>
        INeuralNetwork Get(int id);
    }
}
