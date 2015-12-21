using System;
using Newtonsoft.Json;

namespace DropboxRestAPI.Utils
{
    public class UnixEpochDateTimeConverter : JsonConverter
    {
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long t = 0;
            if (reader.TokenType == JsonToken.String)
                t = long.Parse((string)reader.Value);
            else if(reader.TokenType == JsonToken.Integer)
                t = (long)reader.Value;

            return Epoch.AddMilliseconds(t);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}