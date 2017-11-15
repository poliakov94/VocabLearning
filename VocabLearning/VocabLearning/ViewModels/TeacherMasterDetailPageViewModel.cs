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
		private MasterMenuItem _menuItemSelected;
		public MasterMenuItem MenuItemSelected
		{
			get { return _menuItemSelected; }
			set
			{
				if (_menuItemSelected != value)
					_menuItemSelected = value;
				RaisePropertyChanged("ItemSelected");
				_navigationService.NavigateAsync(_menuItemSelected.NavigationPage.Title);
			}
		}

		public ObservableCollection<MasterMenuItem> _menuItems = new ObservableCollection<MasterMenuItem>();
		public ObservableCollection<MasterMenuItem> MenuItems { get { return _menuItems; } set { _menuItems = value; } }

		public TeacherMasterDetailPageViewModel(INavigationService navigationService)
			:base(navigationService)
		{			
			_menuItems.Add(new MasterMenuItem
			{
				Name = "Overview",
				NavigationPage = new TeacherOverviewPage()
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Progress",
				NavigationPage = new TeacherProgressPage()
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Students",
				NavigationPage = new TeacherStudentsPage()
			});

			_menuItems.Add(new MasterMenuItem
			{
				Name = "Exercises",
				NavigationPage = new TeacherExercisesPage()
			});

			RaisePropertyChanged("MenuItems");
		}
	}
}
