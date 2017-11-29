using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
	public partial class TeacherAssignmentsPage : ContentPage
	{
		public TeacherAssignmentsPageViewModel ViewModel { get { return BindingContext as TeacherAssignmentsPageViewModel; } }
		public TeacherAssignmentsPage()
		{
			InitializeComponent();
		}
	}
}
