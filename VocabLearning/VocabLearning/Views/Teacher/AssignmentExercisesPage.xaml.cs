using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
	public partial class AssignmentExercisesPage : ContentPage
	{
		public AssignmentExercisesPageViewModel ViewModel { get { return BindingContext as AssignmentExercisesPageViewModel; } }
		public AssignmentExercisesPage()
		{
			InitializeComponent();
		}
	}
}
