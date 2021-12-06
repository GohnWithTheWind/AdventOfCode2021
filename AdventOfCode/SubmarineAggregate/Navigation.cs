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
}
