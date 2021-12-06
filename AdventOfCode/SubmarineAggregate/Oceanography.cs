using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.SubmarineAggregate
{
    public class Oceanography
    {
        public long SimulateLanternFishGrowth(List<int> fishColony, int numOfDays)
        {
            List<FishGroup> fishCount = new List<FishGroup>();
            for(int i = 0; i < 9; i++)
            {
                fishCount.Add(new FishGroup(i, 0));
            }
            foreach(var f in fishColony)
            {
                fishCount.Where(col => col.DaysFromBirthing == f).First().NumOfFish++;
            }
            var res = CalculateDailFishGrowth(fishCount, numOfDays);
            return res.Select(r => r.NumOfFish).Sum();
        }

        private List<FishGroup> CalculateDailFishGrowth(List<FishGroup> fishies, int numOfDays)
        {
            if (numOfDays == 0)
            {
                return fishies;
            }

            List<FishGroup> fishGroups = new List<FishGroup>();
            foreach (var item in fishies)
            {
                fishGroups.Add(new FishGroup(item.DaysFromBirthing, item.NumOfFish));
            }
            foreach (var fish in fishies)
            {
                if (fish.NumOfFish > 0)
                {           
                    if (fish.DaysFromBirthing == 0)
                    {
                        var newPos = fishGroups.Where(f => f.DaysFromBirthing == 6).First();
                        newPos.NumOfFish = newPos.NumOfFish + fish.NumOfFish;
                        // happy birthday lil fishy
                        var newFish = fishGroups.Where(f => f.DaysFromBirthing == 8).First();
                        newFish.NumOfFish = newFish.NumOfFish + fish.NumOfFish;
                    }
                    else
                    {
                        var newPos = fishGroups.Where(f => f.DaysFromBirthing == fish.DaysFromBirthing - 1).First();
                        newPos.NumOfFish = newPos.NumOfFish + fish.NumOfFish;
                    }
                    var currentPos = fishGroups.Where(f => f.DaysFromBirthing == fish.DaysFromBirthing).First();
                    currentPos.NumOfFish = currentPos.NumOfFish - fish.NumOfFish;
                }
            }
            return CalculateDailFishGrowth(fishGroups, numOfDays - 1);
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

    }
}
