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
	public class StudentsSearchPageViewModel : BaseViewModel
	{
		private User _studentSelected;
		public User StudentSelected
		{
			get { return _studentSelected; }
			set
			{
				if (_studentSelected != value)
					_studentSelected = value;
				RaisePropertyChanged("ItemSelected");
			}
		}
		public ObservableCollection<User> _students = new ObservableCollection<User>();
		public ObservableCollection<User> Students { get { return _students; } set { _students = value; } }

		public StudentsSearchPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			//RaisePropertyChanged("Students");
		}
	}
}
