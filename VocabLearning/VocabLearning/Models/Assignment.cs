﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VocabLearning.Models
{
	public class Assignment : TableData
    {
		public string StudentGroupID { get; set; }
		[JsonIgnore]
		public StudentGroup StudentGroup { get; set; }
		public string TypeID { get; set; }
		public string Name { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime ValidUntil { get; set; }

		public virtual ICollection<Exercise> Exercises { get; set; }
	}
}
