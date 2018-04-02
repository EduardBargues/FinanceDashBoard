using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Drawing;
using TimeSeriesAnalysis;

namespace Presenter.Generic
{
    public class SeriesIndicatorPresenter
    {
        private readonly ISeriesIndicatorView view;

        public SeriesIndicatorPresenter(ISeriesIndicatorView view)
        {
            this.view = view;
        }

        public void LoadData(CandleTimeSeries series, TimeSeries buySeries, TimeSeries sellSeries, IEnumerable<(TimeSeries, Color)> indicators)
        {
            view.LoadData(series, buySeries, sellSeries, indicators);
        }
    }
}
