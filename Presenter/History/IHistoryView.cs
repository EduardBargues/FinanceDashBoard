using CandleTimeSeriesAnalysis;
using System;
using System.Collections.Generic;
using System.Drawing;
using TimeSeriesAnalysis;

namespace Presenter.History
{
    public interface IHistoryView
    {
        void LoadData(CandleTimeSeries series, IEnumerable<(TimeSeries, Color)> indicators);
        event Action LoadDataRequest;
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        int IndicatorPeriod { get; }
        int SmoothingPeriod { get; }
    }
}
