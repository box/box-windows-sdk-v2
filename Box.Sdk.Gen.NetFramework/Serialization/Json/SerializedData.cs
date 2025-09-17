using Box.Sdk.Gen.Internal;
namespace Box.Sdk.Gen
{

    public class SerializedData
    {
        internal object Data { get; }

        //TODO support other data types
        internal bool IsJson { get; }

        internal SerializedData(object data, bool isJson = false)
        {
            Data = data;
            //TODO determine based on content
            IsJson = isJson;
        }

        internal string AsJson()
        {
            if (IsJson)
            {
                var dataAsString = Data as string;
                if (dataAsString == null)
                {
                    throw new BoxSdkException("Json cannot be converted to string");
                }
                return dataAsString;
            }
            else
            {
                return SimpleJsonSerializer.SdToJson(this);
            }
        }
    }
}
