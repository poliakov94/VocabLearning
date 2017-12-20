using Microsoft.Identity.Client;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Diagnostics;
using VocabLearning.Helpers;
using VocabLearning.Services;
using Xamarin.Forms;

namespace VocabLearning.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		private INavigationService _navigationService;
		public readonly IAzureService _azureService = ServiceLocator.Instance.Resolve<IAzureService>();

		private bool IsTeacher;
		private bool Authenticated;
		private bool showLogin;
		public bool ShowLoginButton
		{
			get { return showLogin; }
			set { SetProperty(ref showLogin, value); }
		}
		private bool isBusy;
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); RaisePropertyChanged("IsBusy"); }
		}

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
			IsBusy = true;
			try
			{
				if (!Authenticated)
				{					
					Authenticated = await App.LoginProvider.LoginAsync();
				}

				IsTeacher = AzureService.DefaultService.User.IsTeacher;

				Debug.WriteLine($"User authenticated, IsTeacher = {IsTeacher}");

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
			IsBusy = false;
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
		}

		public async void OnNavigatingTo(NavigationParameters parameters)
		{
			
			if (!App.LoginProvider.ClientCached())
			{
				Authenticated = false;
				ShowLoginButton = true;
				Debug.WriteLine("Client not cached, showing login page...");
				return;
			}

			IsBusy = true;
			Authenticated = await App.LoginProvider.LoginAsync(true);
			IsBusy = false;

			if (!Authenticated)
			{
				ShowLoginButton = true;			
				return;
			}

			IsTeacher = AzureService.DefaultService.User.IsTeacher;

			if (IsTeacher == true)
			{
				await _navigationService.NavigateAsync("app:///TeacherMasterDetailPage/NavigationPage/TeacherOverviewPage");
			}
			else if (IsTeacher == false)
			{
				await _navigationService.NavigateAsync("app:///StudentMasterDetailPage/NavigationPage/StudentOverviewPage");
			}
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
		}
	}
}
