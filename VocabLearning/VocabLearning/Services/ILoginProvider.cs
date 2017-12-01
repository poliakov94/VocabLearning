using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.Identity.Client;

namespace VocabLearning.Services
{
	public interface ILoginProvider
	{		
		Task<bool?> LoginAsync(bool useSilent = false);
		Task<bool?> IsTeacher(AuthenticationResult authenticationResult, bool useSilent);
		Task<bool> LogoutAsync();
	}
}
