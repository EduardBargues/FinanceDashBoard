using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace DashBoard.PlotViewProviders
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
        public IEnumerable<double> GetUpData()
        {
            return diPlus.Values
                .Select(dv => dv.Value);
        }

        public IEnumerable<double> GetDownData()
        {
            return diMinus.Values
                .Select(dv => dv.Value);
        }
    }
}
