using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class AssignmentCreationPageViewModel : BaseViewModel
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

		public ObservableCollection<StudentGroup> _groups = new ObservableCollection<StudentGroup>();
		public ObservableCollection<StudentGroup> Groups { get { return _groups; } set { _groups = value; RaisePropertyChanged("Groups"); } }

		private StudentGroup _selectedGroup;
		public StudentGroup SelectedGroup
		{
			get { return _selectedGroup; }
			set
			{
				if (_selectedGroup != value)
					_selectedGroup = value;				
			}
		}

		public AssignmentCreationPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
		}
		
		private DelegateCommand _createAssignment;
		public DelegateCommand CreateAssignmentCommand =>
			_createAssignment ?? (_createAssignment = new DelegateCommand(ExecuteCreateAssignmentCommand));

		async void ExecuteCreateAssignmentCommand()
		{
			Assignment.ValidFrom = Assignment.ValidFrom.Date + ValidFromTime;
			Assignment.ValidUntil = Assignment.ValidUntil.Date + ValidUntilTime;
			Assignment.StudentGroup = SelectedGroup;
			Assignment.StudentGroup_Id = SelectedGroup.Id;
			await _azureService.SaveAssignmentAsync(Assignment);
			await _azureService.SynchronizeAssignmentsAsync();

			await _navigationService.GoBackAsync();
		}

		public override async void OnNavigatingTo(NavigationParameters parameters)
		{
			Assignment = new Assignment()
			{
				ValidFrom = System.DateTime.Now,
				ValidUntil = System.DateTime.Now,
				Name = "Enter a name"				
			};

			ValidFromTime = Assignment.ValidFrom.TimeOfDay;
			ValidUntilTime = Assignment.ValidUntil.TimeOfDay;
			RaisePropertyChanged("ValidFromTime");
			RaisePropertyChanged("ValidUntilTime");

			Groups = new ObservableCollection<StudentGroup>(await _azureService.GetGroupsAsync(""));
		}
	}
}
