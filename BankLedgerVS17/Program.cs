using System;

/************************************************************
*                                                           *
* Filename: Program.cs                                      *
* Date: 10/8/2017                                           *
* Author: Hatim Painter                                     *
*                                                           *
* Description: This is a simple bank ledger software        *
*     for keeping track of all your deposits and            *
*     withdrawls. You can also check your balance and       *
*     transaction history. It also has the capability       *
*     of adding multiple accounts to a single user.         *
*                                                           *
* Usage: User must enter the option number to proceed.      *
*                                                           *
*     First time user:                                      *
*                                                           *
*     1. Create a new user                                  *
*     2. Create a new account                               *
*     3. Select an account                                  *
*     4. Deposit/Withdraw/Check Balance/Transaction History *
*     5. Logout                                             *
*                                                           *
*     Registered user:                                      *
*                                                           *
*     1. Login                                              *
*     2. Create a new account / select an account           *
*     3. Deposit/Withdraw/Check Balance/Transaction History *
*     4. Logout                                             *
*                                                           *
************************************************************/

namespace BankLedgerVS17
{
    class Program
    {
        enum UserStatus { LoggedIn, LoggedOut }

        static void Main(string[] args)
        {
            MainMenu();
        }

        /****************************************************************
        * 
        * Function: MainMenu()
        * 
        * Parameter: 
        *      In: none
        *      Out: none
        * 
        * Description: Displays the home and provides the options for the 
        *       user to create a new user, login with existing credentials
        *       or exit the program.
        * 
        *****************************************************************/
        public static void MainMenu()
        {
            LedgerManager ledgerManager = new LedgerManager();

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

                var userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int value))
                {
                    if (value > 0 && value <= 3)
                    {
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
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Incorrect input detected");
                        Console.WriteLine();
                        Console.Write("Press any key to return to menu");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Incorrect input detected");
                    Console.WriteLine();
                    Console.Write("Press any key to return to menu");
                    Console.ReadKey();
                }
            }
        }

        /****************************************************************
         * 
         * Function: CreateNewUser()
         * 
         * Parameter: 
         *      In: User - a user object
         *      Out: none
         * 
         * Description: Displays the registration and asks for an account 
         *      name in order to create a new account for the user passed
         *      in user object   
         * 
         *****************************************************************/
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

        /****************************************************************
        * 
        * Function: UserMenu()
        * 
        * Parameter: 
        *      In: User - a user object
        *      Out: UserStatus - determines if the user is logged in
        *           or logged out
        * 
        * Description: Displays the welcome screen after logging in
        *       and provides the options for the user to create an
        *       account, select an existing account or logout
        * 
        *****************************************************************/
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


                if (int.TryParse(userInput, out int value))
                {
                    if (value > 0 && value <= 3)
                    {
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
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Incorrect input detected");
                        Console.WriteLine();
                        Console.Write("Press any key to return to menu");
                        Console.ReadKey();
                    }

                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Incorrect input detected");
                    Console.WriteLine();
                    Console.Write("Press any key to return to menu");
                    Console.ReadKey();
                }
            }
        }

        /****************************************************************
        * 
        * Function: SelectAccount()
        * 
        * Parameter: 
        *       In: User - a user object
        *       Out: UserStatus - determines if the user is logged in
        *           or logged out
        * 
        * Description: Displays the account selection screen after logging 
        *       in and provides the options for the user to select an 
        *       existing account
        * 
        *****************************************************************/
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
                if (AccountMenu(user.Accounts[(int)(accountNum - 1)]) == UserStatus.LoggedOut)
                {
                    return UserStatus.LoggedOut;
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid account selected! Press any key to return to account menu");
                Console.ReadKey();
            }

            return UserStatus.LoggedIn;
        }

        /****************************************************************
        * 
        * Function: Login()
        * 
        * Parameter: 
        *       In: LedgerManager - ledgerManager object
        *       Out: none
        *       
        * Description: Asks user for username and password upon selecting
        *       Login from the main menu
        * 
        *****************************************************************/
        public static void Login(LedgerManager ledgerManager)
        {
            Console.Clear();

            Console.WriteLine("*****************");
            Console.WriteLine("----- Login -----");
            Console.WriteLine("*****************");
            Console.WriteLine();
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

        /****************************************************************
        * 
        * Function: CreateNewUser()
        * 
        * Parameter: 
        *       In: LedgerManager - ledgerManager object
        *       Out: none
        *       
        * Description: Asks the user to create new username and password
        *       to register a new user
        * 
        *****************************************************************/
        private static void CreateNewUser(LedgerManager ledgerManager)
        {
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine("*----- User Registration -----*");
            Console.WriteLine("*******************************");
            Console.WriteLine();


            while (true)
            {
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                //var password = Console.ReadLine();
                var password = ReadPassword();
                Console.Write("Confirm Password: ");
                var confirmPassword = ReadPassword();

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

        /****************************************************************
        * 
        * Function: AccountMenu()
        * 
        * Parameter: 
        *       In: Account - an account object
        *       Out: UserStatus - determines if the user is logged in
        *           or logged out
        * 
        * Description: Displays the account menu screen after selecting 
        *       an account. Here user has the option to interact with 
        *       the current account they have selected. User can deposit,
        *       withdraw, check balance, or look at the transaction 
        *       history. User can also choose to return to the previous
        *       menu and select another acount or logout.
        * 
        *****************************************************************/
        private static UserStatus AccountMenu(Account account)
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

                if (int.TryParse(userInput, out int value))
                {
                    if (value > 0 && value <= 6)
                    {
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
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Incorrect input detected");
                        Console.WriteLine();
                        Console.Write("Press any key to return to menu");
                        Console.ReadKey();
                    }

                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Incorrect input detected");
                    Console.WriteLine();
                    Console.Write("Press any key to return to menu");
                    Console.ReadKey();
                }

            }
        }

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }

    }
}
