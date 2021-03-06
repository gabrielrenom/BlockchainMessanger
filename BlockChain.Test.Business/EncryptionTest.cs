﻿using BlockChain.Commons.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Test.Business
{
    [TestClass]
    public class EncryptionTest
    {
        private IEncryptionService _encryptionService;

        [TestInitialize]
        public void Setup()
        {
            _encryptionService = Substitute.For<IEncryptionService>();
        }


        [TestMethod]
        public async Task TestSHA256()
        {
            //Arrange
            var hash = new byte[] { 23, 3, 4, 5, 6, 3, 2, 2, 1 };
            _encryptionService.SHAHash<object, SHA256Managed>(new List<object> { "Alice", "RE: Correct report", "Bob" }, ",").Returns(hash);

            //Act            
            var result = await _encryptionService.SHAHash<object, SHA256Managed>(new List<object> { "Alice", "RE: Correct report", "Bob" }, ",");

            //Assert
            Assert.AreEqual(result, hash);
        }

        [TestMethod]
        public async Task TestPlainTextBlock()
        {
            //Arrange
            var returntext = "1,2323,£,£,gagaga,dsd";
            List<string> list = new List<string> { "1", "2323", "£", "£", "gagaga", "dsd" };
            _encryptionService.PlainBlockTextForSignature<string>(list, "<bcfield>").Returns(returntext);

            //Act
            var result = await _encryptionService.PlainBlockTextForSignature<string>(list, "<bcfield>");

            //Assert
            Assert.AreEqual(result, returntext);

        }



    }
}
