using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VocabLearning.Models;

namespace VocabLearning.Services
{
	public class CloudTable<T> : ICloudTable<T> where T : TableData
	{
		private IMobileServiceSyncTable<T> table;
		public IMobileServiceSyncTable<T> Table
		{
			get { return table; }
			private set { table = value; }
		}

		public CloudTable(MobileServiceClient client)
		{
			Table = client.GetSyncTable<T>();
		}
		
		public async Task PullAsync()
		{
			string queryName = $"incsync_{typeof(T).Name}";
			await table.PullAsync(queryName, table.CreateQuery());
		}

		public async Task<T> CreateItemAsync(T item)
		{
			await table.InsertAsync(item);
			return item;
		}

		public async Task<T> UpsertItemAsync(T item)
		{
			return (item.Id == null) ?
				await CreateItemAsync(item) :
				await UpdateItemAsync(item);
		}

		public async Task DeleteItemAsync(T item)
			=> await table.DeleteAsync(item);

		public async Task<ICollection<T>> ReadAllItemsAsync()
		{
			List<T> allItems = new List<T>();

			var pageSize = 50;
			var hasMore = true;
			while (hasMore)
			{
				var pageOfItems = await table.Skip(allItems.Count).Take(pageSize).ToListAsync();
				if (pageOfItems.Count > 0)
				{
					allItems.AddRange(pageOfItems);
				}
				else
				{
					hasMore = false;
				}
			}
			return allItems;
		}

		public async Task<T> ReadItemAsync(string id)
			=> await table.LookupAsync(id);

		public async Task<ICollection<T>> ReadItemsAsync(int start, int count)
		{
			return await table.Skip(start).Take(count).ToListAsync();
		}

		public async Task<T> UpdateItemAsync(T item)
		{
			await table.UpdateAsync(item);
			return item;
		}

		public async Task<ICollection<T>> Where(Expression<Func<T, bool>> predicate)
		{
			return await Table.Where(predicate).ToCollectionAsync<T>();
		}
	}
}