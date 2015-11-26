using System;
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
            var input = "GuillermoNelly";
            var encrypted = Encryption.Encrypt(input);
            var decrypted = Encryption.Decrypt(encrypted);

            Assert.AreEqual(input, decrypted);
        }

        [TestMethod]
        public void PasswordHash()
        {
            var input = "NellyValentina";
            var hashed = Utilities.Security.PasswordHash.CreateHash(input);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CheckPassword()
        {
            var hashed = "1000:wFOcsBVOOPOhvxRKxuiqLTX/IKavknv9:PZzvDCbavU/RbY0V26BeJ94dsADjdwon";
            var passwordValidated = Utilities.Security.PasswordHash.ValidatePassword("NellyValentina", hashed);
            Assert.AreEqual(true, passwordValidated);
        }
    }
}
