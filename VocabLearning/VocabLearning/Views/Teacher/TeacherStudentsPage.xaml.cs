using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
	public partial class TeacherStudentsPage : ContentPage
	{
		public TeacherStudentsPageViewModel ViewModel { get { return BindingContext as TeacherStudentsPageViewModel; } }
		public TeacherStudentsPage()
		{
			InitializeComponent();
		}
	}
}
