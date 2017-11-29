using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class GroupManagingPageViewModel : BaseViewModel
	{
		public StudentGroup _group;
		public StudentGroup Group { get { return _group; } set { _group = value; RaisePropertyChanged("Group"); } }
		public GroupManagingPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

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
