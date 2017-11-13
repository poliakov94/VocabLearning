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
				_navigationService.NavigateAsync("GroupManagingPage?id=" + _groupSelected.Id, null, false);
			}
		}
		public ObservableCollection<StudentGroup> _groups = new ObservableCollection<StudentGroup>();
		public ObservableCollection<StudentGroup> Groups { get { return _groups; } set { _groups = value; } }
		public TeacherStudentsPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			//RaisePropertyChanged("Groups");
		}
	}
}
