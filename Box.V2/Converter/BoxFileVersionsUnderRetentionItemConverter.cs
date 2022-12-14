using System;
using Box.V2.Config;
using Box.V2.Models;
using Newtonsoft.Json.Linq;

namespace Box.V2.Converter
{
    // workaround for https://developer.box.com/reference/get-retention-policy-assignments-id-file-versions-under-retention/
    // which currently returns 'file-version' instead of 'file' in 'type' field
    internal class BoxFileVersionsUnderRetentionItemConverter : BoxItemConverter
    {
        protected override BoxEntity Create(Type objectType, JObject jObject)
        {
            // we need to identify the top level node somehow so we check if 'file version' field is present
            if (FieldExists(ItemType, jObject) && FieldExists("file_version", jObject))
            {
                switch (jObject[ItemType].ToString())
                {
                    // should work even if this bug is fixed 
                    case Constants.TypeFileVersion:
                    case Constants.TypeFile:
                        jObject[ItemType] = Constants.TypeFile;
                        return new BoxFile();
                }
            }

            return base.Create(objectType, jObject);
        }
    }
}
