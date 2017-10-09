using BankLedgerVS17;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankLedgerTests
{
    [TestClass]
    public class LedgerManagerTests
    {
        [TestMethod]
        public void CreateValidUser()
        {
            string usernameToTest = "hatim";
            string passwordToTest = "mypassword";

            LedgerManager ledger = new LedgerManager();
            var newUser = ledger.CreateNewUser(usernameToTest, passwordToTest);

            Assert.IsNotNull(newUser);
            Assert.AreEqual(Authentication.HashPassword(passwordToTest), newUser.Password);
            Assert.AreEqual(usernameToTest.ToUpper(), newUser.Username);

            var loggedinUser = Authentication.Login(usernameToTest, passwordToTest);
            Assert.IsNotNull(loggedinUser);
            Assert.AreEqual(Authentication.HashPassword(passwordToTest), loggedinUser.Password);
            Assert.AreEqual(usernameToTest.ToUpper(), loggedinUser.Username);
        }

        [TestMethod]
        public void CreateInvalidUser()
        {
            string usernameToTest = "hatim2";
            string passwordToTest = "mypassword";

            LedgerManager ledger = new LedgerManager();
            var newUser = ledger.CreateNewUser(usernameToTest, passwordToTest);
            Assert.IsNotNull(newUser);

            var anotherUser = ledger.CreateNewUser(usernameToTest, passwordToTest);
            Assert.IsNull(anotherUser);
        }
    }
}