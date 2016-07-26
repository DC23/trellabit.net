using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trellabit.Services.Habitica.Model
{
    /// <summary>
    /// This is the user.party.quest.progress object. 
    /// May require disambiguation.
    /// </summary>
    // TODO: incomplete
    public class Progress
    {
        // TODO: determine datatype
        [JsonProperty("collect")]
        public object Collect { get; set; }

        [JsonProperty("collectedItems")]
        public int CollectedItems { get; set; }

        [JsonProperty("down")]
        public double Down { get; set; }

        [JsonProperty("up")]
        public double Up { get; set; }
    }
}
