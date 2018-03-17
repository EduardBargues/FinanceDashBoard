using CandleTimeSeriesAnalysis;
using Model.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace Model.StatisticProviders
{
    public static class ProvidersUtils
    {
        private static readonly IPatchSelector Selector = new DxAndAdxPatchSelector(0.5);

        public static IEnumerable<IEnumerable<DateTime>> GetGroupedPatches(TimeSeries dx, TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            IEnumerable<IEnumerable<DateTime>> strongTendencyGroups = Selector.GetGoodPatches(dx, adx, diPlus, diMinus);
            return strongTendencyGroups;
        }

        public static bool IsUpTendency(IEnumerable<DateTime> g, TimeSeries diPlus, TimeSeries diMinus)
        {
            List<DateTime> dates = g
                .ToList();
            DateTime firstDate = dates.Min(date => date);
            return diPlus[firstDate] > diMinus[firstDate];
        }

        public static bool IsDownTendency(IEnumerable<DateTime> g, TimeSeries diPlus, TimeSeries diMinus)
        {
            List<DateTime> dates = g
                .ToList();
            DateTime firstDate = dates.Min(date => date);
            return diPlus[firstDate] < diMinus[firstDate];
        }

        public static double GetPriceVariation(List<DateTime> g, CandleTimeSeries series, bool upTendency = true) => upTendency
                ? series[g.Max(date => date)].Close - series[g.Min(date => date)].Close
                : series[g.Max(date => date)].Close - series[g.Min(date => date)].Close;

    }
}
