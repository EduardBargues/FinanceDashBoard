using CandleTimeSeriesAnalysis;
using Model;
using Model.ClassificationDayMethods;
using System.Collections.Generic;
using System.Linq;

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
            movingAverageInputsPresenter.DoClassificationRequest += input => DoAnalysis(input.GetMethod());
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