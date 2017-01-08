using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MoneyKeeper.Core.Extensions
{
    public static class StringExtension
    {
        public static string Encode(this string @string)
        {
            using (SHA256 hash = SHA256.Create())
            {
                IEnumerable<string> hashedBytes =
                    hash.ComputeHash(Encoding.UTF8.GetBytes(@string))
                    .Select(@byte => @byte.ToString("x2"));

                return string.Concat(hashedBytes);
            }
        }
    }
}