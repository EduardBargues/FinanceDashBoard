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
        private CandleTimeSeries series;
        private TimeSeries adx;
        private TimeSeries diPlus;
        private TimeSeries diMinus;

        public CandleQuantityAdxSustainedSlopeDataProvider(CandleTimeSeries series, TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            this.series = series;
            this.adx = adx;
            this.diPlus = diPlus;
            this.diMinus = diMinus;
        }

        public IEnumerable<(double, double)> GetUpData()
        {
            (List<IGrouping<int, DateValue>> upGroups
                , List<IGrouping<int, DateValue>> downGroups) = ProvidersUtils.GetGroupedPatches(adx, diPlus, diMinus);

            return upGroups
                .Select(group =>
                {
                    DateTime lastDate = group.Max(dv => dv.Date);
                    DateTime firstDate = group.Min(dv => dv.Date);
                    Candle lastCandle = series[lastDate];
                    Candle firstCandle = series[firstDate];
                    return ((double)group.Count(), lastCandle.Min - firstCandle.Max);
                });
        }

        public IEnumerable<(double, double)> GetDownData()
        {
            (List<IGrouping<int, DateValue>> upGroups
                , List<IGrouping<int, DateValue>> downGroups) = ProvidersUtils.GetGroupedPatches(adx, diPlus, diMinus);

            return downGroups
                .Select(group =>
                {
                    DateTime lastDate = group.Max(dv => dv.Date);
                    DateTime firstDate = group.Min(dv => dv.Date);
                    Candle lastCandle = series[lastDate];
                    Candle firstCandle = series[firstDate];
                    return ((double)group.Count(), lastCandle.Min - firstCandle.Max);
                });
        }
    }
}
