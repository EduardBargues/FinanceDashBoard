using CandleTimeSeriesAnalysis;
using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace Model.ClassificationDayMethods
{
    public class MovingAveragesDailyClassificationMethod : IDailyClassificationMethod
    {
        private readonly int slowMovingAveragePeriod;
        private readonly int mediumMovingAveragePeriod;
        private readonly int fastMovingAveragePeriod;

        public MovingAveragesDailyClassificationMethod(int slowMovingAveragePeriod,
                                                     int mediumMovingAveragePeriod,
                                                     int fastMovingAveragePeriod)
        {
            this.slowMovingAveragePeriod = slowMovingAveragePeriod;
            this.mediumMovingAveragePeriod = mediumMovingAveragePeriod;
            this.fastMovingAveragePeriod = fastMovingAveragePeriod;
        }

        public string Name { get; } = "Moving Averages";

        public Tendency Classify(DateTime day, CandleTimeSeries candleSeries)
        {
            TimeSeries series = GetTimeSeries(candleSeries);
            DateTime dayBefore = day.AddDays(-1);
            double slowMaValue = series.GetExponentialMovingAverageAt(dayBefore, slowMovingAveragePeriod);
            double mediumMaValue = series.GetExponentialMovingAverageAt(dayBefore, mediumMovingAveragePeriod);
            double fastMaValue = series.GetExponentialMovingAverageAt(dayBefore, fastMovingAveragePeriod);
            List<double> values = new List<double> { slowMaValue, mediumMaValue, fastMaValue };

            if (values.IsSorted())
                return Tendency.Up;
            return values.IsDescendantlySorted()
                ? Tendency.Down
                : Tendency.Range;
        }

        private TimeSeries GetTimeSeries(CandleTimeSeries series) => series.Candles
            .Select(candle => new DateValue(candle.Start, candle.Close))
            .ToTimeSeries();
    }
}