﻿using AdventOfCode.SubmarineAggregate;
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
        List<int> FirstRowToIntList(string filePath);
        List<BingoBoard> FileToBingoBoards(string filePath);
      //  int[,] FileMatrixToMatrixArray(string filePath);
    }
}
