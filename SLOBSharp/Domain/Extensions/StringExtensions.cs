using Newtonsoft.Json;

namespace SLOBSharp.Domain.Extensions
{
    internal static class StringExtensions
    {
        public static T JsonToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
