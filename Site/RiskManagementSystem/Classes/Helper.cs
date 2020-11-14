using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiskManagementSystem
{
    public static class Helper
    {
        public static string CreateHashPassword(string password)
        {
            byte[] encode = new byte[password.Length];
            encode = System.Text.Encoding.UTF8.GetBytes(password);
            string hashPassword = Convert.ToBase64String(encode);

            return hashPassword;
        }
    }
}