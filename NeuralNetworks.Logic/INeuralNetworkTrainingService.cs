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
        /// <param name="traceActivity">Trace Activity</param>
        void Train(INeuralNetwork neuralNetwork, TrainingProfile trainingProfile, bool traceActivity = false);
    }
}
