using TokenizationService.Core.API.Services.EncryptionServices;

namespace TokenizationService.Core.API.Tests.UnitTests.EncryptionServices
{
    public class AesEncryptionServiceTests
    {
        private const string KEY = "YouWillNotMakeThisPut";
        private const string SALT = "SaltCity";
        private const string CLEAR = "NowYouSeeMe";
        private const string ENCRYPTED = "ux3wI/1Offhmcm93gB0BNQ==p5WQGqGUMSJHR53bPHl8CQ==";

        [Test]
        public void BasicEncryption()
        {
            var service = new AesEncryptionService();

            var encryption = service.EncryptString(CLEAR, KEY, SALT);

            Assert.That(encryption, Is.Not.Null);
        }

        [Test]
        public void BasicDecryption()
        {
            var service = new AesEncryptionService();

            var clear = service.DecryptString(ENCRYPTED, KEY, SALT);

            Assert.That(clear, Is.Not.Null);
            Assert.That(clear, Is.EqualTo(CLEAR));
        }
    }
}
