using System.Collections.Generic;

namespace BankLedgerVS17
{
    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public List<Account> Accounts = new List<Account>();
    }
}
