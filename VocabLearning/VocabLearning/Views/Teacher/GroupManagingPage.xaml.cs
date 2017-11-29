using Prism.Navigation;
using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
    public partial class GroupManagingPage : TabbedPage, INavigatingAware
    {
		public GroupManagingPageViewModel ViewModel { get { return BindingContext as GroupManagingPageViewModel; } }
		public GroupManagingPage()
        {
            InitializeComponent();
        }

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			foreach (var child in Children)
			{
				(child as INavigatingAware)?.OnNavigatingTo(parameters);
				(child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
			}
		}
	}
}
