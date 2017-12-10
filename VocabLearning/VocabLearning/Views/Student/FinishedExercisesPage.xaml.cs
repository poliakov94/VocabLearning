using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views.Student
{
	public partial class FinishedExercisesPage : ContentPage
	{
		public FinishedExercisesPageViewModel ViewModel { get { return BindingContext as FinishedExercisesPageViewModel; } }
		public FinishedExercisesPage()
		{
			InitializeComponent();
		}
	}
}
