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
	public class AssignmentManagingPageViewModel : BaseViewModel
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

		public TimeSpan ValidFromTime { get; set; }
		public TimeSpan ValidUntilTime { get; set; }

		public ObservableCollection<Exercise> _Exercises = new ObservableCollection<Exercise>();
		public ObservableCollection<Exercise> Exercises { get { return _Exercises; } set { _Exercises = value; RaisePropertyChanged("Exercises"); } }

		public AssignmentManagingPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
		}

		private DelegateCommand _saveAssignment;
		public DelegateCommand SaveAssignmentCommand =>
			_saveAssignment ?? (_saveAssignment = new DelegateCommand(ExecuteSaveAssignmentCommand));

		async void ExecuteSaveAssignmentCommand()
		{
			Assignment.ValidFrom = Assignment.ValidFrom.Date + ValidFromTime;
			Assignment.ValidUntil = Assignment.ValidUntil.Date + ValidUntilTime;

			var assignmentsTable = await _azureService.GetTableAsync<Assignment>();
			await assignmentsTable.UpdateItemAsync(Assignment);
			await _azureService.SyncOfflineCacheAsync();

			var navigationParams = new NavigationParameters
			{
				{ "groupId", Assignment.StudentGroup_Id }
			};

			await _navigationService.GoBackAsync(navigationParams);
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
				ValidFromTime = Assignment.ValidFrom.TimeOfDay;
				ValidUntilTime = Assignment.ValidUntil.TimeOfDay;
				RaisePropertyChanged("ValidFromTime");
				RaisePropertyChanged("ValidUntilTime");

				RefreshExercises();
			}
		}

		private async void RefreshExercises()
		{
			var exerercisesTable = await _azureService.GetTableAsync<Exercise>();
			var exercises = await exerercisesTable.Where(e => e.Assignment_Id == Assignment.Id);

			foreach (var exercise in exercises)
			{
				exercise.Assignment = Assignment;
			}

			Exercises = new ObservableCollection<Exercise>(exercises);
		}
	}
}
