using System.Collections.Generic;

namespace DashBoard.PlotViewProviders
{
    public interface I1DimensionDataProvider
    {
        IEnumerable<double> GetUpData();
        IEnumerable<double> GetDownData();
    }
}