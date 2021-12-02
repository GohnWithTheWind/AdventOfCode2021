using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Submarine
    {
        private static Position StartPosition = new Position();
        private static Sonar StartSonar = new Sonar();

        private Position Position { get; set; }
        private Navigation Navigation { get; set; }

        public readonly Sonar Sonar;

        public Submarine()
        {
            Position = StartPosition;
            Sonar = StartSonar;
            Navigation = new Navigation();
        }

        public Position GetPosition()
        {
            return this.Position;
        }

        public void NavigateFromInput(List<NavigationInput> input, int navigationVersion)
        {
            Position = this.Navigation.NavigateFromInput(input, navigationVersion);
        }

    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Aim { get; set; }

        public int PositionHash { get { return X * Y; } }

        public override string ToString()
        {
            return string.Format("Position X: {0}. Position Y: {1}. Aim: {2}. PositionHash {3}", X.ToString(), Y.ToString(), Aim.ToString(), PositionHash.ToString());
        }
    }

    public class Sonar
    {
        public int CalculateDescentSpeed(List<int> sonarInfo, int calculationVersion)
        {
            int numOfIncreases = 0;
            int lastDepthValue = 0;
            if (calculationVersion == 1)
            {
                int currentRow = 1;
                foreach (int i in sonarInfo)
                {
                    if (currentRow != 1)
                    {
                        if (i - lastDepthValue >= 1)
                        {
                            numOfIncreases++;
                        }
                    }
                    lastDepthValue = i;
                    currentRow++;
                }
            }
            else if(calculationVersion == 2)
            {
                for (int i = 0; i < sonarInfo.Count; i++)
                {
                    if (i == 0)
                    {
                        lastDepthValue = sonarInfo[i] + sonarInfo[i + 1] + sonarInfo[i + 2];
                    }
                    else
                    {
                        if (i + 2 <= sonarInfo.Count - 1)
                        {
                            if (sonarInfo[i] + sonarInfo[i + 1] + sonarInfo[i + 2] - lastDepthValue >= 1)
                            {
                                numOfIncreases++;
                            }
                            lastDepthValue = sonarInfo[i] + sonarInfo[i + 1] + sonarInfo[i + 2];
                        }
                    }

                }
            }
            return numOfIncreases;
        }
    }

    public class Navigation
    {
        public Position NavigateFromInput(List<NavigationInput> input, int navigationVersion)
        {
            Position position = new Position();

            if (navigationVersion == 1)
            {
                position.Y = input.Select(t => t.Direction == "down" ? t.Amount : t.Direction == "up" ? t.Amount * -1 : 0).Sum();
                position.X = input.Where(t => t.Direction == "forward").Select(t => t.Amount).Sum();
            }
            else if (navigationVersion == 2)
            {
                int y = 0;
                int x = 0;
                int aim = 0;
                foreach (var i in input)
                {
                    switch (i.Direction)
                    {
                        case "down":
                            aim = aim + i.Amount;
                            break;
                        case "up":
                            aim = aim - i.Amount;
                            break;
                        case "forward":
                            x = x + i.Amount;
                            y = y + (aim * i.Amount);
                            break;
                        default:
                            break;
                    }
                }
                position.Y = y;
                position.X = x;
                position.Aim = aim;
            }
            return position;
        }
    }

    public interface ISubmarine
    {
    }

    public class NavigationInput
    {
        public int Amount { get; set; }
        public string Direction { get; set; }
    }
}
