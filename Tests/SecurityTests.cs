using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements.Tests
{
    [TestClass]
    public class SecurityTests
    {
        [TestMethod]
        public void TestEncryption()
        {
            //var input = "guillermo.pincay@negentek.com";
            var input = "123";
            var encrypted = Encryption.Encrypt(input);
            var decrypted = Encryption.Decrypt(encrypted);

            Assert.AreEqual(input, decrypted);
        }

        [TestMethod]
        public void PasswordHash()
        {
            var input = "123456";
            var hashed = Utilities.Security.PasswordHash.CreateHash(input);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CheckPassword()
        {
            //var hashed = "10:Czvc+Hq0TUs=:0NfCOg1TVjs=";
            var hashed = "10:wEU7KeSiacY=:jpZQf1Vqvys=";
            var passwordValidated = Utilities.Security.PasswordHash.ValidatePassword("123456", hashed);
            Assert.AreEqual(true, passwordValidated);
        }
    }
}
