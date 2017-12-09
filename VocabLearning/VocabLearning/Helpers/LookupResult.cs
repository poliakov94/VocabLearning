using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VocabLearning.Helpers
{
	public partial class LookupResult
	{
		[JsonProperty("results")]
		public List<Result> Results { get; set; }
	}

	public partial class Result
	{
		[JsonProperty("senses")]
		public List<Sense> Senses { get; set; }
	}

	public partial class Sense
	{
		[JsonProperty("definition")]
		public List<string> Definition { get; set; }

		[JsonProperty("examples")]
		public List<Example> Examples { get; set; }
	}
	public partial class Example
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}

	public partial class LookupResult
	{
		public static LookupResult FromJson(string json) => JsonConvert.DeserializeObject<LookupResult>(json, Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this LookupResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
	}

	public class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
		};
	}
}
