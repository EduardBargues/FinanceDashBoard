using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace Model.StatisticProviders
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
            List<IEnumerable<DateTime>> groups = ProvidersUtils.GetGroupedPatches(adx, diPlus, diMinus).ToList();

            List<IEnumerable<DateTime>> upGroups = groups
                .Where(g => ProvidersUtils.IsUpTendency(g, diPlus, diMinus))
                .ToList();
            IEnumerable<DateTime> upGroupMaxCandles = upGroups.Any()
                ? upGroups.MaxBy(group => group.Count())
                : null;
            double upValue = upGroupMaxCandles?.Count() ?? 0;

            List<IEnumerable<DateTime>> downGroups = groups
                .Where(g => ProvidersUtils.IsDownTendency(g, diPlus, diMinus))
                .ToList();
            IEnumerable<DateTime> downGroupMaxCandles = downGroups.Any()
                ? downGroups.MaxBy(group => group.Count())
                : null;
            double downValue = downGroupMaxCandles?.Count() ?? 0;

            return new Statistic("# Candle Adx positive slope"
                , upValue
                , downValue);
        }
    }
}
