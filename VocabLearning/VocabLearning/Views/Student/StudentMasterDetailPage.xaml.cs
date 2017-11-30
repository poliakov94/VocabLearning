using Xamarin.Forms;
using VocabLearning.ViewModels;

namespace VocabLearning.Views
{
	public partial class StudentMasterDetailPage : MasterDetailPage
	{
		public StudentMasterDetailPageViewModel ViewModel { get { return BindingContext as StudentMasterDetailPageViewModel; } }
		public StudentMasterDetailPage()
		{
			InitializeComponent();
		}
	}
}