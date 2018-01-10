using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VocabLearning.Models;
using VocabLearning.Services;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace VocabLearning.Tests.Mocks
{
	public class MockAzureService : IAzureService
	{
		public bool LocalDBExists => throw new NotImplementedException();
		public Dictionary<string, object> tables = new Dictionary<string, object>();
		private User User;

		public bool Initialized { get; set; }

		public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
		{
			var tableName = typeof(T).Name;
			if (!tables.ContainsKey(tableName))
			{
				var table = new MockCloudTable<T>();
				tables[tableName] = table;
			}
			return (ICloudTable<T>)tables[tableName];
		}

		public async Task InitializeAsync()
		{
			if (Initialized)
				return;

			try
			{
				var studentGroups = new MockCloudTable<StudentGroup>();
				tables["StudentGroup"] = studentGroups;

				var assignments = new MockCloudTable<Assignment>();
				tables["Assignment"] = assignments;

				var exercises = new MockCloudTable<Exercise>();
				tables["Exercise"] = exercises;

				var studentExercises = new MockCloudTable<StudentExercise>();
				tables["StudentExercise"] = studentExercises;

				var students = new MockCloudTable<User>();
				tables["User"] = students;

				await studentGroups.CreateItemAsync(new StudentGroup()
				{
					Name = "Group1",
					GroupSize = 2,
					Teacher_Id = "1"
				});
				await studentGroups.CreateItemAsync(new StudentGroup()
				{
					Name = "Group2",
					GroupSize = 1,
					Teacher_Id = "1"
				});
				
				await students.CreateItemAsync(new User()
				{
					Email = "",
					FirstName = "A",
					LastName = "A",
					IsTeacher = false,
					AzureId = "1",
					StudentGroup_Id = (await studentGroups.Where(s => s.Name == "Group1")).FirstOrDefault().Id
				});
				await students.CreateItemAsync(new User()
				{
					Email = "",
					FirstName = "B",
					LastName = "B",
					IsTeacher = false,
					AzureId = "2",
					StudentGroup_Id = (await studentGroups.Where(s => s.Name == "Group2")).FirstOrDefault().Id
				});
				await students.CreateItemAsync(new User()
				{
					Email = "",
					FirstName = "C",
					LastName = "C",
					IsTeacher = false,
					AzureId = "3",
					StudentGroup_Id = null
				});

				await assignments.CreateItemAsync(new Assignment()
				{
					Name = "Assignment1",
					ValidFrom = DateTime.Today,
					ValidUntil = DateTime.Today.AddDays(5),
					StudentGroup_Id = (await studentGroups.Where(s => s.Name == "Group1")).FirstOrDefault().Id
				});
				await assignments.CreateItemAsync(new Assignment()
				{
					Name = "Assignment2",
					ValidFrom = DateTime.Today,
					ValidUntil = DateTime.Today.AddDays(5),
					StudentGroup_Id = (await studentGroups.Where(s => s.Name == "Group2")).FirstOrDefault().Id
				});

				await exercises.CreateItemAsync(new Exercise()
				{
					Assignment_Id = (await assignments.Where(s => s.Name == "Assignment1")).FirstOrDefault().Id,
					Definition = "Definition",
					Phrase = "Phrase",
					Word = "WordOne",
					TranslatedPhrase = "TranslatedPhrase",
					TranslatedWord = "TranslatedWord",
					ImageURI = ""
				});

				await exercises.CreateItemAsync(new Exercise()
				{
					Assignment_Id = (await assignments.Where(s => s.Name == "Assignment1")).FirstOrDefault().Id,
					Definition = "Definition",
					Phrase = "Phrase",
					Word = "WordTwo",
					TranslatedPhrase = "TranslatedPhrase",
					TranslatedWord = "TranslatedWord",
					ImageURI = ""
				});

				await exercises.CreateItemAsync(new Exercise()
				{
					Assignment_Id = (await assignments.Where(s => s.Name == "Assignment2")).FirstOrDefault().Id,
					Definition = "Definition",
					Phrase = "Phrase",
					Word = "Word",
					TranslatedPhrase = "TranslatedPhrase",
					TranslatedWord = "TranslatedWord",
					ImageURI = ""
				});
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
				return;
			}			

			Initialized = true;
			return;
		}

		public async Task SyncOfflineCacheAsync()
		{
			if (!Initialized)
				await InitializeAsync();

			return;
		}

		public User GetUser()
		{
			if (User != null)
				return User;

			User = new User()
			{
				AzureId = "1",
				Id = "1",
				Email = "hello@grr.la",
				FirstName = "Test",
				LastName = "Teacher",
				IsTeacher = true
			};

			return User;
		}
	}
}
