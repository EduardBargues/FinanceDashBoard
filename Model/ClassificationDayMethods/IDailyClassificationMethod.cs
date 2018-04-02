using CandleTimeSeriesAnalysis;
using System;

namespace Model.ClassificationDayMethods
{
    public interface IDailyClassificationMethod
    {
        string Name { get; }
        TendencyType Classify(DateTime day, CandleTimeSeries series);
    }
}
