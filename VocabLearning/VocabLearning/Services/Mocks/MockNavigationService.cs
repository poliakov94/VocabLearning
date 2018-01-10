using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace VocabLearning.Services.Mocks
{
	public class MockNavigationService : INavigationService
	{
		public async Task<bool> GoBackAsync(NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
		{
			return false;
		}

		public Task NavigateAsync(Uri uri, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
		{
			throw new NotImplementedException();
		}

		public Task NavigateAsync(string name, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
		{
			throw new NotImplementedException();
		}
	}
}
