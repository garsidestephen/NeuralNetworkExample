using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] inputs = new double[] { 0.9, 0.1, 0.8 };
            double[,] weights = new double[,] { { 0.9, 0.3, 0.4 }, { 0.2, 0.8, 0.2 }, { 0.1, 0.5, 0.6 } };
            double[] answers = new double[3];

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    var currentAnswer = answers[i];
                    //var weight = weights[i, j];
                    //var input = inputs[j];

                    //var newAnswer = (weight * input) + currentAnswer;
                    //answers[i] = newAnswer;


                    // 0.9  0.3  0.4   0.9      (0.9x0.9) + (0.3*0.1) + (0.4*0.8)
                    // 0.2  0.8  0.2 X 0.1  SO  (0.2x0.9) + (0.8*0.1) + (0.2*0.8)
                    // 0.1  0.5  0.6   0.8      (0.1x0.9) + (0.5*0.1) + (0.6*0.8)

                    answers[i] = (weights[i, j] * inputs[j]) + answers[i];
                }
            }

            for (int i = 0; i < answers.Length; i++)
            {
                answers[i] = Sigmoid(answers[i]);
            }


            for (int i = 0; i < answers.Length; i++)
            {
                Console.WriteLine(answers[i]);
            }

            Console.ReadLine();
        }

        private static double Sigmoid(double value)
        {
            return value;
        }
    }
}
