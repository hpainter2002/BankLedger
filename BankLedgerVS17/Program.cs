using System;

namespace BankLedgerVS17
{
    class Program
    {
        enum UserStatus { LoggedIn, LoggedOut }

        static void Main(string[] args)
        {
            MainMenu();
        }

        public static void MainMenu()
        {
            LedgerManager ledgerManager = new LedgerManager();
            //User CurrentUser = new User();
            string userInput;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("*******************************************");
                Console.WriteLine("*---- Welcome to the Best Bank Ledger ----*");
                Console.WriteLine("*******************************************");
                Console.WriteLine("*                                         *");
                Console.WriteLine("* 1. Create a new user                    *");
                Console.WriteLine("* 2. Login                                *");
                Console.WriteLine("* 3. Exit                                 *");
                Console.WriteLine("*                                         *");
                Console.WriteLine("*******************************************");
                Console.WriteLine();
                Console.Write("Please select an option: ");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        CreateNewUser(ledgerManager);
                        break;
                    case "2":
                        Login(ledgerManager);
                        break;

                    case "3":
                        return;
                }
            }

        }

        private static void CreateNewAccount(User user)
        {
            Console.Clear();
            Console.WriteLine("********************************");
            Console.WriteLine("*---- Account Registration ----*");
            Console.WriteLine("********************************");

            Console.WriteLine();
            Console.Write("Please enter the account name: ");
            string accountName = Console.ReadLine();
            user.Accounts.Add(new Account(accountName));
            //Console.WriteLine("Account was successfuly created");
        }

        private static UserStatus UserMenu(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("****************************************");
                Console.WriteLine($"*----   Welcome {user.Username}   ----*");
                Console.WriteLine("****************************************");
                Console.WriteLine("*                                      *");
                Console.WriteLine("* 1. Create a new account              *");
                Console.WriteLine("* 2. Select an account                 *");
                Console.WriteLine("* 3. Logout                            *");
                Console.WriteLine("*                                      *");
                Console.WriteLine("****************************************");
                Console.WriteLine();
                Console.Write("Please select an option: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        CreateNewAccount(user);
                        break;
                    case "2":
                        if (SelectAccount(user) == UserStatus.LoggedOut)
                        {
                            return UserStatus.LoggedOut;
                        }
                        break;
                    case "3":
                        return UserStatus.LoggedOut;
                }
            }
        }
        private static UserStatus SelectAccount(User user)
        {
            if (user.Accounts.Count == 0)
            {
                Console.WriteLine("\n You don't have any accounts. Please create one first");
                Console.WriteLine();
                Console.WriteLine("Press any key to return to previous menu");
                Console.ReadKey();
                return UserStatus.LoggedIn;
            }

            Console.Clear();
            Console.WriteLine("*****************************");
            Console.WriteLine("----- Account Selection -----");
            Console.WriteLine("*****************************");
            Console.WriteLine();

            for (int i = 0; i < user.Accounts.Count; ++i)
            {
                Console.WriteLine($"{i + 1}. {user.Accounts[i].AccountName}");
            }

            Console.WriteLine();
            Console.Write("Please select an option: ");
            string accountNumStr = Console.ReadLine();

            if (uint.TryParse(accountNumStr, out uint accountNum) && accountNum <= user.Accounts.Count)
            {
                if (AccountMenu(user, user.Accounts[(int)(accountNum - 1)]) == UserStatus.LoggedOut)
                {
                    return UserStatus.LoggedOut;
                }
            }

            return UserStatus.LoggedIn;
        }
        public static void Login(LedgerManager ledgerManager)
        {
            Console.Clear();
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var user = ledgerManager.Login(username, password);

            if (user == null)
            {
                Console.WriteLine("Invalid credentials!");
                Console.WriteLine("Press any key to return to the main menu");
                Console.ReadKey();
                return;
            }

            if (UserMenu(user) == UserStatus.LoggedOut)
            {
                ledgerManager.Logout(user.Token);
            }
        }
        private static void CreateNewUser(LedgerManager ledgerManager)
        {
            Console.Clear();
            Console.WriteLine("**************************");
            Console.WriteLine("*----- Registration -----*");
            Console.WriteLine("**************************");
            Console.WriteLine();


            while (true)
            {
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                Console.Write("Confirm Password: ");
                var confirmPassword = Console.ReadLine();

                if (password != confirmPassword)
                {
                    Console.WriteLine();
                    Console.WriteLine("Passwords do not match, please try again!");
                    Console.WriteLine();
                    continue;
                }

                var user = ledgerManager.CreateNewUser(username, password);

                if (user == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Username already exists. Please choose another one.");
                    continue;
                }

                if (UserMenu(user) == UserStatus.LoggedOut)
                {
                    ledgerManager.Logout(user.Token);
                }

                return;
            }
        }
        private static UserStatus AccountMenu(User user, Account account)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("******************************************");
                Console.WriteLine($"*---- Account {account.AccountName} ----*");
                Console.WriteLine("******************************************");
                Console.WriteLine("*                                        *");
                Console.WriteLine("* 1. Deposit                             *");
                Console.WriteLine("* 2. Withdrawl                           *");
                Console.WriteLine("* 3. Check Balance                       *");
                Console.WriteLine("* 4. Transaction History                 *");
                Console.WriteLine("* 5. Back to Previous Menu               *");
                Console.WriteLine("* 6. Logout                              *");
                Console.WriteLine("*                                        *");
                Console.WriteLine("******************************************");
                Console.WriteLine();
                Console.Write("Please select an option: ");
                string userInput = Console.ReadLine();

                if (userInput == "1" || userInput == "2" && account.AccountBalance != 0)
                {
                    bool isAmountValid = false;

                    while (!isAmountValid)
                    {
                        Console.WriteLine();
                        account.PrintBalance();
                        Console.WriteLine();
                        Console.Write("Please Enter Amount: $");
                        string strAmt = Console.ReadLine();

                        if (decimal.TryParse(strAmt, out decimal amt))
                        {
                            if (userInput == "1")
                            {
                                isAmountValid = account.Deposit(amt);
                            }
                            else
                            {
                                isAmountValid = account.Withdrawl(amt);
                            }

                            if (!isAmountValid)
                            {
                                Console.WriteLine("Invalid amount entered, please enter a valid amount.");
                            }
                        }
                    }
                }
                else if (userInput == "2" && account.AccountBalance == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Unable to withdraw! Insufficient account balance!");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                }
                else if (userInput == "3")
                {
                    Console.Clear();
                    Console.WriteLine();
                    account.PrintBalance();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to previous menu");
                    Console.ReadKey();

                }
                else if (userInput == "4")
                {
                    Console.Clear();
                    Console.WriteLine($"Transaction History for {account.AccountName}\n");
                    account.PrintTransactionHistory();
                    Console.WriteLine();
                    account.PrintBalance();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to previous menu");
                    Console.ReadKey();

                }
                else if (userInput == "5")
                {
                    return UserStatus.LoggedIn;
                }
                else if (userInput == "6")
                {
                    return UserStatus.LoggedOut;
                }
            }
        }
    }
}
