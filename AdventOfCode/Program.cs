using Microsoft.Extensions.DependencyInjection;
using AdventOfCode.Infrastructure;
using AdventOfCode.Interfaces;
using AdventOfCode.Repository;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {


            var serviceProvider = new ServiceCollection()
           .AddSingleton<IRepository, Repository.Repository>()
           .BuildServiceProvider();

            IRepository _repository = serviceProvider.GetRequiredService<IRepository>();

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