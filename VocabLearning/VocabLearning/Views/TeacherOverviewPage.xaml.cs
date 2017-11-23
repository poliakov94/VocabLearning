using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
	public partial class TeacherOverviewPage : ContentPage
	{
		public TeacherOverviewPageViewModel ViewModel { get { return BindingContext as TeacherOverviewPageViewModel; } }
		public TeacherOverviewPage()
		{
			InitializeComponent();
		}
	}
}
