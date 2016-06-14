using System.Security.Cryptography;
using System.Text;

namespace BrettPit.BusinessLogic
{
    public static class EncryptionUtil
    {
        public static string CalculateMd5Hash(this string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var builder = new StringBuilder();

            foreach (var b in hash)
            {
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();

        }
    }
}