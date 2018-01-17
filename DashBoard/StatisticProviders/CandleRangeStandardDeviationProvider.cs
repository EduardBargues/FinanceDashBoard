using CandleTimeSeriesAnalysis;
using CommonUtils;
using System.Collections.Generic;
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
            List<Candle> upCandles = series.Candles
                .Where(candle => candle.GoesUp)
                .ToList();
            List<Candle> downCandles = series.Candles
                .Where(candle => candle.GoesDown)
                .ToList();

            return new Statistic("Candle rande St. Dev."
                , upCandles.Any() ? upCandles.Deviation(candle => candle.Range) : 0
                , downCandles.Any() ? downCandles.Deviation(candle => candle.Range) : 0);
        }
    }
}
