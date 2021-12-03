using Microsoft.Extensions.DependencyInjection;
using AdventOfCode.Infrastructure;
using AdventOfCode.Interfaces;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IReader, FileReader>()
                .BuildServiceProvider();

            IReader _reader = serviceProvider.GetRequiredService<IReader>();

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
                if (day == 1)
                {
                    result = string.Format("Descentspeed: {0}.", submarine.Sonar.CalculateDescentSpeed(
                        _reader.FileToIntList("InputFiles/Day1/input.txt"), step).ToString());
                }

                else if (day == 2)
                {
                    submarine.NavigateFromInput(_reader.FileToNavigationList("InputFiles/Day2/Day2.txt"), step);
                    result = submarine.GetPosition().ToString();
                }
                else if (day == 3)
                {
                    submarine.Diagnostics.ReadPowerConsumptionStream(_reader, "InputFiles/Day3/Day3.txt");
                    submarine.Diagnostics.ReadOxygenStream(_reader, "InputFiles/Day3/Day3.txt");
                    result = string.Format("Current diagnostics readings: {0}.", submarine.Diagnostics.ToString());
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