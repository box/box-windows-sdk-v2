using System;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using System.Runtime.InteropServices;

namespace Box.V2.JWTAuth
{
    public static class DotNetUtilities
    {
        public static RSA ToRSA(RsaPrivateCrtKeyParameters privKey)
        {
            return CreateRSAProvider(ToRSAParameters(privKey));
        }

        private static RSA CreateRSAProvider(RSAParameters rp)
        {
            // -------------------------------------------------------------------------------------
            // When running JWT in an Azure WebJob or Function, you will get an error: 
            //    "System.Security.Cryptography.CryptographicException: The system cannot find the file specified."
            //
            // The error message is misleading and you can see more info here:    
            //     https://blogs.msdn.microsoft.com/winsdk/2009/11/16/opps-system-security-cryptography-cryptographicexception-the-system-cannot-find-the-file-specified/
            //
            // I took the guts of the sealed class DotNetUtilities and overrode it here
            // -------------------------------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                CspParameters csp = new CspParameters()
                {
                    KeyContainerName = string.Format("BouncyCastle-{0}", Guid.NewGuid()),
                    Flags = CspProviderFlags.UseMachineKeyStore // This needs to be here to run as WebJob
                };

                RSACryptoServiceProvider rsaCsp = new RSACryptoServiceProvider(csp);
                rsaCsp.ImportParameters(rp);

                return rsaCsp;
            }
            else
            {
                // Other than windows
                var rsaCsp = RSA.Create();
                rsaCsp.ImportParameters(rp);

                return rsaCsp;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------
        // Note: http://stackoverflow.com/questions/28370414/import-rsa-key-from-bouncycastle-sometimes-throws-bad-data
        // ------------------------------------------------------------------------------------------------------------------
        private static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privKey)
        {
            RSAParameters rp = new RSAParameters();
            rp.Modulus = privKey.Modulus.ToByteArrayUnsigned();
            rp.Exponent = privKey.PublicExponent.ToByteArrayUnsigned();
            rp.P = privKey.P.ToByteArrayUnsigned();
            rp.Q = privKey.Q.ToByteArrayUnsigned();
            rp.D = ConvertRSAParametersField(privKey.Exponent, rp.Modulus.Length);
            rp.DP = ConvertRSAParametersField(privKey.DP, rp.P.Length);
            rp.DQ = ConvertRSAParametersField(privKey.DQ, rp.Q.Length);
            rp.InverseQ = ConvertRSAParametersField(privKey.QInv, rp.Q.Length);
            return rp;
        }

        private static byte[] ConvertRSAParametersField(BigInteger n, int size)
        {
            byte[] bs = n.ToByteArrayUnsigned();
            if (bs.Length == size)
                return bs;
            if (bs.Length > size)
                throw new ArgumentException("Specified size too small", "size");
            byte[] padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
            return padded;
        }
    }
}