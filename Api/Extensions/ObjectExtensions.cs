using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Api.Extensions;

public static class ObjectExtensions
{
    public static string ToJson(this object value)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter> { new StringEnumConverter() }
        };

        return JsonConvert.SerializeObject(value, settings);
    }
}
