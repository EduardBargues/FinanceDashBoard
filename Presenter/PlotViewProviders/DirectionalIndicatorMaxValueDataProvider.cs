using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace Presenter.PlotViewProviders
{
    public class DirectionalIndicatorMaxValueDataProvider : I1DimensionDataProvider
    {
        private TimeSeries diPlus;
        private TimeSeries diMinus;

        public DirectionalIndicatorMaxValueDataProvider(TimeSeries diPlus, TimeSeries diMinus)
        {
            this.diPlus = diPlus;
            this.diMinus = diMinus;
        }
        public IEnumerable<double> GetUpData() => diPlus.Values
                .Select(dv => dv.Value);

        public IEnumerable<double> GetDownData() => diMinus.Values
                .Select(dv => dv.Value);
    }
}
