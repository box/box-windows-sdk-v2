using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Internal {
    public class DataSanitizer {
        internal Dictionary<string, string> KeysToSanitize { get; }

        public DataSanitizer() {
            KeysToSanitize = new Dictionary<string, string>() { { "authorization", "" }, { "access_token", "" }, { "refresh_token", "" }, { "subject_token", "" }, { "token", "" }, { "client_id", "" }, { "client_secret", "" }, { "shared_link", "" }, { "download_url", "" }, { "jwt_private_key", "" }, { "jwt_private_key_passphrase", "" }, { "password", "" } };
        }
        public Dictionary<string, string> SanitizeHeaders(Dictionary<string, string> headers) {
            return Utils.SanitizeMap(mapToSanitize: headers, keysToSanitize: this.KeysToSanitize);
        }

        public SerializedData SanitizeBody(SerializedData body) {
            return JsonUtils.SanitizeSerializedData(sd: body, keysToSanitize: this.KeysToSanitize);
        }

    }
}