using CandleTimeSeriesAnalysis;
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
            return new Statistic("Candle average body"
                , series.Candles
                    .Where(candle => candle.GoesUp)
                    .Average(candle => candle.Body)
                , series.Candles
                    .Where(candle => candle.GoesDown)
                    .Average(candle => candle.Body));
        }
    }
}
