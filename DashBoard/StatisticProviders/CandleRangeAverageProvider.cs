using CandleTimeSeriesAnalysis;
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
            return new Statistic("Candle range average"
                , series.Candles
                    .Where(candle => candle.GoesUp)
                    .Average(candle => candle.Range)
                , series.Candles
                    .Where(candle => candle.GoesDown)
                    .Average(candle => candle.Range));
        }
    }
}
