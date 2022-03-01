using System.Collections;

namespace SimpifiedDES.Functions
{
    /// <summary>
    /// The FK class contains a primary function of the S-DES algorithm. It takes an 8 bit
    /// BitArray and encrypts the first half of it.
    /// </summary>
    public class FK
    {
        /// <summary>
        /// An S-Box used by FK 
        /// </summary>
        private readonly static bool [,,] S0 = new bool[4, 4, 2]
        {
            { {false,true}, {false,false}, {true,true}, {true,false} },
            { {true,true}, {true,false}, {false,true}, {false,false} },
            { {false,false}, {true,false}, {false,true}, {true,true} },
            { {true,true}, {false,true}, {true,true}, {true,false} }
        };
        /// <summary>
        /// An S-Box used by FK 
        /// </summary>
        private readonly static bool [,,] S1 = new bool[4, 4, 2]
        {
            { {false,false}, {false,true}, {true,false}, {true,true} },
            { {true,false}, {false,false}, {false,true}, {true,true} },
            { {true,true}, {false,false}, {false,true}, {false,false} },
            { {true,false}, {false,true}, {false,false}, {true,true} }
        };
        /// <summary>
        /// A mapping of true false tuples to their expected col/row value
        /// </summary>
        private readonly static Dictionary<(bool, bool), int> boolsToIndex = new()
        {
            {(false, false), 0},
            {(false, true), 1},
            {(true, false), 2},
            {(true, true), 3}
        };

        /// <summary>
        /// Encrypts the first half of a BitArray
        /// </summary>
        /// <param name="bits">An 8 Bit BitArray</param>
        /// <param name="key">The 8 bit permutated key</param>
        /// <returns>An 8 bit BitArray encrypted by Fk</returns>
        public static BitArray Compute(BitArray bits, BitArray key)
        {
            BitArray fRes = f(Utils.GetNibble(bits, 4), key);
            BitArray x = xor(Utils.GetNibble(bits, 0), fRes);

            return new BitArray(new bool[]
            {
                x[0],
                x[1],
                x[2],
                x[3],
                bits[4],
                bits[5],
                bits[6],
                bits[7]
            });
        }
        /// <summary>
        /// A method that performs permutations and maps a 4 digit BitArray to the 
        /// S0 and S1 true/false tuples.
        /// </summary>
        /// <param name="right">The right side fo the passed in BitArray</param>
        /// <param name="sk">The key used to encrypt the BitArray</param>
        /// <returns>A translated 4 bit BitArray</returns>
        private static BitArray f(BitArray right, BitArray sk)
        {
            BitArray epRes = ep(right);
            BitArray xorRes = xor(epRes, sk);
            BitArray s0 = sBox(Utils.GetNibble(xorRes, 0), S0);
            BitArray s1 = sBox(Utils.GetNibble(xorRes, 4), S1);
            return p4(s0,s1);
        }
        /// <summary>
        /// The xor method xors two passed in BitArrays
        /// </summary>
        /// <param name="b1">A BitArray</param>
        /// <param name="b2">A BitArray</param>
        /// <returns>The xor of two BitArrays</returns>
        private static BitArray xor(BitArray b1, BitArray b2)
        {
            BitArray x = (BitArray) b1.Clone();
            x.Xor(b2);
            return x;
        }
        /// <summary>
        /// Maps a 4 bit BitArray to a [4,4,2] 3 dimensional array. Note that
        /// there are 16 pozzible 4 bit integers. The same number as the number of rows * cols
        /// </summary>
        /// <param name="input">A 4 bit BitArray</param>
        /// <param name="s">The s-box to use for mapping</param>
        /// <returns>The true/false pair at the row/col of the S-Box as a BitArray</returns>
        private static BitArray sBox(BitArray input, bool [,,] s)
        {
            int row = boolsToIndex[(input[0], input[3])];
            int col = boolsToIndex[(input[1], input[2])];
            
            return new BitArray(new bool[]
            {
                s[row, col, 0],
                s[row, col, 1]
            });
        }
        /// <summary>
        /// Concatenates two 2 bit BitArrays
        /// </summary>
        /// <param name="part1">The first part of the BitArray to be concatenated</param>
        /// <param name="part2">The second part of the BitArray to be concatenated</param>
        /// <returns>A 4 bit BitArray</returns>
        private static BitArray p4(BitArray part1, BitArray part2)
        {
            return new BitArray(new bool[]
            {
                part1[1],
                part2[1],
                part2[0],
                part1[0]
            });
        }
        /// <summary>
        /// Doubles the size of the Bitarray by right shifting the first four element
        /// and adding a copy of the first four elements left shifted to the end
        /// </summary>
        /// <param name="input">A 4 bit BitArray</param>
        /// <returns>An 8 bit BitArray</returns>
        private static BitArray ep(BitArray input)
        {
            return new BitArray(new bool[]
            {
                input[3],
                input[0],
                input[1],
                input[2],
                input[1],
                input[2],
                input[3],
                input[0]
            });
        }
    }
}