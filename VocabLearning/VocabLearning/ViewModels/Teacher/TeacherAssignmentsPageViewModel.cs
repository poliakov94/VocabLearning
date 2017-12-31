using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Helpers;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class TeacherAssignmentsPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

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

				_navigationService.NavigateAsync("AssignmentExercisesPage", navigationParams);

				_assignmentSelected = null;
				RaisePropertyChanged("Assignments");
			}
		}

		public TeacherAssignmentsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
		}		

		public async override void OnNavigatingTo(NavigationParameters parameters)
		{
			await _azureService.SyncOfflineCacheAsync();

			var groupsTable = await _azureService.GetTableAsync<StudentGroup>();
			var groups = (await groupsTable.ReadAllItemsAsync())
				.Where(g => g.Teacher_Id == _azureService.User.Id)
				.ToList();

			var assignmentsTable = await _azureService.GetTableAsync<Assignment>();
			var assignments = (await assignmentsTable.ReadAllItemsAsync())
				//.Where(a => a.ValidUntil > System.DateTime.Now.AddDays(-1))
				.ToList();

			assignments = (from g in groups
						join a in assignments on g.Id equals a.StudentGroup_Id
						select a).ToList();

			foreach (var assignment in assignments)
			{
				assignment.StudentGroup = groups.FirstOrDefault(g => g.Id == assignment.StudentGroup_Id);
			}

			var grouped =
				assignments.OrderBy(a => a.StudentGroup.Name)
				.GroupBy(a => a.StudentGroup.Name)
				.Select(a => new ObservableGroupCollection<string, Assignment>(a))
				.ToList();

			Assignments = new ObservableCollection<ObservableGroupCollection<string, Assignment>>(grouped);
		}
	}
}
