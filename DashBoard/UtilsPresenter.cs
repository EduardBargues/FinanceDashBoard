using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandleTimeSeriesAnalysis;
using CandleTimeSeriesAnalysis.Indicators;
using TimeSeriesAnalysis;

namespace DashBoard
{
    public static class UtilsPresenter
    {
        public static IEnumerable<(TimeSeries, Color)> GetIndicators(CandleTimeSeries series
            , int indicatorPeriod
            , int smoothingPeriod
            , DateTime? startDate = null)
        {
            if (startDate == null)
                startDate = series.Candles.Min(candle => candle.Start);

            DirectionalIndicatorPlus diPlus =
                DirectionalIndicatorPlus.Create(indicatorPeriod, smoothingPeriod);
            TimeSeries diPlusSeries = series.Candles
                .Where((candle, index) => index > 0 &&
                                          candle.Start >= startDate)
                .Select(candle => new DateValue(candle.Start, diPlus[series, candle.Start]))
                .ToTimeSeries("DI+");
            yield return (diPlusSeries, Color.Blue);

            DirectionalIndicatorMinus diMinus =
                DirectionalIndicatorMinus.Create(indicatorPeriod, smoothingPeriod);
            TimeSeries diMinusSeries = series.Candles
                .Where((candle, index) => index > 0 &&
                                          candle.Start >= startDate)
                .Select(candle => new DateValue(candle.Start, diMinus[series, candle.Start]))
                .ToTimeSeries("DI-");
            yield return (diMinusSeries, Color.Red);

            DirectionalMovementIndex dx = DirectionalMovementIndex.Create(indicatorPeriod, smoothingPeriod);
            TimeSeries dxSeries = series.Candles
                .Where((candle, index) => index > 0 &&
                                          candle.Start >= startDate)
                .Select(candle => new DateValue(candle.Start, dx[series, candle.Start]))
                .ToTimeSeries("DX");
            yield return (dxSeries, Color.DarkSlateGray);

            AverageDirectionalMovementIndex adx =
                AverageDirectionalMovementIndex.Create(indicatorPeriod, smoothingPeriod);
            TimeSeries adxSeries = series.Candles
                .Where((candle, index) => index > 0 &&
                                          candle.Start >= startDate)
                .Select(candle => new DateValue(candle.Start, adx[series, candle.Start]))
                .ToTimeSeries("ADX");
            yield return (adxSeries, Color.DarkGray);
        }
    }
}
