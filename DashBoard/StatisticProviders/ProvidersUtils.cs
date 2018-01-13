using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace DashBoard.StatisticProviders
{
    public static class ProvidersUtils
    {
        public static (List<IGrouping<int, DateValue>>, List<IGrouping<int, DateValue>>) GetGroupedPatches(TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            List<IGrouping<int, DateValue>> strongTendencyGroups = adx.Values
                .Where((dv, index) => index > 0 &&
                                      index < adx.Count() - 1)
                .GroupAdjacent(dv => Math.Sign(adx.GetDerivative(dv.Date)))
                .Where(group => group.Key > 0)
                .ToList();
            List<IGrouping<int, DateValue>> upGroups = new List<IGrouping<int, DateValue>>();
            List<IGrouping<int, DateValue>> downGroups = new List<IGrouping<int, DateValue>>();
            foreach (IGrouping<int, DateValue> group in strongTendencyGroups)
            {
                int upCandles = group.Count(dv => diPlus[dv.Date] > diMinus[dv.Date]);
                int downCandles = group.Count(dv => diPlus[dv.Date] < diMinus[dv.Date]);
                if (upCandles > downCandles) upGroups.Add(group);
                else downGroups.Add(group);
            }
            return (upGroups, downGroups);
        }
    }
}
