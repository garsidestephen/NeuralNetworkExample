using System;

namespace NeuralNetworks.Common
{
    /// <summary>
    /// Common Functions
    /// </summary>
    public class ArrayHelper
    {
        /// <summary>
        /// Create Random Array
        /// </summary>
        /// <param name="length">Length of Array</param>
        /// <param name="seed">Seed</param>
        /// <returns>Random Array</returns>
        public static double[] CreateRandomArray(int length, int seed)
        {
            double[] randomArray = new double[length];
            Random random = new Random(seed);

            for (int i = 0; i < length; i++)
            {
                randomArray[i] = random.NextDouble();
            }

            return randomArray;
        }

        /// <summary>
        /// Convert String Array To 2d Array (Of Doubles)
        /// </summary>
        /// <param name="commaDelimitedStringOfDoubles">Comma Delimited String Of Doubles</param>
        /// <param name="numberOfColumnsInArray">Number Of Columns In Array</param>
        /// <returns>2d Array</returns>
        public static double[,] ConvertStringArrayTo2dDoubleArray(string commaDelimitedStringOfDoubles, int numberOfColumnsInArray)
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
    }
}
