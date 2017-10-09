using BankLedgerVS17;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankLedgerTests
{
    [TestClass]
    public class AuthenticationTests
    {
        [TestMethod]
        public void PasswordHashTest()
        {
            string passwordToTest = "!My_Password*123$";

            var hashedPassword = Authentication.HashPassword(passwordToTest);

            Assert.AreNotEqual(passwordToTest, hashedPassword);
        }

        [TestMethod]
        public void LoginTest()
        {
            string usernameToTest = "username";
            string passwordToTest = "!My_Password*123$";

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
    }
}