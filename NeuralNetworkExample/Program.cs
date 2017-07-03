using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkExample
{
    class Program
    {
        private const double SigmoidConst = 2.71828;

        private static bool _loggingEnabled = true;

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

                Log(string.Format("Level {0} Outputs:", i + 1));
                WriteOutputsToConsole(outputs);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="weights"></param>
        /// <param name="activationFunction">Neural Activation Func To Call</param>
        /// <returns></returns>
        private static double[] CalculateNeuronOutputs(double[] inputs, double[,] weights, Func<double, double> activationFunction)
        {
            double[] outputs = new double[inputs.Length];

            // Multiply Inputs * Weights
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    var input = inputs[j];
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
        /// Write Outputs To Console
        /// </summary>
        /// <param name="outputs"></param>
        private static void WriteOutputsToConsole(double[] outputs)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                Log(outputs[i].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void Log(string message)
        {
            if (_loggingEnabled)
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static double[] GetInitialInputs()
        {
            return new double[] { 0.9, 0.1, 0.8 };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static double[,] GetAllNetworkWeights(int numberOfInputNeurons)
        {
            string weights = "0.9,0.3,0.4, 0.2,0.8,0.2, 0.1,0.5,0.6, 0.3,0.7,0.5, 0.6,0.5,0.2, 0.8,0.1,0.9";

            return ConvertStringArrayTo2dDoubleArray(weights, numberOfInputNeurons);
        }

        /// <summary>
        /// 
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
        /// 
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
