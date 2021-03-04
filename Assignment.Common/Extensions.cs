using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Assignment.Common.Extensions
{
    public static class CryptographyExtension
    {
        public static string ToHashHMACSHA1(this string str)
        {
            var hash = new HMACSHA1
            {
                Key = Encoding.Unicode.GetBytes(str)
            };

            return Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(str)));
        }
    }
}