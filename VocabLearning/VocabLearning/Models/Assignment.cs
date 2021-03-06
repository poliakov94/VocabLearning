﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VocabLearning.Models
{
	public class Assignment : TableData
	{	
		public string Name { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime ValidUntil { get; set; }
		[JsonIgnore]
		public virtual ICollection<Exercise> Exercises { get; set; }

		[JsonIgnore]
		public StudentGroup StudentGroup { get; set; }
		public string StudentGroup_Id { get; set; }
	}
}
