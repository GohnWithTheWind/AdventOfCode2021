using Microsoft.Extensions.DependencyInjection;
using AdventOfCode.Infrastructure;
using AdventOfCode.Interfaces;
using AdventOfCode.Repository;
using AdventOfCode.SubmarineAggregate;
using static AdventOfCode.SubmarineAggregate.Oceanography;

namespace AdventOfCode
{
    class Program
    {
        public static void Print2DArray<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
        }
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
                    result = string.Format("Descentspeed: {0}. Elapsed time: {1}.", 
                        submarine.Sonar.CalculateDescentSpeed(_repository.GetDescentData(), step).ToString(), watch.Elapsed);
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
                    result = string.Format("Current diagnostics readings: {0} Elapsed time: {1}.", 
                        submarine.Diagnostics.ToString(), watch.Elapsed);
                }
                else if (day == 4)
                {
                    submarine.Bingo.InitializeBingo(_repository.GetDrawnBingoNumbers(), _repository.GetBingoBoards());
                    if (step == 1)
                    result = string.Format("The winner board is: {0}.  Elapsed time: {1}.", 
                        submarine.Bingo.GetWinner().ToString(), watch.Elapsed);
                    else if(step == 2)
                        result = string.Format("The winner board is: {0}.  Elapsed time: {1}.", 
                            submarine.Bingo.GetLoser().ToString(), watch.Elapsed);
                }
                else if(day == 5)
                {
                    if(step == 1)
                        result = string.Format("Number of danger areas: {0}. Elapsed time: {1}.", 
                            submarine.Sonar.GetNumOfDangerAreas(_repository.GetVentRanges(0)), watch.Elapsed);
                    else if (step == 2)
                        result = string.Format("Number of danger areas: {0}. Elapsed time: {1}.", 
                            submarine.Sonar.GetNumOfDangerAreas(_repository.GetVentRanges(1)), watch.Elapsed);
                }
                else if(day == 6)
                {
                    if (step == 1)
                        result = string.Format("Number of fishies after 80 days: {0}. Elapsed time: {1}.", 
                            submarine.Oceanography.SimulateLanternFishGrowth(_repository.GetLanternFishColony(), 80).ToString(), watch.Elapsed);
                    else if (step == 2)
                    {                      
                        result = string.Format("Number of fishies after 256 days: {0}. Elapsed time: {1}.", 
                            submarine.Oceanography.SimulateLanternFishGrowth(_repository.GetLanternFishColony(), 256).ToString(), watch.Elapsed);
                    }

                }
                else if (day == 7)
                {
                    if (step == 1)
                        result = result + string.Format("Optimal crab submarine fuel useage: {0}. Elapsed time: {1}.", 
                            submarine.Oceanography.OptimizeCrabFuelUseage(_repository.GetCrabSubmarinePositions(), 1).ToString(), watch.Elapsed);
                    if (step == 2)
                        result = string.Format("Optimal crab submarine fuel useage: {0}. Elapsed time: {1}.", 
                            submarine.Oceanography.OptimizeCrabFuelUseage(_repository.GetCrabSubmarinePositions(), 2).ToString(), watch.Elapsed);

                }
                else if (day == 8)
                {
                    if (step == 1)
                        result = string.Format("Occurrences: {0}. Elapsed time {1}.", submarine.Diagnostics.DebugDisplays(_repository.GetDisplayData()).ToString(), watch.Elapsed);
                    else if (step == 2)
                        result = string.Format("Display Info: {0}. Elapsed time: {1}.", submarine.Diagnostics.DebugDisplays2(_repository.GetDisplayData()), watch.Elapsed);

                }
                else if(day == 9)
                {
                    if (step == 1)
                        result = string.Format("Number: {0}. Elapsed time {1}.", submarine.Sonar.GetSmokeFilledAreas(_repository.GetHeightMap()).ToString(), watch.Elapsed);
                    if(step == 2)
                        result = string.Format("Number: {0}. Elapsed time {1}.", submarine.Sonar.GetBasins(_repository.GetHeightMap()).ToString(), watch.Elapsed);
                }
                else if(day == 10)
                {
                    if(step == 1)
                        result = string.Format("Syntax error score: {0}. Elapsed time {1}.", submarine.Navigation.GetIllegalSyntaxScore(_repository.GetSubSystemData()).ToString(), watch.Elapsed);
                    else if (step == 2)
                        result = string.Format("Incomplete line score: {0}. Elapsed time {1}.", submarine.Navigation.GetIncompleteLineScore(_repository.GetSubSystemData()).ToString(), watch.Elapsed);
                }
                else if(day == 11)
                {
                    if (step == 1)
                        result = string.Format("Number of octopus flashes: {0}. Elapsed time {1}.", submarine.Oceanography.SimulateOctopusSignals(_repository.GetOctopusStatus(), 100).ToString(), watch.Elapsed);
                    else if(step == 2)
                        result = string.Format("Simultaneous flash after: {0} loops. Elapsed time {1}.", submarine.Oceanography.SimulateOctopusSimultaneousFlash(_repository.GetOctopusStatus()).ToString(), watch.Elapsed);
                    else if(step == 3)
                    {
                        var res = submarine.Oceanography.SimulateOctopusSignalsGraphic(_repository.GetOctopusStatus(), 100);
                        foreach (var matrix in res.BlinkEvents)
                        {
                            Console.WriteLine(string.Format("Loop Number: {0}.", matrix.LoopSequence.ToString()));
                            Thread.Sleep(200);
                            Console.Clear();
                            Print2DArray(matrix.Matrix);                       
                        }
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