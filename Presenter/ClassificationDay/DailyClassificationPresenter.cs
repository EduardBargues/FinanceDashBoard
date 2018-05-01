using CandleTimeSeriesAnalysis;
using Model;
using Model.ClassificationDayMethods;
using MoreLinq;
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
            MovingAverageInputsPresenter movingAverageInputsPresenter = new MovingAverageInputsPresenter(view.GetMovingAverageInputView());
            ParameterListPresenter statisticsPresenter = new ParameterListPresenter(view.GetClassificationStatisticsView());
            SeriesIndicatorPresenter seriesPresenter = new SeriesIndicatorPresenter(view.GetSeriesIndicatorView());

            adxInputsPresenter.DoClassificationRequest += input => OnClassificationRequest(input,
                                                                                           statisticsPresenter,
                                                                                           adxInputsPresenter,
                                                                                           movingAverageInputsPresenter,
                                                                                           seriesPresenter);
            movingAverageInputsPresenter.DoClassificationRequest += input => OnClassificationRequest(input,
                                                                                                     statisticsPresenter,
                                                                                                     adxInputsPresenter,
                                                                                                     movingAverageInputsPresenter,
                                                                                                     seriesPresenter);
            this.view.SelectedDayChanged += () => LoadSeriesIndicators(adxInputsPresenter,
                                                                       movingAverageInputsPresenter,
                                                                       seriesPresenter);
        }

        private void OnClassificationRequest(IDailyClassificationInput input,
            ParameterListPresenter statisticsPresenter, AdxInputsPresenter adxInputsPresenter,
            MovingAverageInputsPresenter movingAverageInputsPresenter, SeriesIndicatorPresenter seriesPresenter)
        {
            List<DailyClassification> classifications = DoAnalysis(input.GetMethod())
                .ToList();
            view.LoadClassifications(classifications);
            ClassificationStatistics statistics = GetClassificationStatistics(classifications);
            statisticsPresenter.LoadData(statistics.GetParameters());
            LoadSeriesIndicators(adxInputsPresenter, movingAverageInputsPresenter, seriesPresenter);
        }

        private ClassificationStatistics GetClassificationStatistics(IEnumerable<DailyClassification> classifications)
        {
            List<IGrouping<bool, DailyClassification>> groups = classifications
                .GroupAdjacent(classification => classification.Success)
                .ToList();
            List<IGrouping<bool, DailyClassification>> successGroups = groups
                .Where(group => @group.Key)
                .ToList();
            List<IGrouping<bool, DailyClassification>> failGroups = groups
                .Where(group => !@group.Key)
                .ToList();
            return new ClassificationStatistics()
            {
                NumberOfSuccesses = groups
                    .Where(group => group.Key)
                    .Sum(group => group.Count()),
                NumberOfFails = groups
                    .Where(group => !group.Key)
                    .Sum(group => group.Count()),
                NumberOfConsecutiveSuccesses = successGroups.Any()
                    ? successGroups.Max(group => group.Count())
                    : 0,
                NumberOfConsecutiveFails = failGroups.Any()
                    ? failGroups.Max(group => group.Count())
                    : 0,
            };
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

        private IEnumerable<DailyClassification> DoAnalysis(IDailyClassificationMethod method)
        {
            CandleTimeSeries series = Context.Instance.HistoryCandleTimeSeries;
            IEnumerable<DailyClassification> classifications = series.Candles
                .Where(candle => candle.Start >= view.GetStartDay() &&
                                 candle.Start <= view.GetEndDay())
                .Select(candle => new DailyClassification(candle: candle,
                                                          classification: method.Classify(candle.Start.Date, series)));
            return classifications;
        }
    }
}