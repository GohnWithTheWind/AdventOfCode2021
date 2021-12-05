using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Sonar
    {

        public int GetNumOfDangerAreas(List<VentRange> ventRanges)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            foreach(VentRange range in ventRanges)
            {
                int currentX = range.StartX;
                int currentY = range.StartY;

                if (range.StartY == range.EndY)
                {
                    while (currentX <= range.EndX)
                    {
                        coordinates.Add(new Coordinate(currentX, currentY, 1));
                        currentX++;
                    }
                }
                else if (range.StartX == range.EndX)
                {
                    while (currentY <= range.EndY && range.StartX == range.EndX)
                    {
                        coordinates.Add(new Coordinate(currentX, currentY, 1));

                        currentY++;
                    }
                }
                else
                {
                    int distance = Math.Abs(range.StartX - range.EndX);
                    while(currentX <= range.EndX)
                    {
                        coordinates.Add(new Coordinate(currentX, currentY, 1));

                        if(currentY > range.EndY)
                        {
                            currentY--;                         
                        }
                        else
                        {
                            currentY++;
                        }
                        currentX++;
                    }
                }

            }
            var count = coordinates.GroupBy(c => new { c.X, c.Y }).Where(grp => grp.Count() > 1).Count();

            return count;
        }
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

    public class VentRange
    {
        public int StartX { get; set; }
        public int StartY { get; set; } 
        public int EndX { get; set; }   
        public int EndY { get; set; }  
        
        public VentRange(int startX, int startY, int endX, int endY)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;           
            EndY = endY;
        }
    }
    public class Coordinate
    {
        public int X { get; set; } 
        public int Y { get; set; }
        public int Occurences { get; set; } 

        public Coordinate(int x, int y, int occurences)
        {
            X = x;
            Y = y;
            Occurences = occurences;   
        }
    }
}
