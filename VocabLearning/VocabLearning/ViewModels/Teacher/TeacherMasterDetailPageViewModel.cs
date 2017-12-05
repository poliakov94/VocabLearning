using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
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
		IPageDialogService _pageDialogService;

		public ObservableCollection<MasterMenuItem> _menuItems = new ObservableCollection<MasterMenuItem>();
		public ObservableCollection<MasterMenuItem> MenuItems { get { return _menuItems; } set { _menuItems = value; } }

		public TeacherMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
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
				NavigationPage = "NavigationPage/TeacherOverviewPage",
				IconSource = "overview.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Progress",
				NavigationPage = "NavigationPage/TeacherProgressPage",
				IconSource = "progress.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Students",
				NavigationPage = "NavigationPage/TeacherStudentsPage",
				IconSource = "students.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Assignments",
				NavigationPage = "NavigationPage/TeacherAssignmentsPage",
				IconSource = "assignments.png"
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Logout",
				NavigationPage = "",
				IconSource = "assignments.png"
			});

			RaisePropertyChanged("MenuItems");
		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			IsBusy = true;

			//await _azureService.Init();

			IsBusy = false;
		}
	}
}
