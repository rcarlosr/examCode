using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFightOOP
{
    public sealed class SearchFightSolution
    {
        public List<SearchEngine> ListOfSearchEngines = new List<SearchEngine>();

        private SearchFightSolution()
        { }

        private static SearchFightSolution Instance = null;

        public static SearchFightSolution GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new SearchFightSolution();
                }
                return Instance;
            }
        }

        public static SearchFightSolution Initialize(string path)
        {
            Serializer ser = new Serializer();
            string xmlInputData = string.Empty;
            try
            {
                xmlInputData = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ser.Deserialize<SearchFightSolution>(xmlInputData);
        }

        public bool Validate()
        {
            bool validEngines = true;
            foreach(var sEng in ListOfSearchEngines)
                if (!RequestString.URIExists(sEng.reqString.URL))
                {
                    validEngines = false;
                }
            return validEngines;
        }

        public long[,] StartSearch(string[] args)
        {
            long[,] result = new long[args.Length, ListOfSearchEngines.Count];
            
            for (int j = 0; j < args.Length; j++)
                for (int i = 0; i < ListOfSearchEngines.Count; i++)
                {
                    result[j, i] = ListOfSearchEngines[i].RetrieveResults(args[j]);
                }
            return result;
        }
    }
}
