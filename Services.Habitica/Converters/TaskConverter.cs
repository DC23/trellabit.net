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

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Trellabit.Data.Habitica.Converters;
using Trellabit.Services.Habitica.Model;

namespace Trellabit.Services.Habitica.Converters
{
    /// <summary>
    /// Task factory/creator.
    /// </summary>
    /// <seealso cref="Trellabit.Data.Habitica.Converters.JsonCreationConverter{Trellabit.Services.Habitica.Model.Task}" />
    public class TaskConverter : JsonCreationConverter<Task>
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">contents of JSON object that will be deserialized</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        protected override Task Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("type");

            switch (type)
            {
                case "daily":
                    return new Daily();

                case "habit":
                    return new Habit();

                case "todo":
                    return new Todo();

                case "reward":
                    return new Reward();
            }

            throw new Exception(String.Format("Type: {0} not supported", type));
        }
    }
}
