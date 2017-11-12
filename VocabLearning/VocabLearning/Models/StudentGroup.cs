using System.Collections.Generic;

namespace VocabLearning.Models
{
	public class StudentGroup
    {
		public int? ID { get; set; }
		public Teacher Teacher { get; set; }
		public string Name { get; set; }
		public int GroupSize { get; set; }
		public ICollection<Student> Students { get; set; }
		public ICollection<Assignment> Assignments { get; set; }
	}
}
