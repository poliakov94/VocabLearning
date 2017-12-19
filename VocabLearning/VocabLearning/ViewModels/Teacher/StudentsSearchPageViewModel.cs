using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class StudentsSearchPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

		private StudentGroup group;
		public StudentGroup Group
		{
			get { return group; }
			set { SetProperty(ref group, value); }
		}

		private User _studentSelected;
		public User StudentSelected
		{
			get { return _studentSelected; }
			set
			{
				if (_studentSelected != value)
					_studentSelected = value;
			}
		}
		public ObservableCollection<User> _students = new ObservableCollection<User>();
		public ObservableCollection<User> Students { get { return _students; } set { _students = value; RaisePropertyChanged("Students"); } }

		public StudentsSearchPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
		}

		private DelegateCommand _addStudent;
		public DelegateCommand AddStudent =>
			_addStudent ?? (_addStudent = new DelegateCommand(ExecuteAddStudent));

		async void ExecuteAddStudent()
		{
			if (StudentSelected == null)
			{
				await _pageDialogService.DisplayAlertAsync("Error", "Select a student first.", "Ok");
				return;
			}

			StudentSelected.StudentGroup_Id = Group.Id;
			try
			{
				var studentsTable = await _azureService.GetTableAsync<User>();
				var student = await studentsTable.UpdateItemAsync(StudentSelected);

				await _azureService.SyncOfflineCacheAsync();

				var navigationParams = new NavigationParameters
				{
					{ "model", Group }
				};
				await _navigationService.GoBackAsync(navigationParams);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		public async override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Group = (StudentGroup)parameters["model"];
			}

			var studentsTable = (await _azureService.GetTableAsync<User>()).ReturnTable();
			var students = await studentsTable.Where(s => s.IsTeacher == false && s.StudentGroup_Id == null).ToListAsync();
			Students = new ObservableCollection<User>(students);
		}
	}
}
