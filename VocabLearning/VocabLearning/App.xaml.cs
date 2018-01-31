//#define TEST

using Prism.Unity;
using VocabLearning.Views;
using VocabLearning.ViewModels;
using Xamarin.Forms;
using VocabLearning.Services;
using VocabLearning.Helpers;
using Microsoft.Identity.Client;
using VocabLearning.Views.Student;
using VocabLearning.Views.Teacher;
using VocabLearning.Tests.Mocks;

namespace VocabLearning
{
	public partial class App : PrismApplication
	{
		public static ILoginProvider LoginProvider { get; private set; }

		public static UIParent UiParent = null;

		public static IAzureService _azureService;

		public App(IPlatformInitializer initializer = null) : base(initializer)
		{
#if (TEST)
			_azureService = new MockAzureService();
#else
			_azureService = AzureService.DefaultService;
#endif
		}
		public App(IAzureService azureService) : base(null)
		{
			_azureService = azureService;
		}

		protected override void OnInitialized()
		{
			InitializeComponent();
#if (TEST)
			return;
#else
			LoginProvider = new LoginProvider();
			NavigationService.NavigateAsync("NavigationPage/MainPage");			
#endif
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<MainPage>();

			Container.RegisterTypeForNavigation<TeacherMasterDetailPage, TeacherMasterDetailPageViewModel>();
			Container.RegisterTypeForNavigation<TeacherOverviewPage, TeacherOverviewPageViewModel>();
			Container.RegisterTypeForNavigation<TeacherProgressPage, TeacherProgressPageViewModel>();
			Container.RegisterTypeForNavigation<TeacherStudentsPage, TeacherStudentsPageViewModel>();
			Container.RegisterTypeForNavigation<TeacherAssignmentsPage, TeacherAssignmentsPageViewModel>();

			Container.RegisterTypeForNavigation<StudentMasterDetailPage, StudentMasterDetailPageViewModel>();
			Container.RegisterTypeForNavigation<StudentOverviewPage, StudentOverviewPageViewModel>();
			Container.RegisterTypeForNavigation<StudentProgressPage, StudentProgressPageViewModel>();
			Container.RegisterTypeForNavigation<StudentAssignmentsPage, StudentAssignmentsPageViewModel>();
			Container.RegisterTypeForNavigation<StudentTestPage, StudentTestPageViewModel>();

			Container.RegisterTypeForNavigation<AssignmentManagingPage>();
			Container.RegisterTypeForNavigation<GroupManagingPage>();
			Container.RegisterTypeForNavigation<StudentsSearchPage>();
			Container.RegisterTypeForNavigation<StudentCreationPage>();
			Container.RegisterTypeForNavigation<StudentsManagingPage>();
			Container.RegisterTypeForNavigation<GroupExercisesPage>();
			Container.RegisterTypeForNavigation<ExerciseCreationPage>();
			Container.RegisterTypeForNavigation<AssignmentCreationPage>();
			Container.RegisterTypeForNavigation<AssignmentExercisesPage>();
			Container.RegisterTypeForNavigation<StudentLearningPage, StudentLearningPageViewModel>();
			Container.RegisterTypeForNavigation<AssignDefinitionPage, AssignDefinitionPageViewModel>();
			Container.RegisterTypeForNavigation<CompletePhrasePage, CompletePhrasePageViewModel>();
			Container.RegisterTypeForNavigation<TranslateWordPage, TranslateWordPageViewModel>();
			Container.RegisterTypeForNavigation<FinishedExercisesPage, FinishedExercisesPageViewModel>();
			Container.RegisterTypeForNavigation<EditGroupPage>();
		}
	}
}
