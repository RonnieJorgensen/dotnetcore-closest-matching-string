using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnetcore_closest_matching_string.Helpers
{
    public static class StringHelpers
    {
        public static string[] GetAllArrayCombinations_Recursive(string[] str)
        {
            if (str.Length <= 1)
                return str;

            string c = str[str.Length - 1];

            string[] returnArray = GetAllArrayCombinations_Recursive(str.Take(str.Length - 1).ToArray());
            List<string> finalArray = new List<string>();
            foreach (string s in returnArray)
                finalArray.Add(s);
            finalArray.Add(c.ToString());

            foreach (string s in returnArray)
            {
                finalArray.Add(s + " " + c);
            }

            Array.Sort(finalArray.ToArray());
            return finalArray.Distinct().ToArray();
        }
    }
}
