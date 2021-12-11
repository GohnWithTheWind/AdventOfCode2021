using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Navigation
    {
        public long GetIncompleteLineScore(List<string> input)
        {
            List<long> scores = new();
            foreach (string l in input)
            {
                if(GetCorruptionChar(l).CharValue == 0)
                {
                    scores.Add(GetLineScore(l));
                }
            }
            var scoresSorted = scores.OrderBy(s => s).ToList();
            return scoresSorted[scores.Count / 2];
        }

        private long GetLineScore(string line)
        {
            List<Tuple<string, string>> delimiters = new();
            delimiters.Add(new("(", ")"));
            delimiters.Add(new("[", "]"));
            delimiters.Add(new("{", "}"));
            delimiters.Add(new("<", ">"));

            long score = 0;
            List<ChunkCharInfo> charInfo = new();
            for (int i = 0; i < line.Length; i++)
            {
                charInfo.Add(new(i, line.Substring(i, 1)));
            }
            foreach (var stopChar in charInfo.Where(c => c.CharType == "Stop"))
            {
                var startChar = charInfo.Where(c => c.CharType == "Start" && c.IsUsed == false && c.Position < stopChar.Position).OrderByDescending(o => o.Position).FirstOrDefault();
                if (startChar.Character == delimiters.Where(c => c.Item2 == stopChar.Character).Select(s => s.Item1).FirstOrDefault())
                {
                    startChar.IsUsed = true;
                    stopChar.IsUsed = true;
                }
            }
            var resList = charInfo.Where(s => s.IsUsed == false && s.CharType == "Start").ToList();
            for (int i = resList.Count - 1; i >= 0; i--)
            {
                score = (score * 5) + resList[i].CharValue;
            }
            return score;
        }
      
        public int GetIllegalSyntaxScore(List<string> input)
        {
            List<ChunkCharInfo> result = new();
            foreach(string l in input)
            {
                result.Add(GetCorruptionChar(l));
            }
            return result.Select(s => s.CharValue).Sum();
        }
        private ChunkCharInfo GetCorruptionChar(string line)
        {
            List<Tuple<string,string>> delimiters = new();
            delimiters.Add(new("(", ")"));
            delimiters.Add(new("[", "]"));
            delimiters.Add(new("{", "}"));
            delimiters.Add(new("<", ">"));

            List<ChunkCharInfo> charInfo = new();
            for (int i = 0; i < line.Length; i++)
            {
                charInfo.Add(new(i,line.Substring(i, 1)));
            }
            foreach(var stopChar in charInfo.Where(c => c.CharType == "Stop"))
            {
                var startChar = charInfo.Where(c => c.CharType == "Start" && c.IsUsed == false && 
                c.Position < stopChar.Position).OrderByDescending(o => o.Position).FirstOrDefault();
                if(startChar.Character != delimiters.Where(c => c.Item2 == stopChar.Character).Select(s => s.Item1).FirstOrDefault())
                {
                    return stopChar;
                }
                else
                {
                    startChar.IsUsed = true;
                }
            }
            return new ChunkCharInfo(0, "");
        }

        public Position NavigateFromInput(List<NavigationInput> input, int navigationVersion)
        {
            Position position = new();

            if (navigationVersion == 1)
            {
                position = NavigationVersionOne(input);
            }
            else if (navigationVersion == 2)
            {
                position = NavigationVersionTwo(input);
            }
            return position;
        }

        private static Position NavigationVersionOne(List<NavigationInput> input)
        {
            Position position = new();
            position.Y = input.Select(t => t.Direction == "down" ? t.Amount : t.Direction == "up" ? t.Amount * -1 : 0).Sum();
            position.X = input.Where(t => t.Direction == "forward").Select(t => t.Amount).Sum();
            return position;
        }

        private Position NavigationVersionTwo(List<NavigationInput> input)
        {
            Position position = new();
            int y = 0;
            int x = 0;
            int aim = 0;
            foreach (var i in input)
            {
                switch (i.Direction)
                {
                    case "down":
                        aim += i.Amount;
                        break;
                    case "up":
                        aim -= i.Amount;
                        break;
                    case "forward":
                        x += i.Amount;
                        y += (aim * i.Amount);
                        break;
                    default:
                        break;
                }
            }
            position.Y = y;
            position.X = x;
            position.Aim = aim;
            return position;
        }
    }

    public class NavigationInput
    {
        public NavigationInput(string direction, int amount)
        {
            Amount = amount;
            Direction = direction;
        }
        public int Amount { get; set; }
        public string Direction { get; set; }
    }

    public class DelimiterInfo
    {
        public string StartChar { get; set; }
        public string StopChar { get; set; }
        public DelimiterInfo(string startChar, string stopChar)
        {
            StartChar = startChar;
            StopChar = stopChar;
        }
    }

    public class ChunkCharInfo
    {
        public int Position { get; set; }
        public string Character { get; set; }
        public int Sequence { get; set; }
        public bool IsUsed { get; set; }
        public string CharType { get
            {
                return Character == "<" || Character == "(" || Character == "{" || Character == "[" ? "Start" : "Stop";
            } 
        }
        public int CharValue
        {
            get
            {
                return Character == ")" ? 3 : Character == "]" ? 57 : Character == "}" ? 1197 : Character == ">" ? 25137 :
                    Character == "(" ? 1 : Character == "[" ? 2 : Character == "{" ? 3 : Character == "<" ? 4 : 0;
            }
        }
        public ChunkCharInfo(int position, string character)
        {
            Position = position;
            Character = character;
            IsUsed = false;
        }
    }

}
