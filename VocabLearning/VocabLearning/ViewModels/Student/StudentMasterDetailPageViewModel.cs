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
	public class StudentMasterDetailPageViewModel : BaseViewModel
	{
		public ObservableCollection<MasterMenuItem> _menuItems = new ObservableCollection<MasterMenuItem>();
		public ObservableCollection<MasterMenuItem> MenuItems { get { return _menuItems; } set { _menuItems = value; } }
		public StudentMasterDetailPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}

		private MasterMenuItem _menuItemSelected;
		public MasterMenuItem MenuItemSelected
		{
			get { return _menuItemSelected; }
			set
			{
				if (_menuItemSelected != value)
					_menuItemSelected = value;
				RaisePropertyChanged("ItemSelected");
				_navigationService.NavigateAsync(_menuItemSelected.NavigationPage);
			}
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			_menuItems.Clear();

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Overview",
				NavigationPage = "NavigationPage/StudentOverviewPage",
				IconSource = "overview.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Progress",
				NavigationPage = "NavigationPage/StudentProgressPage",
				IconSource = "progress.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Assignments",
				NavigationPage = "NavigationPage/StudentAssignmentsPage",
				IconSource = "students.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Test",
				NavigationPage = "NavigationPage/StudentTestPage",
				IconSource = "assignments.png"
			});

			RaisePropertyChanged("MenuItems");
		}

		public override async void OnNavigatingTo(NavigationParameters parameters)
		{
			IsBusy = true;

			//await _azureService.Init();

			IsBusy = false;
		}
	}
}
