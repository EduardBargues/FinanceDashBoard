using CandleTimeSeriesAnalysis;
using Model;
using Model.ClassificationDayMethods;
using Presenter.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TimeSeriesAnalysis;

namespace Presenter.ClassificationDay
{
    public class DailyClassificationPresenter
    {
        private readonly IDailyClassificationView view;

        public DailyClassificationPresenter(IDailyClassificationView view)
        {
            this.view = view;
            AdxInputsPresenter adxInputsPresenter = new AdxInputsPresenter(view.GetAdxInputsView());
            adxInputsPresenter.DoClassificationRequest += input => DoAnalysis(input.GetMethod());

            MovingAverageInputsPresenter movingAverageInputsPresenter = new MovingAverageInputsPresenter(view.GetMovingAverageInputView());
            SeriesIndicatorPresenter seriesPresenter = new SeriesIndicatorPresenter(view.GetSeriesIndicatorView());

            movingAverageInputsPresenter.DoClassificationRequest += input =>
            {
                DoAnalysis(input.GetMethod());
                LoadSeriesIndicators(adxInputsPresenter, movingAverageInputsPresenter, seriesPresenter);
            };
            this.view.SelectedDayChanged += () => LoadSeriesIndicators(adxInputsPresenter,
                                                                       movingAverageInputsPresenter,
                                                                       seriesPresenter);
        }

        private void LoadSeriesIndicators(
            AdxInputsPresenter adxInputsPresenter,
            MovingAverageInputsPresenter movingAverageInputsPresenter,
            SeriesIndicatorPresenter seriesPresenter)
        {
            if (view.SelectedDay == DateTime.MinValue)
                return;
            CandleTimeSeries series = Context.Instance.HistoryCandleTimeSeries.Candles
                .Where(candle => candle.Start >= view.SelectedDay.Date.AddMonths(-3) &&
                                 candle.Start <= view.SelectedDay.Date)
                .ToCandleTimeSeries();
            IEnumerable<(TimeSeries, Color)> indicators = UtilsPresenter.GetIndicators(
                series: Context.Instance.HistoryCandleTimeSeries,
                indicatorPeriod: adxInputsPresenter.GetDxPeriod(),
                smoothingPeriod: adxInputsPresenter.GetMovingAveragePeriod());
            TimeSeries timeSeries = series.Candles
                .Select(candle => new DateValue(candle.Start, candle.Close))
                .ToTimeSeries();

            TimeSeries slowMovingAverage = timeSeries
                .GetExponentialMovingAverage(movingAverageInputsPresenter.SlowMovingAveragePeriod)
                .ToTimeSeries("Slow MA");
            TimeSeries mediumMovingAverage = timeSeries
                .GetExponentialMovingAverage(movingAverageInputsPresenter.MediumMovingAveragePeriod)
                .ToTimeSeries("Medium MA");
            TimeSeries fastMovingAverage = timeSeries
                .GetExponentialMovingAverage(movingAverageInputsPresenter.FastMovingAveragePeriod)
                .ToTimeSeries("Fast MA");

            seriesPresenter.LoadData(
                series: series,
                indicators: indicators,
                slowMovingAverage: slowMovingAverage,
                mediumMovingAverage: mediumMovingAverage,
                fastMovingAverage: fastMovingAverage
            );
        }

        private void DoAnalysis(IDailyClassificationMethod method)
        {
            CandleTimeSeries series = Context.Instance.HistoryCandleTimeSeries;
            IEnumerable<DailyClassification> classifications = series.Candles
                .Where(candle => candle.Start >= view.GetStartDay() &&
                                 candle.Start <= view.GetEndDay())
                .Select(candle => new DailyClassification(candle: candle,
                                                          classification: method.Classify(candle.Start.Date, series)));
            view.LoadClassifications(classifications);
        }
    }
}