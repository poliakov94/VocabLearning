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
		private INavigationService _navigationService;
		public readonly IAzureService _azureService = ServiceLocator.Instance.Resolve<IAzureService>();

		private bool? IsTeacher = null;
		
		public MainPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			_azureService = DependencyService.Get<AzureService>();
		}

		private DelegateCommand _loginCommand;
		public DelegateCommand LoginCommand =>
			_loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand));

		async void ExecuteLoginCommand()
		{
			try
			{
				IsTeacher = await App.LoginProvider.LoginAsync();
				if (IsTeacher == true)
				{
					await _navigationService.NavigateAsync("app:///TeacherMasterDetailPage/NavigationPage/TeacherOverviewPage");
				}
				else if (IsTeacher == false)
				{
					await _navigationService.NavigateAsync("app:///StudentMasterDetailPage/NavigationPage/StudentOverviewPage");
				}
			}
			catch (MsalException ex)
			{
				await Application.Current.MainPage.DisplayAlert("Login Failed", ex.Message, "OK");
			}
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
		}

		public async void OnNavigatingTo(NavigationParameters parameters)
		{
			//try
			//{
			//	IsTeacher = await App.LoginProvider.LoginAsync(true);
			//	if (IsTeacher == true)
			//	{
			//		await _navigationService.NavigateAsync("app:///TeacherMasterDetailPage/NavigationPage/TeacherOverviewPage");
			//	}
			//	else if (IsTeacher == false)
			//	{
			//		await _navigationService.NavigateAsync("app:///StudentMasterDetailPage/NavigationPage/StudentOverviewPage");
			//	}
			//}
			//catch (MsalException ex)
			//{
			//	await Application.Current.MainPage.DisplayAlert("Login Failed", ex.Message, "OK");
			//}
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
		}
	}
}
