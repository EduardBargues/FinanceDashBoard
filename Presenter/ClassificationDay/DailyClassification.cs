using CandleTimeSeriesAnalysis;
using Model;
using System;

namespace Presenter.ClassificationDay
{
    public class DailyClassification
    {
        private readonly Candle candle;
        private double tolerance = 0;//;.25;

        public DailyClassification(Candle candle,
                                   Tendency classification)
        {
            this.candle = candle;
            Tendency = classification;
        }

        public DateTime Day => candle.Start.Date;

        private double priceVariation => candle.Close - candle.Open;
        public string PriceVariation => priceVariation.ToString("N2");

        private double bodyRangeRatio => candle.Body / (candle.Range > 0 ? candle.Range : 1);
        public string BodyRangeRatio => bodyRangeRatio.ToString("N2");

        public Tendency Tendency { get; }

        private Tendency TheoreticalTendency
        {
            get
            {
                if (candle.Close > candle.Open &&
                    bodyRangeRatio > tolerance)
                    return Tendency.Up;
                if (candle.Open > candle.Close &&
                    bodyRangeRatio > tolerance)
                    return Tendency.Down;
                return Tendency.Range;
            }
        }

        public bool Success => TheoreticalTendency == Tendency;
    }
}
