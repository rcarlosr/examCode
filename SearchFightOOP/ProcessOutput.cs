using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchFightOOP
{
    class ProcessOutput
    {
        public static long ExtractOutput(string Results, int Multiplier)
        {
            char[] digits;
            long tmp=0;

            if (Results != "" && Results != null)
            {
                Results = Results.Replace(",", "");
                Results = Results.Replace(".", "");
                digits = Results.SkipWhile(c => !Char.IsDigit(c))
                        .TakeWhile(Char.IsDigit)
                        .ToArray();
                Results = new string(digits);

                if (Int64.TryParse(Results, out tmp))
                {
                    tmp = tmp * Multiplier;
                }
                else
                {
                    return -1;
                }
            }
            return tmp;
        }
    }
}
