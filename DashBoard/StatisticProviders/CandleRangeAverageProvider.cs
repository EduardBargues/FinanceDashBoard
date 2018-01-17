using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace DashBoard.StatisticProviders
{
    public class CandleRangeAverageProvider : IStatisticProvider
    {
        private readonly CandleTimeSeries series;

        public CandleRangeAverageProvider(CandleTimeSeries series)
        {
            this.series = series;
        }

        public Statistic GetStatistic()
        {
            List<Candle> upCandles = series.Candles
                .Where(candle => candle.GoesUp)
                .ToList();
            List<Candle> downCandles = series.Candles
                .Where(candle => candle.GoesDown)
                .ToList();
            return new Statistic("Candle range average"
                , upCandles.Any() ? upCandles.Average(candle => candle.Range) : 0
                , downCandles.Any() ? downCandles.Average(candle => candle.Range) : 0);
        }
    }
}
