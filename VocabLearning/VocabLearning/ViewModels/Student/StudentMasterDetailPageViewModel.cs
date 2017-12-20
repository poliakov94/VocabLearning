using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;
using Xamarin.Forms;

namespace VocabLearning.ViewModels
{
	public class StudentMasterDetailPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

		public ObservableCollection<MasterMenuItem> _menuItems = new ObservableCollection<MasterMenuItem>();
		public ObservableCollection<MasterMenuItem> MenuItems { get { return _menuItems; } set { _menuItems = value; } }

		private string student;
		public string Student
		{
			get { return student; }
			set { SetProperty(ref student, value); }
		}
		public StudentMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
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

				if (_menuItemSelected.Name != "Logout")
					_navigationService.NavigateAsync(_menuItemSelected.NavigationPage);
				else
				{
					Device.BeginInvokeOnMainThread(async () => {
						var answer = await _pageDialogService.DisplayAlertAsync("Confirm", "Are you sure you want to sign out?", "Yes", "No");
						if (!answer)
						{
							_menuItemSelected = null;
							return;
						}

						var exit = await App.LoginProvider.LogoutAsync();
						if (exit)
							await _navigationService.NavigateAsync("app:///NavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
					});
				}
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

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Logout",
				NavigationPage = "",
				IconSource = "ic_not_interested_black_24dp.png"
			});

			RaisePropertyChanged("MenuItems");

			Student = $"{_azureService.User.FirstName} {_azureService.User.LastName}";
		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			IsBusy = true;

			//await _azureService.Init();

			IsBusy = false;
		}
	}
}
