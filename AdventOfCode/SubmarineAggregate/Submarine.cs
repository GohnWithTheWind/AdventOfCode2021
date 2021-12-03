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
        private static Diagnostics StartDiagnostics = new Diagnostics(0,0);

        private Position Position { get; set; }
        private Navigation Navigation { get; set; }

        public readonly Sonar Sonar;

        public readonly Diagnostics Diagnostics;

        public Submarine()
        {
            Position = StartPosition;
            Sonar = StartSonar;
            Navigation = new Navigation();
            Diagnostics = StartDiagnostics;
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
