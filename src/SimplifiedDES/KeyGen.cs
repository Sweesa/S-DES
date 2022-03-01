using System.Collections;

namespace SimpifiedDES
{
    /// <summary>
    /// The KeyGen class contains all methods required to generate the two permutated keys used in S-DES   
    // from an unmodified master key 
    /// </summary>
    public class KeyGen
    {
        /// <summary>
        /// The single entry point for the KeyGen class.
        /// </summary>
        /// <param name="masterKey">The 10 bit S-DES umodified master Key</param>
        /// <returns>Two 8 bit permutated keys</returns>
        public static List<BitArray> GetKey(BitArray masterKey)
        {
            BitArray p10result = p10(masterKey);
            
            BitArray c1 = circularLeftShift(p10result, 1);     
            BitArray c2 = circularLeftShift(c1, 2);

            BitArray p8c1 = p8(c1);
            BitArray p8c2 = p8(c2);

            return new List<BitArray>()
            {
                p8c1,
                p8c2
            };
        }
        /// <summary>
        /// The p10 method generates a permutation of the key BitArray
        /// </summary>
        /// <param name="key">The 10 bit S-DES key</param>
        /// <returns>A 10 bit modified S-DES key</returns>
        private static BitArray p10(BitArray key)
        {
            return new BitArray(new bool[]
            {
                key[2],
                key[4],
                key[1],
                key[6],
                key[3],
                key[9],
                key[0],
                key[8],
                key[7],
                key[5],
            });
        }
        /// <summary>
        /// The p8 method generates a permutation of the key[1..9] BitArray
        /// </summary>
        /// <param name="key">The 10 bit S-DES key</param>
        /// <returns>An 8 bit modified S-DES key</returns>
        private static BitArray p8(BitArray key)
        {
            return new BitArray(new bool[]
            {
                key[5],
                key[2],
                key[6],
                key[3],
                key[7],
                key[4],
                key[9],
                key[8],
            });
        }

        /// <summary>
        /// Splits a 10 bit BitArray down the middle and shifts the left and right side
        ///  circularly shift times.
        /// </summary>
        /// <param name="key">The 10 bit S-DES key</param>
        /// <param name="shift">The number of times to shift the bits left</param>
        /// <returns>A circularly shift 10 bit S-DES key</returns>
        private static BitArray circularLeftShift(BitArray key, int shift)
        {
            BitArray leftShifted = new(10);
            // shift the left 5 digits left
            for (int i=0; i < 5; i++)
            {
                int shiftedIndex = i - shift;
                if (shiftedIndex < 0)
                {
                    leftShifted.Set(5 + shiftedIndex, key[i]);
                }
                else
                {
                    leftShifted.Set(shiftedIndex, key[i]);
                }
            }
            // shift the right 5 digits right
            for (int i=5; i < 10; i++)
            {
                int shiftedIndex = i - shift;
                if (shiftedIndex < 5)
                {
                    leftShifted.Set(5 + shiftedIndex, key[i]);
                }
                else
                {
                    leftShifted.Set(shiftedIndex, key[i]);
                }
            }
            return leftShifted;
        }
    }
}
