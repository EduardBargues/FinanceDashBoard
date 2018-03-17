using CandleTimeSeriesAnalysis;
using CommonUtils;
using Model;
using Model.StatisticProviders;
using Model.Strategies;
using Presenter.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TimeSeriesAnalysis;

namespace Presenter.Daily
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
        void LoadData(CandleTimeSeries series, TimeSeries buySeries, TimeSeries sellSeries, IEnumerable<(TimeSeries, Color)> indicators);
        void LoadDays(IEnumerable<DateTime> days);
        bool DaysLoaded { get; }
        int Period { get; }
        int SmoothingPeriod { get; }
        void LoadStrategies(IEnumerable<string> strategiesNames);
    }

    public class DailyPresenter
    {
        private readonly IDailyView view;
        private readonly StatisticsPresenter statisticsPresenter;

        private List<Strategy> strategies = new List<Strategy>()
        {
            new StrategyEduard("EDUARD ST"),
            new StrategyVicente("VICENTE ST"),
        };

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

        public void LoadStrategies()
        {
            IEnumerable<string> names = strategies.Select(st => st.Name);
            view.LoadStrategies(names);
        }

        private void View_RefreshRequest() => LoadData();

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
                (TimeSeries buyTimeSeries, TimeSeries sellTimeSeries) = GetBuyAndSellTimeSeries(daySeries, indicators);
                statisticsPresenter.LoadData(daySeries, indicators.Select(ind => ind.Item1));
                view.LoadData(daySeries, buyTimeSeries, sellTimeSeries, indicators);
            }
        }

        private (TimeSeries, TimeSeries) GetBuyAndSellTimeSeries(CandleTimeSeries series, IEnumerable<(TimeSeries, Color)> indicators)
        {
            Dictionary<string, TimeSeries> indicatorsByName = indicators
                .ToDictionary(item => item.Item1.Name,
                              item => item.Item1);
            TimeSeries dx = indicatorsByName[UtilsPresenter.DxIndicatorName];
            TimeSeries adx = indicatorsByName[UtilsPresenter.AdxIndicatorName];
            TimeSeries diMinus = indicatorsByName[UtilsPresenter.DiMinusIndicatorName];
            TimeSeries diPlus = indicatorsByName[UtilsPresenter.DiPlusIndicatorName];

            List<IEnumerable<DateTime>> patches = ProvidersUtils.GetGroupedPatches(dx, adx, diPlus, diMinus)
                .ToList();
            Dictionary<bool, List<DateValue>> datesValuesByAction = patches
                .SelectMany(patch =>
                {
                    List<DateTime> dates = patch.ToList();
                    bool isUpTendency = ProvidersUtils.IsUpTendency(dates, diPlus, diMinus);
                    DateTime firstDate = dates.Min(d => d);
                    DateTime lastDate = dates.Max(d => d);
                    return new[] { firstDate, lastDate }
                        .Select(date => new
                        {
                            Buy = date == firstDate ? isUpTendency : !isUpTendency,
                            DateValue = new DateValue(date, series[date].Close)
                        });
                })
                .GroupBy(action => action.Buy)
                .ToDictionary(g => g.Key, g => g.Select(action => action.DateValue).ToList());
            TimeSeries buyTimeSeries = datesValuesByAction.GetValueOrDefault(true)
                ?.ToTimeSeries("Buy");
            TimeSeries sellTimeSeries = datesValuesByAction.GetValueOrDefault(false)
                ?.ToTimeSeries("Sell");
            return (buyTimeSeries, sellTimeSeries);
            //return (null,null);
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

        private CandleTimeSeries GetCandleTimeSeriesByDuration(IEnumerable<Trade> trades, TimeSpan candleDuration, DateTime firstCandleStart) => trades
                .ToCandlesByDuration(candleDuration, firstCandleStart)
                .ToCandleTimeSeries($"{firstCandleStart:yyyy/MM/dd} - By duration");
    }
}
