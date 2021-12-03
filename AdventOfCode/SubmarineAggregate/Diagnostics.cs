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
        public void ReadPowerConsumptionStream(IReader data, string path)
        {
            List<string> readings = data.FileToStringList(path);

            int numOfRows = readings.Count();
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

        public void ReadOxygenStream(IReader data, string path)
        {
            List<string> readings = data.FileToStringList(path);

            int numOfPositions = readings[1].Length;

            List<string> oxygenReadings = new List<string>(readings);

            int pos = 0;

            while (pos < numOfPositions && oxygenReadings.Count > 1)
            {
                int oneSum = oxygenReadings.Where(o => o.Substring(pos, 1) == "1").Count();

                string stringToKeep = oneSum >= (decimal)oxygenReadings.Count() / 2 ? "1" : "0";

                oxygenReadings.RemoveAll(o => o.Substring(pos, 1) != stringToKeep);

                pos++;
            }
            OxygenGeneratorRating = Convert.ToInt32(oxygenReadings[0], 2);

            List<string> scrubberReadings = new List<string>(readings);

            pos = 0;

            while (pos < numOfPositions && scrubberReadings.Count > 1)
            {
                int oneSum = scrubberReadings.Where(o => o.Substring(pos, 1) == "1").Count();

                string stringToKeep = oneSum < (decimal)scrubberReadings.Count() / 2 ? "1" : "0";

                scrubberReadings.RemoveAll(o => o.Substring(pos, 1) != stringToKeep);

                pos++;
            }

            CO2ScrubberRating = Convert.ToInt32(scrubberReadings[0], 2);
        }

    }
}
