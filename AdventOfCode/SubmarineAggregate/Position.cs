using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
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
}
