namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day2Step2();
        }

        public static void Day1Step1()
        {
            List<int> depthValues = new List<int>();

            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(@"C:\temp\AdventOfCode\Day1\input.txt"))
            {
                depthValues.Add(int.Parse(line));
            }
            int lastDepthValue = 0;
            int numOfIncreases = 0;
            int currentRow = 1;

            foreach (int i in depthValues)
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

            Console.WriteLine("NumOfIncreases: " + numOfIncreases.ToString());
        }

        public static void Day1Step2()
        {
            List<int> depthValues = new List<int>();
 
            foreach (string line in System.IO.File.ReadLines(@"C:\temp\AdventOfCode\Day1\input.txt"))
            {
                depthValues.Add(int.Parse(line));
            }
            int lastDepthValue = 0;
            int numOfIncreases = 0;

            for (int i = 0; i < depthValues.Count; i++)
            {
                if(i == 0)
                {
                    lastDepthValue = depthValues[i] + depthValues[i + 1] + depthValues[i + 2];
                }
                else
                {
                    if (i + 2 <= depthValues.Count - 1)
                    {
                        if(depthValues[i] + depthValues[i + 1] + depthValues[i + 2] - lastDepthValue >= 1)
                        {
                            numOfIncreases++;
                        }
                        lastDepthValue = depthValues[i] + depthValues[i + 1] + depthValues[i + 2];
                    }
                }

            }

                Console.WriteLine("NumOfIncreases: " + numOfIncreases.ToString());
        }

        public static void Day2Step1()
        {

            var input = new List<Tuple<string, int>>()
            .Select(t => new { Direction = t.Item1, Amount = t.Item2 }).ToList();

            foreach (string line in System.IO.File.ReadLines(@"C:\temp\AdventOfCode\Day2\Day2.txt"))
            {
                input.Add(new {Direction = line.Substring(0, line.IndexOf(" ")), Amount = int.Parse(line.Substring(line.IndexOf(" ") + 1, line.Length - (line.IndexOf(" ") + 1))) });
            }
            // Get the end position.
            var y = input.Select(t => t.Direction == "down" ? t.Amount : t.Direction == "up" ? t.Amount * -1 : 0).Sum();
            var x = input.Where(t => t.Direction == "forward").Select(t => t.Amount).Sum();

            Console.WriteLine("CurrentDepth: {0}. CurrentForwardPosition: {1}. Y * X: {2}", y.ToString(), x.ToString(), y * x);
        }

        public static void Day2Step2()
        {

            var input = new List<Tuple<string, int>>()
            .Select(t => new { Direction = t.Item1, Amount = t.Item2 }).ToList();

            foreach (string line in System.IO.File.ReadLines(@"C:\temp\AdventOfCode\Day2\Day2.txt"))
            {
                input.Add(new { Direction = line.Substring(0, line.IndexOf(" ")), Amount = int.Parse(line.Substring(line.IndexOf(" ") + 1, line.Length - (line.IndexOf(" ") + 1))) });
            }

            int y = 0;
            int x = 0;
            int aim = 0;
            foreach(var i in input)
            {
                switch(i.Direction)
                {
                    case "down":
                        aim = aim + i.Amount;
                        break;
                    case "up":
                        aim = aim - i.Amount;
                        break;
                    case "forward":
                        x = x + i.Amount;
                        y = y + (aim * i.Amount);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("CurrentDepth: {0}. CurrentForwardPosition: {1}. CurrentAim: {2}. Y * X: {3}", y.ToString(), x.ToString(), aim.ToString(), y * x);
        }

    }
}