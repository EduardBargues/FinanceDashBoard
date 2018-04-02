using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Drawing;
using TimeSeriesAnalysis;

namespace Presenter.Generic
{
    public interface ISeriesIndicatorView
    {
        void LoadData(CandleTimeSeries series, TimeSeries buySeries, TimeSeries sellSeries, IEnumerable<(TimeSeries, Color)> indicators);
    }
}
