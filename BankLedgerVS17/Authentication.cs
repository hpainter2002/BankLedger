using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BankLedgerVS17
{
    static class Authentication
    {
        private static Dictionary<string, bool> Authentications = new Dictionary<string, bool>();

        public static bool IsAuthenticated(string token)
        {
            return Authentications[token];
        }

        public static string HashPassword(string password)
        {
            StringBuilder hash = new StringBuilder();

            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(password));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static User Login(string username, string password)
        {
            if (Datastore.Data.ContainsKey(username.ToUpper()))
            {
                var user = Datastore.Data[username.ToUpper()];

                if (HashPassword(password) == user.Password)
                {
                    user.Token = GenerateToken();
                    Authentications[user.Token] = true;

                    return user;
                }
            }

            return null;
        }

        private static string GenerateToken()
        {
            return "token";
        }

        public static void Logout(string token)
        {
            if (Authentications.ContainsKey(token))
            {
                Authentications[token] = false;
            }
        }
    }
}
