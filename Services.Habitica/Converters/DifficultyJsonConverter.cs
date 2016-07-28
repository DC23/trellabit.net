using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Trellabit.Services.Habitica.Converters
{
    public class DifficultyJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Difficulty);
        }

        internal static readonly Dictionary<Difficulty, string> DifficultyValues = new Dictionary<Difficulty, string>
        {
            { Difficulty.Trivial, "0.1" },
            { Difficulty.Easy, "1.0" },
            { Difficulty.Medium, "1.5" },
            { Difficulty.Hard, "2.0" },
        };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.Value.ToString();
            return DifficultyValues.FirstOrDefault(x => x.Value == value).Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(DifficultyValues[(Difficulty)value]);
        }
    }
}
