﻿using Newtonsoft.Json;
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

        /// <summary>
        /// Instantiates a new BoxJsonConverter that converts JSON
        /// </summary>
        public BoxJsonConverter()
        {
            _settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:sszzz"
            };
        }

        /// <summary>
        /// Parses a JSON string into the provided type T
        /// </summary>
        /// <typeparam name="T">The type that the content should be parsed into</typeparam>
        /// <param name="content">The JSON string</param>
        /// <returns>The box representation of the JSON</returns>
        public T Parse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content, new BoxItemConverter());
        }

        /// <summary>
        /// Serializes the Box type into JSON
        /// </summary>
        /// <typeparam name="T">The type of the entity to serialize</typeparam>
        /// <param name="entity">The entity to serialize</param>
        /// <returns>JSON string</returns>
        public string Serialize<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity, _settings);
        }
    }
}
