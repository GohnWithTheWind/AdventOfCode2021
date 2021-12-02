namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {

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
                        FileReader.FileToIntList("InputFiles/Day1/input.txt"), step).ToString());
                }

                else if (day == 2)
                {
                    submarine.NavigateFromInput(FileReader.FileToNavigationList("InputFiles/Day2/Day2.txt"), step);
                    result = submarine.GetPosition().ToString();
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