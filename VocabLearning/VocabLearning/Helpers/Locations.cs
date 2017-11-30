using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabLearning.Helpers
{
	public static class Locations
	{
		// Azure Mobile App
		public static readonly string AzureMobileAppURL = @"https://vocablearning.azurewebsites.net/";

		public static string Tenant = "vocablearning.onmicrosoft.com";
		public static string ClientID = "7237b039-b1f6-4cd0-bf4a-d9f7235ce053";
		public static string PolicySignUpSignIn = "b2c_1_emailPolicy";
		public static string PolicyEditProfile = "b2c_1_edit_profile";
		public static string PolicyResetPassword = "b2c_1_reset";

		public static string[] Scopes = { "https://fabrikamb2c.onmicrosoft.com/demoapi/demo.read" };
		public static string ApiEndpoint = "https://fabrikamb2chello.azurewebsites.net/hello";

		public static string AuthorityBase = $"https://login.microsoftonline.com/tfp/{Tenant}/";
		public static string Authority = $"{AuthorityBase}{PolicySignUpSignIn}";
		public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
		public static string AuthorityPasswordReset = $"{AuthorityBase}{PolicyResetPassword}";
	}
}
