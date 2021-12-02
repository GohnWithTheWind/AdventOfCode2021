using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        IReader FileReader = new FileReader();
        [TestMethod]
        public void Day1Step1()
        {
            Submarine submarine = new Submarine();

            int result = submarine.Sonar.CalculateDescentSpeed(FileReader.FileToIntList("InputFiles/Day1/Day1Tests.txt"), 1);

            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Day1Step2()
        {
            Submarine submarine = new Submarine();

            int result = submarine.Sonar.CalculateDescentSpeed(FileReader.FileToIntList("InputFiles/Day1/Day1Tests.txt"), 2);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Day2Step1()
        {
            Submarine submarine = new Submarine();

            submarine.NavigateFromInput(FileReader.FileToNavigationList("InputFiles/Day2/Day2Tests.txt"), 1);

            Assert.AreEqual(150, submarine.GetPosition().PositionHash);
        }

        [TestMethod]
        public void Day2Step2()
        {
            Submarine submarine = new Submarine();

            submarine.NavigateFromInput(FileReader.FileToNavigationList(@"InputFiles/Day2/Day2Tests.txt"), 2);

            Assert.AreEqual(900, submarine.GetPosition().PositionHash);
        }

    }
}