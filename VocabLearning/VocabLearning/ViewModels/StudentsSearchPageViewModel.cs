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
		private Student _studentSelected;
		public Student StudentSelected
		{
			get { return _studentSelected; }
			set
			{
				if (_studentSelected != value)
					_studentSelected = value;
				RaisePropertyChanged("ItemSelected");
			}
		}
		public ObservableCollection<Student> _students = new ObservableCollection<Student>();
		public ObservableCollection<Student> Students { get { return _students; } set { _students = value; } }

		public StudentsSearchPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			_students.Add(new Student
			{
				ID = 1,
				FirstName = "Michał",
				LastName = "Topolski",
				Email = "palkazbagien@gmail.com"
			});

			_students.Add(new Student
			{
				ID = 1,
				FirstName = "Adam",
				LastName = "Duluk",
				Email = "adamduluk@gmail.com"
			});

			RaisePropertyChanged("Students");
		}
	}
}
