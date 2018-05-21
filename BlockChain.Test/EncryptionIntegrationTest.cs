using BlockChain.Business.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Test
{
    [TestClass]
    public class EncryptionIntegrationTest
    {
        private EncryptionService _encryptionService;

        [TestInitialize]
        public void Setup()
        {
            _encryptionService = new EncryptionService();
        }

        [TestMethod]
        public async Task GenerateSHA256Hash()
        {
            //Arrange
            string subject = "First Central";

            //Act
            var result = await _encryptionService.SHAHash<object,SHA256Managed>(new List<object> { "A","B","34","1"}, "<bcfield>");

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


    }
}
