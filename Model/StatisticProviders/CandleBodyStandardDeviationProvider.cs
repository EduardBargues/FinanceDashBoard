using CandleTimeSeriesAnalysis;
using CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace Model.StatisticProviders
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
            List<Candle> upCandles = series.Candles
                .Where(candle => candle.GoesUp)
                .ToList();
            List<Candle> downCandles = series.Candles
                .Where(candle => candle.GoesDown)
                .ToList();
            return new Statistic("Candle Body St. Dev."
                , upCandles.Any() ? upCandles.Deviation(candle => candle.Body) : 0
                , downCandles.Any() ? downCandles.Deviation(candle => candle.Body) : 0);
        }
    }
}
