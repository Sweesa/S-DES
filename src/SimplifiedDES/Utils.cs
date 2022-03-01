using System.Collections;

namespace SimpifiedDES
{
    /// <summary>
    /// A utility class for the S-DES project
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// GetNibble generates a nibble sized subset of a BitArray starting from the "start" variable
        /// </summary>
        /// <param name="arr">The BitArray to take a subset from</param>
        /// <param name="from">The starting index of the nibble</param>
        /// <returns>A nibble from the BitArray</returns>
        public static BitArray GetNibble(BitArray arr, int start)
        {
            BitArray temp = new(4);
            if(start+4 > arr.Length)
            {
                Console.WriteLine("Wrong index. Provide a starting index that is at least 4 bits from the end of the BitArray");
                return temp;
            }

            for(int i = 0; i < 4; i++)
            {
                temp.Set(i, arr[i+start]);
            }
            return temp;
        }
    }
}