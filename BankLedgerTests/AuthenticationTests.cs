using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankLedgerVS17;

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

            User user = new User();

            Assert.AreNotSame(Authentication.Login(usernameToTest, passwordToTest), user);

            Assert.AreEqual(Authentication.Login(usernameToTest, passwordToTest), null);
        }

        [TestMethod]
        public void GenerateValidToken()
        {


        }

        [TestMethod]
        public void LogoutTest()
        {

        }
    }
}