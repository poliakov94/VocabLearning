using Prism.Unity;
using VocabLearning.Views;
using VocabLearning.ViewModels;
using Xamarin.Forms;
using VocabLearning.Services;
using VocabLearning.Helpers;
using Microsoft.Identity.Client;
using VocabLearning.Views.Student;
using VocabLearning.Views.Teacher;

namespace VocabLearning
{
	public partial class App : PrismApplication
	{
		public static ILoginProvider LoginProvider { get; private set; }

		public static UIParent UiParent = null;

		public App(IPlatformInitializer initializer = null) : base(initializer) { }
		public App() : base(null)
		{
			ServiceLocator.Instance.Add<IAzureService, AzureService>();
		}

		protected override void OnInitialized()
		{
			InitializeComponent();
			LoginProvider = new LoginProvider();
			NavigationService.NavigateAsync("NavigationPage/MainPage");			
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
