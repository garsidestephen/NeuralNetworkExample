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

        static void Main(string[] args)
        {
            double[] inputs = new double[] { 0.9, 0.1, 0.8 };
            double[,] weights = new double[,] { { 0.9, 0.3, 0.4 }, { 0.2, 0.8, 0.2 }, { 0.1, 0.5, 0.6 } };
            
            double[] outputs = CalculateNeuronOutputs(inputs, weights, Sigmoid);

            WriteOutputsToConsole(outputs);

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
                    //var currentAnswer = answers[i];
                    //var weight = weights[i, j];
                    //var input = inputs[j];

                    //var newAnswer = (weight * input) + currentAnswer;
                    //answers[i] = newAnswer;


                    // 0.9  0.3  0.4   0.9      (0.9x0.9) + (0.3*0.1) + (0.4*0.8)
                    // 0.2  0.8  0.2 X 0.1  SO  (0.2x0.9) + (0.8*0.1) + (0.2*0.8)
                    // 0.1  0.5  0.6   0.8      (0.1x0.9) + (0.5*0.1) + (0.6*0.8)

                    outputs[i] = (weights[i, j] * inputs[j]) + outputs[i];
                }
            }

            // Apply Sigmoid to outputs
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
                Console.WriteLine(outputs[i]);
            }
        }
    }
}
