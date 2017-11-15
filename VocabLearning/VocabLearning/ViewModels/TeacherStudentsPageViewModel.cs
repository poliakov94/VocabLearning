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
		public TeacherStudentsPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
		}

		private DelegateCommand _createGroup;
		public DelegateCommand CreateGroupCommand =>
			_createGroup ?? (_createGroup = new DelegateCommand(ExecuteCreateGroupCommand));

		public async void ExecuteCreateGroupCommand()
		{

			var answer = await App.Current.MainPage.DisplayAlert ("Confirm", "Would you like to create a new group?", "Yes", "No");
			
			if (!answer)
				return;

			IsBusy = true;

			var group = new StudentGroup()
			{
				GroupSize = 0,
				Name = "New group"
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

		public override async void OnNavigatedTo(NavigationParameters parameters)
		{
			//if (Groups == null && parameters.ContainsKey("teacherid"))
			//{
			//	var teacherId = (string)parameters["teacherid"];
			//	Groups = new ObservableCollection<StudentGroup>(await _azureService.GetGroupsAsync(teacherId));
			//}			
			
			IsBusy = true;

			try
			{
				await _azureService.SynchronizeGroupsAsync();
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
