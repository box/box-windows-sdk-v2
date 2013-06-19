using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Box.V2.Converter
{
    public class BoxJsonConverter : IBoxConverter
    {
        JsonSerializerSettings _settings;

        public BoxJsonConverter()
        {
            _settings = new JsonSerializerSettings();
            _settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public T Parse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public string Serialize<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity, _settings);
        }
    }
}
