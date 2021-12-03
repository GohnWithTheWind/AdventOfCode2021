using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Interfaces
{
    public interface IRepository
    {
        List<int> GetDescentData();
        List<NavigationInput> GetNavigationData();
        List<string> GetDiagnosticsData();
    }
}
