using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabLearning.Models
{
	class StudentExercise : TableData
	{
		[JsonIgnore]
		public Student Student { get; set; }
		public string Student_Id { get; set; }

		[JsonIgnore]
		public Exercise Exercise { get; set; }
		public string Exercise_Id { get; set; }
	}
}
