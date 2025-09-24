using Newtonsoft.Json;

namespace Box.V2.Converter
{
    internal class BoxFileVersionsUnderRetentionJsonConverter : BoxJsonConverter
    {
        public override T Parse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content, new BoxFileVersionsUnderRetentionItemConverter());
        }
    }
}
