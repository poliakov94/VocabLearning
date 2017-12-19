using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class StudentAssignmentsPageViewModel : BaseViewModel
	{
		public ObservableCollection<Assignment> _Assignments;
		public ObservableCollection<Assignment> Assignments { get { return _Assignments; } set { _Assignments = value; RaisePropertyChanged("Assignments"); } }

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
			await _azureService.SyncOfflineCacheAsync();

			var groupsTable = (await _azureService.GetTableAsync<StudentGroup>()).ReturnTable();
			var group = (
				await groupsTable
				.Where(g => g.Id == _azureService.User.StudentGroup_Id)
				.ToListAsync()
			).FirstOrDefault();

			var assignmentsTable = (await _azureService.GetTableAsync<Assignment>()).ReturnTable();
			var assignments = await assignmentsTable
				.Where(a => a.StudentGroup_Id == group.Id)
				//.Where(a => a.ValidUntil > System.DateTime.Now.AddDays(-1))
				.ToListAsync();			
			
			Assignments = new ObservableCollection<Assignment>(assignments);
		}
	}
}
