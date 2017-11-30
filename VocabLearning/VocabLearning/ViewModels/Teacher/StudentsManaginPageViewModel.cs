using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class StudentsManagingPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

		public StudentGroup Group;
		public bool IsEmpty
		{
			get { return (Students == null || Students.Count == 0); }			
		}

		public ObservableCollection<User> _Students = new ObservableCollection<User>();
		public ObservableCollection<User> Students { get { return _Students; } set { _Students = value; RaisePropertyChanged("Students"); } }
		
		public StudentsManagingPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
        {
			_pageDialogService = pageDialogService;
		}
				
		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Group = (StudentGroup)parameters["model"];
				if (Group.Students != null)
					Students = new ObservableCollection<User>(Group.Students);
			}

			RaisePropertyChanged("IsEmpty");
		}
	}
}
