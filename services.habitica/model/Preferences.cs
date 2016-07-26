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
using System.Collections.Generic;
using System;

namespace HabitRPG.Client.Model
{
	public class Preferences
	{
		[JsonProperty("language")]
		public string Language { get; set; }

		[JsonProperty("shirt")]
		public string Shirt { get; set; }

		[JsonProperty("skin")]
		public string Skin { get; set; }

		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("hair")]
		public Hair Hair { get; set; }

		[JsonProperty("costume")]
		public bool Costume { get; set; }

		[JsonProperty("sleep")]
		public bool Sleep { get; set; }

		[JsonProperty("background")]
		public string Background { get; set; }

		[JsonProperty("dayStart")]
		public int DayStartsAt { get; set; }

		[JsonProperty("webhooks")]
		public Dictionary<Guid, WebHook> Webhooks { get; set; }

		[JsonProperty("toolbarCollapsed")]
		public bool ToolbarCollapsed { get; set; }

		[JsonProperty("advancedCollapsed")]
		public bool AdvancedCollapsed { get; set; }

		[JsonProperty("tagsCollapsed")]
		public bool TagsCollapsed { get; set; }

		[JsonProperty("dailyDueDefaultView")]
		public bool DailyDueDefaultView { get; set; }

		[JsonProperty("newTaskEdit")]
		public bool NewTaskEdit { get; set; }

		[JsonProperty("disableClasses")]
		public bool DisableClasses { get; set; }

		[JsonProperty("stickyHeader")]
		public bool StickyHeader { get; set; }

		[JsonProperty("allocationMode")]
		public string AllocationMode { get; set; }

		[JsonProperty("sound")]
		public string Sound { get; set; }

		[JsonProperty("hideHeader")]
		public bool HideHeader { get; set; }

		[JsonProperty("timezoneOffset")]
		public int TimezoneOffset { get; set; }

		[JsonProperty("automaticAllocation")]
		public bool AutomaticAllocation { get; set; }
	}
}
