using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class FileReader
    {
        public static List<string> FileToStringList(string filePath)
        {
            List<string> result = new List<string>();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                result.Add(line);
            }
            return result;
        }

        public static List<int> FileToIntList(string filePath)
        {
            List<int> result = new List<int>();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                result.Add(int.Parse(line));
            }
            return result;
        }

        public static List<NavigationInput> FileToNavigationList(string filePath)
        {

            var List = System.IO.File.ReadLines(filePath).Select(line => new NavigationInput{ Direction = line.Substring(0, line.IndexOf(" ")), Amount = int.Parse(line.Substring(line.IndexOf(" ") + 1)) }).ToList();


            return List;
        }
    }
}
