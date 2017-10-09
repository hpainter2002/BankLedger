using BankLedgerVS17;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankLedgerTests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void DepositValidAmount()
        {
            decimal beginningBalance = 23.15m;
            decimal amountToDeposit = 56.65m;
            decimal expectedAmount = 23.15m + 56.65m;

            Account acct = new Account("My Test Account");

            acct.Deposit(beginningBalance);

            Assert.AreEqual(beginningBalance, acct.AccountBalance);

            acct.Deposit(amountToDeposit);

            Assert.AreEqual(expectedAmount, acct.AccountBalance);
        }

        [TestMethod]
        public void DepositInvalidAmount()
        {
            decimal beginningBalance = 197.76m;
            decimal amountToDeposit = 92.42m;
            decimal expectedAmount = 197.76m + 56.65m;
            decimal negativeAmount = -51.89m;

            Account acct = new Account("My Test Account");

            acct.Deposit(beginningBalance);
            Assert.AreEqual(beginningBalance, acct.AccountBalance);

            acct.Deposit(amountToDeposit);
            Assert.AreNotEqual(expectedAmount, acct.AccountBalance);
            Assert.AreEqual(acct.Deposit(negativeAmount), false);
        }

        [TestMethod]
        public void WtihdrawValidAmount()
        {
            decimal beginningBalance = 273.15m;
            decimal amountToWithdraw = 44.65m;
            decimal expectedAmount = 273.15m - 44.65m;

            Account acct = new Account("My Test Account");

            acct.Deposit(beginningBalance);
            Assert.AreEqual(beginningBalance, acct.AccountBalance);

            acct.Withdrawl(amountToWithdraw);
            Assert.AreEqual(expectedAmount, acct.AccountBalance);
        }

        [TestMethod]
        public void WithdrawInvalidAmount()
        {
            decimal beginningBalance = 273.15m;
            decimal amountToWithdraw = 300m;

            Account acct = new Account("My Test Account");

            acct.Deposit(beginningBalance);
            Assert.AreEqual(beginningBalance, acct.AccountBalance);

            Assert.AreEqual(acct.Withdrawl(amountToWithdraw), false);
        }
    }
}
