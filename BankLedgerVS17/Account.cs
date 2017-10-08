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
            trans.amount = amount;
            trans.date = DateTime.Now;
            trans.description = "Deposit";

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
            trans.amount = -amount;
            trans.date = DateTime.Now;
            trans.description = "Withdrawl";

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
                Console.WriteLine($"Date: {item.date}");
                Console.WriteLine($"Description: {item.description}");
                Console.WriteLine($"Amount: {getPrettyMoney(item.amount)}");
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
