using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.Identity.Client;

namespace VocabLearning.Services
{
	public interface ILoginProvider
	{
		bool ClientCached();
		Task<bool> LoginAsync(bool useSilent = false);
		Task FindUser(AuthenticationResult authenticationResult, bool useSilent);
		Task<bool> LogoutAsync();
	}
}
