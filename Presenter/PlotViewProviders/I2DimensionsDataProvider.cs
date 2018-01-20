using System.Collections.Generic;

namespace Presenter.PlotViewProviders
{
    public interface I2DimensionsDataProvider
    {
        IEnumerable<(double, double)> GetUpData();
        IEnumerable<(double, double)> GetDownData();
    }
}
