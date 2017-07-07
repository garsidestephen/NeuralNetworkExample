using System;

namespace NeuralNetworkExample.BL.Helpers
{
    /// <summary>
    /// Array Helper
    /// </summary>
    public static class ArrayHelper
    {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static double[,] Get2dArraySlice(double[,] originalArray, int startRow, int endRow)
        {
            int numRows = originalArray.GetLength(0);
            int numCols = originalArray.GetLength(1);

            double[,] newArray = new double[(endRow + 1) - startRow, numCols];
            int rowCounter = 0;
            int colCounter = 0;

            for (int i = startRow; i < endRow + 1; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    newArray[colCounter, rowCounter] = originalArray[i, j];
                    rowCounter++;
                }
                rowCounter = 0;
                colCounter++;
            }

            return newArray;
        }

        /// <summary>
        /// Create Random Array
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of cols</param>
        /// <returns>Random Array</returns>
        public static double[,] CreateRandomArray(int rows, int cols)
        {
            // ToDo: Implement
            return ArrayHelper.ConvertStringArrayTo2dDoubleArray("0.9,0.3,0.4, 0.2,0.8,0.2, 0.1,0.5,0.6, 0.3,0.7,0.5, 0.6,0.5,0.2, 0.8,0.1,0.9", rows);
        }
    }
}
