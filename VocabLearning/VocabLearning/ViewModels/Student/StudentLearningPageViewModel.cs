using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class StudentLearningPageViewModel : BaseViewModel
	{
		private Assignment assignment;

		public Assignment Assignment
		{
			get { return assignment; }
			set { assignment = value; RaisePropertyChanged("Assignment"); }
		}

		private ObservableCollection<Exercise> exercises;
		public ObservableCollection<Exercise> Exercises
		{
			get { return exercises; }
			set { SetProperty(ref exercises, value); }
		}

		private Exercise currentExercise;
		public Exercise CurrentExercise
		{
			get { return currentExercise; }
			set { SetProperty(ref currentExercise, value); }
		}

		public StudentLearningPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}

		public override async void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Assignment = (Assignment)parameters["model"];

				var exercisesTable = await _azureService.GetTableAsync<Exercise>();
				var exercises = (await exercisesTable.ReadAllItemsAsync())
					.ToList();

				Exercises = new ObservableCollection<Exercise>(exercises);
				CurrentExercise = exercises.FirstOrDefault();
			}
		}
	}
}
