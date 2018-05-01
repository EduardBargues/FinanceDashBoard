using CandleTimeSeriesAnalysis;
using CandleTimeSeriesAnalysis.Indicators;
using System;

namespace Model.ClassificationDayMethods
{
    public class AdxDailyClassificationMethod : IDailyClassificationMethod
    {
        private readonly int directionalIndexPeriod;
        private readonly int averagePeriod;
        private readonly double adxMinimumValue;

        public AdxDailyClassificationMethod(int directionalIndexPeriod,
                                            int averagePeriod,
                                            double adxMinimumValue)
        {
            this.directionalIndexPeriod = directionalIndexPeriod;
            this.averagePeriod = averagePeriod;
            this.adxMinimumValue = adxMinimumValue;
        }

        public string Name { get; } = "Average Directional Index";

        public Tendency Classify(DateTime day, CandleTimeSeries series)
        {
            AverageDirectionalMovementIndex adx = AverageDirectionalMovementIndex.Create(directionalIndexPeriod, averagePeriod);
            double adxValue = adx.GetValueAt(series, day.Date);
            double adxSlope = 0;
            DateTime previousDay = day.Date.Subtract(TimeSpan.FromDays(1));
            bool containsPreviousDayCandle = series.ContainsCandleAt(previousDay);
            if (containsPreviousDayCandle)
            {
                double previousDayAdxValue = adx.GetValueAt(series, previousDay);
                adxSlope = (adxValue - previousDayAdxValue) / 1;
            }
            DirectionalMovementPlus diPlus = DirectionalMovementPlus.Create();
            double diPlusValue = diPlus.GetValueAt(series, day.Date);
            DirectionalMovementMinus diMinus = DirectionalMovementMinus.Create();
            double diMinusValue = diMinus.GetValueAt(series, day.Date);

            if (adxValue >= adxMinimumValue &&
                adxSlope >= 0)
                return diPlusValue > diMinusValue
                    ? Tendency.Up
                    : Tendency.Down;
            return Tendency.Range;
        }
    }
}
