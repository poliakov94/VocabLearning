using NUnit.Framework;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabLearning.Models;
using VocabLearning.Services.Mocks;
using VocabLearning.Tests.Mocks;
using VocabLearning.ViewModels;

namespace VocabLearning.Tests
{
	class StudentViewModelTest
	{
		public StudentViewModelTest()
		{
			MockForms.Init();
		}

		#region StudentAssignmentsPage

		[Test]
		public async Task AssignmentsNotNullAfterAssignmentsPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			await _azureService.SyncOfflineCacheAsync();
			var userTable = await _azureService.GetTableAsync<User>();
			_azureService.User = (await userTable.Where(u => u.LastName == "A")).FirstOrDefault();

			var app = new App(_azureService);
			var viewModel = new StudentAssignmentsPageViewModel(null);
			viewModel.OnNavigatingTo(null);

			Assert.NotNull(viewModel.Assignments);
		}
		#endregion

		#region StudentProgressPage

		#endregion

		#region StudentLearningPage
		[Test]
		public async Task AssignmentNotNullAfterLearningPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			await _azureService.SyncOfflineCacheAsync();

			var app = new App(_azureService);
			var viewModel = new StudentLearningPageViewModel(null);

			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			assignment.Exercises = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(e => e.Assignment_Id == assignment.Id));

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatedTo(navigationParams);

			Assert.NotNull(viewModel.Assignment);
		}
		#endregion

		#region AssignDefinitionPage
		[Test]
		public async Task AssignmentNotNullAfterAssignDefinitionPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			await _azureService.SyncOfflineCacheAsync();

			var app = new App(_azureService);
			var viewModel = new AssignDefinitionPageViewModel(null, _dialogService);

			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			assignment.Exercises = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(e => e.Assignment_Id == assignment.Id));

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatedTo(navigationParams);

			Assert.NotNull(viewModel.Assignment);
		}

		[Test]
		public async Task DefinitionsNotNullAfterAssignDefinitionPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			await _azureService.SyncOfflineCacheAsync();

			var app = new App(_azureService);
			var viewModel = new AssignDefinitionPageViewModel(null, _dialogService);

			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			assignment.Exercises = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(e => e.Assignment_Id == assignment.Id));

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatedTo(navigationParams);

			Assert.NotNull(viewModel.Definitions);
		}

		[Test]
		public async Task ExerciceCheckCurrentChangedInAssignDefinitionPageTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			await _azureService.SyncOfflineCacheAsync();

			var app = new App(_azureService);
			var viewModel = new AssignDefinitionPageViewModel(null, _dialogService);

			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			assignment.Exercises = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(e => e.Assignment_Id == assignment.Id));

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatedTo(navigationParams);

			var exerciseBefore = viewModel.CurrentExercise;
			viewModel.SelectedDefinition = viewModel.CurrentExercise.Definition;

			viewModel.CheckCommand.Execute();

			Assert.IsTrue(viewModel.CurrentExercise != exerciseBefore);
		}
		#endregion

		#region FinishedExercisesPage

		#endregion
	}
}
