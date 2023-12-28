using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// Custom branding applied to notifications and signature requests.
    /// </summary>
    public class BoxSignTemplateCustomBranding
    {
        public const string FieldBrandingColor = "branding_color";
        public const string FieldCompanyName = "company_name";
        public const string FieldEmailFooterText = "email_footer_text";
        public const string FieldLogoUri = "logo_uri";

        /// <summary>
        /// Custom branding color in hex.
        /// </summary>
        [JsonProperty(PropertyName = FieldBrandingColor)]
        public virtual string BrandingColor { get; private set; }

        /// <summary>
        /// Name of the company.
        /// </summary>
        [JsonProperty(PropertyName = FieldCompanyName)]
        public virtual string CompanyName { get; private set; }

        /// <summary>
        /// Custom email footer text.
        /// </summary>
        [JsonProperty(PropertyName = FieldEmailFooterText)]
        public virtual string EmailFooterText { get; private set; }

        /// <summary>
        /// Custom branding logo URI in the form of a base64 image.
        /// </summary>
        [JsonProperty(PropertyName = FieldLogoUri)]
        public virtual string LogoUri { get; private set; }
    }
}
