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
    // TODO: incomplete
    public class Party
    {
        //[JsonProperty("order")]
        //public PartyOrder Order { get; set; }

        //[JsonProperty("orderAscending")]
        //public OrderAscending OrderAscending { get; set; }

        [JsonProperty("quest")]
        public Quest Quest { get; set; }
    }
}
