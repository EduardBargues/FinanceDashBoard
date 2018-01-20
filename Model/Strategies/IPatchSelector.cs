using System;
using System.Collections.Generic;
using TimeSeriesAnalysis;

namespace Model.Strategies
{
    public interface IPatchSelector
    {
        IEnumerable<IEnumerable<DateTime>> GetGoodPatches(TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus);
    }
}