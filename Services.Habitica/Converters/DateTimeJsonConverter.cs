//------------------------------------------------------------------------------
// Original version of this file is from the habitrpg-api-dotnet-client project:
// https://github.com/marska/habitrpg-api-dotnet-client
// The original form of these files can be viewed in GitHub commit:
// https://github.com/DC23/trellabit.net/commit/2f2a304285c347cdb15f1addd70fec7ea3631e57
// All modifications since that commit are my own.
//
// In short, my goals for wrapping the Habitica (HabitRPG) API are quite different.
// At least initially, I am not trying to wrap the complete API, and I am using
// a different approach to mechanics of wrapping the API. Finally I am working
// with Habitica API v3 while habitrpg-api-dotnet-client has been written for
// API v2. Thus I am not motivated to update the entire habitrpg-api-dotnet-client
// code to APIv3. However, the model classes are mostly exactly what I need.
// So rather than write a set of nearly identical JSON.Net annoted serialisation
// classes, I am reusing these here. Both projects are Apache v2 licensed, so
// by distributing this code with the same license, along with this header giving
// acknowledgement of the original source, and the indication of the modifications
// I have made (via GitHub commits) I believe I am in compliance with the terms
// of the Apache V2 license.
//------------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Trellabit.Services.Habitica.Converters
{
    public class DateTimeJsonConverter : JsonConverter
    {
        private readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // TODO: For v3, would it be better to write an ISO8601 string instead of epoch time?
            var dateTime = ((DateTime)value).ToUniversalTime();
            var intMilliseconds = (Int64)((dateTime - _epoch).TotalMilliseconds);
            writer.WriteRawValue(intMilliseconds.ToString());
        }

        // SO MANY different values inside a DateTime Property...
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is DateTime)
                return reader.Value;

            if (reader.Value == null)
                return _epoch;

            long longValue;

            if (long.TryParse(reader.Value.ToString(), out longValue))
                return _epoch.AddMilliseconds(longValue);

            DateTime dateTime;

            if (DateTime.TryParse(reader.Value.ToString(), out dateTime))
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            return _epoch;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}
