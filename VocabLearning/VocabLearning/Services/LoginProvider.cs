using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabLearning.Helpers;
using VocabLearning.Models;

namespace VocabLearning.Services
{
	class LoginProvider : ILoginProvider
	{
		public static PublicClientApplication ADB2CClient { get; private set; }
		public static MobileServiceUser User { get; private set; }

		public LoginProvider()
		{
			ADB2CClient = new PublicClientApplication(Locations.ClientID, Locations.Authority);
			ADB2CClient.RedirectUri = $"msal{Locations.ClientID}://auth";			
		}

		public async Task<bool?> LoginAsync(bool useSilent = false)
		{
			bool success = false;
			bool? isTeacher = null;
			try
			{
				AuthenticationResult authenticationResult;

				if (useSilent)
				{
					authenticationResult = await ADB2CClient.AcquireTokenSilentAsync(
						Locations.Scopes,
						GetUserByPolicy(ADB2CClient.Users, Locations.PolicySignUpSignIn),
						Locations.Authority,
						false);
				}
				else
				{
					authenticationResult = await ADB2CClient.AcquireTokenAsync(
						Locations.Scopes,
						GetUserByPolicy(ADB2CClient.Users, Locations.PolicySignUpSignIn),
						App.UiParent);
				}

				if (User == null)
				{
					var payload = new JObject();
					if (authenticationResult != null && !string.IsNullOrWhiteSpace(authenticationResult.IdToken))
					{
						payload["access_token"] = authenticationResult.IdToken;
					}

					User = await AzureService.DefaultService.CurrentClient.LoginAsync(
						MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory,
						payload);
					success = true;

					if (success)
						isTeacher = await IsTeacher(authenticationResult, useSilent);
				}

				
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return isTeacher;
		}

		public async Task<bool?> IsTeacher(AuthenticationResult authenticationResult, bool useSilent)
		{
			JObject azureUser = ParseIdToken(authenticationResult.IdToken);
			var azureId = azureUser["oid"]?.ToString();
			var _azureService = AzureService.DefaultService;

			await _azureService.SyncOfflineCacheAsync();
			var usersTable = await _azureService.GetTableAsync<User>();
			var user = await usersTable.ReadItemAsync(azureId);

			if (user == null && !useSilent)
			{
				User newUser = new User
				{
					FirstName = azureUser["given_name"]?.ToString(),
					LastName = azureUser["family_name"]?.ToString(),
					Email = azureUser["emails"]?.ToArray()[0].ToString(),
					Id = azureUser["oid"]?.ToString(),
					IsTeacher = false
				};
				user = await usersTable.CreateItemAsync(newUser);
				await _azureService.SyncOfflineCacheAsync();
			}

			return user?.IsTeacher;
		}

		public async Task<bool> LogoutAsync()
		{
			bool success = false;
			try
			{
				if (User != null)
				{
					await AzureService.DefaultService.CurrentClient.LogoutAsync();

					foreach (var user in ADB2CClient.Users)
					{
						ADB2CClient.Remove(user);
					}
					User = null;
					success = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return success;
		}

		private IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
		{
			foreach (var user in users)
			{
				string userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
				if (userIdentifier.EndsWith(policy.ToLower())) return user;
			}

			return null;
		}

		string Base64UrlDecode(string str)
		{
			str = str.Replace('-', '+').Replace('_', '/');
			str = str.PadRight(str.Length + (4 - str.Length % 4) % 4, '=');
			var byteArray = Convert.FromBase64String(str);
			var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
			return decoded;
		}

		JObject ParseIdToken(string idToken)
		{
			idToken = idToken.Split('.')[1];
			idToken = Base64UrlDecode(idToken);
			return JObject.Parse(idToken);
		}
	}
}
