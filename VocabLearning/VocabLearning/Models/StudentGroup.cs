using System.Collections.Generic;

namespace VocabLearning.Models
{
	public class StudentGroup
    {
		public int ID { get; set; }
		public int TeacherID { get; set; }
		public string Name { get; set; }
		public int GroupSize { get; set; }
		public List<Student> Students { get; set; }
		public List<Exercise> Exercises { get; set; }
	}
}
