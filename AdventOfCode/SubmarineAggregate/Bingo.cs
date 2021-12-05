using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.SubmarineAggregate
{
    public class Bingo
    {
        public List<int> DrawnNumbers { get; set; }
        public List<BingoBoard> Boards { get; set; }

        public Bingo(List<int> drawnNumbers, List<BingoBoard> boards)
        {
            Boards = boards;
            DrawnNumbers = drawnNumbers;
        }


        public BingoBoard GetWinner()
        {
            foreach(var drawnNum in DrawnNumbers)
            {
                foreach(var board in Boards)
                {
                    foreach(var num in board.Matrix)
                    {
                        if(num.Number == drawnNum)
                        {
                            num.IsChecked = true;
                        }
                    }
                }
                var winningBoard = CheckForWinner();
                {
                    if (winningBoard != null)
                    {
                        winningBoard.Score = (from BingoNumber num in winningBoard.Matrix where num.IsChecked == false select num.Number).Sum() * drawnNum;
                        return winningBoard;
                    }
                }
            }
            return null;
        }

        public BingoBoard GetLoser()
        {
            foreach (var drawnNum in DrawnNumbers)
            {
                foreach (var board in Boards)
                {
                    foreach (var num in board.Matrix)
                    {
                        if (num.Number == drawnNum)
                        {
                            num.IsChecked = true;
                        }
                    }
                }
                Boards = MarkWinners(Boards, drawnNum);
                {
                    if (Boards.Where(b => b.IsWinner == false).Count() == 0)
                    {                       
                        return Boards.OrderBy(b => b.WinSequence).Last();
                    }
                }
            }
            return null;
        }

        public BingoBoard CheckForWinner()
        {
            foreach(var board in Boards)
            {
                for (int i = 0; i < 5; i++)
                {
                    var xCheckCount = (from BingoNumber num in board.Matrix where num.PosX == i && num.IsChecked == true select num).Count();
                    var yCheckCount = (from BingoNumber num in board.Matrix where num.PosY == i && num.IsChecked == true select num).Count();

                    if(xCheckCount == 5 || yCheckCount == 5)
                    {
                        board.IsWinner = true;
                        return board;
                    }
                }
            }
            return null;
        }

        public List<BingoBoard> MarkWinners(List<BingoBoard> boards, int drawnNum)
        {
            foreach (var board in Boards.Where(x => x.IsWinner == false))
            {
                for (int i = 0; i < 5; i++)
                {
                    var xCheckCount = (from BingoNumber num in board.Matrix where num.PosX == i && num.IsChecked == true select num).Count();
                    var yCheckCount = (from BingoNumber num in board.Matrix where num.PosY == i && num.IsChecked == true select num).Count();

                    if (xCheckCount == 5 || yCheckCount == 5)
                    {
                        board.IsWinner = true;
                        board.WinSequence = (from BingoBoard b in boards where b.IsWinner == true select b.WinSequence).Max() + 1;                       
                        board.Score = (from BingoNumber num in board.Matrix where num.IsChecked == false select num.Number).Sum() * drawnNum;
                        
                    }
                }
            }
            return boards;
        }
    }

    public class BingoBoard
    {
        public string BoardName { get; set; }
        public BingoNumber[,] Matrix { get; set; }
        public bool IsWinner { get; set;}
        public int Score { get; set; }
        public int WinSequence { get; set; }

        public BingoBoard(string name)
        {
            BoardName = name;
            Matrix = new BingoNumber[5,5];
            IsWinner = false;
            Score = 0;
        }

        public override string ToString()
        {
            return string.Format("Board Name: {0}. Score: {1}.", BoardName, Score.ToString());
        }
    }

    public class BingoNumber
    {
        public int Number { get; set; }
        public bool IsChecked { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public BingoNumber (int number, bool isChecked, int posX, int posY)
        {
            Number = number;
            IsChecked = isChecked;
            PosX = posX;
            PosY = posY;
        }
    }
}
