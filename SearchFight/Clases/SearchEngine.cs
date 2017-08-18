using SearchFight.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Clases
{
    public class SearchEngine
    {
        protected ISearchEngine Engine;

        public int HighTotalMatches { get; set; }

        public string EngineName { get; set; }

        public string KeywordWinner { get; set; }
        public SearchEngine(ISearchEngine _engine)
        {
            EngineName = _engine.GetType().Name;
            KeywordWinner = "";
            HighTotalMatches = 0;
            Engine = _engine;
        }

        public int Search(string query)
        {
            var auxMatches = Engine.Search(query);
            if (auxMatches > HighTotalMatches)
            {
                KeywordWinner = query;
                HighTotalMatches = auxMatches;
            }
            return auxMatches;
        }
    }
}
