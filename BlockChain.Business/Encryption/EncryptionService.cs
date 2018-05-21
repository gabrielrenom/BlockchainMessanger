using BlockChain.Commons.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace BlockChain.Business.Encryption
{
    public class EncryptionService : IEncryptionService
    {                

        public async Task<X509Certificate2> GetCertificateFromStore(string storename, System.Security.Cryptography.X509Certificates.StoreLocation location)
        {
            //to access to store we need to specify store name and location    
            X509Store x509Store = new X509Store(storename, location);

            //obtain read only access to get cert    
            x509Store.Open(OpenFlags.ReadOnly);

            return x509Store.Certificates[0];
        }

        public async Task<string> EncryptWithX509Certificate2(X509Certificate2 cert, string plaintext)
        {
            using (RSA rsa = cert.GetRSAPrivateKey())
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
                byte[] encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.Pkcs1);
                string encryptedText = Encoding.UTF8.GetString(encryptedBytes);
                return encryptedText;
            }            
        }
      

        public async Task<string> DecryptWithX509Certificate2(X509Certificate2 cert, string encryptedtext)
        {
            using (RSA rsa = cert.GetRSAPrivateKey())
            {
                byte[] encryptedBytes = Encoding.UTF8.GetBytes(encryptedtext);
                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
                string decryptedText = decryptedBytes.ToString();
                return decryptedText;
            }
        
        }

        public async Task<string> PlainBlockTextForSignature<T>(IList<T> elements, T separation)
        {
       
            string result = string.Empty;

            for (int i = 0; i < elements.Count; i++)
            {
                result += i == elements.Count - 1 ? (string)(object)elements[i]  : (string)(object)elements[i] + (string)(object)separation;
            }

            return result;
        }

        /// <summary>
        /// It can use any type of Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="D">SHA256Managed,SHA1Managed etc</typeparam>
        /// <param name="elements"></param>
        /// <param name="separation"></param>
        /// <returns></returns>
        public async Task<byte[]> SHAHash<T,D>(IList<T> elements, T separation)  where D: HashAlgorithm, new()
        {    

            D managedHash = new D();            
            
            string result = string.Empty;

            for (int i = 0; i < elements.Count; i++)
            {
                result += i == elements.Count - 1 ? (string)(object)elements[i]  : (string)(object)elements[i] + (string)(object)separation;
            }

            return managedHash.ComputeHash(Encoding.Unicode.GetBytes(result));
        }       
    }
}
