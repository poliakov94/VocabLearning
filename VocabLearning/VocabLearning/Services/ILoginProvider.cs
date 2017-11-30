using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;


namespace VocabLearning.Services
{
	public interface ILoginProvider
	{
		Task LoginAsync(MobileServiceClient client);
		Task<bool> LoginAsync(bool useSilent = false);
		Task<bool> LogoutAsync();
	}
}
