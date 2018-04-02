using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Drawing;
using TimeSeriesAnalysis;

namespace Presenter.Generic
{
    public interface ISeriesIndicatorView
    {
        void LoadData(
            CandleTimeSeries series,
            IEnumerable<(TimeSeries, Color)> indicators,
            TimeSeries buySeries = null,
            TimeSeries sellSeries = null,
            TimeSeries slowMovingAverage = null,
            TimeSeries mediumMovingAverage = null,
            TimeSeries fastMovingAverage = null);
    }
}
