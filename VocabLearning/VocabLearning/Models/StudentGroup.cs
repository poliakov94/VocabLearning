using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace VocabLearning.Models
{
	public class StudentGroup : TableData
    {
		public string Name { get; set; }
		public int GroupSize { get; set; }
		public ICollection<Student> Students { get; set; }
		public ICollection<Assignment> Assignments { get; set; }
		public int TeacherID { get; set; }

		[JsonIgnore]
		public Teacher Teacher { get; set; }
	}
}
