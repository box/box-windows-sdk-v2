using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Asymmetric;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Operators;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Box.Sdk.Gen
{
    internal class DefaultPrivateKeyDecryptor : IPrivateKeyDecryptor
    {

        internal DefaultPrivateKeyDecryptor()
        {
        }

        /// <summary>
        /// Decrypts private key using a passphrase.
        /// </summary>
        public RSA DecryptPrivateKey(string encryptedPrivateKey, string passphrase)
        {
            using (var keyReader = new StringReader(encryptedPrivateKey))
            {
                var reader = new OpenSslPemReader(keyReader);
                var privateCrtKeyParams = reader.ReadObject();

                if (privateCrtKeyParams == null)
                {
                    throw new ArgumentException("Invalid private JWT key!");
                }

                if (privateCrtKeyParams is Pkcs8EncryptedPrivateKeyInfo)
                {
                    var pkcs8 = (Pkcs8EncryptedPrivateKeyInfo)privateCrtKeyParams;
                    PrivateKeyInfo privateKeyInfo = pkcs8.DecryptPrivateKeyInfo(
                new PkixPbeDecryptorProviderBuilder().Build(passphrase.ToCharArray()));
                    var bcKey = AsymmetricKeyFactory.CreatePrivateKey(privateKeyInfo.GetEncoded());

                    if (bcKey is AsymmetricRsaPrivateKey)
                    {
                        var bcRsaKey = (AsymmetricRsaPrivateKey)bcKey;
                        var rsaParams = ToRSAParameters(bcRsaKey);

                        var rsa = RSA.Create();
                        rsa.ImportParameters(rsaParams);

                        return rsa;
                    }
                }

                throw new ArgumentException("Provided JWT Key format is not supported");
            }
        }

        private static RSAParameters ToRSAParameters(AsymmetricRsaPrivateKey privateKey)
        {
            RSAParameters rp = new RSAParameters();
            rp.Modulus = privateKey.Modulus.ToByteArrayUnsigned();
            rp.Exponent = privateKey.PublicExponent.ToByteArrayUnsigned();
            rp.P = privateKey.P.ToByteArrayUnsigned();
            rp.Q = privateKey.Q.ToByteArrayUnsigned();
            rp.D = ConvertRSAParametersField(privateKey.PrivateExponent, rp.Modulus.Length);
            rp.DP = ConvertRSAParametersField(privateKey.DP, rp.P.Length);
            rp.DQ = ConvertRSAParametersField(privateKey.DQ, rp.Q.Length);
            rp.InverseQ = ConvertRSAParametersField(privateKey.QInv, rp.Q.Length);
            return rp;
        }

        private static byte[] ConvertRSAParametersField(Org.BouncyCastle.Math.BigInteger n, int size)
        {
            byte[] bs = n.ToByteArrayUnsigned();
            if (bs.Length == size)
            {
                return bs;
            }

            if (bs.Length > size)
            {
                throw new ArgumentException("Specified size too small", "size");
            }

            byte[] padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
            return padded;
        }
    }
}
