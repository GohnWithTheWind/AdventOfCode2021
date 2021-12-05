using AdventOfCode.Interfaces;
using AdventOfCode.SubmarineAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Infrastructure
{
    public class FileReader: IReader
    {
        public List<string> FileToStringList(string filePath)
        {
            List<string> result = new List<string>();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                result.Add(line);
            }
            return result;
        }

        public List<int> FileToIntList(string filePath)
        {
            List<int> result = new List<int>();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                result.Add(int.Parse(line));
            }
            return result;
        }

        public List<NavigationInput> FileToNavigationList(string filePath)
        {

            var List = System.IO.File.ReadLines(filePath).Select(line => new NavigationInput{ Direction = line.Substring(0, line.IndexOf(" ")), Amount = int.Parse(line.Substring(line.IndexOf(" ") + 1)) }).ToList();


            return List;
        }

        public List<int> FirstRowToIntList(string filePath)
        {
            List<int> result = new List<int>();

            var file = System.IO.File.ReadLines(filePath);
            int pos = 0;
            foreach (var l in file)
            {
                if (pos == 0)
                {
                    result = l.Split(',').Select(Int32.Parse).ToList();
                    break;
                }
            }
            return result;
        }

        public List<BingoBoard> FileToBingoBoards(string filePath)
        {
            var file = System.IO.File.ReadLines(filePath);

            int fileLen = file.Count();

            List<BingoBoard> boards = new List<BingoBoard>();

            int boardNo = 1;
            BingoBoard board = new BingoBoard("Board " + boardNo.ToString());
            int pos = 0;
            int matrixRow = 0;

            foreach (var l in file)
            {
                if (pos == 0)
                {
                }
                else if (l == "")
                {
                    matrixRow = 0;
                    if (pos > 1)
                    {
                        boards.Add(board);
                        board = new BingoBoard("Board " + boardNo.ToString());
                        boardNo++;
                    }
                }
                else
                {
                    var split = l.Split(new char[0]);
                    var rowNumbers = l.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();

                    for (int i = 0; i < rowNumbers.Count; i++)
                    {
                        board.Matrix[matrixRow, i] = new BingoNumber(rowNumbers[i], false, matrixRow, i);
                    }
                    matrixRow++;
                }
                if (pos == fileLen - 1)
                {
                    boards.Add(board);
                }
                pos++;
            }
            return boards;
        }
    }
}
