using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
    public partial class GroupExercisesPage : ContentPage
    {
		public GroupExercisesPageViewModel ViewModel { get { return BindingContext as GroupExercisesPageViewModel; } }
		public GroupExercisesPage()
        {
            InitializeComponent();
        }		
    }
}
