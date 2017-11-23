using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;
using VocabLearning.Views;
using Xamarin.Forms;

namespace VocabLearning.ViewModels
{
	public class TeacherMasterDetailPageViewModel : BaseViewModel
	{
		public ObservableCollection<MasterMenuItem> _menuItems = new ObservableCollection<MasterMenuItem>();
		public ObservableCollection<MasterMenuItem> MenuItems { get { return _menuItems; } set { _menuItems = value; } }

		public TeacherMasterDetailPageViewModel(INavigationService navigationService)
			:base(navigationService)
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
				NavigationPage = "TeacherOverviewPage",
				IconSource = "overview.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Progress",
				NavigationPage = "TeacherProgressPage",
				IconSource = "progress.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Students",
				NavigationPage = "TeacherStudentsPage",
				IconSource = "students.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Assignments",
				NavigationPage = "TeacherAssignmentsPage",
				IconSource = "assignments.png"
			});

			RaisePropertyChanged("MenuItems");
		}

		public override async void OnNavigatingTo(NavigationParameters parameters)
		{
			IsBusy = true;

			await _azureService.Init();

			IsBusy = false;
		}
	}
}
