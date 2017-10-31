using System;

namespace VocabLearning.Models
{
	public class Task
    {
		public int ID { get; set; }
		public string Word { get; set; }
		public string Definition { get; set; }
		public string Phrase { get; set; }
		public string TranslatedWord { get; set; }
		public string TranslatedPhrase { get; set; }
		public int LanguageID { get; set; }
		public Uri ImageURI { get; set; }
	}
}
