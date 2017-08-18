using Newtonsoft.Json.Linq;
using SearchFight.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SearchFight.Clases
{
    public class Google : ISearchEngine
    {

        public int Search(string query)
        {
            try
            {


                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=AIzaSyAYKBLINxMWiEeXSjNeyFxzGjYKBfLwpnE&cx=006469441686315824696:jlvbuea85y8&q=" + query);
                webrequest.Method = "GET";
                webrequest.ContentType = "application/json";
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                string result = string.Empty;
                result = responseStream.ReadToEnd();
                webresponse.Close();

                if (webresponse.StatusCode == HttpStatusCode.OK)
                {
                    dynamic obj = JObject.Parse(result);

                    return Convert.ToInt32(obj["queries"]["request"][0]["totalResults"]);

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
