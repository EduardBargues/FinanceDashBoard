using CandleTimeSeriesAnalysis;
using System;

namespace Model.ClassificationDayMethods
{
    public interface IDailyClassificationMethod
    {
        string Name { get; }
        Tendency Classify(DateTime day, CandleTimeSeries series);
    }
}
