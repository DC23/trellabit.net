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

namespace trellabit.services.habitica.model
{
	public class Item : StatsBase
	{
		[JsonProperty("value")]
		public int Value { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("klass")]
		public string Klass { get; set; }

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("event")]
		public Event Event { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		public override string ToString()
		{
			return string.Format("[Item: Value={0}, Type={1}, Key={2}, Klass={3}, Index={4}, Event={5}, Text={6}, Notes={7}]", Value, Type, Key, Klass, Index, Event, Text, Notes);
		}
	}
}
