using Prism.Commands;
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

		private int exerciseCount;
		public int ExerciseCount
		{
			get { return exerciseCount; }
			set { SetProperty(ref exerciseCount, value); }
		}

		public StudentLearningPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}

		private DelegateCommand<string> _typeSelected;
		public DelegateCommand<string> TypeSelectedCommand =>
			_typeSelected ?? (_typeSelected = new DelegateCommand<string>(ExecuteTypeSelectedCommand));

		void ExecuteTypeSelectedCommand(string parameter)
		{
			var navigationParams = new NavigationParameters
			{
				{ "model", Assignment }
			};

			switch (parameter)
			{
				case "Assign":
					_navigationService.NavigateAsync("AssignDefinitionPage", navigationParams);
					break;
				case "Complete":
					_navigationService.NavigateAsync("CompletePhrasePage", navigationParams);
					break;
				case "Translate":
					_navigationService.NavigateAsync("TranslateWordPage", navigationParams);
					break;
				default:
					break;
			}
		}

		public override async void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Assignment = (Assignment)parameters["model"];

				var exercisesTable = await _azureService.GetTableAsync<Exercise>();
				Assignment.Exercises = (await exercisesTable.ReadAllItemsAsync())
					.Where(e => e.Assignment_Id == Assignment.Id)
					.ToList();
				
				ExerciseCount = Assignment.Exercises.Count();
			}
		}
	}
}
