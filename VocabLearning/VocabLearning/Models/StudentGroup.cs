using System.Collections.Generic;

namespace VocabLearning.Models
{
	class StudentGroup
    {
		public int ID { get; set; }
		public int TeacherID { get; set; }
		public List<Student> Students { get; set; }
		public List<Exercise> Exercises { get; set; }
	}
}
