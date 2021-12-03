using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Navigation
    {
        public Position NavigateFromInput(List<NavigationInput> input, int navigationVersion)
        {
            Position position = new Position();

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

        private Position NavigationVersionOne(List<NavigationInput> input)
        {
            Position position = new Position();
            position.Y = input.Select(t => t.Direction == "down" ? t.Amount : t.Direction == "up" ? t.Amount * -1 : 0).Sum();
            position.X = input.Where(t => t.Direction == "forward").Select(t => t.Amount).Sum();
            return position;
        }

        private Position NavigationVersionTwo(List<NavigationInput> input)
        {
            Position position = new Position();
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
            return position;
        }
    }

    public class NavigationInput
    {
        public int Amount { get; set; }
        public string Direction { get; set; }
    }
}
