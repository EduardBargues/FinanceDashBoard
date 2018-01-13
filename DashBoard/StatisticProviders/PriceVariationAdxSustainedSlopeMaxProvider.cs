using CandleTimeSeriesAnalysis;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace DashBoard.StatisticProviders
{
    public class PriceVariationAdxSustainedSlopeMaxProvider : IStatisticProvider
    {
        private readonly CandleTimeSeries candleSeries;
        private readonly TimeSeries adx;
        private readonly TimeSeries diPlus;
        private readonly TimeSeries diMinus;

        public PriceVariationAdxSustainedSlopeMaxProvider(CandleTimeSeries candleSeries, TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            this.candleSeries = candleSeries;
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
            IGrouping<int, DateValue> downGroupMaxCandles = downGroups.Any()
                ? downGroups.MaxBy(group => group.Count())
                : null;

            double upPriceVariation = upGroupMaxCandles != null
                ? candleSeries[upGroupMaxCandles.Max(dv => dv.Date)].Min - candleSeries[upGroupMaxCandles.Min(dv => dv.Date)].Max
                : 0;
            double downPriceVariation = downGroupMaxCandles != null
                ? candleSeries[downGroupMaxCandles.Max(dv => dv.Date)].Min - candleSeries[downGroupMaxCandles.Min(dv => dv.Date)].Max
                : 0;
            return new Statistic("Price variation adx positive slope"
                , upPriceVariation
                , downPriceVariation);
        }
    }
}
