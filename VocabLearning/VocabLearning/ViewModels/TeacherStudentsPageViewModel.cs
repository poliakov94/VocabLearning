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
				Name = "New group, hold to edit or delete."
			};

			try
			{
				await _azureService.SaveGroupAsync(group);
				await _azureService.SynchronizeGroupsAsync();
				Groups = new ObservableCollection<StudentGroup>(await _azureService.GetGroupsAsync(""));
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
				await _azureService.DeleteGroupAsync(group);
				await _azureService.SynchronizeGroupsAsync();
				Groups = new ObservableCollection<StudentGroup>(await _azureService.GetGroupsAsync(""));				
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
			//if (Groups == null && parameters.ContainsKey("teacherid"))
			//{
			//	var teacherId = (string)parameters["teacherid"];
			//	Groups = new ObservableCollection<StudentGroup>(await _azureService.GetGroupsAsync(teacherId));
			//}			
			
			IsBusy = true;

			try
			{
				Groups = new ObservableCollection<StudentGroup>(await _azureService.GetGroupsAsync(""));				
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
						
			IsBusy = false;
		}
	}
}
