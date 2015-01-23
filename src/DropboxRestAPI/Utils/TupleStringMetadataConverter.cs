using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DropboxRestAPI.Models.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DropboxRestAPI.Utils
{
    public class TupleStringMetadataConverter : JsonConverter
    {
        #region Overrides of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<Tuple<string, JRaw>> jlist = ((List<Tuple<string, MetaData>>)value).Select(t => new Tuple<string, JRaw>(t.Item1, new JRaw(t.Item2))).ToList();
            serializer.Serialize(writer, jlist);

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray jarray = JArray.Load(reader);

            var result = new List<Tuple<string, MetaData>>(jarray.Count);
            foreach (JToken j in jarray)
            {
                string item1 = ((JArray)j)[0].ToString();
                var item2 = ((JArray)j)[1].ToObject<MetaData>();
                result.Add(new Tuple<string, MetaData>(item1, item2));
            }
            return result;

        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<Tuple<string, MetaData>>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        #endregion
    }
}