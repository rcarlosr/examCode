using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchFightOOP
{
    public static class Report
    {
        public static void PrintGrid(string[] args, List<SearchEngine> ListOfSearchEngines, long[,] result)
        {
            Console.WriteLine("");
            for (int j = 0; j < args.Length; j++)
            {
                Console.Write(args[j] + " :\t");
                for (int i = 0; i < ListOfSearchEngines.Count; i++)
                    Console.Write("\t" + ListOfSearchEngines[i].Name + " : " + result[j, i] + "\t");
                Console.WriteLine("");
            }
        }

        public static void PrintWinnerPerEngine(string[] args, List<SearchEngine> ListOfSearchEngines, long[,] result)
        {
            Console.WriteLine("");
            for (int i = 0; i < ListOfSearchEngines.Count; i++)
            {
                    var colMax = GetColumn(result, i).Max();
                    var indexColMax = GetColumn(result, i).ToList().IndexOf(colMax);
                    Console.WriteLine(ListOfSearchEngines[i].Name + " winner:\t" + args[indexColMax]);
            }
        }

        public static void PrintTotal(string[] args, long[,] result)
        {
            long highestRowSum = 0, indHighRowSum = 0;
            for (int i = 0; i < args.Length; i++)
            {
                    var rowSum = GetRow(result, i).Sum();
                    if (rowSum > highestRowSum)
                    {
                        highestRowSum = rowSum;
                        indHighRowSum = i;
                    }
            }
            Console.WriteLine("Total winner:\t" + args[indHighRowSum]);
        }

        public static void PrintEndProgram()
        {
            Console.ReadLine();
        }

        public static IEnumerable<T> GetRow<T>(T[,] array, int row)
        {
            for (int i = 0; i <= array.GetUpperBound(1); ++i)
                yield return array[row, i];
        }

        public static IEnumerable<T> GetColumn<T>(T[,] array, int column)
        {
            for (int i = 0; i <= array.GetUpperBound(0); ++i)
                yield return array[i, column];
        }
    }
}
