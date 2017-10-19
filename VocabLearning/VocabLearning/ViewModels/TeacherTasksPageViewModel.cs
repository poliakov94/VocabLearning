using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VocabLearning.ViewModels
{
	public class TeacherTasksPageViewModel : BaseViewModel
	{
		public TeacherTasksPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}
	}
}
