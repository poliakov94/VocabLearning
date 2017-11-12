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
	public class StudentCreationPageViewModel : BaseViewModel
	{
		private Student _student;
		public Student Student
		{
			get { return _student; }
			set { SetProperty(ref _student, value); }
		}

		public ObservableCollection<StudentGroup> _groups = new ObservableCollection<StudentGroup>();
		public ObservableCollection<StudentGroup> Groups { get { return _groups; } set { _groups = value; } }
		
		public StudentCreationPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			_groups.Add(new StudentGroup
			{
				ID = 1,
				Name = "Grupa 1"
			});

			_groups.Add(new StudentGroup
			{
				ID = 2,
				Name = "Grupa 2"
			});

			_groups.Add(new StudentGroup
			{
				ID = 3,
				Name = "Grupa 3"
			});

			RaisePropertyChanged("Groups");
		}
	}
}
