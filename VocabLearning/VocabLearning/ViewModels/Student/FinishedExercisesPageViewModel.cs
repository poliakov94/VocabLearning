using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class FinishedExercisesPageViewModel : BaseViewModel
	{
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

		private int passedCount;
		public int PassedCount
		{
			get { return passedCount; }
			set { SetProperty(ref passedCount, value); }
		}

		private string counter;
		public string Counter
		{
			get { return counter; }
			set { SetProperty(ref counter, value); }
		}

		private int attempt;
		public int Attempt
		{
			get { return attempt; }
			set { SetProperty(ref attempt, value); }
		}

		public FinishedExercisesPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}

		private DelegateCommand _finishCommand;
		public DelegateCommand FinishCommand =>
			_finishCommand ?? (_finishCommand = new DelegateCommand(ExecuteFinishCommand));

		void ExecuteFinishCommand()
		{
			_navigationService.NavigateAsync("app:///StudentMasterDetailPage/NavigationPage/StudentAssignmentsPage");
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Assignment = (Assignment)parameters["model"];
			}

			if (parameters.ContainsKey("results"))
			{
				var results = (List<StudentExercise>)parameters["results"];
				ExerciseCount = results.Count();
				PassedCount = results.Where(p => p.Passed).Count();
				Attempt = results.FirstOrDefault().Attempt;
				Counter = $"{PassedCount} / {ExerciseCount}";
			}
		}
	}
}
