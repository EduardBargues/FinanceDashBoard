using System.Linq;
using TimeSeriesAnalysis;

namespace DashBoard.StatisticProviders
{
    public class DirectionalIndicatorMaxValueProvider : IStatisticProvider
    {
        private readonly TimeSeries diPlus;
        private readonly TimeSeries diMinus;

        public DirectionalIndicatorMaxValueProvider(TimeSeries diPlus, TimeSeries diMinus)
        {
            this.diPlus = diPlus;
            this.diMinus = diMinus;
        }
        public Statistic GetStatistic()
        {
            return new Statistic("Di max value"
                , diPlus.Values.Max(dv => dv.Value)
                , diMinus.Values.Max(dv => dv.Value));
        }
    }
}
