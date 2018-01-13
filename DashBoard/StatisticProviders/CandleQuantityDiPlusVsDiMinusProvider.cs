using System.Linq;
using TimeSeriesAnalysis;

namespace DashBoard.StatisticProviders
{
    public class CandleQuantityDiPlusVsDiMinusProvider : IStatisticProvider
    {
        private readonly TimeSeries diPlus;
        private readonly TimeSeries diMinus;

        public CandleQuantityDiPlusVsDiMinusProvider(TimeSeries diPlus, TimeSeries diMinus)
        {
            this.diPlus = diPlus;
            this.diMinus = diMinus;
        }

        public Statistic GetStatistic()
        {
            return new Statistic("# Candle DI+ vs. DI-"
                , diPlus.Values.Count(dv => dv.Value > diMinus[dv.Date])
                , diPlus.Values.Count(dv => dv.Value < diMinus[dv.Date]));
        }
    }
}
