using Newtonsoft.Json.Linq;
using SearchFight.Interfaces;
using System;
using System.Net.Http;
using System.Web;

namespace SearchFight.Clases
{
    public class Bing : ISearchEngine
    {
        public int Search(string query)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "2a95fb83498e4bffbd0e7dd4aaa06f4a");
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["q"] = query;
                var url = "https://api.cognitive.microsoft.com/bing/v5.0/search?" + queryString;

                HttpResponseMessage httpResponseMessage = client.GetAsync(query).Result;

                var responseContentString = httpResponseMessage.Content.ReadAsStringAsync().Result;
                JObject responseObjects = JObject.Parse(responseContentString);


                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return Convert.ToInt32(responseObjects["webPages"]["totalEstimatedMatches"]);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }


    }
}
