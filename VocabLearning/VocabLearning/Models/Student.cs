using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace VocabLearning.Models
{
	public class Student : TableData
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string AzureId { get; set; }

		[JsonIgnore]
		public StudentGroup StudentGroup { get; set; }		
	}
}
