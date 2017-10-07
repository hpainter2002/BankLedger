using System;
using System.Collections.Generic;

namespace BankLedgerVS17
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class Authentication
    {
        public void CreateNewAccount(User user)
        {

        }

        public void AuthenticateUser(User user)
        {

        }

        public void Login(User user)
        {

        }

        public void Logout(User user)
        {

        }
    }

    class Datastore
    {

    }

    class ViewAccount : Account
    {
        public void CheckBalance()
        {
            Console.WriteLine("The current balance is: $" + GetBalance());
        }

        public void ViewTransactionHistory()
        {
            foreach (var item in TransactionHistory)
            {
                Console.WriteLine(item + "\n");
            }
        }
    }

    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool ValidUser { get; set; }
    }

    class Account
    {
        public int AccountNumber { get; set; }
        private double AccountBalance { get; set; }
        public List<string> TransactionHistory { get; set; }

        public void Deposit(double amount)
        {
            AccountBalance += amount;
        }

        public void Withdraw(double amount)
        {
            AccountBalance -= amount;
        }

        public double GetBalance()
        {
            return AccountBalance;
        }

        public int GetAccountNumber()
        {
            return AccountNumber;
        }

    }
}
