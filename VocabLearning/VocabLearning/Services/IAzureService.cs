using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabLearning.Models;

namespace VocabLearning.Services
{
	public interface IAzureService
	{
		Task InitializeAsync();
		Task SyncOfflineCacheAsync();
		Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData;

		bool LocalDBExists { get; }
		User GetUser();
	}
}
