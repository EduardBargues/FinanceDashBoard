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

        public void LoadData(
            CandleTimeSeries series,
            IEnumerable<(TimeSeries, Color)> indicators,
            TimeSeries buySeries = null,
            TimeSeries sellSeries = null,
            TimeSeries slowMovingAverage = null,
            TimeSeries mediumMovingAverage = null,
            TimeSeries fastMovingAverage = null
            )
        {
            view.LoadData(
                series: series,
                buySeries: buySeries,
                sellSeries: sellSeries,
                indicators: indicators,
                slowMovingAverage: slowMovingAverage,
                mediumMovingAverage: mediumMovingAverage,
                fastMovingAverage: fastMovingAverage
                );
        }
    }
}
