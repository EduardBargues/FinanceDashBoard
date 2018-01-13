using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace DashBoard.PlotViewProviders
{
    public class CandleRangeDataProvider : I1DimensionDataProvider
    {
        private readonly CandleTimeSeries series;

        public CandleRangeDataProvider(CandleTimeSeries series)
        {
            this.series = series;
        }
        public IEnumerable<double> GetUpData()
        {
            return series.Candles
                .Where(candle => candle.GoesUp)
                .Select(candle => candle.Range);
        }

        public IEnumerable<double> GetDownData()
        {
            return series.Candles
                .Where(candle => candle.GoesDown)
                .Select(candle => candle.Range);
        }
    }
}
