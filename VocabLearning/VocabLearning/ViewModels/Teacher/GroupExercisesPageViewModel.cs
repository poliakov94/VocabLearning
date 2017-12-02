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
	public class GroupExercisesPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

		public StudentGroup Group;
		public bool IsEmpty
		{
			get { return (Assignments == null || Assignments.Count == 0); }
		}

		private Assignment _assignmentSelected;
		public Assignment AssignmentSelected
		{
			get { return _assignmentSelected; }
			set
			{
				if (_assignmentSelected != value)
					_assignmentSelected = value;

				var navigationParams = new NavigationParameters
				{
					{ "model", _assignmentSelected }
				};

				_navigationService.NavigateAsync("AssignmentManagingPage", navigationParams, false);
			}
		}
		public ObservableCollection<Assignment> _Assignments = new ObservableCollection<Assignment>();
		public ObservableCollection<Assignment> Assignments { get { return _Assignments; } set { _Assignments = value; RaisePropertyChanged("Assignments"); } }

		public GroupExercisesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
		}

		private DelegateCommand _createAssignment;
		public DelegateCommand CreateAssignmentCommand =>
			_createAssignment ?? (_createAssignment = new DelegateCommand(ExecuteCreateAssignmentCommand));

		public async void ExecuteCreateAssignmentCommand()
		{
			var answer = await _pageDialogService.DisplayAlertAsync("Confirm", "Would you like to create a new assignment?", "Yes", "No");

			if (!answer)
				return;

			IsBusy = true;

			var assignment = new Assignment()
			{
				Name = "New, tap to edit.",
				ValidFrom = System.DateTime.Now,
				ValidUntil = System.DateTime.Now,
				//StudentGroup = Group,
				StudentGroup_Id = Group.Id
			};

			try
			{
				var assignmentsTable = await _azureService.GetTableAsync<Assignment>();
				assignment = await assignmentsTable.CreateItemAsync(assignment);
				await _azureService.SyncOfflineCacheAsync();

				Assignments.Add(assignment);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}

			IsBusy = false;
		}

		public async override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Group = (StudentGroup)parameters["model"];

				var assignmentsTable = await _azureService.GetTableAsync<Assignment>();
				var assignments = (await assignmentsTable.ReadAllItemsAsync()).Where(a => a.StudentGroup_Id == Group.Id);

				Assignments = new ObservableCollection<Assignment>(assignments);
			}
			else if (parameters.ContainsKey("groupId"))
			{
				var groupId = (string)parameters["groupId"];

				var assignmentsTable = await _azureService.GetTableAsync<Assignment>();
				var assignments = (await assignmentsTable.ReadAllItemsAsync()).Where(a => a.StudentGroup_Id == groupId);

				Assignments = new ObservableCollection<Assignment>(assignments);
			}

			RaisePropertyChanged("IsEmpty");
		}
	}
}
