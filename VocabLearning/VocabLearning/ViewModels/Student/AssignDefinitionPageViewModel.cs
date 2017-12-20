using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class AssignDefinitionPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

		private Assignment assignment;
		public Assignment Assignment
		{
			get { return assignment; }
			set { SetProperty(ref assignment, value); }
		}

		private int exerciseCount;
		public int ExerciseCount
		{
			get { return exerciseCount; }
			set { SetProperty(ref exerciseCount, value); }
		}

		private int exerciseNo;
		public int ExerciseNo
		{
			get { return exerciseNo; }
			set { SetProperty(ref exerciseNo, value); }
		}

		private string counter;
		public string Counter
		{
			get { return counter; }
			set { SetProperty(ref counter, value); }
		}

		private List<Exercise> exercises;
		public List<Exercise> Exercises
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

		private List<StudentExercise> results;
		public List<StudentExercise> Results
		{
			get { return results; }
			set { SetProperty(ref results, value); }
		}

		private string selectedDefinition;
		public string SelectedDefinition
		{
			get { return selectedDefinition; }
			set { SetProperty(ref selectedDefinition, value); }
		}

		private ObservableCollection<string> definitions;
		public ObservableCollection<string> Definitions
		{
			get { return definitions; }
			set { SetProperty(ref definitions, value); }
		}

		private static Random rng = new Random();

		public AssignDefinitionPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
		}

		private DelegateCommand _check;
		public DelegateCommand CheckCommand =>
			_check ?? (_check = new DelegateCommand(ExecuteCheckCommand));

		async void ExecuteCheckCommand()
		{
			var passed = currentExercise.Definition == SelectedDefinition;

			var result = new StudentExercise()
			{
				Exercise_Id = CurrentExercise.Id,
				Student_Id = _azureService.User.Id,
				Passed = passed
			};

			if (passed)
				await _pageDialogService.DisplayAlertAsync("Perfect!", "Good answer", "Ok");
			else
				await _pageDialogService.DisplayAlertAsync("Wrong!", $"You failed, the correct answer was: {CurrentExercise.Definition}", "Ok");

			Results.Add(result);

			if (ExerciseCount == ExerciseNo)
				await ExercisesFinished();
			else
			{
				ExerciseNo++;
				CurrentExercise = Exercises[ExerciseNo - 1];
				Counter = $"{ExerciseNo} / {ExerciseCount}";

				var definitions = new List<string>
				{
					currentExercise.Definition
				};
				var exs = Exercises.Where(e => e.Id != CurrentExercise.Id)
					.OrderBy(e => rng.Next())
					.Take(2)
					.Select(e => e.Definition)
					.ToList();

				definitions.AddRange(exs);

				Definitions = new ObservableCollection<string>(definitions.OrderBy(e => rng.Next()));

				return;
			}			
		}

		async Task ExercisesFinished()
		{
			await _azureService.SyncOfflineCacheAsync();
			var resultsTable = await _azureService.GetTableAsync<StudentExercise>();
			var attempt = (await resultsTable.ReadAllItemsAsync())
				.Where(r => r.Student_Id == _azureService.User.Id && r.Exercise_Id == CurrentExercise.Id)
				.Count();				

			foreach (var res in Results)
			{
				res.Attempt = attempt + 1;
				await resultsTable.CreateItemAsync(res);
			}
			await _azureService.SyncOfflineCacheAsync();

			var navigationParams = new NavigationParameters
			{
				{"model", Assignment },
				{"results", Results }
			};

			await _navigationService.NavigateAsync("app:///FinishedExercisesPage", navigationParams);
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Assignment = (Assignment)parameters["model"];

				Exercises = new List<Exercise>(Assignment.Exercises.OrderBy(e => rng.Next()));
				ExerciseCount = Assignment.Exercises.Count();
				ExerciseNo = 1;
				CurrentExercise = Exercises[ExerciseNo - 1];			
				Counter = $"{ExerciseNo} / {ExerciseCount}";

				Results = new List<StudentExercise>();

				var definitions = new List<string>
				{
					currentExercise.Definition
				};
				var exs = Exercises.Where(e => e.Id != CurrentExercise.Id)
					.OrderBy(e => rng.Next())
					.Take(2)
					.Select(e => e.Definition)
					.ToList();

				definitions.AddRange(exs);

				Definitions = new ObservableCollection<string>(definitions.OrderBy(e => rng.Next()));
			}
		}
	}
}
