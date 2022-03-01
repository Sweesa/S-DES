using System.Collections;

namespace SimpifiedDES
{
    /// <summary>
    /// A utility class for the S-DES project
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// GetSubsetBitArray generates a subset of a BitArray based on the passed in params
        /// </summary>
        /// <param name="arr">The BitArray to take a subset from</param>
        /// <param name="start">The starting index of the subset</param>
        /// <param name="end">The ending index of the subset</param>
        /// <returns>A subset BitArray</returns>
        public static BitArray GetSubsetBitArray(BitArray arr, int start, int end)
        {
            BitArray temp = new(end-start);
            int i = 0;
            for(int j = start; j < end; j++)
            {
                temp.Set(i, arr[j]);
                i++;
            }
            return temp;
        }
    }
}