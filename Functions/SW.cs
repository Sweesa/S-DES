using System.Collections;

namespace SimpifiedDES.Functions
{
    /// <summary>
    /// The SW class contains one of the primary functions of S-DES which swaps the front and
    // back half of the 8 bit BitArray
    /// </summary>
    public class SW
    {
        /// <summary>
        /// The computer method swaps the front 4 and back 4 bits of the given BitArray
        /// </summary>
        /// <param name="input">The BitArray to swap</param>
        /// <returns>A BitArray that has the front 4 and back 4 swapped</returns>
        public static BitArray Compute(BitArray input)
        {
            return new BitArray( new bool[]
            {
                input[4],
                input[5],
                input[6],
                input[7],
                input[0],
                input[1],
                input[2],
                input[3]
            });
        }
    }
}