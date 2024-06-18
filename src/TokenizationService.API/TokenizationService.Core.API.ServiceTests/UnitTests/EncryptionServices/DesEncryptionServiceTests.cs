using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizationService.Core.API.Services.EncryptionServices;

namespace TokenizationService.Core.API.Tests.UnitTests.EncryptionServices
{
    public class DesEncryptionServiceTests
    {
        private const string KEY = "YouWillNotMakeThisPut";
        private const string SALT = "SaltCity";
        private const string CLEAR = "NowYouSeeMe";
        private const string ENCRYPTED = "T2euMnI5XHZKycWVMzVfBA==";

        [Test]
        public void BasicEncryption()
        {
            var service = new DesEncryptionService();

            var encryption = service.EncryptString(CLEAR, KEY, SALT);

            Assert.That(encryption, Is.Not.Null);
        }

        [Test]
        public void BasicDecryption()
        {
            var service = new DesEncryptionService();

            var clear = service.DecryptString(ENCRYPTED, KEY, SALT);

            Assert.That(clear, Is.Not.Null);
            Assert.That(clear, Is.EqualTo(CLEAR));
        }
    }
}
