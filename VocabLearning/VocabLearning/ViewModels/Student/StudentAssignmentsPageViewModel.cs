using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Helpers;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class StudentAssignmentsPageViewModel : BaseViewModel
	{
		public ObservableCollection<ObservableGroupCollection<string, Assignment>> _Assignments;
		public ObservableCollection<ObservableGroupCollection<string, Assignment>> Assignments { get { return _Assignments; } set { _Assignments = value; RaisePropertyChanged("Assignments"); } }

		private Assignment _assignmentSelected;
		public Assignment AssignmentSelected
		{
			get { return _assignmentSelected; }
			set
			{
				if (_assignmentSelected != value)
					_assignmentSelected = value;

				var navigationParams = new NavigationParameters
				{
					{ "model", _assignmentSelected }
				};

				//_navigationService.NavigateAsync("AssignmentExercisesPage", navigationParams, false);
			}
		}
		public StudentAssignmentsPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			//var assignments = (await _azureService.GetAssignmentsAsync())
			//	//.Where(a => a.ValidUntil > System.DateTime.Now.AddDays(-1))
			//	.ToList();

			//var grouped =
			//	assignments.OrderBy(a => a.StudentGroup.Name)
			//	.GroupBy(a => a.StudentGroup.Name)
			//	.Select(a => new ObservableGroupCollection<string, Assignment>(a))
			//	.ToList();

			//Assignments = new ObservableCollection<ObservableGroupCollection<string, Assignment>>(grouped);
		}
	}
}
