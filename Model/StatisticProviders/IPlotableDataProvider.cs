using System.Collections.Generic;

namespace Model.StatisticProviders
{
    public interface IPlotableDataProvider
    {
        IEnumerable<(double, double)> GetUpData();
        IEnumerable<(double, double)> GetDownData();
    }
}
