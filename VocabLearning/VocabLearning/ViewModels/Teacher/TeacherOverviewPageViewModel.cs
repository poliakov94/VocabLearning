using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class TeacherOverviewPageViewModel : BaseViewModel
	{
		public TeacherOverviewPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}
	}
}
