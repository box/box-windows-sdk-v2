using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// A representation of a signer on a sign template.
    /// </summary>
    public class BoxSignTemplateSigner
    {
        public const string FieldEmail = "email";
        public const string FieldInputs = "inputs";
        public const string FieldIsInPerson = "is_in_person";
        public const string FieldOrder = "order";
        public const string FieldRole = "role";

        /// <summary>
        /// The email address of the signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldEmail)]
        public virtual string Email { get; private set; }

        /// <summary>
        /// The input fields for the signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldInputs)]
        public virtual List<BoxSignTemplateSignerInput> Inputs { get; private set; }

        /// <summary>
        /// Used in combination with an embed URL for a sender.
        /// After the sender signs, they will be redirected to the next in_person signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsInPerson)]
        public virtual bool IsInPerson { get; private set; }

        /// <summary>
        /// The order of the signer in the list of signers.
        /// </summary>
        [JsonProperty(PropertyName = FieldOrder)]
        public virtual int Order { get; private set; }

        /// <summary>
        /// The role of the signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldRole)]
        public virtual BoxSignTemplateSignerRole Role { get; private set; }
    }

    /// <summary>
    /// The role of the signer.
    /// </summary>
    public enum BoxSignTemplateSignerRole
    {
        signer,
        approver,
        final_copy_reader
    }

    /// <summary>
    /// A representation of an input field for a signer on a sign template.
    /// </summary>
    public class BoxSignTemplateSignerInput
    {
        public const string FieldType = "type";
        public const string FieldCheckboxValue = "checkbox_value";
        public const string FieldContentType = "content_type";
        public const string FieldCoordinates = "coordinates";
        public const string FieldDateValue = "date_value";
        public const string FieldDimensions = "dimensions";
        public const string FieldDocumentId = "document_id";
        public const string FieldDocumentTagId = "document_tag_id";
        public const string FieldDropdownChoices = "dropdown_choices";
        public const string FieldGroupId = "group_id";
        public const string FieldIsRequired = "is_required";
        public const string FieldPageIndex = "page_index";
        public const string FieldTextValue = "text_value";
        public const string FieldLabel = "label";

        /// <summary>
        /// The type of input field.
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public virtual BoxSignTemplateSignerInputType Type { get; private set; }

        /// <summary>
        /// The value of the checkbox.
        /// </summary>
        [JsonProperty(PropertyName = FieldCheckboxValue)]
        public virtual bool? CheckboxValue { get; private set; }

        /// <summary>
        /// The content type of the input field.
        /// </summary>
        [JsonProperty(PropertyName = FieldContentType)]
        public virtual BoxSignTemplateSignerInputContentType ContentType { get; private set; }

        /// <summary>
        /// The coordinates of the input field.
        /// </summary>
        [JsonProperty(PropertyName = FieldCoordinates)]
        public virtual BoxSignTemplateSignerInputCoordinates Coordinates { get; private set; }

        /// <summary>
        /// The value of the date.
        /// </summary>
        [JsonProperty(PropertyName = FieldDateValue)]
        public virtual DateTimeOffset? DateValue { get; private set; }

        /// <summary>
        /// The dimensions of the input field.
        /// </summary>
        [JsonProperty(PropertyName = FieldDimensions)]
        public virtual BoxSignTemplateSignerInputDimensions Dimensions { get; private set; }

        /// <summary>
        /// The ID of the document.
        /// </summary>
        [JsonProperty(PropertyName = FieldDocumentId)]
        public virtual string DocumentId { get; private set; }

        /// <summary>
        /// The ID of the document tag.
        /// </summary>
        [JsonProperty(PropertyName = FieldDocumentTagId)]
        public virtual string DocumentTagId { get; private set; }

        /// <summary>
        /// The choices for the dropdown.
        /// </summary>
        [JsonProperty(PropertyName = FieldDropdownChoices)]
        public virtual List<string> DropdownChoices { get; private set; }

        /// <summary>
        /// The ID of the group.
        /// </summary>
        [JsonProperty(PropertyName = FieldGroupId)]
        public virtual string GroupId { get; private set; }

        /// <summary>
        /// Indicates if the input field is required.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsRequired)]
        public virtual bool IsRequired { get; private set; }

        /// <summary>
        /// The index of the page.
        /// </summary>
        [JsonProperty(PropertyName = FieldPageIndex)]
        public virtual int PageIndex { get; private set; }

        /// <summary>
        /// The value of the text.
        /// </summary>
        [JsonProperty(PropertyName = FieldTextValue)]
        public virtual string TextValue { get; private set; }

        /// <summary>
        /// The label field is used especially for text, attachment, radio, and checkbox type inputs.
        /// </summary>
        [JsonProperty(PropertyName = FieldLabel)]
        public virtual string Label { get; private set; }
    }

    /// <summary>
    /// The type of input field.
    /// </summary>
    public enum BoxSignTemplateSignerInputType
    {
        signature,
        date,
        text,
        checkbox,
        attachment,
        radio,
        dropdown
    }

    /// <summary>
    /// The content type of the input field.
    /// </summary>
    public enum BoxSignTemplateSignerInputContentType
    {
        signature,
        initial,
        stamp,
        date,
        checkbox,
        text,
        full_name,
        first_name,
        last_name,
        company,
        title,
        email,
        attachment,
        radio,
        dropdown
    }

    /// <summary>
    /// The coordinates of the input field.
    /// </summary>
    public class BoxSignTemplateSignerInputCoordinates
    {
        public const string FieldX = "x";
        public const string FieldY = "y";

        /// <summary>
        /// The x coordinate of the input field.
        /// </summary>
        [JsonProperty(PropertyName = FieldX)]
        public virtual double X { get; private set; }

        /// <summary>
        /// The y coordinate of the input field.
        /// </summary>
        [JsonProperty(PropertyName = FieldY)]
        public virtual double Y { get; private set; }
    }

    /// <summary>
    /// The dimensions of the input field.
    /// </summary>
    public class BoxSignTemplateSignerInputDimensions
    {
        public const string FieldHeight = "height";
        public const string FieldWidth = "width";

        /// <summary>
        /// The height of the input field.
        /// </summary>
        [JsonProperty(PropertyName = FieldHeight)]
        public virtual double Height { get; private set; }

        /// <summary>
        /// The width of the input field.
        /// </summary>
        [JsonProperty(PropertyName = FieldWidth)]
        public virtual double Width { get; private set; }
    }
}
