using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
    public partial class AssignmentManagingPage : ContentPage
    {
		public AssignmentManagingPageViewModel ViewModel { get { return BindingContext as AssignmentManagingPageViewModel; } }
		public AssignmentManagingPage()
        {
            InitializeComponent();
        }
    }
}
