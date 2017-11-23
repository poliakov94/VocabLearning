using Prism.Unity;
using VocabLearning.Views;
using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }
		public App() : base(null) { }

		protected override void OnInitialized()
		{
			InitializeComponent();

			NavigationService.NavigateAsync("NavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<MainPage>();
			Container.RegisterTypeForNavigation<SpeakPage>();

			Container.RegisterTypeForNavigation<TeacherMasterDetailPage, TeacherMasterDetailPageViewModel>();
			Container.RegisterTypeForNavigation<TeacherOverviewPage, TeacherOverviewPageViewModel>();
			Container.RegisterTypeForNavigation<TeacherProgressPage, TeacherProgressPageViewModel>();
			Container.RegisterTypeForNavigation<TeacherStudentsPage, TeacherStudentsPageViewModel>();
			Container.RegisterTypeForNavigation<TeacherAssignmentsPage, TeacherAssignmentsPageViewModel>();

			Container.RegisterTypeForNavigation<StudentMasterDetailPage, StudentMasterDetailPageViewModel>();
			Container.RegisterTypeForNavigation<StudentOverviewPage, StudentOverviewPageViewModel>();
			Container.RegisterTypeForNavigation<StudentProgressPage, StudentProgressPageViewModel>();
			Container.RegisterTypeForNavigation<StudentExercisesPage, StudentExercisesPageViewModel>();
			Container.RegisterTypeForNavigation<StudentTestPage, StudentTestPageViewModel>();

			Container.RegisterTypeForNavigation<AssignmentManagingPage>();
			Container.RegisterTypeForNavigation<TaskCreationPage>();
			Container.RegisterTypeForNavigation<GroupManagingPage>();
			Container.RegisterTypeForNavigation<StudentsSearchPage>();
			Container.RegisterTypeForNavigation<StudentCreationPage>();
			Container.RegisterTypeForNavigation<LoginPage>();
			Container.RegisterTypeForNavigation<RegisterPage>();
			Container.RegisterTypeForNavigation<StudentsManagingPage>();
			Container.RegisterTypeForNavigation<GroupExercisesPage>();
			Container.RegisterTypeForNavigation<ExerciseCreationPage>();
			Container.RegisterTypeForNavigation<AssignmentCreationPage>();
			Container.RegisterTypeForNavigation<AssignmentExercisesPage>();
		}
	}
}
