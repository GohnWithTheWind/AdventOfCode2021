﻿using Microsoft.Extensions.DependencyInjection;
using AdventOfCode.Infrastructure;
using AdventOfCode.Interfaces;
using AdventOfCode.Repository;
using AdventOfCode.SubmarineAggregate;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {


            var serviceProvider = new ServiceCollection()
           .AddSingleton<IRepository, Repository.Repository>()
           .BuildServiceProvider();

            IRepository _repository = serviceProvider.GetRequiredService<IRepository>();

            Submarine submarine = new();

            bool exit = false;

            while (exit == false)
            {
                Console.Write("Enter day (int):");

                bool isNumeric = int.TryParse(Console.ReadLine(), out int day);
                while (!isNumeric)
                {
                    Console.Write("Not an int. Enter day (int):");
                    isNumeric = int.TryParse(Console.ReadLine(), out day);
                }

                Console.Write("Enter step:");
                isNumeric = int.TryParse(Console.ReadLine(), out int step);
                while (!isNumeric)
                {
                    Console.Write("Not an int. Enter step (int):");
                    isNumeric = int.TryParse(Console.ReadLine(), out step);
                }


                string result = "";
                if (day == 1)
                {
                    result = string.Format("Descentspeed: {0}.", submarine.Sonar.CalculateDescentSpeed(
                        _repository.GetDescentData(), step).ToString());
                }

                else if (day == 2)
                {
                    submarine.NavigateFromInput(_repository.GetNavigationData(), step);
                    result = submarine.GetPosition().ToString();
                }
                else if (day == 3)
                {
                    submarine.Diagnostics.ReadPowerConsumptionStream(_repository.GetDiagnosticsData());
                    submarine.Diagnostics.ReadOxygenStream(_repository.GetDiagnosticsData());
                    result = string.Format("Current diagnostics readings: {0}.", submarine.Diagnostics.ToString());
                }
                else if (day == 4)
                {
                    submarine.Bingo.InitializeBingo(_repository.GetDrawnBingoNumbers(), _repository.GetBingoBoards());
                    if (step == 1)
                    result = string.Format("The winner board is: {0}", submarine.Bingo.GetWinner().ToString());
                    else if(step == 2)
                        result = string.Format("The winner board is: {0}", submarine.Bingo.GetLoser().ToString());
                }
                else if(day == 5)
                {
                    if(step == 1)
                        result = string.Format("Number of danger areas: {0}.", submarine.Sonar.GetNumOfDangerAreas(_repository.GetVentRanges(0)));
                    else if (step == 2)
                        result = string.Format("Number of danger areas: {0}.", submarine.Sonar.GetNumOfDangerAreas(_repository.GetVentRanges(1)));
                }
                else if(day == 6)
                {
                    if(step == 1)
                    result = string.Format("Number of fishies after 80 days: {0}.",submarine.Oceanography.SimulateLanternFishGrowth(_repository.GetLanternFishColony(), 80).ToString());
                    else if(step==2)
                        result = string.Format("Number of fishies after 256 days: {0}.", submarine.Oceanography.SimulateLanternFishGrowth(_repository.GetLanternFishColony(), 256).ToString());

                }
                Console.WriteLine(result);

                Console.Write("Type Exit to exit or hit enter to go again.");

                if(Console.ReadLine() == "Exit")
                {
                    exit = true;
                }
            }
            
        }

    }
}