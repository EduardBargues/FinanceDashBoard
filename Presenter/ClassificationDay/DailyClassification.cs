using CandleTimeSeriesAnalysis;
using Model;
using System;

namespace Presenter.ClassificationDay
{
    public class DailyClassification
    {
        private readonly Candle candle;
        public DailyClassification(Candle candle,
                                   TendencyType classification)
        {
            this.candle = candle;
            Tendency = classification;
        }

        public DateTime Day => candle.Start.Date;
        public double PriceVariation => candle.Close - candle.Open;
        public double BodyRangeRatio => candle.Body / (candle.Range > 0 ? candle.Range : 1);
        public TendencyType Tendency { get; }
    }
}
