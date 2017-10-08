using System;
using System.Collections.Generic;

namespace BankLedgerVS17
{
    class Account
    {
        public Account(string accountName)
        {
            AccountName = accountName;
        }

        public string AccountName { get; private set; }
        public decimal AccountBalance { get; private set; } = 0;
        public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            AccountBalance += amount;

            var trans = new Transaction();
            trans.Amount = amount;
            trans.Date = DateTime.Now;
            trans.Description = "Deposit";

            TransactionHistory.Add(trans);

            return true;
        }

        public bool Withdrawl(decimal amount)
        {
            if (amount <= 0 || AccountBalance - amount < 0)
            {
                return false;
            }

            AccountBalance -= amount;

            var trans = new Transaction();
            trans.Amount = -amount;
            trans.Date = DateTime.Now;
            trans.Description = "Withdrawl";

            TransactionHistory.Add(trans);

            return true;
        }

        public void PrintBalance()
        {
            Console.WriteLine($"Your current balance is: {getPrettyMoney(AccountBalance)}");
        }

        public void PrintTransactionHistory()
        {
            Console.WriteLine("******************************************");

            foreach (var item in TransactionHistory)
            {
                Console.WriteLine($"Date: {item.Date}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Amount: {getPrettyMoney(item.Amount)}");
                Console.WriteLine("******************************************");
            }
        }

        private string getPrettyMoney(decimal amount)
        {
            if (amount < 0)
            {
                return $"(${Math.Round(Math.Abs(amount), 2)})";
            }

            return $"${Math.Round(amount, 2)}";
        }
    }
}
