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
            List<string> result = new();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                result.Add(line);
            }
            return result;
        }

        public List<int> FileToIntList(string filePath)
        {
            List<int> result = new();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                result.Add(int.Parse(line));
            }
            return result;
        }

        public List<NavigationInput> FileToNavigationList(string filePath)
        {

            var List = System.IO.File.ReadLines(filePath).Select(line => new NavigationInput(line[..line.IndexOf(" ")],  int.Parse(line[(line.IndexOf(" ") + 1)..]))).ToList();


            return List;
        }

        public List<int> FirstRowToIntList(string filePath)
        {
            List<int> result = new();

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

            List<BingoBoard> boards = new();

            int boardNo = 1;
            BingoBoard board = new("Board " + boardNo.ToString());
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

        public List<VentRange> FileToVentRanges(string filePath, int includeDiagonal = 0)
        {
            var file = System.IO.File.ReadLines(filePath);
            List<VentRange> ventRanges = new();
           
            int startX;
            int startY;
            int endX;
            int endY;
            foreach (var l in file)
            {
                var ranges = l.Split("->", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(',').Select(Int32.Parse).ToList());

                startX = ranges.ToList()[0][0];
                startY = ranges.ToList()[0][1];
                endX = ranges.ToList()[1][0];
                endY = ranges.ToList()[1][1];

                if(startX == endX || startY == endY)
                {
                    if (startX < endX || startY < endY)
                    {
                        ventRanges.Add(new VentRange(startX, startY, endX, endY));
                    }
                    else
                    {
                        ventRanges.Add(new VentRange(endX, endY, startX, startY));
                    }
                }
                else if(includeDiagonal == 1)
                {
                    if (Math.Abs(startX - endX) == Math.Abs(startY - endY))
                    {
                        if (startX < endX)
                        {
                            ventRanges.Add(new VentRange(startX, startY, endX, endY));
                        }
                        else
                        {
                            ventRanges.Add(new VentRange(endX, endY, startX, startY));
                        }
                    }
                }

            }
            return ventRanges;
        }
        public List<int> FileToIntListSplit(string filePath, char splitChar)
        {
            List<int> result = new();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                result.AddRange(line.Split(',').Select(Int32.Parse));
            }
            return result;
        }
    }
}
