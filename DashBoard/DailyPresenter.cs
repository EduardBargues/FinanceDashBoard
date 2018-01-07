using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandleTimeSeriesAnalysis;
using TimeSeriesAnalysis;

namespace DashBoard
{
    public interface IDailyView
    {
        IStatisticsView GetStatisticsView();
        DateTime? SelectedDay { get; }
        event Action RefreshRequest;
        bool ComputeCandlesByDuration { get; }
        bool ComputeCandlesByTicks { get; }
        bool ComputeCandlesByVolume { get; }
        TimeSpan CandlesDuration { get; }
        int CandlesTicks { get; }
        double CandlesVolume { get; }
        void LoadData(CandleTimeSeries series, IEnumerable<(TimeSeries, Color)> indicators);
        void LoadDays(IEnumerable<DateTime> days);
        bool DaysLoaded { get; }
        int Period { get; }
        int SmoothingPeriod { get; }
    }

    public class DailyPresenter
    {
        private readonly IDailyView view;
        private readonly StatisticsPresenter statisticsPresenter;

        public DailyPresenter(IDailyView view)
        {
            this.view = view;
            statisticsPresenter = new StatisticsPresenter(view.GetStatisticsView());
            this.view.RefreshRequest += View_RefreshRequest;
        }

        public void LoadDays()
        {
            if (!view.DaysLoaded)
            {
                IEnumerable<DateTime> days = Context.Instance.HistoryCandleTimeSeries.Candles
                    .Select(candle => candle.Start.Date)
                    .Distinct();
                view.LoadDays(days);
            }
        }

        private void View_RefreshRequest()
        {
            LoadData();
        }

        public void LoadData()
        {
            if (view.SelectedDay != null)
            {
                DateTime day = view.SelectedDay.Value.Date;
                List<Trade> dayBeforeTrades = Context.Instance.GetDailyTrades(day.AddDays(-1))
                    .ToList();
                List<Trade> dayTrades = Context.Instance.GetDailyTrades(day)
                    .ToList();
                (CandleTimeSeries daySeries, CandleTimeSeries twoDaysSeries) = CandleTimeSeries(dayTrades, day, dayBeforeTrades);
                List<(TimeSeries, Color)> indicators = UtilsPresenter.GetIndicators(twoDaysSeries
                    , view.Period
                    , view.SmoothingPeriod
                    , day.Date)
                    .ToList();
                statisticsPresenter.LoadData(daySeries, indicators.Select(ind => ind.Item1));
                view.LoadData(daySeries, indicators);
            }
        }

        private (CandleTimeSeries, CandleTimeSeries) CandleTimeSeries(List<Trade> dayTrades, DateTime day, List<Trade> dayBeforeTrades)
        {
            CandleTimeSeries daySeries;
            CandleTimeSeries twoDaysSeries;
            if (view.ComputeCandlesByDuration)
            {
                TimeSpan candleDuration = view.CandlesDuration;
                daySeries = GetCandleTimeSeriesByDuration(dayTrades, candleDuration, day.Date);
                CandleTimeSeries dayBeforeSeries = GetCandleTimeSeriesByDuration(dayBeforeTrades, candleDuration, day.Date.AddDays(-1));
                twoDaysSeries = dayBeforeSeries.Candles
                    .Union(daySeries.Candles)
                    .ToCandleTimeSeries();
            }
            else if (view.ComputeCandlesByTicks)
            {
                // TODO
                throw new NotImplementedException();
            }
            else if (view.ComputeCandlesByVolume)
            {
                // TODO
                throw new NotImplementedException();
            }
            else
                throw new NotImplementedException();
            return (daySeries, twoDaysSeries);
        }

        private CandleTimeSeries GetCandleTimeSeriesByDuration(IEnumerable<Trade> trades, TimeSpan candleDuration, DateTime firstCandleStart)
        {

            return trades
                .ToCandlesByDuration(candleDuration, firstCandleStart)
                .ToCandleTimeSeries($"{firstCandleStart:yyyy/MM/dd} - By duration");
        }
    }
}
