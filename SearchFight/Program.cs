using SearchFight.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {

            var query = Console.ReadLine();

            Console.WriteLine();

            string[] queries = GetKeyWords(query);

            List<Search> results = new List<Search>();

            List<SearchEngine> searchEngines = new List<SearchEngine>
            {
                new SearchEngine(new Google()),
                new SearchEngine(new Bing())
            };

            for (int i = 0; i < queries.Count(); i++)
            {
                Search SearchResult = new Search();

                string output = queries[i] + ": ";
                SearchResult.keyword = queries[i];

                foreach (var searchEngine in searchEngines)
                {
                    output += searchEngine.EngineName + ": " + searchEngine.Search(queries[i]) + " ";
                }

                Console.WriteLine(output);

                SearchResult.highresult = searchEngines.OrderByDescending(x => x.HighTotalMatches).Select(p => p.HighTotalMatches).FirstOrDefault();
                results.Add(SearchResult);
            }
            

            CalculateWinners(searchEngines, results);

            Console.ReadKey();
            Console.ReadKey();
        }

        static string[] GetKeyWords(string query)
        {
            List<int> indices = new List<int>();
            List<string> keywords = new List<string>();

            //Get all indexes of "
            for (int i = 0; i < query.Length; i++)
            {
                if (query[i] == '\"')
                    indices.Add(i);
            }

            //if the list has elements and the index of " is not the first position of the string
            if (indices.Count > 0)
            {
                if (indices[0] > 0)
                    keywords.AddRange(query.Substring(0, indices[0] - 1).Split(' ').ToList());
            }
            else
                keywords.AddRange(query.Split(' ').ToList());
            //Loop trough the array and start obtaining all the keyword for the search.
            for (int i = 0; i < indices.Count; i += 2)
            {
                int startindex = indices[i] + 1;
                int nextindex = indices[i + 1];
                int endindex = nextindex - startindex;


                keywords.Add(query.Substring(startindex, endindex));

                string newquery = "";

                if (indices.Count > (i + 2))
                {
                    newquery = query.Substring(nextindex + 2, (indices[i + 2] - nextindex) - 3);

                    keywords.AddRange(newquery.Split(' ').ToList());
                }
                else
                {
                    if (nextindex + 2 < query.Length)
                    {
                        newquery = query.Substring(nextindex + 2);

                        keywords.AddRange(newquery.Split(' ').ToList());
                    }
                }

            }

            return keywords.ToArray();
        }

        static void CalculateWinners(List<SearchEngine> searchEngines, List<Search> results)
        {
            Console.WriteLine();
            foreach (var searchEngine in searchEngines)
            {
                Console.WriteLine(searchEngine.EngineName + " winner: " + searchEngine.KeywordWinner);
            }
            results = results.OrderByDescending(x => x.highresult).ToList();
            Console.WriteLine();
            Console.WriteLine("Total winner: " + results.First().keyword);

        }

    }
}
