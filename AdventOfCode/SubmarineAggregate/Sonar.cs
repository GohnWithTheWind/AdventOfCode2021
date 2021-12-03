using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
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
            else if (calculationVersion == 2)
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
}
