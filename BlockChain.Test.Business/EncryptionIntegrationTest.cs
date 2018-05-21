using BlockChain.Business.Encryption;
using BlockChain.Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlockChain.Test.Business
{
    [TestClass]
    public class EncryptionIntegrationTest
    {
        private Commons.Services.Interfaces.IEncryptionService _encryptionService;

        [TestInitialize]
        public void Setup()
        {
            
            var query = Assembly.GetAssembly(typeof(Class1))
                    .GetTypes()
                    .Where(t => t.IsEnum);

            foreach (Type t in query)
            {
                var value = (int)Enum.Parse(t, "elmundo");
                //var names = t.GetEnumNames();

            }
            _encryptionService = new EncryptionService();
        }

        [TestMethod]
        public async Task GenerateSHA256Hash()
        {
            //Arrange
            string subject = "First Central";

            //Act
            //60030<bcfield>1000<bcfield>1001<bcfield>Hey<bcfield>Hey Alice! How are you?<bcfield>2018-03-26 13:24
            //0x0ED29AF1064D08559B5EA9913B96485618B583A391D1514CFB57530D0808F554
            
            var result = await _encryptionService.SHAHash<object, SHA256Managed>(new List<object> { "60028", "1000", "1001", "Hey", "Hey Alice! How are you?", "2018-03-26 13:14:08.95 +01:00",1 }, "<bcfield>");
            var hexresult = BitConverter.ToString(result);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GenerateSHA1Hash()
        {
            //Arrange
            string subject = "First Central";

            //Act
            var result = await _encryptionService.SHAHash<object, SHA1Managed>(new List<object> { "A", "B", "34", "1" }, "<bcfield>");

            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task GeneratePlainForCertificate()
        {
            //Arrange

            //Act
            var result = await _encryptionService.PlainBlockTextForSignature<string>(new List<string> { "A", "B", "34", "1" }, "<bcfield>");

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task EncryptWithCertificate()
        {
            //Arrange
            string storename = "sampleCertStore";
            System.Security.Cryptography.X509Certificates.StoreLocation location = System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser;
            var certificate = await _encryptionService.GetCertificateFromStore(storename, location);

            //Act
            var result = await _encryptionService.EncryptWithX509Certificate2(certificate, "hello,how are you");

            //Assert

        }
    }
}
