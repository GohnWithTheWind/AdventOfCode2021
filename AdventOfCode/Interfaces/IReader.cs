using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Interfaces
{
    public interface IReader
    {
        List<string> FileToStringList(string filePath);
        List<int> FileToIntList(string filePath);
        List<NavigationInput> FileToNavigationList(string filePath);
    }
}
