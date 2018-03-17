using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Presenter.PlotViewProviders
{
    public class CandleRangeDataProvider : I1DimensionDataProvider
    {
        private readonly CandleTimeSeries series;

        public CandleRangeDataProvider(CandleTimeSeries series)
        {
            this.series = series;
        }
        public IEnumerable<double> GetUpData() => series.Candles
                .Where(candle => candle.GoesUp)
                .Select(candle => candle.Range);

        public IEnumerable<double> GetDownData() => series.Candles
                .Where(candle => candle.GoesDown)
                .Select(candle => candle.Range);
    }
}
