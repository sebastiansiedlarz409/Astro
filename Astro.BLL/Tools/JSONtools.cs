using Newtonsoft.Json.Linq;
using System;

namespace Astro.BLL.Tools
{
    public class JSONtools
    {
        public T GetValue<T>(JObject jObject, string key)
        {
            JToken token = jObject.SelectToken(key);

            TypeCode type = Type.GetTypeCode(typeof(T));

            dynamic returnValue = type switch
            {
                TypeCode.Int32 => 0,
                TypeCode.Int64 => 0,
                TypeCode.Double => 0,
                TypeCode.String => "none",
                _ => "none"
            };

            return token switch
            {
                { } => token.Value<T>(),
                null => returnValue
            };
        }

        public JObject GetJObject(JObject jObject, string key)
        {
            JToken token = jObject.SelectToken(key);

            return token switch
            {
                { } => token.Value<JObject>(),
                null => new JObject()
            };
        }

        public JArray GetJArray(JObject jObject, string key)
        {
            JToken token = jObject.SelectToken(key);

            return token switch
            {
                { } => token.Value<JArray>(),
                null => new JArray()
            };
        }
    }
}
