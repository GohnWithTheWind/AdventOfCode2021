using AdventOfCode.SubmarineAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Diagnostics;

namespace AdventOfCode.Interfaces
{
    public interface IRepository
    {
        List<int> GetDescentData();
        List<NavigationInput> GetNavigationData();
        List<string> GetDiagnosticsData();
        List<int> GetDrawnBingoNumbers();
        List<BingoBoard> GetBingoBoards();
        List<VentRange> GetVentRanges(int includeDiagonal);
        List<int> GetLanternFishColony();
        List<int> GetCrabSubmarinePositions();
        List<Display> GetDisplayData();
        HeightMap GetHeightMap();
    }
}
