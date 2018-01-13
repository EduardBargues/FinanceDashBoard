using System.Collections.Generic;

namespace DashBoard.StatisticProviders
{
    public interface IPlotableDataProvider
    {
        IEnumerable<(double, double)> GetUpData();
        IEnumerable<(double, double)> GetDownData();
    }
}
