using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
    public partial class StudentsSearchPage : ContentPage
    {
		public StudentsSearchPageViewModel ViewModel { get { return BindingContext as StudentsSearchPageViewModel; } }
		public StudentsSearchPage()
        {
            InitializeComponent();
        }
    }
}
