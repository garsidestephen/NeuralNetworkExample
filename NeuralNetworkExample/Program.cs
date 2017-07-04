using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkExample
{
    /// <summary>
    /// C# Neural Network Example
    /// </summary>
    class Program
    {
        /// <summary>
        /// Sigmoid Constant
        /// </summary>
        private const double SigmoidConst = 2.71828;

        /// <summary>
        /// Logging Enabled
        /// </summary>
        private static bool _loggingEnabled = true;

        /// <summary>
        /// Main Prog Entry Point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int numberOfNetworkLayers = 2;

            double[] initialInputs = GetInitialInputs();
            int numberOfInitialInputs = initialInputs.Length;

            double[,] allNetworkWeights = GetAllNetworkWeights(numberOfInitialInputs);

            double[] outputs = initialInputs;
            double[,] weightsForCurrentLayer;

            for (int i = 0; i < numberOfNetworkLayers; i++)
            {
                weightsForCurrentLayer = GetWeightsForCurrentLayer(allNetworkWeights, i, numberOfInitialInputs);
                outputs = CalculateNeuronOutputs(outputs, allNetworkWeights, Sigmoid);

                WriteToConsole(string.Format("Level {0} Outputs:", i + 1));
                WriteNeuronOutputsToConsole(outputs);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Calculate Neuron Outputs
        /// </summary>
        /// <param name="neuronInputs"></param>
        /// <param name="weights"></param>
        /// <param name="activationFunction"></param>
        /// <returns>Neuron Outputs</returns>
        private static double[] CalculateNeuronOutputs(double[] neuronInputs, double[,] weights, Func<double, double> activationFunction)
        {
            double[] outputs = new double[neuronInputs.Length];

            // Multiply Inputs * Weights
            for (int i = 0; i < neuronInputs.Length; i++)
            {
                for (int j = 0; j < neuronInputs.Length; j++)
                {
                    var input = neuronInputs[j];
                    var weight = weights[i, j];

                    // 0.9  0.3  0.4   0.9      (0.9x0.9) + (0.3*0.1) + (0.4*0.8)
                    // 0.2  0.8  0.2 X 0.1  SO  (0.2x0.9) + (0.8*0.1) + (0.2*0.8)
                    // 0.1  0.5  0.6   0.8      (0.1x0.9) + (0.5*0.1) + (0.6*0.8)

                    outputs[i] = (input * weight) + outputs[i];
                }
            }

            // Apply Activation Function to outputs
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i] = activationFunction(outputs[i]);
            }

            return outputs;
        }

        /// <summary>
        /// Sigmoid Func 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static double Sigmoid(double value)
        {
            return 1 / (1 + Math.Pow(SigmoidConst, -value));
        }

        /// <summary>
        /// Write Neuron Output To Console
        /// </summary>
        /// <param name="outputs"></param>
        private static void WriteNeuronOutputsToConsole(double[] outputs)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                WriteToConsole(outputs[i].ToString());
            }
        }

        /// <summary>
        /// Write String to Console
        /// </summary>
        /// <param name="message"></param>
        private static void WriteToConsole(string message)
        {
            if (_loggingEnabled)
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Gets Initial Inputs
        /// </summary>
        /// <returns></returns>
        private static double[] GetInitialInputs()
        {
            // ToDo: Get from DB
            return new double[] { 0.9, 0.1, 0.8 };
        }

        /// <summary>
        /// Get All Network Weights
        /// </summary>
        /// <param name="numberOfInputNeurons"></param>
        /// <returns></returns>
        private static double[,] GetAllNetworkWeights(int numberOfInputNeurons)
        {
            //ToDo: Pass in network ID and return a network Obj
            string weights = "0.9,0.3,0.4, 0.2,0.8,0.2, 0.1,0.5,0.6, 0.3,0.7,0.5, 0.6,0.5,0.2, 0.8,0.1,0.9";

            return ConvertStringArrayTo2dDoubleArray(weights, numberOfInputNeurons);
        }

        /// <summary>
        /// Convert String Array To 2d Array (Of Doubles)
        /// </summary>
        /// <param name="commaDelimitedStringOfDoubles"></param>
        /// <param name="numberOfColumnsInArray"></param>
        /// <returns></returns>
        private static double[,] ConvertStringArrayTo2dDoubleArray(string commaDelimitedStringOfDoubles, int numberOfColumnsInArray)
        {
            string[] arrayOfStringDoubles = commaDelimitedStringOfDoubles.Replace(" ", string.Empty).Split(',');
            int numberOfRowsInArray = arrayOfStringDoubles.Length / numberOfColumnsInArray;

            double[,] arrayOfDoubles = new double[numberOfRowsInArray, numberOfColumnsInArray];

            int counter = 0;

            for (int i = 0; i < numberOfRowsInArray; i++)
            {
                for (int j = 0; j < numberOfColumnsInArray; j++)
                {
                    arrayOfDoubles[i, j] = Double.Parse(arrayOfStringDoubles[counter]);
                    counter++;
                }
            }

            return arrayOfDoubles;
        }

        /// <summary>
        /// Get Weights For Current Layer
        /// </summary>
        /// <param name="allNetworkWeights"></param>
        /// <param name="layerNumber"></param>
        /// <param name="numberOfInitialInputs"></param>
        /// <returns></returns>
        private static double[,] GetWeightsForCurrentLayer(double[,] allNetworkWeights, int layerNumber, int numberOfInitialInputs)
        {
            // Return segment of allNetworkWeights based on layerNumber and numberOfInitialInputs
            return null;
        }
    }
}
