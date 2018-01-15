using CandleTimeSeriesAnalysis;
using DashBoard.StatisticProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace DashBoard.PlotViewProviders
{
    public class CandleQuantityAdxSustainedSlopeDataProvider : I2DimensionsDataProvider
    {
        private readonly CandleTimeSeries series;
        private readonly TimeSeries adx;
        private readonly TimeSeries diPlus;
        private readonly TimeSeries diMinus;

        public CandleQuantityAdxSustainedSlopeDataProvider(CandleTimeSeries series, TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            this.series = series;
            this.adx = adx;
            this.diPlus = diPlus;
            this.diMinus = diMinus;
        }

        public IEnumerable<(double, double)> GetUpData()
        {
            IEnumerable<IEnumerable<DateTime>> groups = ProvidersUtils.GetGroupedPatches(adx, diPlus, diMinus);

            return groups
                .Where(g => ProvidersUtils.IsUpTendency(g, diPlus, diMinus))
                .Select(group =>
                {
                    List<DateTime> dates = group.ToList();
                    double priceVariation = ProvidersUtils.GetPriceVariation(dates, series, true);
                    return ((double)dates.Count, priceVariation);
                });
        }

        public IEnumerable<(double, double)> GetDownData()
        {
            IEnumerable<IEnumerable<DateTime>> groups = ProvidersUtils.GetGroupedPatches(adx, diPlus, diMinus);

            return groups
                .Where(g => ProvidersUtils.IsDownTendency(g, diPlus, diMinus))
                .Select(group =>
                {
                    List<DateTime> dates = group.ToList();
                    double priceVariation = ProvidersUtils.GetPriceVariation(dates, series, true);
                    return ((double)dates.Count, priceVariation);
                });
        }
    }
}
