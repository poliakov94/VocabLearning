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
		public int LanguageID { get; set; }
		public string ImageURI { get; set; }

		public string AssignmentID { get; set; }
		[JsonIgnore]
		public Assignment Assignment { get; set; }
	}
}
