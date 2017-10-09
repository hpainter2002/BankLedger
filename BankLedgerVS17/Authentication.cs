using System.Security.Cryptography;
using System.Text;

namespace BankLedgerVS17
{
    static class Authentication
    {
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
                    return user;
                }
            }

            return null;
        }

        public static void Logout()
        {

        }
    }
}
