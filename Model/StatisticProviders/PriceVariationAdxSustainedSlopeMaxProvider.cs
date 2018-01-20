using CandleTimeSeriesAnalysis;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace Model.StatisticProviders
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
            List<IEnumerable<DateTime>> groups = ProvidersUtils.GetGroupedPatches(adx, diPlus, diMinus)
                .ToList();
            List<IEnumerable<DateTime>> upGroups = groups
                .Where(g => ProvidersUtils.IsUpTendency(g, diPlus, diMinus))
                .ToList();
            IEnumerable<DateTime> upGroupMaxCandles = upGroups.Any()
                ? upGroups.MaxBy(group => ProvidersUtils.GetPriceVariation(group.ToList(), candleSeries))
                : null;
            List<IEnumerable<DateTime>> downGroups = groups
                .Where(g => ProvidersUtils.IsDownTendency(g, diPlus, diMinus))
                .ToList();
            IEnumerable<DateTime> downGroupMaxCandles = downGroups.Any()
                ? downGroups.MaxBy(group => -ProvidersUtils.GetPriceVariation(group.ToList(), candleSeries, upTendency: false))
                : null;

            double upPriceVariation = upGroupMaxCandles != null
                ? ProvidersUtils.GetPriceVariation(upGroupMaxCandles.ToList(), candleSeries)
                : 0;
            double downPriceVariation = downGroupMaxCandles != null
                ? ProvidersUtils.GetPriceVariation(downGroupMaxCandles.ToList(), candleSeries, upTendency: false)
                : 0;
            return new Statistic("Price variation adx positive slope"
                , upPriceVariation
                , downPriceVariation);
        }
    }
}
