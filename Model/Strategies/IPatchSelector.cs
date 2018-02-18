using System;
using System.Collections.Generic;
using TimeSeriesAnalysis;

namespace Model.Strategies
{
    public interface IPatchSelector
    {
        IEnumerable<IEnumerable<DateTime>> GetGoodPatches(TimeSeries dx, TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus);
    }
}