using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VocabLearning.Models;
using VocabLearning.Services;
using VocabLearning.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace VocabLearning.Services
{
	public class AzureService : IAzureService
	{
		static AzureService defaultInstance = new AzureService();
		private MobileServiceClient _MobileService { get; set; }
		public User User { get; set; }
		
		public AzureService()
		{
			_MobileService = new MobileServiceClient("https://vocablearning.azurewebsites.net");			
		}

		public static AzureService DefaultService
		{
			get
			{
				return defaultInstance;
			}
			private set
			{
				defaultInstance = value;
			}
		}

		public MobileServiceClient CurrentClient
		{
			get { return _MobileService; }
		}

		public async Task InitializeAsync()
		{
			if (LocalDBExists)
				return;

			var store = new MobileServiceSQLiteStore("syncstorenew.db");

			store.DefineTable<Assignment>();
			store.DefineTable<Exercise>();
			store.DefineTable<StudentGroup>();
			store.DefineTable<StudentExercise>();
			store.DefineTable<User>();

			try
			{
				await _MobileService.SyncContext.InitializeAsync(store);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"Failed to initialize sync context: {0}", ex.Message);
			}
			
			await SyncOfflineCacheAsync();
		}

		public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
		{
			await InitializeAsync();
			return new CloudTable<T>(_MobileService);
		}

		public bool LocalDBExists => _MobileService.SyncContext.IsInitialized;

		public async Task SyncOfflineCacheAsync()
		{
			if (!LocalDBExists)
				await InitializeAsync();

			try
			{
				await _MobileService.SyncContext.PushAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"Failed to push async: {0}", ex.ToString());
			}

			var assignmentTable = await GetTableAsync<Assignment>(); await assignmentTable.PullAsync();
			var studentGroupTable = await GetTableAsync<StudentGroup>(); await studentGroupTable.PullAsync();
			var exerciseTable = await GetTableAsync<Exercise>(); await exerciseTable.PullAsync();
			var studentExerciseTable = await GetTableAsync<StudentExercise>(); await studentExerciseTable.PullAsync();
			var userTable = await GetTableAsync<User>(); await userTable.PullAsync();
		}

		public User GetUser()
		{
			return User;
		}
	}
}
