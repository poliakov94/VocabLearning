using Prism.Unity;
using VocabLearning.Views;
using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

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
			Container.RegisterTypeForNavigation<TeacherTasksPage, TeacherTasksPageViewModel>();

			Container.RegisterTypeForNavigation<StudentMasterDetailPage, StudentMasterDetailPageViewModel>();
			Container.RegisterTypeForNavigation<StudentOverviewPage, StudentOverviewPageViewModel>();
			Container.RegisterTypeForNavigation<StudentProgressPage, StudentProgressPageViewModel>();
			Container.RegisterTypeForNavigation<StudentTasksPage, StudentTasksPageViewModel>();
			Container.RegisterTypeForNavigation<StudentTestPage, StudentTestPageViewModel>();
		}
	}
}
