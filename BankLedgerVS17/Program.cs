using System;
using System.Collections.Generic;

namespace BankLedgerVS17
{
    class Program
    {
        static void Main(string[] args)
        {
            Datastore MyDatastore = new Datastore();
            User CurrentUser = new User();

            Console.WriteLine("Welcome to the Best Bank Ledger");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("To create an account press [C]");
            Console.WriteLine("To Login press [L]");
            Console.WriteLine("To exit press [E]");
            Console.WriteLine("To logout press [O]");
            var userInput = Console.ReadLine();

            if (userInput.ToLower() == "c")
            {
                Console.WriteLine("Please enter the username");
                var username = Console.ReadLine();
                Console.WriteLine("Please enter the password");
                var password = Console.ReadLine();
                Console.WriteLine("Please confirm the password");
                var confirmPassword = Console.ReadLine();

                if (password == confirmPassword)
                {
                    User user1 = new User();
                    user1.Username = username;
                    user1.Password = password;

                    if (user1.Username != null)
                    {
                        MyDatastore.Data.Add(user1.Username, user1);
                    }

                    CurrentUser = user1;
                }
                else
                {
                    Console.WriteLine("Passwords didn't match please try again");
                }

                Console.WriteLine(CurrentUser.Username + " you are now logged in!");

            }

        }
    }

    class Authentication
    {
        public void CreateNewAccount(User user)
        {
            if (AuthenticateUser(user)) ;
        }

        public bool AuthenticateUser(User user)
        {
            try
            {

            }
            catch (Exception)
            {

            }
            return true;
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
        public Dictionary<string, User> Data = new Dictionary<string, User>();
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
        public string ConfirmPassword { get; set; }
        public bool ValidUser { get; set; }

        public List<Account> Accounts = new List<Account>();
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
