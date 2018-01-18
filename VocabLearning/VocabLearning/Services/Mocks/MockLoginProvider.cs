using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace VocabLearning.Services.Mocks
{
	class MockLoginProvider : ILoginProvider
	{
		public bool ClientCached()
		{
			throw new NotImplementedException();
		}

		public Task FindUser(AuthenticationResult authenticationResult, bool useSilent)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> LoginAsync(bool useSilent = false)
		{
			return true;
		}

		public Task<bool> LogoutAsync()
		{
			throw new NotImplementedException();
		}
	}
}
