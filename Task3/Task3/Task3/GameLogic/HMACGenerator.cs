using System;
using System.Security.Cryptography;
using System.Text;

namespace GameLogic
{
    public class HMACGenerator
    {
        public string GenerateHMAC(string message, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }
    }

}
