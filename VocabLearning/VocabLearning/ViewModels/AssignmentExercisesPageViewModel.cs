using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class AssignmentExercisesPageViewModel : BaseViewModel
	{
		private Assignment _assignment;
		public Assignment Assignment
		{
			get { return _assignment; }
			set
			{
				_assignment = value;
				RaisePropertyChanged("Assignment");
			}
		}
		public ObservableCollection<Exercise> _Exercises = new ObservableCollection<Exercise>();
		public ObservableCollection<Exercise> Exercises { get { return _Exercises; } set { _Exercises = value; RaisePropertyChanged("Exercises"); } }

		public AssignmentExercisesPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Assignment = (Assignment)parameters["model"];
				//Exercises = new ObservableCollection<Exercise>(await _azureService.GetExercisesAsync(Assignment.Id));
			}
		}
	}
}
