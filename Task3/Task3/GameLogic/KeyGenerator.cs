using System;
using System.Security.Cryptography;
using System.Text;

namespace GameLogic
{
    public class KeyGenerator
    {
        public string GenerateKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[32];
                rng.GetBytes(keyBytes);

                StringBuilder key = new StringBuilder();
                foreach (var b in keyBytes)
                {
                    key.Append(b.ToString("X2"));
                }

                return key.ToString();
            }
        }
    }
}

