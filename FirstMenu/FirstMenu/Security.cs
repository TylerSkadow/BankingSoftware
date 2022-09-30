using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FirstMenu
{
    class Security
    {
        public String Salt()
        {
            var saltBytes = new byte[16];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }
            String salted = Convert.ToBase64String(saltBytes);
            return salted;
        }

        public String Hash(String text, String salted)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(text + salted))
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        private byte[] GetHash(String text)
        {
            using (HashAlgorithm hash = SHA256.Create())
            {
                return hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            }
        }
    }
}
