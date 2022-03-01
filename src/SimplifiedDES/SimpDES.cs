using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using SimpifiedDES.Functions;

namespace SimpifiedDES
{
    /// <summary>
    /// The SimpleDES class does S-DES encryption of characters based using the S-DES
    /// algorithm and provided key.
    /// </summary>
    public class SimpDES
    {
        /// <summary>
        /// The S-DES key used for encryption and decryption
        /// </summary>
        private BitArray masterKey;
        /// <summary>
        /// Setter and custom getter for the private masterKey
        /// </summary>
        public BitArray MasterKey
        {
            get
            { 
                return (BitArray)masterKey.Clone(); 
            }
            set 
            { 
                this.masterKey = value; 
            }
        }

        /// <summary>
        /// Constructor for SimpDES. Validates and sets the provided key, throwing and error if 
        // it does not match the regex.
        /// </summary>
        /// <param name="providedKey">A 10 bit string</param>
        public SimpDES(string providedKey)
        {
            //Validate the format of the string
            Regex keyFormat = new("[0,1]{10}");
            if(!keyFormat.IsMatch(providedKey))
            {
                throw new ArgumentException("Invalid key. The key must be a String of length 10 containing only 0 or 1");
            }
            
            //Translate the string to a bit array
            BitArray key = new (10);
            for(int i=0; i < 10; i++)
            {
                key.Set(i, providedKey[i] == '1');
            }
            MasterKey = key;
        }
        /// <summary>
        /// Performs an S-DES encrypt on a character
        /// </summary>
        /// <param name="c">The character to encrypt</param>
        /// <returns>An encrypted character</returns>
        public char encrypt(char c)
        {
            //Generate and get keys
            List<BitArray> keys = KeyGen.GetKey(MasterKey);
            BitArray k1 = keys[0];
            BitArray k2 = keys[1];
            
            // Convert character to binary string
            // Doesn't put the 0 at the left, stop at the last 1.
            string binary = Convert.ToString((int)c, 2).PadLeft(8, '0');
            if (binary.Length > 8)
            {
                throw new ArgumentException("Wrong charset, characters must be encoded with 8 bit");
            }
            
            BitArray b = new(8);
            for(int k = 7; k >= 0; k--)
            {
                b.Set(k, binary[k] == '1');
            }
            
            BitArray ip = IP.Forward(b);
            BitArray fk1 = FK.Compute(ip, k1);
            //Because FK is only performed on the first half of the BitArray, the left and right 
            //sides must be Swappped and FK mnust be performed again
            BitArray inverse = SW.Compute(fk1);
            BitArray fk2 = FK.Compute(inverse, k2);
            BitArray enc = IP.Reverse(fk2);
            
            StringBuilder res = new();
            for (int j=0; j<8; j++)
            {
                res.Append(enc[j] ? "1" : "0");
            }
            
            int i = Convert.ToInt32(res.ToString(), 2);
            return (char)i;
        }
        /// <summary>
        /// Performs an S-DES decypt on a character
        /// </summary>
        /// <param name="c">The character to decrypt</param>
        /// <returns>A decrypted character</returns>
        public char decrypt(char c)
        {
            List<BitArray> keys = KeyGen.GetKey(MasterKey);
            BitArray k1 = keys[0];
            BitArray k2 = keys[1];
            
            string binary = Convert.ToString((int)c, 2).PadLeft(8, '0');
            if (binary.Length != 8)
            {
                throw new ArgumentException("Wrong charset, characters must be encoded with 8 bit");
            }
            BitArray b = new(8);
            for(int j= 7; j>=0; j--)
            {
                b.Set(j, binary[j] == '1');
            }
            
            BitArray ip = IP.Forward(b);
            BitArray fk1 = FK.Compute(ip,k2);
            //Because FK is only performed on the first half of the BitArray, the left and right 
            //sides must be Swappped and FK mnust be performed again
            BitArray inverse = SW.Compute(fk1);
            BitArray fk2 = FK.Compute(inverse, k1);
            BitArray dec = IP.Reverse(fk2);
            
            StringBuilder res = new();
            for (int k=0; k<8; k++)
            {
                res.Append(dec[k] ? "1" : "0") ;
            }
            int i = Convert.ToInt32(res.ToString(), 2);
            return (char)i;
        }
    }
}