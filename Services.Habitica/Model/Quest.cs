using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trellabit.Services.Habitica.Model
{
    /// <summary>
    /// This is the user.party.quest object. 
    /// May require disambiguation.
    /// </summary>
    // TODO: incomplete
	public class Quest
	{
        // TODO: determine datatype
        [JsonProperty("completed")]
        public object Completed { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("RSVPNeeded")]
		public bool RSVPNeeded { get; set; }

		[JsonProperty("progress")]
		public Progress Progress { get; set; }
	}
}
