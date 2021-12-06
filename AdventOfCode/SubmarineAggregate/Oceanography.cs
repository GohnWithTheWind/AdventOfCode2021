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
            var res = CalculateDailyFishGrowth(fishCount, numOfDays);
            return res.Select(r => r.NumOfFish).Sum();
        }

        private List<FishGroup> CalculateDailyFishGrowth(List<FishGroup> fishies, int numOfDays)
        {
            if (numOfDays == 0)
            {
                return fishies;
            }

            List<FishGroup> fishGroups = new List<FishGroup>();
            foreach (var item in fishies)
            {
                if(item.DaysFromBirthing == 0)
                {
                    fishGroups.Add(new FishGroup(8, item.NumOfFish));
                    fishGroups.Add(new FishGroup(6, item.NumOfFish + fishies.Where(f => f.DaysFromBirthing == 7).Select(s => s.NumOfFish).First()));
                }
                else if(item.DaysFromBirthing != 7)
                {
                    fishGroups.Add(new FishGroup(item.DaysFromBirthing - 1, item.NumOfFish));
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

    }
}


