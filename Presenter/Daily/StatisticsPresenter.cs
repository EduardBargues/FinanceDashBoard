using CandleTimeSeriesAnalysis;
using Model.StatisticProviders;
using OxyPlot.WindowsForms;
using Presenter.Generic;
using Presenter.PlotViewProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TimeSeriesAnalysis;

namespace Presenter.Daily
{
    public interface IStatisticsView
    {
        void LoadData(DataTable data);
        void LoadHistograms(PlotView histograms);
        event Action RefreshRequest;
        string SelectedStatisticsLabel { get; }
        int Groups { get; }
    }

    internal class StatisticsPresenter
    {
        private readonly IStatisticsView view;

        public StatisticsPresenter(IStatisticsView view)
        {
            this.view = view;
            this.view.RefreshRequest += RefreshHistogram;
        }

        private void RefreshHistogram()
        {
            string statisticsLabel = view.SelectedStatisticsLabel;
            PlotView histograms = !string.IsNullOrEmpty(statisticsLabel)
                ? GetPlotView(statisticsLabel)
                : null;
            view.LoadHistograms(histograms);
        }

        private CandleTimeSeries series;
        private TimeSeries diPlus;
        private TimeSeries diMinus;
        private TimeSeries dx;
        private TimeSeries adx;
        public void LoadData(CandleTimeSeries candleSeries, IEnumerable<TimeSeries> indicators)
        {
            Dictionary<string, TimeSeries> inds = indicators
                .ToDictionary(i => i.Name);
            diPlus = inds[UtilsPresenter.DiPlusIndicatorName];
            diMinus = inds[UtilsPresenter.DiMinusIndicatorName];
            adx = inds[UtilsPresenter.AdxIndicatorName];
            dx = inds[UtilsPresenter.DxIndicatorName];
            series = candleSeries;

            DataTable table = GetStatistics();
            view.LoadData(table);
        }

        private PlotView GetPlotView(string statisticsLabel) => plotViewProviders[statisticsLabel].GetPlotView();

        private Dictionary<string, IPlotViewProvider> plotViewProviders;
        private const string Format = "N2";
        private DataTable GetStatistics()
        {
            plotViewProviders = new Dictionary<string, IPlotViewProvider>();
            string columnNameDescription = "DESCRIPTION";
            string columnNameCandlesUp = "UP";
            string columnNameCandlesDown = "DOWN";
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn(columnNameDescription, typeof(string)));
            table.Columns.Add(new DataColumn(columnNameCandlesUp, typeof(string)));
            table.Columns.Add(new DataColumn(columnNameCandlesDown, typeof(string)));
            List<(IStatisticProvider, IPlotViewProvider)> providers = new List<(IStatisticProvider, IPlotViewProvider)>()
            {
                (new CandleQuantityProvider(series),new HistogramPlotViewProvider(new CandleBodyDataProvider(series),"Candle body","$",view.Groups))
                ,(new CandleBodyAverageProvider(series), new HistogramPlotViewProvider(new CandleBodyDataProvider(series),"Candle body","$",view.Groups))
                ,(new CandleBodyStandardDeviationProvider(series), new HistogramPlotViewProvider(new CandleBodyDataProvider(series),"Candle body","$",view.Groups))
                ,(new CandleRangeAverageProvider(series), new HistogramPlotViewProvider(new CandleRangeDataProvider(series),"Candle range","$",view.Groups))
                ,(new CandleRangeStandardDeviationProvider(series), new HistogramPlotViewProvider(new CandleRangeDataProvider(series),"Candle range","$",view.Groups))
                ,(new CandleQuantityDiPlusVsDiMinusProvider(diPlus,diMinus), new HistogramPlotViewProvider(new CandleQuantityDiPlusVsDiMinusDataProvider(series,diPlus,diMinus), "Candles Di+ vs Di-","",view.Groups))
                ,(new DirectionalIndicatorMaxValueProvider(diPlus,diMinus), new HistogramPlotViewProvider(new DirectionalIndicatorMaxValueDataProvider(diPlus,diMinus), "Di+ vs Di-","",view.Groups))
                ,(new CandleQuantityAdxSustainedSlopeProvider(dx, adx,diPlus,diMinus), new ScatterPlotViewProvider(new CandleQuantityAdxSustainedSlopeDataProvider(series, dx, adx, diPlus, diMinus), "Patches","$","#"))
                ,(new PriceVariationAdxSustainedSlopeMaxProvider(series, dx,adx,diPlus,diMinus),new ScatterPlotViewProvider(new CandleQuantityAdxSustainedSlopeDataProvider(series, dx, adx, diPlus, diMinus), "Patches","$","#"))
                ,(new BenefitFollowingStrategyProvider(series,dx,adx,diPlus,diMinus), new ScatterPlotViewProvider(new CandleQuantityAdxSustainedSlopeDataProvider(series, dx, adx, diPlus, diMinus), "Patches","$","#"))
            };

            foreach ((IStatisticProvider, IPlotViewProvider) tuple in providers)
            {
                DataRow row = table.NewRow();
                Statistic statistic = tuple.Item1.GetStatistic();
                string description = statistic.Description;

                row[columnNameDescription] = statistic.Description;
                row[columnNameCandlesUp] = statistic.UpValue.ToString(Format);
                row[columnNameCandlesDown] = statistic.DownValue.ToString(Format);
                table.Rows.Add(row);

                plotViewProviders.Add(description, tuple.Item2);
            }

            return table;
        }
    }
}