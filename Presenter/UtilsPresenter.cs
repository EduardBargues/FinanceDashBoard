using CandleTimeSeriesAnalysis;
using CandleTimeSeriesAnalysis.Indicators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TimeSeriesAnalysis;

namespace Presenter
{
    public static class UtilsPresenter
    {
        public static string DiPlusIndicatorName = "DI+";
        public static string DiMinusIndicatorName = "DI-";
        public static string DxIndicatorName = "DX";
        public static string AdxIndicatorName = "ADX";

        public static IEnumerable<(TimeSeries, Color)> GetIndicators(CandleTimeSeries series
            , int indicatorPeriod
            , int smoothingPeriod
            , DateTime? startDate = null)
        {
            if (startDate == null)
                startDate = series.Candles.Min(candle => candle.Start);

            DirectionalIndicatorPlus diPlus =
                DirectionalIndicatorPlus.Create(indicatorPeriod);
            TimeSeries diPlusSeries = series.Candles
                .Where((candle, index) => index > 0 &&
                                          candle.Start >= startDate)
                .Select(candle => new DateValue(candle.Start, diPlus[series, candle.Start]))
                .ToTimeSeries(DiPlusIndicatorName);
            yield return (diPlusSeries, Color.Blue);

            DirectionalIndicatorMinus diMinus =
                DirectionalIndicatorMinus.Create(indicatorPeriod);
            TimeSeries diMinusSeries = series.Candles
                .Where((candle, index) => index > 0 &&
                                          candle.Start >= startDate)
                .Select(candle => new DateValue(candle.Start, diMinus[series, candle.Start]))
                .ToTimeSeries(DiMinusIndicatorName);
            yield return (diMinusSeries, Color.Red);

            DirectionalMovementIndex dx = DirectionalMovementIndex.Create(indicatorPeriod);
            TimeSeries dxSeries = series.Candles
                .Where((candle, index) => index > 0 &&
                                          candle.Start >= startDate)
                .Select(candle => new DateValue(candle.Start, dx[series, candle.Start]))
                .ToTimeSeries(DxIndicatorName);
            yield return (dxSeries, Color.DarkSlateGray);

            AverageDirectionalMovementIndex adx =
                AverageDirectionalMovementIndex.Create(indicatorPeriod, smoothingPeriod);
            TimeSeries adxSeries = series.Candles
                .Where((candle, index) => index > 0 &&
                                          candle.Start >= startDate)
                .Select(candle => new DateValue(candle.Start, adx[series, candle.Start]))
                .ToTimeSeries(AdxIndicatorName);
            yield return (adxSeries, Color.DarkGray);
        }
    }
}
