using System.Collections.Generic;

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
            return password;
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
