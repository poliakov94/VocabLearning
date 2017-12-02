using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class AssignmentManagingPageViewModel : BaseViewModel
	{
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

		public AssignmentManagingPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{			

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

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Assignment = (Assignment)parameters["model"];
				ValidFromTime = Assignment.ValidFrom.TimeOfDay;
				ValidUntilTime = Assignment.ValidUntil.TimeOfDay;
				RaisePropertyChanged("ValidFromTime");
				RaisePropertyChanged("ValidUntilTime");
				//Exercises = new ObservableCollection<Exercise>(await _azureService.GetExercisesAsync(Assignment.Id));
			}
		}
	}
}
