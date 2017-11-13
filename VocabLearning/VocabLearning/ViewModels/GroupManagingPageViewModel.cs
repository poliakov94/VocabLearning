using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class GroupManagingPageViewModel : BaseViewModel
	{
		private StudentGroup Group;
        public GroupManagingPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

        }

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("id"))
				Group.Id = (string)parameters["id"];
		}
	}
}
