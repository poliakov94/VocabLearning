using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabLearning.Models
{
	public class StudentExercise : TableData
	{
		[JsonIgnore]
		public User Student { get; set; }
		public string Student_Id { get; set; }

		[JsonIgnore]
		public Exercise Exercise { get; set; }
		public string Exercise_Id { get; set; }

		[JsonIgnore]
		public Type Type { get; set; }
		public string Type_Id { get; set; }

		public bool Passed { get; set; }		
		public int Attempt { get; set; }
	}

	public enum Type
	{
		AssignDefinition,
		CompletePhrase,
		TranslateWord
	}
}
