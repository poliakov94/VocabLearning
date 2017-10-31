using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VocabLearning.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		private INavigationService _navigationService;

		public DelegateCommand NavigateToSpeakPageCommand { get; private set; }
		public DelegateCommand NavigateToTeacherMasterDetailPageCommand { get; private set; }
		public DelegateCommand NavigateToStudentMasterDetailPageCommand { get; private set; }
		public DelegateCommand NavigateToLoginPageCommand { get; private set; }


		public MainPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			NavigateToSpeakPageCommand = new DelegateCommand(NavigateToSpeakPage);
			NavigateToTeacherMasterDetailPageCommand = new DelegateCommand(NavigateToTeacherMasterDetailPage);
			NavigateToStudentMasterDetailPageCommand = new DelegateCommand(NavigateToStudentMasterDetailPage);
			NavigateToLoginPageCommand = new DelegateCommand(NavigateToLoginPage);
		}

		private void NavigateToLoginPage()
		{
			_navigationService.NavigateAsync("LoginPage");
		}

		public void NavigateToSpeakPage()
		{
			_navigationService.NavigateAsync("SpeakPage");
		}

		public void NavigateToTeacherMasterDetailPage()
		{
			_navigationService.NavigateAsync("app:///NavigationPage/TeacherMasterDetailPage");
		}

		public void NavigateToStudentMasterDetailPage()
		{
			_navigationService.NavigateAsync("app:///NavigationPage/StudentMasterDetailPage");
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("title"))
				Title = (string)parameters["title"] + " and Prism";
		}
	}
}
