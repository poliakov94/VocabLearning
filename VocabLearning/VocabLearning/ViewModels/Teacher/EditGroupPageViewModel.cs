using Prism.Commands;
using Prism.Navigation;
using VocabLearning.Models;

namespace VocabLearning.ViewModels.Teacher
{
	public class EditGroupPageViewModel : BaseViewModel
	{
		private StudentGroup group;
		public StudentGroup Group
		{
			get { return group; }
			set { SetProperty(ref group, value); }
		}

		public EditGroupPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
		}

		private DelegateCommand _saveGroup;
		public DelegateCommand SaveGroup =>
			_saveGroup ?? (_saveGroup = new DelegateCommand(ExecuteSaveGroupCommand));

		async void ExecuteSaveGroupCommand()
		{
			var groupsTable = await _azureService.GetTableAsync<StudentGroup>();
			await groupsTable.UpdateItemAsync(Group);
			await _azureService.SyncOfflineCacheAsync();

			await _navigationService.GoBackAsync();
		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Group = (StudentGroup)parameters["model"];
			}
		}
	}
}
