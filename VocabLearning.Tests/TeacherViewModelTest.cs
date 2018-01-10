using NUnit.Framework;
using Prism.Services;
using VocabLearning.Services.Mocks;
using VocabLearning.ViewModels;
using VocabLearning.Tests.Mocks;
using VocabLearning.Services;
using VocabLearning.Models;
using System.Threading.Tasks;
using System.Linq;

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

		[Test]
		public void StudentGroupsNotNullAfterViewModelNavigatedToTest()
		{
			var _azureService = new MockAzureService();
			var app = new App(_azureService);
			var studentsViewModel = new TeacherStudentsPageViewModel(null, null);
			studentsViewModel.OnNavigatingTo(null);

			Assert.NotNull(studentsViewModel.Groups);
		}

		[Test]
		public async Task StudentGroupCreatedTest()
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
		public async Task StudentGroupDeletedTest()
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
	}
}
