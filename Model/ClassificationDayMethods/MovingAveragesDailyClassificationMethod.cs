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

        public TendencyType Classify(DateTime day, CandleTimeSeries candleSeries)
        {
            TimeSeries series = GetTimeSeries(candleSeries);
            double slowMaValue = GetMovingAverageValue(series, day, slowMovingAveragePeriod);
            double mediumMaValue = GetMovingAverageValue(series, day, mediumMovingAveragePeriod);
            double fastMaValue = GetMovingAverageValue(series, day, fastMovingAveragePeriod);

            List<double> values = new List<double> { slowMaValue, mediumMaValue, fastMaValue };

            if (values.IsSorted())
                return TendencyType.Up;
            return values.IsDescendantlySorted()
                ? TendencyType.Down
                : TendencyType.Range;
        }

        private TimeSeries GetTimeSeries(CandleTimeSeries series) => series.Candles
            .Select(candle => new DateValue(candle.Start, candle.Close))
            .ToTimeSeries();

        private double GetMovingAverageValue(TimeSeries series, DateTime day, int movingAveragePeriod)
        {
            int dayIndex = series.GetIndex(day.Date);
            double movingAverage = dayIndex.GetIntegersTo(dayIndex - movingAveragePeriod)
                .Where(index => index >= 0)
                .Select(series.GetValue)
                .WeightedAverage((value, index) => value,
                                 (value, index) => index + 1);

            return movingAverage;
        }
    }
}