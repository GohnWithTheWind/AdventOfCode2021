using AdventOfCode.Infrastructure;
using AdventOfCode.Interfaces;
using AdventOfCode.SubmarineAggregate;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Diagnostics;
using static AdventOfCode.SubmarineAggregate.Oceanography;

namespace AdventOfCode.Repository
{
    public class TestRepository : IRepository
    {
        private readonly IReader _reader;

        public TestRepository()
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IReader, FileReader>()
            .BuildServiceProvider();

            _reader = serviceProvider.GetRequiredService<IReader>();
        }

        public List<int> GetDescentData()
        {
            return _reader.FileToIntList("InputFiles/Day1/Day1Tests.txt");
        }
        public List<NavigationInput> GetNavigationData()
        {
            return _reader.FileToNavigationList("InputFiles/Day2/Day2Tests.txt");
        }

        public List<string> GetDiagnosticsData()
        {
            return  _reader.FileToStringList("InputFiles/Day3/Day3Tests.txt");
        }

        public List<int> GetDrawnBingoNumbers()
        {
            return _reader.FirstRowToIntList("InputFiles/Day4/Day4Tests.txt");
        }
        public List<BingoBoard> GetBingoBoards()
        {
            return _reader.FileToBingoBoards("InputFiles/Day4/Day4Tests.txt");
        }
        public List<VentRange> GetVentRanges(int includeDiagonal)
        {
            return _reader.FileToVentRanges("InputFiles/Day5/Day5Tests.txt", includeDiagonal);
        }

        public List<int> GetLanternFishColony()
        {
            return _reader.FirstRowToIntList("InputFiles/Day6/Day6Tests.txt");
        }
        public List<int> GetCrabSubmarinePositions()
        {
            return _reader.FirstRowToIntList("InputFiles/Day7/Day7Tests.txt");
        }
        public List<Display> GetDisplayData()
        {
            return _reader.FileToDisplays("InputFiles/Day8/Day8Tests.txt");
        }
        public HeightMap GetHeightMap()
        {
            return _reader.FileToHeightMap("InputFiles/Day9/Day9Tests.txt");
        }
        public List<string> GetSubSystemData()
        {
            return _reader.FileToStringList("InputFiles/Day10/Day10Tests.txt");
        }
        public OctopusGroup GetOctopusStatus()
        {
            return _reader.FileToOctopusGroup("InputFiles/Day11/Day11Tests.txt");
        }

    }
}
