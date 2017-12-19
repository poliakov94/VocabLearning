using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabLearning.Models;

namespace VocabLearning.Services
{
	public interface ICloudTable<T> where T : TableData
	{
		Task<T> CreateItemAsync(T item);
		Task<T> ReadItemAsync(string id);
		Task<T> UpdateItemAsync(T item);
		Task<T> UpsertItemAsync(T item);
		Task DeleteItemAsync(T item);
		Task<ICollection<T>> ReadAllItemsAsync();
		Task<ICollection<T>> ReadItemsAsync(int start, int count);
		Task PullAsync();
		IMobileServiceSyncTable<T> ReturnTable();
	}
}
