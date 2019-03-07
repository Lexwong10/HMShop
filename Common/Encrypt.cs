using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Common
{
    public static class Encrypt
    {
        public static string Md5(string str) {
            var input = Encoding.Default.GetBytes(str);
            MD5 md5 = MD5.Create("MD5");
            var output = md5.ComputeHash(input);
            string output1 = BitConverter.ToString(output);
            output1 = output1.Replace("-", "");
            return output1;
        }
    }
}
