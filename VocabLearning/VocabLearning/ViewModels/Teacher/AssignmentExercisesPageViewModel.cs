using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class AssignmentExercisesPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

		private Assignment _assignment;
		public Assignment Assignment
		{
			get { return _assignment; }
			set
			{
				_assignment = value;
				RaisePropertyChanged("Assignment");
			}
		}
		public ObservableCollection<Exercise> _Exercises = new ObservableCollection<Exercise>();
		public ObservableCollection<Exercise> Exercises { get { return _Exercises; } set { _Exercises = value; RaisePropertyChanged("Exercises"); } }

		public AssignmentExercisesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
		}
		private DelegateCommand<Exercise> _deleteExercise;
		public DelegateCommand<Exercise> DeleteExerciseCommand =>
			_deleteExercise ?? (_deleteExercise = new DelegateCommand<Exercise>(ExecuteDeleteExerciseCommand));

		async void ExecuteDeleteExerciseCommand(Exercise exercise)
		{
			var answer = await _pageDialogService.DisplayAlertAsync("Confirm", $"Are you sure to delete this exercise ?", "Yes", "No");

			if (!answer)
				return;
			try
			{
				var exercisesTable = await _azureService.GetTableAsync<Exercise>();
				await exercisesTable.DeleteItemAsync(exercise);
				await _azureService.SyncOfflineCacheAsync();

				RefreshExercises();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private DelegateCommand<Exercise> _editExercise;
		public DelegateCommand<Exercise> EditExerciseCommand =>
			_editExercise ?? (_editExercise = new DelegateCommand<Exercise>(ExecuteEditExerciseCommand));

		void ExecuteEditExerciseCommand(Exercise exercise)
		{
			var navigationParams = new NavigationParameters
				{
					{ "model", exercise }
				};
			_navigationService.NavigateAsync("ExerciseCreationPage", navigationParams, false);
		}

		private DelegateCommand _addExerciseCommand;
		public DelegateCommand AddExerciseCommand =>
			_addExerciseCommand ?? (_addExerciseCommand = new DelegateCommand(ExecuteAddExerciseCommand));

		async void ExecuteAddExerciseCommand()
		{
			var navigationParams = new NavigationParameters
			{
				{ "model", Assignment }
			};
			await _navigationService.NavigateAsync("ExerciseCreationPage", navigationParams, false);
		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Assignment = (Assignment)parameters["model"];

				RefreshExercises();

				Exercises = new ObservableCollection<Exercise>(Assignment.Exercises);
			}
		}

		private async void RefreshExercises()
		{
			var exerercisesTable = await _azureService.GetTableAsync<Exercise>();
			Assignment.Exercises = await exerercisesTable.Where(e => e.Assignment_Id == Assignment.Id);

			foreach (var exercise in Assignment.Exercises)
			{
				exercise.Assignment = Assignment;
			}

			Exercises = new ObservableCollection<Exercise>(Assignment.Exercises);
		}
	}
}
