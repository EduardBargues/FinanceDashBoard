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
        public string PriceVariation => (candle.Close - candle.Open).ToString("N2");
        public string BodyRangeRatio => (candle.Body / (candle.Range > 0 ? candle.Range : 1)).ToString("N2");
        public TendencyType Tendency { get; }
    }
}
