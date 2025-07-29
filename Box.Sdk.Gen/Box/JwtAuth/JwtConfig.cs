using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class JwtConfig {
        /// <summary>
        /// App client ID
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// App client secret
        /// </summary>
        public string ClientSecret { get; }

        /// <summary>
        /// Public key ID
        /// </summary>
        public string JwtKeyId { get; }

        /// <summary>
        /// Private key
        /// </summary>
        public string PrivateKey { get; }

        /// <summary>
        /// Passphrase
        /// </summary>
        public string PrivateKeyPassphrase { get; }

        /// <summary>
        /// Enterprise ID
        /// </summary>
        public string? EnterpriseId { get; init; }

        /// <summary>
        /// User ID
        /// </summary>
        public string? UserId { get; init; }

        internal JwtAlgorithm? Algorithm { get; init; } = JwtAlgorithm.Rs256;

        public ITokenStorage TokenStorage { get; }

        public IPrivateKeyDecryptor PrivateKeyDecryptor { get; }

        public JwtConfig(string clientId, string clientSecret, string jwtKeyId, string privateKey, string privateKeyPassphrase, ITokenStorage? tokenStorage = default, IPrivateKeyDecryptor? privateKeyDecryptor = default) {
            ClientId = clientId;
            ClientSecret = clientSecret;
            JwtKeyId = jwtKeyId;
            PrivateKey = privateKey;
            PrivateKeyPassphrase = privateKeyPassphrase;
            TokenStorage = tokenStorage ?? new InMemoryTokenStorage();
            PrivateKeyDecryptor = privateKeyDecryptor ?? new DefaultPrivateKeyDecryptor();
        }
        /// <summary>
        /// Create an auth instance as defined by a string content of JSON file downloaded from the Box Developer Console.
        /// See https://developer.box.com/en/guides/authentication/jwt/ for more information.
        /// </summary>
        /// <param name="configJsonString">
        /// String content of JSON file containing the configuration.
        /// </param>
        /// <param name="tokenStorage">
        /// Object responsible for storing token. If no custom implementation provided, the token will be stored in memory
        /// </param>
        /// <param name="privateKeyDecryptor">
        /// Object responsible for decrypting private key for jwt auth. If no custom implementation provided, the DefaultPrivateKeyDecryptor will be used.
        /// </param>
        public static JwtConfig FromConfigJsonString(string configJsonString, ITokenStorage? tokenStorage = null, IPrivateKeyDecryptor? privateKeyDecryptor = null) {
            JwtConfigFile configJson = SimpleJsonSerializer.Deserialize<JwtConfigFile>(JsonUtils.JsonToSerializedData(text: configJsonString));
            ITokenStorage? tokenStorageToUse = tokenStorage == null ? new InMemoryTokenStorage() : tokenStorage;
            IPrivateKeyDecryptor? privateKeyDecryptorToUse = privateKeyDecryptor == null ? new DefaultPrivateKeyDecryptor() : privateKeyDecryptor;
            JwtConfig newConfig = new JwtConfig(clientId: configJson.BoxAppSettings.ClientId, clientSecret: configJson.BoxAppSettings.ClientSecret, jwtKeyId: configJson.BoxAppSettings.AppAuth.PublicKeyId, privateKey: configJson.BoxAppSettings.AppAuth.PrivateKey, privateKeyPassphrase: configJson.BoxAppSettings.AppAuth.Passphrase, tokenStorage: tokenStorageToUse, privateKeyDecryptor: privateKeyDecryptorToUse) { EnterpriseId = configJson.EnterpriseId, UserId = configJson.UserId };
            return newConfig;
        }

        /// <summary>
        /// Create an auth instance as defined by a JSON file downloaded from the Box Developer Console.
        /// See https://developer.box.com/en/guides/authentication/jwt/ for more information.
        /// </summary>
        /// <param name="configFilePath">
        /// Path to the JSON file containing the configuration.
        /// </param>
        /// <param name="tokenStorage">
        /// Object responsible for storing token. If no custom implementation provided, the token will be stored in memory.
        /// </param>
        /// <param name="privateKeyDecryptor">
        /// Object responsible for decrypting private key for jwt auth. If no custom implementation provided, the DefaultPrivateKeyDecryptor will be used.
        /// </param>
        public static JwtConfig FromConfigFile(string configFilePath, ITokenStorage? tokenStorage = null, IPrivateKeyDecryptor? privateKeyDecryptor = null) {
            string configJsonString = Utils.ReadTextFromFile(filepath: configFilePath);
            return JwtConfig.FromConfigJsonString(configJsonString: configJsonString, tokenStorage: tokenStorage, privateKeyDecryptor: privateKeyDecryptor);
        }

    }
}