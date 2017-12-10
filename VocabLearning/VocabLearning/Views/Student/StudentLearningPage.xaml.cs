using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views.Student
{
    public partial class StudentLearningPage : ContentPage
    {
		public StudentLearningPageViewModel ViewModel { get { return BindingContext as StudentLearningPageViewModel; } }
		public StudentLearningPage()
        {
            InitializeComponent();
        }
    }
}
