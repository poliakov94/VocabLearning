namespace VocabLearning.Models
{
	public class Student
    {
		public int? ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public StudentGroup StudentGroup { get; set; }
	}
}
