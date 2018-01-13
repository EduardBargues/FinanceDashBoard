using CandleTimeSeriesAnalysis;
using CommonUtils;
using System.Linq;

namespace DashBoard.StatisticProviders
{
    public class CandleBodyStandardDeviationProvider : IStatisticProvider
    {
        private readonly CandleTimeSeries series;

        public CandleBodyStandardDeviationProvider(CandleTimeSeries series)
        {
            this.series = series;
        }
        public Statistic GetStatistic()
        {
            return new Statistic("Candle Body St. Dev."
                , series.Candles
                    .Where(candle => candle.GoesUp)
                    .Deviation(candle => candle.Body)
                , series.Candles
                    .Where(candle => candle.GoesDown)
                    .Deviation(candle => candle.Body));
        }
    }
}
