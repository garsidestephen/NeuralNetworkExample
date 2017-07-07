﻿using NeuralNetworkExample.Entities;
using NeuralNetworkExample.Entities.Implementation;

namespace NeuralNetworkExample.DAL.Implementation
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
        public INeuralNetwork Get(int id)
        {
            // ToDo: Implement EF
            var neuralNetwork = new NeuralNetwork();

            return neuralNetwork;
        }
    }
}
