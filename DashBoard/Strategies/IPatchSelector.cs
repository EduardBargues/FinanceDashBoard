using System;
using System.Collections.Generic;
using TimeSeriesAnalysis;

namespace DashBoard.StatisticProviders
{
    public interface IPatchSelector
    {
        IEnumerable<IEnumerable<DateTime>> GetGoodPatches(TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus);
    }
}