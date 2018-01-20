using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CandleTimeSeriesAnalysis;
using Model;
using TimeSeriesAnalysis;

namespace Presenter
{
    public interface IHistoryView
    {
        void LoadData(CandleTimeSeries series, IEnumerable<(TimeSeries, Color)> indicators);
        event Action LoadDataRequest;
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        int IndicatorPeriod { get; }
        int SmoothingPeriod { get; }
    }

    public class HistoryPresenter
    {
        private readonly IHistoryView view;

        public HistoryPresenter(IHistoryView view)
        {
            this.view = view;
            this.view.LoadDataRequest += View_LoadDataRequest;
        }
        private void View_LoadDataRequest()
        {
            CandleTimeSeries data = GetCandleTimeSeries();
            IEnumerable<(TimeSeries, Color)> indicators = GetIndicators();
            view.LoadData(data, indicators);
        }
        private IEnumerable<(TimeSeries, Color)> GetIndicators()
        {
            CandleTimeSeries series = Context.Instance.HistoryCandleTimeSeries.Candles
                .ToCandleTimeSeries();
            return UtilsPresenter.GetIndicators(series, view.IndicatorPeriod, view.SmoothingPeriod);
        }
        private CandleTimeSeries GetCandleTimeSeries()
        {
            return Context.Instance.HistoryCandleTimeSeries.Candles
                .Where(candle => candle.Start >= view.StartDate &&
                                 candle.Start <= view.EndDate)
                .ToCandleTimeSeries(name: "BitCoin");
        }
    }
}
