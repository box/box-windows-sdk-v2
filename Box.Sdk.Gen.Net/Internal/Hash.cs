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
        Sha1
    }

    class Hash
    {
        internal HashName Algorithm { get; }
        private SHA1 _sha1;

        internal Hash(HashName algorithm)
        {
            switch (algorithm)
            {
                case HashName.Sha1:
                    Algorithm = algorithm;
                    _sha1 = SHA1.Create();
                    break;
                default:
                    throw new ArgumentException($"Provided hash algorithm {algorithm} not supported");
            }
        }

        internal async Task<string> DigestHashAsync(string encoding)
        {
            _sha1.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            if (_sha1.Hash == null)
            {
                throw new ArgumentException("Hash is empty");
            }
            return await Task.FromResult(Convert.ToBase64String(_sha1.Hash));
        }

        internal void UpdateHash(byte[] data)
        {
            _sha1.TransformBlock(data, 0, data.Length, null, 0);
        }
    }

}
