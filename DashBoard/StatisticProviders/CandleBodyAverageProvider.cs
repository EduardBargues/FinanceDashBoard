using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace DashBoard.StatisticProviders
{
    public class CandleBodyAverageProvider : IStatisticProvider
    {
        private readonly CandleTimeSeries series;

        public CandleBodyAverageProvider(CandleTimeSeries s)
        {
            series = s;
        }
        public Statistic GetStatistic()
        {
            List<Candle> upCandles = series.Candles
                .Where(candle => candle.GoesUp)
                .ToList();
            List<Candle> downCandles = series.Candles
                .Where(candle => candle.GoesDown)
                .ToList();
            return new Statistic("Candle average body"
                , upCandles.Any() ? upCandles.Average(candle => candle.Body) : 0
                , downCandles.Any() ? downCandles.Average(candle => candle.Body) : 0);
        }
    }
}
