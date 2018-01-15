using DashBoard.StatisticProviders;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace DashBoard.Strategies
{
    public class PatchSelector : IPatchSelector
    {
        public IEnumerable<IEnumerable<DateTime>> GetGoodPatches(TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            IEnumerable<IGrouping<int, DateValue>> patches = adx.Values
                .Where((dv, index) => index > 0 &&
                                      index < adx.Count() - 1)
                .GroupAdjacent(dv => Math.Sign(adx.GetDerivative(dv.Date)))
                .Where(group => @group.Key > 0);

            foreach (IGrouping<int, DateValue> patch in patches)
            {
                List<DateTime> dateValues = patch
                    .Where(dv => adx[dv.Date] > 0.25)
                    .Select(dv => dv.Date)
                    .ToList();
                if (dateValues.Any())
                    yield return dateValues;
            }
        }
    }
}
