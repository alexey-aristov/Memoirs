using System;
using Newtonsoft.Json;

namespace Memoirs.Android.Common.Login
{
    public class ExpiresInSerializer:JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = Convert.ToDouble(reader.Value);
            return TimeSpan.FromMinutes(t);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}