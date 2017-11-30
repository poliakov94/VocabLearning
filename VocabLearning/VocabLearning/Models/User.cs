using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabLearning.Models
{
	public class User : TableData
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string AzureId { get; set; }
		public bool IsTeacher { get; set; }

		[JsonIgnore]
		public StudentGroup StudentGroup { get; set; }
		public string StudentGroup_Id { get; set; }

		public ICollection<StudentGroup> StudentGroups { get; set; }
	}
}
