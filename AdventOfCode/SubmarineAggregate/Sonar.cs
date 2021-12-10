using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Sonar
    {
        public int GetBasins(HeightMap map)
        {
            foreach (var num in map.Matrix)
            {
                num.IsChecked = IsSmokeFilled(map, num);
            }
            var basinCenters = (from MapNumber n in map.Matrix where n.IsChecked == true select n).ToList();
            foreach(var center in basinCenters)
            {
                var nodes = GetBasinNodes(map, center, new());
                center.BasinNodes = nodes.Distinct().Count() + 1;
            }
            var top3 = basinCenters.OrderByDescending(o => o.BasinNodes).Take(3).ToList();

            return top3[0].BasinNodes * top3[1].BasinNodes * top3[2].BasinNodes;

        }
        private List<MapNumber> GetBasinNodes(HeightMap map, MapNumber number, List<MapNumber> nodes)
        {

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (Math.Abs(y) + Math.Abs(x) == 1)
                    if ((number.PosY + y) >= 0 && (number.PosX + x) >= 0 && 
                            number.PosY + y < map.Matrix.GetLength(0) &&
                            number.PosX + x < map.Matrix.GetLength(1))
                    {
                        if (map.Matrix[number.PosY + y, number.PosX + x].Number > number.Number && 
                                map.Matrix[number.PosY + y, number.PosX + x].Number != 9 && 
                                !(map.Matrix[number.PosY + y, number.PosX + x].PosX == number.PosX && 
                                  map.Matrix[number.PosY + y, number.PosX + x].PosY == number.PosY))
                        {                               
                            nodes.Add(map.Matrix[number.PosY + y, number.PosX + x]);
                            nodes = GetBasinNodes(map, map.Matrix[number.PosY + y, number.PosX + x], nodes);
                        }
                    }
                }
            }
            return nodes;
        }

        public int GetSmokeFilledAreas(HeightMap map)
        {
            foreach (var num in map.Matrix)
            {
                num.IsChecked = IsSmokeFilled(map, num);
            }
            return (from MapNumber n in map.Matrix where n.IsChecked == true select n.Number).Sum() + (from MapNumber n in map.Matrix where n.IsChecked == true select n.Number).Count();
        }
        private bool IsSmokeFilled(HeightMap map, MapNumber number)
        {
            bool isLowest = true;
            int breakLoop = 0;
            if(breakLoop == 0)
            for(int y = -1; y <= 1; y++)
            {
                for(int x = -1; x <= 1; x++)
                {
                    if (Math.Abs(y) + Math.Abs(x) != 1)
                    {
                    }
                    else if ((number.PosY + y) >= 0 && (number.PosX + x) >= 0 && number.PosY + y < map.Matrix.GetLength(0) && number.PosX + x < map.Matrix.GetLength(1))
                    {
                        if (map.Matrix[number.PosY + y, number.PosX + x].Number < number.Number)
                        {
                            breakLoop = 1;
                            isLowest = false;
                        }
                    }
                }
            }
            return isLowest;
        }

        public int GetNumOfDangerAreas(List<VentRange> ventRanges)
        {
            List<Coordinate> coordinates = new();

            foreach(VentRange range in ventRanges)
            {
                int currentX = range.StartX;
                int currentY = range.StartY;

                if (range.StartY == range.EndY)
                {
                    while (currentX <= range.EndX)
                    {
                        coordinates.Add(new Coordinate(currentX, currentY));
                        currentX++;
                    }
                }
                else if (range.StartX == range.EndX)
                {
                    while (currentY <= range.EndY && range.StartX == range.EndX)
                    {
                        coordinates.Add(new Coordinate(currentX, currentY));

                        currentY++;
                    }
                }
                else
                {
                    while(currentX <= range.EndX)
                    {
                        coordinates.Add(new Coordinate(currentX, currentY));

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

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;  
        }
    }

    public class HeightMap
    {
        public MapNumber[,] Matrix { get; set; }

        public HeightMap(int dimensionX, int dimensionY)
        {
            Matrix = new MapNumber[dimensionX, dimensionY];
        }
    }

    public class MapNumber
    {
        public int Number { get; set; }
        public bool IsChecked { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int BasinNodes { get; set; }

        public MapNumber(int number, bool isChecked, int posX, int posY)
        {
            Number = number;
            IsChecked = isChecked;
            PosX = posX;
            PosY = posY;
        }

    }
}
