using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
    public partial class GroupManagingPage : TabbedPage
    {
		public GroupManagingPageViewModel ViewModel { get { return BindingContext as GroupManagingPageViewModel; } }
		public GroupManagingPage()
        {
            InitializeComponent();
        }
    }
}
