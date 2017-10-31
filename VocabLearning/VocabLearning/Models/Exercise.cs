﻿using System;
using System.Collections.Generic;

namespace VocabLearning.Models
{
	public class Exercise
    {
		public int ID { get; set; }
		public int TeacherID { get; set; }
		public ExerciseType Type { get; set; }
		public string Name { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime ValidUntil { get; set; }
		public List<Task> Tasks{ get; set; }
	}

	public enum ExerciseType
	{
		AssignDefintion,
		Translate,
		CompletePhrase,
		AssignImage
	}
}
