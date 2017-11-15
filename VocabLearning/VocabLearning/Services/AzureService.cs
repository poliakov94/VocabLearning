﻿using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabLearning.Models;
using VocabLearning.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace VocabLearning.Services
{
	public class AzureService : IAzureService
	{
		IMobileServiceSyncTable<Assignment> _AssignmentTable;
		IMobileServiceSyncTable<Exercise> _ExerciseTable;
		IMobileServiceSyncTable<Student> _StudentTable;
		IMobileServiceSyncTable<StudentGroup> _StudentGroupTable;

		public MobileServiceClient _MobileService { get; set; }
		public AzureService()
		{
			_MobileService = new MobileServiceClient("https://vocablearning.azurewebsites.net");
		}

		static readonly AzureService instance = new AzureService();
		public static AzureService Instance
		{
			get
			{
				return instance;
			}
		}

		public async Task Init()
		{
			if (LocalDBExists)
				return;

			var store = new MobileServiceSQLiteStore("syncstorea.db");

			store.DefineTable<Assignment>();
			store.DefineTable<Exercise>();
			store.DefineTable<Student>();
			store.DefineTable<StudentGroup>();

			try
			{
				await _MobileService.SyncContext.InitializeAsync(store);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"Failed to initialize sync context: {0}", ex.Message);
			}

			_AssignmentTable = _MobileService.GetSyncTable<Assignment>();
			_ExerciseTable = _MobileService.GetSyncTable<Exercise>();
			_StudentTable = _MobileService.GetSyncTable<Student>();
			_StudentGroupTable = _MobileService.GetSyncTable<StudentGroup>();
		}

		public bool LocalDBExists => _MobileService.SyncContext.IsInitialized;

		bool _isSeeded;
		public bool IsSeeded => _isSeeded;

		public async Task SeedLocalDataAsync()
		{
			if (_isSeeded)
				return;

			await SynchronizeAssignmentsAsync();
			await SynchronizeExercisesAsync();
			await SynchronizeGroupsAsync();
			await SynchronizeStudentsAsync();

			_isSeeded = true;
		}
		public async Task SynchronizeAssignmentsAsync()
		{
			if (!LocalDBExists)
				await Init();

			await _AssignmentTable.PullAsync("syncAssignments", _AssignmentTable.CreateQuery());
		}
		public async Task SaveAssignmentAsync(Assignment item)
		{
			if (item.Id == null)
				await _AssignmentTable.InsertAsync(item);
			else
				await _AssignmentTable.UpdateAsync(item);
		}

		public async Task DeleteAssignmentAsync(Assignment item)
		{
			await _AssignmentTable.DeleteAsync(item);
		}

		public async Task<IEnumerable<Assignment>> GetAssignmentAsync(string id)
		{
			return await _AssignmentTable.Where(a => a.Id == id).ToEnumerableAsync();
		}

		public async Task<IEnumerable<Assignment>> GetAssignmentsAsync(string groupId)
		{
			return await _AssignmentTable.Where(a => a.StudentGroup.Id == groupId).ToEnumerableAsync();
		}

		public async Task SynchronizeStudentsAsync()
		{
			if (!LocalDBExists)
				await Init();

			await _StudentTable.PullAsync("syncStudents", _StudentTable.CreateQuery());
		}

		public async Task SynchronizeGroupsAsync()
		{
			if (!LocalDBExists)
				await Init();

			await _StudentGroupTable.PullAsync("syncGroups", _StudentGroupTable.CreateQuery());
		}

		public async Task SynchronizeExercisesAsync()
		{
			if (!LocalDBExists)
				await Init();

			await _ExerciseTable.PullAsync("syncExercises", _ExerciseTable.CreateQuery());
		}

		public async Task SaveStudentAsync(Student item)
		{
			if (item.Id == null)
				await _StudentTable.InsertAsync(item);
			else
				await _StudentTable.UpdateAsync(item);
		}

		public async Task SaveGroupAsync(StudentGroup item)
		{
			try
			{
				if (item.Id == null)
					await _StudentGroupTable.InsertAsync(item);
				else
					await _StudentGroupTable.UpdateAsync(item);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}			
		}

		public async Task DeleteGroupAsync(StudentGroup item)
		{
			await _StudentGroupTable.DeleteAsync(item);
		}

		public async Task SaveExerciseAsync(Exercise item)
		{
			if (item.Id == null)
				await _ExerciseTable.InsertAsync(item);
			else
				await _ExerciseTable.UpdateAsync(item);
		}

		public async Task DeleteExerciseAsync(Exercise item)
		{
			await _ExerciseTable.DeleteAsync(item);
		}

		public async Task<IEnumerable<Student>> GetStudentAsync(string id)
		{
			return await _StudentTable.Where(a => a.Id == id).ToEnumerableAsync();
		}

		public async Task<IEnumerable<Student>> GetStudentsAsync(string teacherId)
		{
			return await _StudentTable.Where(a => a.StudentGroup.Teacher.Id == teacherId).ToEnumerableAsync();
		}

		public async Task<StudentGroup> GetGroupAsync(string id)
		{
			return (await _StudentGroupTable.Where(a => a.Id == id).ToEnumerableAsync()).FirstOrDefault();
		}

		public async Task<IEnumerable<StudentGroup>> GetGroupsAsync(string teacherId)
		{
			try
			{
				if (string.IsNullOrEmpty(teacherId))
					return await _StudentGroupTable.ToEnumerableAsync();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
			return await _StudentGroupTable.Where(a => a.Teacher.Id == teacherId).ToEnumerableAsync();
		}

		public async Task<IEnumerable<Exercise>> GetExerciseAsync(string id)
		{
			return await _ExerciseTable.Where(a => a.Id == id).ToEnumerableAsync();
		}

		public async Task<IEnumerable<Exercise>> GetExercisesAsync(string assignmentId)
		{
			return await _ExerciseTable.Where(a => a.Assignment.Id == assignmentId).ToEnumerableAsync();
		}		
	}
}
