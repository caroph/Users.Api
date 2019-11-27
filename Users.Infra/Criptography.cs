using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Users.Infra
{
    public static class Criptography
    {
        private const string SALT = "user-password";
        public static string Generate(string pass, string guid)
        {
            var chavePrimaria = Md5("|g?6lJ7>RY`n6}3[`\"=G'5OTv3:+Ny?Tg19o9?J7k?qNpZ97g9wS425/;>JN<O%");
            var key = Md5($"{Md5(pass)}{chavePrimaria}{SALT}{guid}");

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
        }

        private static string Md5(string parameter)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] md5input = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(parameter));
                StringBuilder sb = new StringBuilder();

                foreach (var item in md5input)
                    sb.Append(item.ToString("X2"));

                return sb.ToString();
            }
        }
    }
}
