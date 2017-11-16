using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class TeacherExercisesPageViewModel : BaseViewModel
	{		
		public TeacherExercisesPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}		
	}
}
