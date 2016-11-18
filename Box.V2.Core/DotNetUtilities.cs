using System;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Box.V2.JWTAuth
{
    public static class DotNetUtilities
    {
        public static RSA ToRSA(RsaPrivateCrtKeyParameters rsaKey)
        {
            var parameters = new RSAParameters
                             {
                                 Modulus = rsaKey.Modulus.ToByteArrayUnsigned(),
                                 Exponent = rsaKey.PublicExponent.ToByteArrayUnsigned(),
                                 P = rsaKey.P.ToByteArrayUnsigned(),
                                 Q = rsaKey.Q.ToByteArrayUnsigned()
                             };
            parameters.D = ConvertRsaParametersField(rsaKey.Exponent, parameters.Modulus.Length);
            parameters.DP = ConvertRsaParametersField(rsaKey.DP, parameters.P.Length);
            parameters.DQ = ConvertRsaParametersField(rsaKey.DQ, parameters.Q.Length);
            parameters.InverseQ = ConvertRsaParametersField(rsaKey.QInv, parameters.Q.Length);

            var cryptoServiceProvider = RSA.Create();
            cryptoServiceProvider.ImportParameters(parameters);
            return cryptoServiceProvider;
        }

        private static Byte[] ConvertRsaParametersField(BigInteger n, Int32 size)
        {
            var byteArrayUnsigned = n.ToByteArrayUnsigned();
            if (byteArrayUnsigned.Length == size)
                return byteArrayUnsigned;
            if (byteArrayUnsigned.Length > size)
                throw new ArgumentException("Specified size too small", nameof(size));
            var numArray = new Byte[size];
            Array.Copy(byteArrayUnsigned, 0, numArray, size - byteArrayUnsigned.Length, byteArrayUnsigned.Length);
            return numArray;
        }
    }
}