using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Interfaces;

namespace AdventOfCode
{
    public class Diagnostics
    {
        public int GammaRate { get; set; }
        public int EpsilonRate { get; set; }
        public int PowerConsumption { get { return GammaRate * EpsilonRate; } }
        public int OxygenGeneratorRating { get; set; }
        public int CO2ScrubberRating { get; set; }
        public int LifeSupportRating { get { return OxygenGeneratorRating * CO2ScrubberRating; } }


        public override string ToString()
        {
            return String.Format("GammaRate: {0}. EpsilonRate: {1}. Powerconsumption: {2}. OxygenGeneratorRating: {3}. CO2ScrubberRating: {4}. LifeSupportRating {5}.",
                GammaRate.ToString(), EpsilonRate.ToString(), PowerConsumption.ToString(), OxygenGeneratorRating.ToString(), CO2ScrubberRating.ToString(), LifeSupportRating.ToString());
        }

        public Diagnostics(int gammaRate, int epsilonRate)
        {
            GammaRate = gammaRate;
            EpsilonRate = epsilonRate;
        }

        public int DebugDisplays2(List<Display> displays)
        {

            int totRes = 0;
            List<ResList> res = new();

            List<char> charList = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

            foreach (var dis in displays)
            {

                List<DisplayNumber> numbers = new();
                numbers.Add(new(0, 6, new List<int> { 1, 2, 3, 5, 6, 7 }));
                numbers.Add(new(1, 2, new List<int> { 3, 6 }));
                numbers.Add(new(2, 5, new List<int> { 1, 3, 4, 5, 7 }));
                numbers.Add(new(3, 5, new List<int> { 1, 3, 4, 6, 7 }));
                numbers.Add(new(4, 4, new List<int> { 2, 3, 4, 6 }));
                numbers.Add(new(5, 5, new List<int> { 1, 2, 4, 6, 7 }));
                numbers.Add(new(6, 6, new List<int> { 1, 2, 4, 5, 6, 7 }));
                numbers.Add(new(7, 3, new List<int> { 1, 3, 6 }));
                numbers.Add(new(8, 7, new List<int> { 1, 2, 3, 4, 5, 6, 7 }));
                numbers.Add(new(9, 6, new List<int> { 1, 2, 3, 4, 6, 7 }));

                List<Tuple<int, char>> intList = new();

                foreach (char c in charList)
                {
                    int occurrence = dis.Pattern.Where(s => s.Contains(c.ToString())).Count();
                    intList.Add(new(occurrence, c));
                }
                char char5 = intList.Where(i => i.Item1 == 4).First().Item2;

                char char6 = intList.Where(i => i.Item1 == 9).First().Item2;

                char char3 = dis.Pattern.Where(d => d.Length == 2).First().Replace(char6.ToString(), string.Empty).ToCharArray()[0];

                char char1 = dis.Pattern.Where(d => d.Length == 3).First().Replace(char6.ToString(), string.Empty).Replace(char3.ToString(), string.Empty).Substring(0, 1).ToCharArray()[0];

                string trimToChar2 = dis.Pattern.Where(d => d.Length == 4).First();
                string seven = dis.Pattern.Where(d => d.Length == 3).First();
                string four = dis.Pattern.Where(d => d.Length == 4).First(); ;

                foreach (var c in seven)
                {
                    trimToChar2 = trimToChar2.Replace(c.ToString(), string.Empty);
                }
                    
                trimToChar2 = trimToChar2.Replace(char5.ToString(), string.Empty);

                var replacer = dis.Pattern.Where(dis => dis.Length == 5 && dis.Contains(char3.ToString())).First();
                foreach (var c in replacer)
                    trimToChar2 = trimToChar2.Replace(c.ToString(), string.Empty);

                char char2 = trimToChar2.ToCharArray()[0];

                char char4 = four.Replace(char2.ToString(), string.Empty).Replace(char3.ToString(), string.Empty).Replace(char6.ToString(), string.Empty).ToCharArray()[0];

                string eight = dis.Pattern.Where(d => d.Length == 7).First();
                char char7 = eight.Replace(char1.ToString(), string.Empty).Replace(char2.ToString(), string.Empty).Replace(char3.ToString(), string.Empty).
                    Replace(char4.ToString(), string.Empty).Replace(char5.ToString(), string.Empty).Replace(char6.ToString(), string.Empty).ToCharArray()[0];

                foreach (var n in numbers)
                {
                    if (n.UsedSegments.Contains(1))
                        n.StringSequence.Add(char1.ToString());
                    if (n.UsedSegments.Contains(2))
                        n.StringSequence.Add(char2.ToString());
                    if (n.UsedSegments.Contains(3))
                        n.StringSequence.Add(char3.ToString());
                    if (n.UsedSegments.Contains(4))
                        n.StringSequence.Add(char4.ToString());
                    if (n.UsedSegments.Contains(5))
                        n.StringSequence.Add(char5.ToString());
                    if (n.UsedSegments.Contains(6))
                        n.StringSequence.Add(char6.ToString());
                    if (n.UsedSegments.Contains(7))
                        n.StringSequence.Add(char7.ToString());
                }

                string resultNumber = "";
                foreach (var o in dis.Output)
                {
                    foreach (var n in numbers)
                    {
                        int miss = 0;
                        foreach (var c in o)
                        {
                            if (o.Length != n.StringSequence.Count)
                            {
                                miss = 1;
                                break;
                            }
                            if (!n.StringSequence.Contains(c.ToString()))
                            {
                                miss = 1;
                                break;
                            }
                        }
                        if (miss == 0)
                        {
                            resultNumber = resultNumber + n.Number.ToString();
                        }
                    }
                }
                 totRes = totRes + int.Parse(resultNumber);
             }

            return totRes;
        }

        public int DebugDisplays(List<Display> displays)
        {  
            List<ResList> res = new();

            foreach(var v in displays)
            {
                res.AddRange(v.Output.GroupBy(o => o.Length).Select(g => new ResList { Count = g.Count(), Length = g.Key }).ToList());
            }
            res.RemoveAll(o => o.Length != 2 && o.Length != 4 && o.Length != 3 && o.Length != 7);
            var ret = res.GroupBy(r => r.Length).Select(g => new ResList { Count = g.Sum(s => s.Count), Length = g.Key });

            return ret.Sum(s => s.Count);
 
        }

        public void ReadPowerConsumptionStream(List<string> readings)
        {
            int numOfRows = readings.Count;
            int numOfPositions = readings[1].Length;
            string gammaString = "";
            string epsilonString = "";

            int pos = 0;
            while (pos < numOfPositions)
            {
                int oneSum = 0;
                foreach (string reading in readings)
                {
                    oneSum += int.Parse(reading.Substring(pos, 1));
                }
                gammaString += oneSum > numOfRows / 2 ? "1" : "0";
                epsilonString += oneSum < numOfRows / 2 ? "1" : "0";
                pos++;
            }
            GammaRate = Convert.ToInt32(gammaString, 2);
            EpsilonRate = Convert.ToInt32(epsilonString, 2);
        }

        public void ReadOxygenStream(List<string> readings)
        {

            int numOfPositions = readings[1].Length;

            List<string> oxygenReadings = new(readings);

            int pos = 0;

            while (pos < numOfPositions && oxygenReadings.Count > 1)
            {
                int oneSum = oxygenReadings.Where(o => o.Substring(pos, 1) == "1").Count();

                string stringToKeep = oneSum >= (decimal)oxygenReadings.Count / 2 ? "1" : "0";

                oxygenReadings.RemoveAll(o => o.Substring(pos, 1) != stringToKeep);

                pos++;
            }
            OxygenGeneratorRating = Convert.ToInt32(oxygenReadings[0], 2);

            List<string> scrubberReadings = new(readings);

            pos = 0;

            while (pos < numOfPositions && scrubberReadings.Count > 1)
            {
                int oneSum = scrubberReadings.Where(o => o.Substring(pos, 1) == "1").Count();

                string stringToKeep = oneSum < (decimal)scrubberReadings.Count / 2 ? "1" : "0";

                scrubberReadings.RemoveAll(o => o.Substring(pos, 1) != stringToKeep);

                pos++;
            }

            CO2ScrubberRating = Convert.ToInt32(scrubberReadings[0], 2);
        }

        private class DisplayNumber
        {
            public DisplayNumber(int number, int numOfValues, List<int> segments)
            {
                Number = number;
                NumOfValues = numOfValues;
                UsedSegments = segments;
                StringSequence = new List<string>();
            }
            public int Number { get; set; }
            public string? NumCode { get; set; }
            public int NumOfValues { get; set; }
            public List<int> UsedSegments { get; set; }
            public List<string> StringSequence { get; set; }
        }

        public class ResList
        {
            public int Count { get; set; }
            public int Length { get; set; }

        }

        public class Display
        {
            public Display(List<string> pattern, List<string> output)
            {
                Pattern = pattern;
                Output = output;
            }
            public List<string> Pattern { get; set; }
            public List<string> Output { get; set; }
        }
    }
}
