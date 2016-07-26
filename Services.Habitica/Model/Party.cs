using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trellabit.Services.Habitica.Model
{

    /// <summary>
    /// This is the user.party object. I expect a more detailed structure is
    /// returned from other functions in the v3 API. I might need to
    /// disambiguate the classes when I get that far.
    /// </summary>
	public class Party
	{
		/// <summary>
		/// DataType is String because the Tavern has the GroupId: habitrpg
		/// </summary>
        // TODO: Does the tavern still have a special-case ID?
		[JsonProperty("_id")]
		public string Id { get; set; }

        [JsonProperty("quest")]
        public Quest Quest { get; set; }
	}
}
