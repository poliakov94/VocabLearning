﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VocabLearning.ViewModels
{
	public class TranslateWordPageViewModel : BaseViewModel
	{
		public TranslateWordPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}
	}
}
