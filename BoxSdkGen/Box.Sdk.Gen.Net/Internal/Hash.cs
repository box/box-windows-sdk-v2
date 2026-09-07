using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Box.Sdk.Gen.Internal
{
    [JsonConverter(typeof(StringEnumConverter<HashName>))]
    internal enum HashName
    {
        [Description("sha1")]
        Sha1,
        [Description("sha512")]
        Sha512
    }

    class Hash
    {
        internal HashName Algorithm { get; }
        private HashAlgorithm _hashAlgorithm;

        internal Hash(HashName algorithm)
        {
            switch (algorithm)
            {
                case HashName.Sha1:
                    Algorithm = algorithm;
                    _hashAlgorithm = SHA1.Create();
                    break;
                case HashName.Sha512:
                    Algorithm = algorithm;
                    _hashAlgorithm = SHA512.Create();
                    break;
                default:
                    throw new ArgumentException($"Provided hash algorithm {algorithm} not supported");
            }
        }

        internal async Task<string> DigestHashAsync(string encoding)
        {
            _hashAlgorithm.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            if (_hashAlgorithm.Hash == null)
            {
                throw new ArgumentException("Hash is empty");
            }
            if (encoding == "hex")
            {
                return await Task.FromResult(BitConverter.ToString(_hashAlgorithm.Hash).Replace("-", "").ToLowerInvariant());
            }
            return await Task.FromResult(Convert.ToBase64String(_hashAlgorithm.Hash));
        }

        internal void UpdateHash(byte[] data)
        {
            _hashAlgorithm.TransformBlock(data, 0, data.Length, null, 0);
        }
    }

}
