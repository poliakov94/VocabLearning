using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using VocabLearning.Services;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class BaseViewModel : BindableBase, INavigationAware
	{
		protected readonly INavigationService _navigationService;
		public readonly AzureService _azureService;

		private bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				RaisePropertyChanged("IsBusy");
			}
		}
		
		public DelegateCommand<string> NavigateCommand { get; set; }
		public BaseViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			NavigateCommand = new DelegateCommand<string>(Navigate);
			_azureService = DependencyService.Get<AzureService>();
		}

		public BaseViewModel()
		{

		}

		private async void Navigate(string name)
		{
			try
			{
				await _navigationService.NavigateAsync(name, null, false);
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		public virtual void OnNavigatedFrom(NavigationParameters parameters)
		{
			
		}

		public virtual void OnNavigatedTo(NavigationParameters parameters)
		{
			
		}

		public virtual void OnNavigatingTo(NavigationParameters parameters)
		{
			
		}
	}
}
