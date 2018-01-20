using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Presenter.PlotViewProviders
{
    public class CandleBodyDataProvider : I1DimensionDataProvider
    {
        private readonly CandleTimeSeries series;

        public CandleBodyDataProvider(CandleTimeSeries series)
        {
            this.series = series;
        }
        public IEnumerable<double> GetUpData()
        {
            return series.Candles
                .Where(candle => candle.GoesUp)
                .Select(candle => candle.Body);
        }

        public IEnumerable<double> GetDownData()
        {
            return series.Candles
                .Where(candle => candle.GoesDown)
                .Select(candle => candle.Body);
        }
    }
}
