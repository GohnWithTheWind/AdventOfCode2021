using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;
using AdventOfCode.Interfaces;
using AdventOfCode.Infrastructure;
using AdventOfCode.Repository;
using AdventOfCode.SubmarineAggregate;

namespace UnitTests
{
    [TestClass]
    public class SubmarineTests
    {
        readonly IRepository _repository = new TestRepository();

        [TestMethod]
        public void Day1Step1()
        {
            Submarine submarine = new();

            int result = submarine.Sonar.CalculateDescentSpeed(_repository.GetDescentData(), 1);

            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Day1Step2()
        {
            Submarine submarine = new();

            int result = submarine.Sonar.CalculateDescentSpeed(_repository.GetDescentData(), 2);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Day2Step1()
        {
            Submarine submarine = new();

            submarine.NavigateFromInput(_repository.GetNavigationData(), 1);

            Assert.AreEqual(150, submarine.GetPosition().PositionHash);
        }

        [TestMethod]
        public void Day2Step2()
        {
            Submarine submarine = new();

            submarine.NavigateFromInput(_repository.GetNavigationData(), 2);

            Assert.AreEqual(900, submarine.GetPosition().PositionHash);
        }
        [TestMethod]
        public void Day3Step1()
        {
            Submarine submarine = new();

            submarine.Diagnostics.ReadPowerConsumptionStream(_repository.GetDiagnosticsData());

            var read = submarine.Diagnostics.PowerConsumption;

            Assert.AreEqual(198, read);
        }

        [TestMethod]
        public void Day3Step2()
        {
            Submarine submarine = new();

            submarine.Diagnostics.ReadOxygenStream(_repository.GetDiagnosticsData());

            var read = submarine.Diagnostics.LifeSupportRating;

            Assert.AreEqual(230, read);
        }

        [TestMethod]
        public void Day4Step1()
        {
            Submarine submarine = new();
            submarine.Bingo.InitializeBingo(_repository.GetDrawnBingoNumbers(), _repository.GetBingoBoards());
            var winningBoard = submarine.Bingo.GetWinner();
            Assert.AreEqual(4512, winningBoard.Score);
        }
        [TestMethod]
        public void Day4Step2()
        {
            Submarine submarine = new();
            submarine.Bingo.InitializeBingo(_repository.GetDrawnBingoNumbers(), _repository.GetBingoBoards());
            var losingBoard = submarine.Bingo.GetLoser();
            Assert.AreEqual(1924, losingBoard.Score);
        }
        [TestMethod]
        public void Day5Step1()
        {   
            Submarine submarine = new();
            var res = submarine.Sonar.GetNumOfDangerAreas(_repository.GetVentRanges(0));
            Assert.AreEqual(5, res);
        }
        [TestMethod]
        public void Day5Step2()
        {
            Submarine submarine = new();
            var res = submarine.Sonar.GetNumOfDangerAreas(_repository.GetVentRanges(1));
            Assert.AreEqual(12, res);
        }

        [TestMethod]
        public void Day6Step1()
        {
            Submarine submarine = new();
            var res = submarine.Oceanography.SimulateLanternFishGrowth(_repository.GetLanternFishColony(), 80);
            Assert.AreEqual(5934, res);
        }
        [TestMethod]
        public void Day6Step2()
        {
            Submarine submarine = new();
            var res = submarine.Oceanography.SimulateLanternFishGrowth(_repository.GetLanternFishColony(), 256);
            Assert.AreEqual(26984457539, res);
        }

        [TestMethod]
        public void Day7Step1()
        {
            Submarine submarine = new();
            var res = submarine.Oceanography.OptimizeCrabFuelUseage(_repository.GetCrabSubmarinePositions(), 1);
            Assert.AreEqual(37, res);
        }
        [TestMethod]
        public void Day7Step2()
        {
            Submarine submarine = new();
            var res = submarine.Oceanography.OptimizeCrabFuelUseage(_repository.GetCrabSubmarinePositions(), 2);
            Assert.AreEqual(168, res);
        }

    }
}