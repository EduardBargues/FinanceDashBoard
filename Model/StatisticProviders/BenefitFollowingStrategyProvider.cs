using CandleTimeSeriesAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace Model.StatisticProviders
{
    public class BenefitFollowingStrategyProvider : IStatisticProvider
    {
        private readonly CandleTimeSeries series;
        private TimeSeries adx;
        private TimeSeries diPlus;
        private TimeSeries diMinus;

        public BenefitFollowingStrategyProvider(CandleTimeSeries series, TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            this.series = series;
            this.adx = adx;
            this.diPlus = diPlus;
            this.diMinus = diMinus;
        }

        public Statistic GetStatistic()
        {
            List<IEnumerable<DateTime>> patches = ProvidersUtils.GetGroupedPatches(adx, diPlus, diMinus)
                .ToList();
            double benefitUp = patches
                .Where(patch => ProvidersUtils.IsUpTendency(patch, diPlus, diMinus))
                .Sum(patch => ProvidersUtils.GetPriceVariation(patch.ToList(), series));
            double benefitDown = patches
                .Where(patch => ProvidersUtils.IsDownTendency(patch, diPlus, diMinus))
                .Sum(patch => -ProvidersUtils.GetPriceVariation(patch.ToList(), series, false));

            return new Statistic("Benefit", benefitUp, benefitDown);
        }
    }
}
