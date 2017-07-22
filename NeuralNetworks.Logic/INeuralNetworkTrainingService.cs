using NeuralNetworks.Entities;
using System;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Neural Network Training Service Interface
    /// </summary>
    public interface INeuralNetworkTrainingService
    {
        /// <summary>
        /// Train a Neural Network
        /// </summary>
        /// <param name="neuralNetwork">Neural Network</param>
        /// <param name="trainingProfile">Training Profile</param>
        void Train(INeuralNetwork neuralNetwork, TrainingProfile trainingProfile);
    }
}
