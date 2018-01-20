using System.Collections.Generic;

namespace Presenter.PlotViewProviders
{
    public interface I1DimensionDataProvider
    {
        IEnumerable<double> GetUpData();
        IEnumerable<double> GetDownData();
    }
}