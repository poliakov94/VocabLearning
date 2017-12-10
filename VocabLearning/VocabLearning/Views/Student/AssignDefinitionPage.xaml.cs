using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views.Student
{
	public partial class AssignDefinitionPage : ContentPage
	{
		public AssignDefinitionPageViewModel ViewModel { get { return BindingContext as AssignDefinitionPageViewModel; } }
		public AssignDefinitionPage()
		{
			InitializeComponent();
		}
	}
}
