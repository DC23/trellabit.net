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
using System.Collections.Generic;
using Newtonsoft.Json;
using Trellabit.Services.Habitica.Converters;

namespace Trellabit.Services.Habitica.Model
{
	public class Achievements
	{
		[JsonProperty("challenges")]
		public List<string> Challenges { get; set; }

		[JsonProperty("perfect")]
		public int Perfect { get; set; }

		[JsonProperty("streak")]
		public int Streak { get; set; }

		[JsonProperty("quests")]
		public Dictionary<string, int> Quests { get; set; }

		[JsonProperty("habitBirthdays")]
		public int HabitBirthdays { get; set; }

		[JsonProperty("habitBirthday")]
		public bool HabitBirthday { get; set; }
	}
}