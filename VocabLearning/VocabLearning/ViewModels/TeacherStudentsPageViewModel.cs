using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;
using VocabLearning.Views;

namespace VocabLearning.ViewModels
{
	public class TeacherStudentsPageViewModel : BaseViewModel
	{
		private StudentGroup _groupSelected;
		public StudentGroup GroupSelected
		{
			get { return _groupSelected; }
			set
			{
				if (_groupSelected != value)
					_groupSelected = value;
				RaisePropertyChanged("ItemSelected");
				_navigationService.NavigateAsync("GroupManagingPage?id=" + _groupSelected.ID, null, false);
			}
		}
		public ObservableCollection<StudentGroup> _groups = new ObservableCollection<StudentGroup>();
		public ObservableCollection<StudentGroup> Groups { get { return _groups; } set { _groups = value; } }
		public TeacherStudentsPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			_groups.Add(new StudentGroup
			{
				ID = 1,
				Name = "Grupa 1",
				GroupSize = 10
			});

			_groups.Add(new StudentGroup
			{
				ID = 2,
				Name = "Grupa 2",
				GroupSize = 1
			});

			_groups.Add(new StudentGroup
			{
				ID = 3,
				Name = "Grupa 3",
				GroupSize = 5
			});

			RaisePropertyChanged("Groups");
		}
	}
}
