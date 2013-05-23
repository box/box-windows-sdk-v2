using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Box.V2.Services
{
    public class BoxJsonConverter : IBoxConverter
    {
        public T Parse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public string Serialize<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}
