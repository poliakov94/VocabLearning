using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VocabLearning.Helpers
{
    public class DictLookup
    {
		public const String ApiKey = "<not_set>"; //add your API key here - otherwise sandbox is used

		public async static Task<LookupResult> Get(String term)
		{
			try
			{
				String requestBody = "https://api.pearson.com/v2/dictionaries/ldoce5/entries?headword.exact=<term>&offset=0";

				String requestUri = requestBody.Replace("<term>", term);
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
				HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());

				StreamReader reader = new StreamReader(response.GetResponseStream());
				String responseJson = reader.ReadToEnd();
				reader.Dispose();

				return LookupResult.FromJson(responseJson);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
				return null;
			}			
		}
	}
}
