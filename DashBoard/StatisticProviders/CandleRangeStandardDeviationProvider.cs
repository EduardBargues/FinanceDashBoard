using CandleTimeSeriesAnalysis;
using CommonUtils;
using System.Linq;

namespace DashBoard.StatisticProviders
{
    public class CandleRangeStandardDeviationProvider : IStatisticProvider
    {
        private readonly CandleTimeSeries series;

        public CandleRangeStandardDeviationProvider(CandleTimeSeries series)
        {
            this.series = series;
        }

        public Statistic GetStatistic()
        {
            return new Statistic("Candle rande St. Dev."
                , series.Candles
                    .Where(candle => candle.GoesUp)
                    .Deviation(candle => candle.Range)
                , series.Candles
                    .Where(candle => candle.GoesDown)
                    .Deviation(candle => candle.Range));
        }
    }
}
