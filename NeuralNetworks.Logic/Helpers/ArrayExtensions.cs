using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Logic
{
    /// <summary>
    /// Array Extensions
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Get Slice of Array
        /// </summary>
        /// <param name="originalArray">Original Array</param>
        /// <param name="startRow">Start Row</param>
        /// <param name="endRow">End Row</param>
        /// <returns>Slice of original array as new array</returns>
        public static double[] GetSlice(this double[] originalArray, int startRow, int endRow)
        {
            double[] newArray = new double[(endRow) - startRow];
            var counter = 0;

            for (int i = startRow; i < endRow; i++)
            {
                newArray[counter] = originalArray[i];
                counter++;
            }

            return newArray;
        }

        /// <summary>
        /// Get 2d Array Slice
        /// </summary>
        /// <param name="originalArray">Original Array</param>
        /// <param name="startRow">Start Row</param>
        /// <param name="endRow">End Row</param>
        /// <returns>2d Array Slice</returns>
        public static double[,] Get2dArraySlice(this double[,] originalArray, int startRow, int endRow)
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
    }
}
