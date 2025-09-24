using System.Security.Cryptography;

namespace Box.V2.JWTAuth
{
    /// <summary>
    /// Interface used for private key decryption in JWT auth.
    /// </summary>
    public interface IPrivateKeyDecryptor
    {
        /// <summary>
        /// Decrypts private key using a passphrase.
        /// </summary>
        RSA DecryptPrivateKey(string encryptedPrivateKey, string passphrase);
    }
}
