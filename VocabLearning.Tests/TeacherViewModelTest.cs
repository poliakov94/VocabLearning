using NUnit.Framework;
using Prism.Navigation;
using System.Linq;
using System.Threading.Tasks;
using VocabLearning.Models;
using VocabLearning.Services.Mocks;
using VocabLearning.Tests.Mocks;
using VocabLearning.ViewModels;

namespace VocabLearning.Tests
{
	[TestFixture]
	class TeacherViewModelTest
	{		
		public TeacherViewModelTest()
		{
			MockForms.Init();			
		}

		[Test]
		public void ApplicationIsNotNullTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);

			Assert.NotNull(app);
		}

		#region StudentsPage
				
		[Test]
		public void StudentGroupsNotNullAfterStudentsPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var studentsViewModel = new TeacherStudentsPageViewModel(null, null);
			studentsViewModel.OnNavigatingTo(null);

			Assert.NotNull(studentsViewModel.Groups);
		}

		[Test]
		public async Task StudentGroupCreatedInStudentsPageTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			var app = new App(_azureService);
			

			var studentsViewModel = new TeacherStudentsPageViewModel(null, _dialogService);

			studentsViewModel.OnNavigatingTo(null);
			studentsViewModel.ExecuteCreateGroupCommand();

			var studentGroupsTable = await _azureService.GetTableAsync<StudentGroup>();
			var studentGroups = await studentGroupsTable.ReadAllItemsAsync();

			Assert.IsTrue(studentGroups.Count == 3);
		}

		[Test]
		public async Task StudentGroupDeletedInStudentsPageTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			var app = new App(_azureService);


			var studentsViewModel = new TeacherStudentsPageViewModel(null, _dialogService);
			studentsViewModel.OnNavigatingTo(null);

			var studentGroupsTable = await _azureService.GetTableAsync<StudentGroup>();
			var groupToDelete = (await studentGroupsTable.Where(s => s.Name == "Group1")).FirstOrDefault();

			studentsViewModel.ExecuteDeleteGroupCommand(groupToDelete);
			var studentGroups = await studentGroupsTable.ReadAllItemsAsync();

			Assert.IsTrue(studentGroups.Count == 1);
		}

		#endregion

		#region AssignmentsPage
		[Test]
		public void AssignmentsNotNullAfterAssignmentsPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var assignmentsViewModel = new TeacherAssignmentsPageViewModel(null, null);
			assignmentsViewModel.OnNavigatingTo(null);

			Assert.NotNull(assignmentsViewModel.Assignments);
		}
		#endregion

		#region StudentsSearchPage
		[Test]
		public async Task StudentsNotNullAfterStudentsSearchPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var viewModel = new StudentsSearchPageViewModel(null, null);

			await _azureService.SyncOfflineCacheAsync();
			var group = new StudentGroup();
			var navigationParams = new NavigationParameters
				{
					{ "model", group }
				};

			viewModel.OnNavigatingTo(navigationParams);

			Assert.NotNull(viewModel.Students);
		}

		[Test]
		public async Task StudentAddedInStudentsSearchPageTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();

			var app = new App(_azureService);
			var viewModel = new StudentsSearchPageViewModel(null, _dialogService);

			await _azureService.SyncOfflineCacheAsync();
			var group = (await (await _azureService.GetTableAsync<StudentGroup>())
				.Where(s => s.Name == "Group2"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", group }
				};
			viewModel.OnNavigatingTo(navigationParams);

			var groupBefore = (await (await _azureService.GetTableAsync<User>())
				.Where(s => s.StudentGroup_Id == group.Id))
				.Count;

			var student = (await (await _azureService.GetTableAsync<User>())
				.Where(s => s.FirstName == "C" && s.LastName == "C"))
				.FirstOrDefault();

			viewModel.StudentSelected = student;
			viewModel.AddStudent.Execute();

			var groupAfter = (await (await _azureService.GetTableAsync<User>())
				.Where(s => s.StudentGroup_Id == group.Id))
				.Count;

			

			Assert.IsTrue(groupAfter == groupBefore + 1);
		}
		#endregion

		#region StudentsManagingPage
		[Test]
		public async Task StudentsNotNullAfterStudentsManagingPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var viewModel = new StudentsSearchPageViewModel(null, null);

			await _azureService.SyncOfflineCacheAsync();
			var group = (await (await _azureService.GetTableAsync<StudentGroup>())
				.Where(s => s.Name == "Group1"))
				.FirstOrDefault();
			var navigationParams = new NavigationParameters
				{
					{ "model", group }
				};

			viewModel.OnNavigatingTo(navigationParams);

			Assert.NotNull(viewModel.Students);
		}
		#endregion

		#region GroupExercisesPage
		[Test]
		public async Task AssignmentsNotNullAfterGroupExercisesPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			var app = new App(_azureService);

			var viewModel = new GroupExercisesPageViewModel(null, _dialogService);

			await _azureService.SyncOfflineCacheAsync();
			var group = (await (await _azureService.GetTableAsync<StudentGroup>())
				.Where(s => s.Name == "Group1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", group }
				};

			viewModel.OnNavigatingTo(navigationParams);

			Assert.NotNull(viewModel.Assignments);
		}

		[Test]
		public async Task AssignmentsIDNotNullAfterGroupExercisesPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			var app = new App(_azureService);

			var viewModel = new GroupExercisesPageViewModel(null, _dialogService);

			await _azureService.SyncOfflineCacheAsync();
			var group = (await (await _azureService.GetTableAsync<StudentGroup>())
				.Where(s => s.Name == "Group1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "groupId", group.Id }
				};

			viewModel.OnNavigatingTo(navigationParams);

			Assert.NotNull(viewModel.Assignments);
		}

		[Test]
		public async Task AssignmentCreatedInGroupExercisesPageTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			var app = new App(_azureService);

			var viewModel = new GroupExercisesPageViewModel(null, _dialogService);

			await _azureService.SyncOfflineCacheAsync();
			var group = (await (await _azureService.GetTableAsync<StudentGroup>())
				.Where(s => s.Name == "Group1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "groupId", group.Id }
				};

			viewModel.OnNavigatingTo(navigationParams);
			viewModel.CreateAssignmentCommand.Execute();

			var assignmentsTable = await _azureService.GetTableAsync<Assignment>();
			var assignments = await assignmentsTable.ReadAllItemsAsync();

			Assert.IsTrue(assignments.Count == 2);
		}

		#endregion

		#region ExerciseCreationPage
		[Test]
		public async Task ExerciseNotNullAfterExerciseCreationPageNavigatedToEditTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			var app = new App(_azureService);

			var viewModel = new ExerciseCreationPageViewModel(null, _dialogService);

			await _azureService.SyncOfflineCacheAsync();
			var exercise = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(s => s.Word == "WordOne"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", exercise }
				};

			viewModel.OnNavigatingTo(navigationParams);

			Assert.NotNull(viewModel.Exercise);
		}

		[Test]
		public async Task ExerciseNotNullAfterExerciseCreationPageNavigatedToNewTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			var app = new App(_azureService);

			var viewModel = new ExerciseCreationPageViewModel(null, _dialogService);

			await _azureService.SyncOfflineCacheAsync();
			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatingTo(navigationParams);

			Assert.NotNull(viewModel.Exercise);
		}

		[Test]
		public async Task NewExerciseSavedInExerciseCreationPageTest()
		{
			var _azureService = new MockAzureService();
			var _dialogService = new MockPageDialogService();
			var _navigationService = new MockNavigationService();
			var app = new App(_azureService);

			var viewModel = new ExerciseCreationPageViewModel(_navigationService, _dialogService);

			await _azureService.SyncOfflineCacheAsync();
			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatingTo(navigationParams);

			viewModel.Exercise.Word = "New";
			viewModel.Exercise.Definition = "New";
			viewModel.Exercise.Phrase = "New";
			viewModel.Exercise.TranslatedPhrase = "New";
			viewModel.Exercise.TranslatedWord = "New";

			viewModel.SaveExerciseCommand.Execute();

			var exercise = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(s => s.Word == "New"))
				.FirstOrDefault();

			Assert.NotNull(exercise);
		}
		#endregion

		#region AssignmentManagingPage
		[Test]
		public async Task ExercisesNotNullAfterAssignmentManagingPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var viewModel = new AssignmentManagingPageViewModel(null, null);
			
			await _azureService.SyncOfflineCacheAsync();
			var assignment = (await(await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatingTo(navigationParams);

			Assert.NotNull(viewModel.Exercises);
		}

		[Test]
		public async Task ExerciseDeletedInAssignmentManagingPageTest()
		{
			var _azureService = new MockAzureService();
			var _pageDialogService = new MockPageDialogService();
			var app = new App(_azureService);
			var viewModel = new AssignmentManagingPageViewModel(null, _pageDialogService);

			await _azureService.SyncOfflineCacheAsync();
			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatingTo(navigationParams);

			var exercisesBefore = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(s => s.Assignment_Id == assignment.Id))
				.Count;

			var exercise = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(s => s.Word == "WordOne"))
				.FirstOrDefault();

			viewModel.DeleteExerciseCommand.Execute(exercise);

			var exercisesAfter = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(s => s.Assignment_Id == assignment.Id))
				.Count;

			Assert.IsTrue(exercisesAfter == exercisesBefore - 1 && viewModel.Exercises.Count == exercisesAfter);
		}

		[Test]
		public async Task AssignmentSavedInAssignmentManagingPageTest()
		{
			var _azureService = new MockAzureService();
			var _navigationService = new MockNavigationService();
			var app = new App(_azureService);
			var viewModel = new AssignmentManagingPageViewModel(_navigationService, null);

			await _azureService.SyncOfflineCacheAsync();
			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatingTo(navigationParams);

			viewModel.Assignment.Name = "AssignmentChanged";
			
			viewModel.SaveAssignmentCommand.Execute();

			var assignmentAfter = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "AssignmentChanged"))
				.FirstOrDefault();

			Assert.IsNotNull(assignmentAfter);
		}
		#endregion

		#region AssignmentExercisesPage
		[Test]
		public async Task ExercisesNotNullAfterAssignmentExercisesPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var viewModel = new AssignmentExercisesPageViewModel(null, null);

			await _azureService.SyncOfflineCacheAsync();
			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatingTo(navigationParams);

			Assert.NotNull(viewModel.Exercises);
		}

		[Test]
		public async Task ExerciseDeletedInAssignmentExercisesPageTest()
		{
			var _azureService = new MockAzureService();
			var _pageDialogService = new MockPageDialogService();
			var app = new App(_azureService);
			var viewModel = new AssignmentManagingPageViewModel(null, _pageDialogService);

			await _azureService.SyncOfflineCacheAsync();
			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "Assignment1"))
				.FirstOrDefault();

			var navigationParams = new NavigationParameters
				{
					{ "model", assignment }
				};

			viewModel.OnNavigatingTo(navigationParams);

			var exercisesBefore = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(s => s.Assignment_Id == assignment.Id))
				.Count;

			var exercise = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(s => s.Word == "WordOne"))
				.FirstOrDefault();

			viewModel.DeleteExerciseCommand.Execute(exercise);

			var exercisesAfter = (await (await _azureService.GetTableAsync<Exercise>())
				.Where(s => s.Assignment_Id == assignment.Id))
				.Count;

			Assert.IsTrue(exercisesAfter == exercisesBefore - 1 && viewModel.Exercises.Count == exercisesAfter);
		}
		#endregion

		#region AssignmentCreationPage
		[Test]
		public async Task GroupsNotNullAfterAssignmentCreationPageNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var viewModel = new AssignmentCreationPageViewModel(null);

			await _azureService.SyncOfflineCacheAsync();

			viewModel.OnNavigatingTo(null);

			Assert.NotNull(viewModel.Groups);
		}

		[Test]
		public async Task AssignmentCreatedInAssignmentCreationPageTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var viewModel = new AssignmentCreationPageViewModel(null);

			await _azureService.SyncOfflineCacheAsync();

			viewModel.OnNavigatingTo(null);

			viewModel.Assignment.Name = "NewAssignment";
			viewModel.SelectedGroup = viewModel.Groups.Where(s => s.Name == "Group1").FirstOrDefault();

			viewModel.CreateAssignmentCommand.Execute();

			var assignment = (await (await _azureService.GetTableAsync<Assignment>())
				.Where(s => s.Name == "NewAssignment"))
				.FirstOrDefault();

			Assert.NotNull(assignment);
		}
		#endregion
	}
}
