using Newtonsoft.Json;

namespace Box.V2.Converter
{
    public class BoxFileVersionUnderRetentionJsonConverter : BoxJsonConverter
    {
        public override T Parse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content, new BoxFileVersionUnderRetentionItemConverter());
        }
    }
}
