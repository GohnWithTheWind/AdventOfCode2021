using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.SubmarineAggregate
{
    public class Oceanography
    {
        public int SimulateOctopusSimultaneousFlash(OctopusGroup map)
        {
            int leftToFlash = 1;
            int i = 0;

            while(leftToFlash > 0)
            {
                foreach (var n in map.Matrix)
                {
                    n.Energy++;
                    n.HasFlashed = false;
                }
                map = FlashOctopuses(map, i, 0);
                i++;
                leftToFlash = (from Octopus o in map.Matrix where o.HasFlashed == false select o).Count();
            }
            return i;
        }
        public OctopusGroup SimulateOctopusSignalsWithEvents(OctopusGroup map, int samplingSize)
        {
            for (int i = 0; i < samplingSize; i++)
            {
                foreach (var n in map.Matrix)
                {
                    n.Energy++;
                    n.HasFlashed = false;
                }
                map = FlashOctopuses(map, i, 0);
            }
            return map;
        }
        public int SimulateOctopusSignals(OctopusGroup map, int samplingSize)
        {
            for (int i = 0; i < samplingSize; i++)
            {
                foreach (var n in map.Matrix)
                {
                    n.Energy++;
                    n.HasFlashed = false;
                }
                map = FlashOctopuses(map, i, 0);
            }
            return (from Octopus o in map.Matrix select o.FlashCount).Sum();
        }

        private OctopusGroup FlashOctopuses(OctopusGroup map, int loopNo, int flashSequence)
        {
            var keepFlashing = (from Octopus n in map.Matrix where n.Energy > 9 && n.HasFlashed == false select n).FirstOrDefault();
            if (keepFlashing == null)
            {
                foreach(var n in map.Matrix)
                {
                    if (n.Energy > 9)
                        n.Energy = 0;
                }
                return map;
            }
            foreach(var n in map.Matrix)
            {
                if(n.Energy > 9 && n.HasFlashed == false)
                {
                    n.HasFlashed = true;
                    n.FlashCount++;
                    // Collect flash events to be able to play through them in console. 
                    var events = GetNewOctoGroup(map);
                    events.LoopSequence = loopNo;
                    events.SpreadSequence = flashSequence;
                    map.FlashEvents.Add(events);

                    for(int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            if (!(x == 0 && y == 0))
                            {                              
                                var posX = x + n.PosX;
                                var posY = y + n.PosY;
                                if (posX >= 0 && posX <= 9 && posY >= 0 && posY <= 9)
                                    map.Matrix[posY, posX].Energy++;
                            }
                        }
                    }
                    break;
                }
            }
            return FlashOctopuses(map, loopNo, flashSequence++);
        }

        private OctopusGroup GetNewOctoGroup(OctopusGroup group)
        {
            var g = new OctopusGroup(10, 10);
            foreach(var v in group.Matrix)
            {
                g.Matrix[v.PosY, v.PosX] = new Octopus(v.Energy, v.HasFlashed, v.PosX, v.PosY);
            }
            return g;
        }

        public int OptimizeCrabFuelUseage(List<int> crabPositions, int calculationVersion)
        {
            int maxPos = crabPositions.Max();
            int minPos = crabPositions.Min();

            List<Tuple<int, int>> possiblePositions = new();

            for(int i = minPos; i <= maxPos; i++)
            {
                int fuelUseage = 0;
                foreach(var crabSub in crabPositions)
                {
                    fuelUseage = fuelUseage + (calculationVersion == 1? Math.Abs(crabSub - i) : Math.Abs(crabSub - i) * (Math.Abs(crabSub - i) + 1) / 2);
                }
                possiblePositions.Add(new(i, fuelUseage));
            }
            return possiblePositions.Min(i => i.Item2);
        }

        public long SimulateLanternFishGrowth(List<int> fishColony, int numOfDays)
        {
            List<FishGroup> fishCount = new();
            for(int i = 0; i < 9; i++)
            {
                fishCount.Add(new(i, 0));
            }
            foreach(var f in fishColony)
            {
                fishCount.Where(col => col.DaysFromBirthing == f).First().NumOfFish++;
            }
            var res = CalculateDailyFishGrowth(fishCount, numOfDays);
            return res.Select(r => r.NumOfFish).Sum();
        }

        private List<FishGroup> CalculateDailyFishGrowth(List<FishGroup> fishies, int numOfDays)
        {
            if (numOfDays == 0)
            {
                return fishies;
            }

            List<FishGroup> fishGroups = new();
            foreach (var item in fishies)
            {
                if(item.DaysFromBirthing == 0)
                {
                    fishGroups.Add(new(8, item.NumOfFish));
                    fishGroups.Add(new(6, item.NumOfFish + fishies.Where(f => f.DaysFromBirthing == 7).Select(s => s.NumOfFish).First()));
                }
                else if(item.DaysFromBirthing != 7)
                {
                    fishGroups.Add(new(item.DaysFromBirthing - 1, item.NumOfFish));
                }
            }
            return CalculateDailyFishGrowth(fishGroups, numOfDays - 1);
        }


        private class FishGroup
        {
            public int DaysFromBirthing { get; set; }
            public long NumOfFish { get; set; }

            public FishGroup(int daysFromBirthing, long numOfFish)
            {
                DaysFromBirthing = daysFromBirthing;
                NumOfFish = numOfFish; 
            }
        }

        public class OctopusGroup
        {
            public Octopus[,] Matrix { get; set; }
            public List<OctopusGroup> FlashEvents { get; set; }
            public int SpreadSequence { get; set; }
            public int LoopSequence { get; set; }

            public OctopusGroup(int dimensionX, int dimensionY)
            {
                Matrix = new Octopus[dimensionX, dimensionY];
                FlashEvents = new List<OctopusGroup>();
            }
        }

        public class Octopus
        {
            public int Energy { get; set; }
            public bool HasFlashed { get; set; }
            public int FlashCount { get; set; }
            public int PosX { get; set; }
            public int PosY { get; set; }

            public Octopus(int energy, bool hasFlashed, int posX, int posY)
            {
                Energy = energy;
                HasFlashed = hasFlashed;
                PosX = posX;
                PosY = posY;
                FlashCount = 0;
            }

            public override string ToString()
            {
                //return Energy.ToString();
                return HasFlashed ? "-1" : Energy.ToString();
            }

        }

    }
}


