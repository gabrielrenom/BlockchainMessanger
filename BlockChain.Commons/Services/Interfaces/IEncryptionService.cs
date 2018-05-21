using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Commons.Services.Interfaces
{
    public interface IEncryptionService
    {
        Task<byte[]> SHAHash<T, D>(IList<T> elements, T separation) where D : HashAlgorithm, new();
        Task<string> PlainBlockTextForSignature<T>(IList<T> elements, T separation);        
        Task<X509Certificate2> GetCertificateFromStore(string storename, System.Security.Cryptography.X509Certificates.StoreLocation location);
        Task<string> EncryptWithX509Certificate2(X509Certificate2 cert, string plaintext);
        Task<string> DecryptWithX509Certificate2(X509Certificate2 cert, string encryptedtext);
        // Task<X509Certificate2> GenerateSelfSignedCertificate(string subjectName);

    }
}
