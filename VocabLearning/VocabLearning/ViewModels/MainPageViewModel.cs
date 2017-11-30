using Microsoft.Identity.Client;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VocabLearning.Helpers;
using VocabLearning.Models;
using VocabLearning.Services;
using Xamarin.Forms;

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
		public readonly IAzureService _azureService = ServiceLocator.Instance.Resolve<IAzureService>();

		public DelegateCommand NavigateToSpeakPageCommand { get; private set; }
		public DelegateCommand NavigateToTeacherMasterDetailPageCommand { get; private set; }
		public DelegateCommand NavigateToStudentMasterDetailPageCommand { get; private set; }
		public DelegateCommand NavigateToLoginPageCommand { get; private set; }


		public MainPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			NavigateToSpeakPageCommand = new DelegateCommand(NavigateToSpeakPage);
			NavigateToTeacherMasterDetailPageCommand = new DelegateCommand(NavigateToTeacherMasterDetailPageAsync);
			NavigateToStudentMasterDetailPageCommand = new DelegateCommand(NavigateToStudentMasterDetailPage);
			NavigateToLoginPageCommand = new DelegateCommand(NavigateToLoginPage);

			_azureService = DependencyService.Get<AzureService>();
		}

		private void NavigateToLoginPage()
		{
			_navigationService.NavigateAsync("LoginPage");
		}

		public void NavigateToSpeakPage()
		{
			_navigationService.NavigateAsync("SpeakPage");
		}

		public async void NavigateToTeacherMasterDetailPageAsync()
		{
			try
			{
				bool IsTeacher = await App.LoginProvider.LoginAsync();
				if (IsTeacher)
				{
					await _navigationService.NavigateAsync("app:///TeacherMasterDetailPage/NavigationPage/TeacherOverviewPage");
				}
			}
			catch (MsalException ex)
			{
				await Application.Current.MainPage.DisplayAlert("Login Failed", ex.Message, "OK");
			}			
		}

		public void NavigateToStudentMasterDetailPage()
		{
			_navigationService.NavigateAsync("app:///StudentMasterDetailPage/NavigationPage/StudentOverviewPage");
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
