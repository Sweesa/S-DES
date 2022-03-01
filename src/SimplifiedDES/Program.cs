using System.Text;

namespace SimpifiedDES
{
    class Program
    {
        /// <summary>
        /// Entry point for the S-DES project. It runs an S-DES encrypt and decrypt
        /// on the msg using the given 10 bit key.
        /// </summary>
        static void Main()
        {
            string key = "1101101110";
            string msg = "Simplified DES is simpler than you would think.";

            //Encrypt the message
            Console.WriteLine($"Message to encode: {msg} with key: {key}");
            SimpDES sdes = new(key);
            StringBuilder encMsg = new();
            for (int i=0; i<msg.Length;i++)
            {
                encMsg.Append(sdes.encrypt(msg[i]));
            }    	

            //Decrypt the message
            Console.WriteLine($"Message to decode {encMsg.ToString()} with key: {key}");
            StringBuilder msgDec = new();
            for (int i=0; i<encMsg.Length;i++)
            {
                msgDec.Append(sdes.decrypt(encMsg[i]));
            }

            Console.WriteLine($"Decoded message is: {msgDec}");
        }
    }
}
