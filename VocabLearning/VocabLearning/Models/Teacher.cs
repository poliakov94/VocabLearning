using System.Collections.Generic;

namespace VocabLearning.Models
{
	public class Teacher : TableData
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string AzureId { get; set; }

		public ICollection<StudentGroup> StudentGroups { get; set; }
	}
}
