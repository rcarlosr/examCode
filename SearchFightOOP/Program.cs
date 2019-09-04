using System;
using System.IO;

namespace SearchFightOOP
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + @"\setupSearchEngines.xml";
            SearchFightSolution cfg = SearchFightSolution.GetInstance;
            cfg = SearchFightSolution.Initialize(path);

            if (cfg != null)
                if (cfg.Validate())
                {
                    long[,] results = cfg.StartSearch(args);
                    Report.PrintGrid(args, cfg.ListOfSearchEngines, results);
                    Report.PrintWinnerPerEngine(args, cfg.ListOfSearchEngines, results);
                    Report.PrintTotal(args, results);
                    Report.PrintEndProgram();
                }
        }
    }
}
