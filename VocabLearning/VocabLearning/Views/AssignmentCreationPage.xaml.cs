using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
	public partial class AssignmentCreationPage : ContentPage
	{
		public AssignmentCreationPageViewModel ViewModel { get { return BindingContext as AssignmentCreationPageViewModel; } }
		public AssignmentCreationPage()
		{
			InitializeComponent();
		}
	}
}
