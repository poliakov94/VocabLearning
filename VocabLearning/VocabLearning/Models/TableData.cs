using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabLearning.Models
{
	public abstract class TableData
	{
		public string Id { get; set; }

		public DateTimeOffset? UpdatedAt { get; set; }

		public DateTimeOffset? CreatedAt { get; set; }

		public string Version { get; set; }
	}
}
