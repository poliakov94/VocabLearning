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

		private Assignment _groupSelected;
		public Assignment GroupSelected
		{
			get { return _groupSelected; }
			set
			{
				if (_groupSelected != value)
					_groupSelected = value;

				//var navigationParams = new NavigationParameters
				//{
				//	{ "model", _groupSelected }
				//};
				//_navigationService.NavigateAsync("GroupManagingPage", navigationParams, false);
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
			_createAssignment ?? (_createAssignment = new DelegateCommand(ExecuteCreateAssignmentCommandCommand));

		public async void ExecuteCreateAssignmentCommandCommand()
		{
			var answer = await _pageDialogService.DisplayAlertAsync("Confirm", "Would you like to create a new assignment?", "Yes", "No");

			if (!answer)
				return;

			IsBusy = true;

			var assignment = new Assignment()
			{
				Name = "Home vocabulary",
				ValidFrom = System.DateTime.Now,
				ValidUntil = System.DateTime.Now,
				StudentGroup = Group			
			};

			try
			{
				//await _azureService.SaveAssignmentAsync(assignment);
				//await _azureService.SynchronizeAssignmentsAsync();
				//Assignments = new ObservableCollection<Assignment>(await _azureService.GetAssignmentsAsync(GroupId));
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

				IsBusy = true;

				try
				{
					await _azureService.SynchronizeAssignmentsAsync();
					Assignments = new ObservableCollection<Assignment>(await _azureService.GetAssignmentsAsync(Group.Id));
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.ToString());
				}

				IsBusy = false;
			}
		}
	}
}
