using System.Collections;

namespace SimpifiedDES.Functions
{
    /// <summary>
    /// The IP class contains two primary functions of the S-DES algorithm: Forward or IP, and
    /// Reverse which is also known as RIP.
    /// </summary>
    public class IP
    {
        /// <summary>
        /// The Forward method performs an IP Permutation on a plaintext character BitArray.
        /// </summary>
        /// <param name="plainText">A BitArray representation of a char</param>
        /// <returns>The modified character BitArray</returns>
        public static BitArray Forward(BitArray plainText)
        {
            return new BitArray(new bool[]
            {
                plainText[1],
                plainText[5],
                plainText[2],
                plainText[0],
                plainText[3],
                plainText[7],
                plainText[4],
                plainText[6]
            });
        }
        /// <summary>
        /// The Reverse method (called RIP in the documentation) is a reflection of the Forward method. If you were to use the 
        /// forward method on an 8 bit BitArray and subsequently use the Reverse, you would have your original BitArray again.
        /// </summary>
        /// <param name="permutedText">The 8 bit BitArray to reverse</param>
        /// <returns>The modified BitArray</returns>
        public static BitArray Reverse(BitArray permutedText)
        {
            return new BitArray(new bool[]
            {
                permutedText[3],
                permutedText[0],
                permutedText[2],
                permutedText[4],
                permutedText[6],
                permutedText[1],
                permutedText[7],
                permutedText[5]
            });
        }
    }
}