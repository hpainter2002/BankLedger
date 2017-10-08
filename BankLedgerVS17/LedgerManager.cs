namespace BankLedgerVS17
{
    class LedgerManager
    {
        static int NextAccountNumber
        {
            get => m_nextAccountNumber++;
        }

        private static int m_nextAccountNumber = 1;

        public User CreateNewUser(string username, string password)
        {

            if (!Datastore.Data.ContainsKey(username.ToUpper()))
            {
                User newUser = new User()
                {
                    Username = username.ToUpper(),
                    Password = Authentication.HashPassword(password)
                };

                Datastore.Data[newUser.Username] = newUser;

                return Login(username, password);
            }

            return null;
        }

        public User Login(string username, string password) => Authentication.Login(username, password);

        public void Logout(string token) => Authentication.Logout(token);

    }
}
