using System;
using System.Collections.Generic;

namespace VocabLearning.Models
{
	public class Assignment
    {
		public int? ID { get; set; }
		public StudentGroup StudentGroup { get; set; }
		public ExerciseType Type { get; set; }
		public string Name { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime ValidUntil { get; set; }
		public ICollection<Exercise> Exercises{ get; set; }
	}

	public enum ExerciseType
	{
		AssignDefintion,
		Translate,
		CompletePhrase,
		AssignImage
	}
}
