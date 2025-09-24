using System;
using System.IO;
using System.Security.Cryptography;
using Box.V2.Exceptions;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;

namespace Box.V2.JWTAuth
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
            var pwf = new PEMPasswordFinder(passphrase);

            object privateKey = null;

            using (var reader = new StringReader(encryptedPrivateKey))
            {
                privateKey = new PemReader(reader, pwf).ReadObject();
            }

            if (privateKey == null)
            {
                throw new BoxCodingException("Invalid private key!");
            }

            if (privateKey is AsymmetricCipherKeyPair)
            {
                var ackp = (AsymmetricCipherKeyPair)privateKey;

                return ToRSA((RsaPrivateCrtKeyParameters)ackp.Private);
            }
            else if (privateKey is RsaPrivateCrtKeyParameters)
            {
                return ToRSA((RsaPrivateCrtKeyParameters)privateKey);
            }

            throw new BoxCodingException("Private key could not be decrypted");
        }

        private static RSA ToRSA(RsaPrivateCrtKeyParameters privateKeyParameters)
        {
            var rsaParameters = ToRSAParameters(privateKeyParameters);
            var rsa = RSA.Create();
            rsa.ImportParameters(rsaParameters);

            return rsa;
        }

        private static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privKey)
        {
            var rp = new RSAParameters
            {
                Modulus = privKey.Modulus.ToByteArrayUnsigned(),
                Exponent = privKey.PublicExponent.ToByteArrayUnsigned(),
                P = privKey.P.ToByteArrayUnsigned(),
                Q = privKey.Q.ToByteArrayUnsigned()
            };
            rp.D = ConvertRSAParametersField(privKey.Exponent, rp.Modulus.Length);
            rp.DP = ConvertRSAParametersField(privKey.DP, rp.P.Length);
            rp.DQ = ConvertRSAParametersField(privKey.DQ, rp.Q.Length);
            rp.InverseQ = ConvertRSAParametersField(privKey.QInv, rp.Q.Length);
            return rp;
        }

        private static byte[] ConvertRSAParametersField(BigInteger n, int size)
        {
            var bs = n.ToByteArrayUnsigned();
            if (bs.Length == size)
            {
                return bs;
            }

            if (bs.Length > size)
            {
                throw new ArgumentException("Specified size too small", "size");
            }

            var padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
            return padded;
        }
    }

    internal class PEMPasswordFinder : IPasswordFinder
    {
        private readonly string _pword;
        public PEMPasswordFinder(string password) { _pword = password; }
        public char[] GetPassword() { return _pword.ToCharArray(); }
    }
}
