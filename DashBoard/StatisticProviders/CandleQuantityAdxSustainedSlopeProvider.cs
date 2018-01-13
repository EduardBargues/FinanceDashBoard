using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace DashBoard.StatisticProviders
{
    public class CandleQuantityAdxSustainedSlopeProvider : IStatisticProvider
    {
        private readonly TimeSeries adx;
        private readonly TimeSeries diPlus;
        private readonly TimeSeries diMinus;

        public CandleQuantityAdxSustainedSlopeProvider(TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            this.adx = adx;
            this.diPlus = diPlus;
            this.diMinus = diMinus;
        }

        public Statistic GetStatistic()
        {
            (List<IGrouping<int, DateValue>> upGroups
                , List<IGrouping<int, DateValue>> downGroups) = ProvidersUtils.GetGroupedPatches(adx, diPlus, diMinus);

            IGrouping<int, DateValue> upGroupMaxCandles = upGroups.Any()
                ? upGroups.MaxBy(group => group.Count())
                : null;
            double upValue = upGroupMaxCandles?.Count() ?? 0;
            IGrouping<int, DateValue> downGroupMaxCandles = downGroups.Any()
                ? downGroups.MaxBy(group => group.Count())
                : null;
            double downValue = downGroupMaxCandles?.Count() ?? 0;

            return new Statistic("# Candle Adx positive slope"
                , upValue
                , downValue);
        }


    }
}
