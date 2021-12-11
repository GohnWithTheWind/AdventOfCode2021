using AdventOfCode.SubmarineAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Diagnostics;
using static AdventOfCode.SubmarineAggregate.Oceanography;

namespace AdventOfCode.Interfaces
{
    public interface IReader
    {
        List<string> FileToStringList(string filePath);
        List<int> FileToIntList(string filePath);
        List<NavigationInput> FileToNavigationList(string filePath);
        List<int> FirstRowToIntList(string filePath);
        List<BingoBoard> FileToBingoBoards(string filePath);
        List<VentRange> FileToVentRanges(string filePath, int includeDiagonal = 0);
        List<int> FileToIntListSplit(string filePath, char splitChar);
        List<Display> FileToDisplays(string filePath);
        HeightMap FileToHeightMap(string filePath);
        OctopusGroup FileToOctopusGroup(string filePath);
    }
}
