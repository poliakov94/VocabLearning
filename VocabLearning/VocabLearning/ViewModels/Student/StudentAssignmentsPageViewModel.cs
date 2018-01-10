using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class StudentAssignmentsPageViewModel : BaseViewModel
	{
		public ObservableCollection<StudentAssignment> _Assignments;
		public ObservableCollection<StudentAssignment> Assignments { get { return _Assignments; } set { _Assignments = value; RaisePropertyChanged("Assignments"); } }

		private StudentAssignment _assignmentSelected;
		public StudentAssignment AssignmentSelected
		{
			get { return _assignmentSelected; }
			set
			{
				if (_assignmentSelected != value)
					_assignmentSelected = value;

				var navigationParams = new NavigationParameters
				{
					{ "model", _assignmentSelected.Assignment }
				};

				_navigationService.NavigateAsync("StudentLearningPage", navigationParams, false);

				_assignmentSelected = null;
			}
		}
		public StudentAssignmentsPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}

		public async override void OnNavigatingTo(NavigationParameters parameters)
		{
			_assignmentSelected = null;
			await _azureService.SyncOfflineCacheAsync();

			var groupsTable = await _azureService.GetTableAsync<StudentGroup>();
			var group = (
				await groupsTable
				.Where(g => g.Id == _user.StudentGroup_Id)
			).FirstOrDefault();

			var assignmentsTable = await _azureService.GetTableAsync<Assignment>();
			var assignments = await assignmentsTable
				.Where(a => a.StudentGroup_Id == group.Id);

			var exercisesTable = await _azureService.GetTableAsync<Exercise>();

			var studentAssignments = new List<StudentAssignment>();

			var isActive = false;

			foreach (var assignment in assignments)
			{
				assignment.Exercises = await exercisesTable.Where(e => e.Assignment_Id == assignment.Id);
				isActive = assignment.ValidFrom < System.DateTime.Now && assignment.ValidUntil > System.DateTime.Now && assignment.Exercises.Any();
				studentAssignments.Add(new StudentAssignment
				{
					Assignment = assignment,
					IsActive = isActive
				});
			}
			
			Assignments = new ObservableCollection<StudentAssignment>(studentAssignments);
		}

		public class StudentAssignment
		{
			public Assignment Assignment { get; set; }
			public bool IsActive { get; set; }
		}
	}
}
