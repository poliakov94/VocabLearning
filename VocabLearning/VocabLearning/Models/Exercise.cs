using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace VocabLearning.Models
{
	public class Exercise : TableData
    {
		public string Word { get; set; }
		public string Definition { get; set; }
		public string Phrase { get; set; }
		public string TranslatedWord { get; set; }
		public string TranslatedPhrase { get; set; }		
		public string ImageURI { get; set; }
		
		[JsonIgnore]
		public Language Language { get; set; }
		public string Language_Id { get; set; }

		[JsonIgnore]
		public Assignment Assignment { get; set; }
		public string Assignment_Id { get; set; }
	}

	public class Language : TableData
	{
		public string Name { get; set; }
	}
}
