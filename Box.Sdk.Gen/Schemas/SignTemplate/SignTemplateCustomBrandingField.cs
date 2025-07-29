using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignTemplateCustomBrandingField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscompany_nameSet")]
        protected bool _isCompanyNameSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_islogo_uriSet")]
        protected bool _isLogoUriSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isbranding_colorSet")]
        protected bool _isBrandingColorSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isemail_footer_textSet")]
        protected bool _isEmailFooterTextSet { get; set; }

        protected string? _companyName { get; set; }

        protected string? _logoUri { get; set; }

        protected string? _brandingColor { get; set; }

        protected string? _emailFooterText { get; set; }

        /// <summary>
        /// Name of the company.
        /// </summary>
        [JsonPropertyName("company_name")]
        public string? CompanyName { get => _companyName; init { _companyName = value; _isCompanyNameSet = true; } }

        /// <summary>
        /// Custom branding logo URI in the form of a base64 image.
        /// </summary>
        [JsonPropertyName("logo_uri")]
        public string? LogoUri { get => _logoUri; init { _logoUri = value; _isLogoUriSet = true; } }

        /// <summary>
        /// Custom branding color in hex.
        /// </summary>
        [JsonPropertyName("branding_color")]
        public string? BrandingColor { get => _brandingColor; init { _brandingColor = value; _isBrandingColorSet = true; } }

        /// <summary>
        /// Content of the email footer.
        /// </summary>
        [JsonPropertyName("email_footer_text")]
        public string? EmailFooterText { get => _emailFooterText; init { _emailFooterText = value; _isEmailFooterTextSet = true; } }

        public SignTemplateCustomBrandingField() {
            
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}