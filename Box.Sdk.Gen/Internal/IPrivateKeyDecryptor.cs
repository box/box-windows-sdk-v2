using System.Security.Cryptography;

namespace Box.Sdk.Gen
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
