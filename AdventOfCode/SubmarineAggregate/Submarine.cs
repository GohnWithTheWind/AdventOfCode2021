using AdventOfCode.SubmarineAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Submarine
    {
        private static readonly Position StartPosition = new();
        private static readonly Sonar StartSonar = new();
        private static readonly Diagnostics StartDiagnostics = new(0,0);
        private static readonly Bingo StartBingo = new();
        private static readonly Oceanography StartOceanography = new();

        private Position Position { get; set; }
        private Navigation Navigation { get; set; }

        public readonly Bingo Bingo;

        public readonly Sonar Sonar;

        public readonly Diagnostics Diagnostics;
        public readonly Oceanography Oceanography;

        public Submarine()
        {
            Position = StartPosition;
            Sonar = StartSonar;
            Navigation = new Navigation();
            Diagnostics = StartDiagnostics;
            Bingo = StartBingo;
            Oceanography = StartOceanography;
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
}
