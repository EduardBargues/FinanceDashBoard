using CandleTimeSeriesAnalysis;
using System.Linq;

namespace Model.StatisticProviders
{
    public class CandleQuantityProvider : IStatisticProvider
    {
        private readonly CandleTimeSeries series;

        public CandleQuantityProvider(CandleTimeSeries series)
        {
            this.series = series;
        }
        public Statistic GetStatistic()
        {
            Statistic statistic = new Statistic("Candle #"
                , series.Candles.Count(candle => candle.GoesUp)
                , series.Candles.Count(candle => candle.GoesDown));
            return statistic;
        }
    }
}
