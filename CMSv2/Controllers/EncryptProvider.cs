using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CMSv2.Service
{
    public class EncryptProvider
    {
        public static string Base64Decrypt(string password)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(password);

            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        }
        public static string Base64Encrypt(string password)
        {
            byte[] strPassword = Convert.FromBase64String(password);

            return Convert.ToBase64String(strPassword);
        }

        public static string Sha256(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
