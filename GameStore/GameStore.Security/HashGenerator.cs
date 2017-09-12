using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Security
{
    public class HashGenerator : IHashGenerator<string>
    {
        public string Generate(string input)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder();

            foreach (var @byte in hash)
            {
                sb.Append(@byte.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
