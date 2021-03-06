﻿using Prism.Commands;
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
		private User _student;
		public User Student
		{
			get { return _student; }
			set { SetProperty(ref _student, value); }
		}

		public ObservableCollection<StudentGroup> _groups = new ObservableCollection<StudentGroup>();
		public ObservableCollection<StudentGroup> Groups { get { return _groups; } set { _groups = value; } }
		
		public StudentCreationPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			//RaisePropertyChanged("Groups");
		}
	}
}
