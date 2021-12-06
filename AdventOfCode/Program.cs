using Microsoft.Extensions.DependencyInjection;
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

            Console.Write("Test or Input data?");
            var serviceProvider =
            Console.ReadLine() switch {
                "Input" => 
                    new ServiceCollection()
                        .AddSingleton<IRepository, Repository.InputRepository>()
                        .BuildServiceProvider(),
                _ =>
                    new ServiceCollection()
                            .AddSingleton<IRepository, Repository.TestRepository>()
                            .BuildServiceProvider(),
            };

            IRepository _repository = serviceProvider.GetRequiredService<IRepository>();

            Console.WriteLine(string.Format("Loaded Repository of type: {0}.", _repository.GetType().Name));

            Submarine submarine = new Submarine();

            bool exit = false;

            while (exit == false)
            {
                Console.Write("Enter day (int):");
                int day;
                int step;

                bool isNumeric = int.TryParse(Console.ReadLine(), out day);
                while(!isNumeric)
                {
                    Console.Write("Not an int. Enter day (int):");
                    isNumeric = int.TryParse(Console.ReadLine(), out day);
                }

                Console.Write("Enter step:");
                isNumeric = int.TryParse(Console.ReadLine(), out step);
                while (!isNumeric)
                {
                    Console.Write("Not an int. Enter step (int):");
                    isNumeric = int.TryParse(Console.ReadLine(), out step);
                }


                string result = "";
                var watch = System.Diagnostics.Stopwatch.StartNew();
                if (day == 1)
                {
                    result = string.Format("Descentspeed: {0}. Elapsed time: {1}.", submarine.Sonar.CalculateDescentSpeed(
                        _repository.GetDescentData(), step).ToString(), watch.Elapsed);
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
                    result = string.Format("Current diagnostics readings: {0} Elapsed time: {1}.", submarine.Diagnostics.ToString(), watch.Elapsed);
                }
                else if (day == 4)
                {
                    submarine.Bingo.InitializeBingo(_repository.GetDrawnBingoNumbers(), _repository.GetBingoBoards());
                    if (step == 1)
                    result = string.Format("The winner board is: {0}.  Elapsed time: {1}.", submarine.Bingo.GetWinner().ToString(), watch.Elapsed);
                    else if(step == 2)
                        result = string.Format("The winner board is: {0}.  Elapsed time: {1}.", submarine.Bingo.GetLoser().ToString(), watch.Elapsed);
                }
                else if(day == 5)
                {
                    if(step == 1)
                        result = string.Format("Number of danger areas: {0}. Elapsed time: {1}.", submarine.Sonar.GetNumOfDangerAreas(_repository.GetVentRanges(0)), watch.Elapsed);
                    else if (step == 2)
                        result = string.Format("Number of danger areas: {0}. Elapsed time: {1}.", submarine.Sonar.GetNumOfDangerAreas(_repository.GetVentRanges(1)), watch.Elapsed);
                }
                else if(day == 6)
                {
                    if (step == 1)
                        result = string.Format("Number of fishies after 80 days: {0}. Elapsed time: {1}.", submarine.Oceanography.SimulateLanternFishGrowth(_repository.GetLanternFishColony(), 80).ToString(), watch.Elapsed);
                    else if (step == 2)
                    {                      
                        result = string.Format("Number of fishies after 256 days: {0}. Elapsed time: {1}.", submarine.Oceanography.SimulateLanternFishGrowth(_repository.GetLanternFishColony(), 256).ToString(), watch.Elapsed);
                    }

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