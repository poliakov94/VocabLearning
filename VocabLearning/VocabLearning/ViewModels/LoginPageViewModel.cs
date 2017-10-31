using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VocabLearning.ViewModels
{
	public class LoginPageViewModel : BaseViewModel
	{
        public LoginPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{ 
		}
	}
}
