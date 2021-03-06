﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace VocabLearning.Models
{
	public class StudentGroup : TableData
	{
		public string Name { get; set; }
		public int GroupSize { get; set; }
		public ICollection<User> Students { get; set; }
		public ICollection<Assignment> Assignments { get; set; }

		[JsonIgnore]
		public User Teacher { get; set; }
		public string Teacher_Id { get; set; }

		[JsonIgnore]
		public int AssignmentsCount { get; set; }
	}
}
