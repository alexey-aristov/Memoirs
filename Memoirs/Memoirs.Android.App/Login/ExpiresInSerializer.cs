using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Memoirs.Android.App.Login
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