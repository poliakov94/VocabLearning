using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VocabLearning.Models;
using VocabLearning.Services;
using VocabLearning.Views;

namespace VocabLearning.ViewModels
{
	public class TeacherStudentsPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

		private StudentGroup _groupSelected;
		public StudentGroup GroupSelected
		{
			get { return _groupSelected; }
			set
			{
				if (_groupSelected != value)
					_groupSelected = value;

				var navigationParams = new NavigationParameters
				{
					{ "model", _groupSelected }
				};
				_navigationService.NavigateAsync("GroupManagingPage", navigationParams, false);

				_groupSelected = null;
				RaisePropertyChanged("GroupSelected");
				RaisePropertyChanged("Groups");
			}
		}
		public ObservableCollection<StudentGroup> _groups = new ObservableCollection<StudentGroup>();
		public ObservableCollection<StudentGroup> Groups { get { return _groups; } set { _groups = value; RaisePropertyChanged("Groups"); } }
		public TeacherStudentsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
		}

		private DelegateCommand _createGroup;
		public DelegateCommand CreateGroupCommand =>
			_createGroup ?? (_createGroup = new DelegateCommand(ExecuteCreateGroupCommand));

		public async void ExecuteCreateGroupCommand()
		{
			var answer = await _pageDialogService.DisplayAlertAsync("Confirm", "Would you like to create a new group?", "Yes", "No");
						
			if (!answer)
				return;

			IsBusy = true;

			var group = new StudentGroup()
			{
				GroupSize = 0,
				Name = "New group, hold to edit or delete.",
				Teacher_Id = _azureService.User.Id,
				Teacher = _azureService.User,
				AssignmentsCount = 0
			};

			try
			{
				var groupsTable = await _azureService.GetTableAsync<StudentGroup>();
				group = await groupsTable.CreateItemAsync(group);
				await _azureService.SyncOfflineCacheAsync();

				Groups.Add(group);				
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}

			IsBusy = false;
		}

		private DelegateCommand<StudentGroup> _deleteGroup;
		public DelegateCommand<StudentGroup> DeleteGroupCommand =>
			_deleteGroup ?? (_deleteGroup = new DelegateCommand<StudentGroup>(ExecuteDeleteGroupCommand));

		async void ExecuteDeleteGroupCommand(StudentGroup group)
		{
			var answer = await _pageDialogService.DisplayAlertAsync("Confirm", $"Are you sure to delete {group.Name} ?", "Yes", "No");

			if (!answer)
				return;
			try
			{		
				var groupsTable = await _azureService.GetTableAsync<StudentGroup>();
				await groupsTable.DeleteItemAsync(group);
				await _azureService.SyncOfflineCacheAsync();

				OnNavigatingTo(null);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private DelegateCommand<StudentGroup> _editGroup;
		public DelegateCommand<StudentGroup> EditGroupCommand =>
			_editGroup ?? (_editGroup = new DelegateCommand<StudentGroup>(ExecuteEditGroupCommand));

		void ExecuteEditGroupCommand(StudentGroup group)
		{
			var navigationParams = new NavigationParameters
				{
					{ "model", group }
				};
			_navigationService.NavigateAsync("GroupManagingPage", navigationParams, false);
		}

		public override async void OnNavigatingTo(NavigationParameters parameters)
		{			
			IsBusy = true;

			try
			{
				await _azureService.SyncOfflineCacheAsync();
				var groupsTable = (await _azureService.GetTableAsync<StudentGroup>()).ReturnTable();
				var groups = await groupsTable.Where(g => g.Teacher_Id == _azureService.User.Id).ToListAsync();

				var assignmentsTable = (await _azureService.GetTableAsync<Assignment>()).ReturnTable();

				var studentsTable = (await _azureService.GetTableAsync<User>()).ReturnTable();
				

				foreach (var group in groups)
				{
					group.Assignments = await assignmentsTable.Where(a => a.StudentGroup_Id == group.Id).ToListAsync();
					group.AssignmentsCount = group.Assignments.Count();
					group.Students = await studentsTable.Where(s => s.StudentGroup_Id == group.Id).ToListAsync();
					group.GroupSize = group.Students.Count();
				}

				Groups = new ObservableCollection<StudentGroup>(groups);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
						
			IsBusy = false;
		}
	}
}
