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
            DailyClassificationInputPresenter dailyClassificationInputPresenter = new DailyClassificationInputPresenter(view.GetMethodSelectorView());
            dailyClassificationInputPresenter.DoAnalysisRequest += DoAnalysis;
        }

        private void DoAnalysis(IDailyClassificationMethod method)
        {
            CandleTimeSeries series = Context.Instance.HistoryCandleTimeSeries;
            IEnumerable<DailyClassification> classifications = series.Candles
                .Where(candle => candle.Start >= method.StartDay &&
                                 candle.Start <= method.EndDay)
                .Select(candle => new DailyClassification(candle: candle,
                                                          classification: method.Classify(candle.Start.Date, series)));
            view.LoadClassifications(classifications);
        }
    }
}