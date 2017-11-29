using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
	public partial class StudentCreationPage : ContentPage
	{
		public StudentCreationPageViewModel ViewModel { get { return BindingContext as StudentCreationPageViewModel; } }
		public StudentCreationPage()
		{
			InitializeComponent();
		}
	}
}
