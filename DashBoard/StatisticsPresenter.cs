using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using CandleTimeSeriesAnalysis;
using CommonUtils;
using MoreLinq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace DashBoard
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
                ? GetHistograms(series, statisticsLabel)
                : null;
            view.LoadHistograms(histograms);
        }

        private CandleTimeSeries series;
        public void LoadData(CandleTimeSeries candleSeries)
        {
            this.series = candleSeries;
            DataTable table = GetStatistics(candleSeries);
            this.view.LoadData(table);
        }

        private PlotView GetHistograms(CandleTimeSeries candleSeries, string statisticsLabel)
        {
            string propertyName;
            switch (statisticsLabel)
            {
                case CandlesQuantityLabel:
                case BodyAverageLabel:
                case BodyStandardDeviationLabel:
                    propertyName = nameof(Candle.Body);
                    break;
                case RangeAverageLabel:
                case RangeStandardDeviationLabel:
                    propertyName = nameof(Candle.Range);
                    break;
                default:
                    throw new NotImplementedException();
            }
            PlotView plotView = GetHistogram(candleSeries, view.Groups, propertyName);
            return plotView;
        }
        private PlotView GetHistogram(CandleTimeSeries candlesSeries
            , int groups
            , string propertyName)
        {
            PropertyInfo property = typeof(Candle).GetProperty(propertyName);
            double Function(Candle candle)
            {
                return (double)property.GetValue(candle);
            }
            List<double> values = candlesSeries.Candles
                .Select(Function)
                .ToList();
            double min = values.Min();
            double max = values.Max();
            double increment = (max - min) / groups;

            ColumnSeries seriesCandlesUp = new ColumnSeries { Title = "Candles Up", FillColor = OxyColors.Black, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            ColumnSeries seriesCandleDown = new ColumnSeries { Title = "Candles Down", FillColor = OxyColors.Red, StrokeColor = OxyColors.Red, StrokeThickness = 1 };
            LinearAxis valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0, Title = "#" };
            CategoryAxis categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom, Title = "$" };
            Dictionary<int, int> candlesUpByGroup = candlesSeries.Candles
                .Where(candle => candle.GoesUp)
                .GroupBy(candle => (int)((Function(candle) - min) / increment))
                .ToDictionary(grouping => grouping.Key, grouping => grouping.Count());
            Dictionary<int, int> candlesDownByGroup = candlesSeries.Candles
                .Where(candle => candle.GoesDown)
                .GroupBy(candle => (int)((Function(candle) - min) / increment))
                .ToDictionary(grouping => grouping.Key, grouping => grouping.Count());
            values
                .GroupBy(value => (int)((value - min) / increment))
                .OrderBy(grouping => grouping.Key)
                .ForEach((grouping, groupIndex) =>
                {
                    int candlesUpNum = candlesUpByGroup.ContainsKey(grouping.Key)
                        ? candlesUpByGroup[grouping.Key]
                        : 0;
                    int candlesDownNum = candlesDownByGroup.ContainsKey(grouping.Key)
                        ? candlesDownByGroup[grouping.Key]
                        : 0;
                    seriesCandlesUp.Items.Add(new ColumnItem(candlesUpNum, groupIndex));
                    seriesCandleDown.Items.Add(new ColumnItem(candlesDownNum, groupIndex));
                    string label = $"{grouping.Key * increment:N0}-{(grouping.Key + 1) * increment:N0}";
                    categoryAxis.Labels.Add(label);
                });

            PlotModel model = new PlotModel
            {
                LegendPlacement = LegendPlacement.Inside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                Title = propertyName == nameof(Candle.Body) ? "Candles Body" : "Candles Range",
            };
            model.Series.Add(seriesCandlesUp);
            model.Series.Add(seriesCandleDown);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);
            return new PlotView { Model = model };
        }

        private const string CandlesQuantityLabel = "#";
        private const string BodyAverageLabel = "Body Average";
        private const string BodyStandardDeviationLabel = "Body St. Dev.";
        private const string RangeAverageLabel = "Range Average";
        private const string RangeStandardDeviationLabel = "Range St. Dev.";
        private static DataTable GetStatistics(CandleTimeSeries series)
        {
            const string columnNameDescription = "Description";
            const string columnNameCandlesUp = "Candles UP";
            const string columnNameCandlesDown = "Candles DOWN";
            const string format = "N2";
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn(columnNameDescription, typeof(string)));
            table.Columns.Add(new DataColumn(columnNameCandlesUp, typeof(string)));
            table.Columns.Add(new DataColumn(columnNameCandlesDown, typeof(string)));

            DataRow row = table.NewRow();
            row[columnNameDescription] = CandlesQuantityLabel;
            row[columnNameCandlesUp] = series.Candles
                .Count(candle => candle.GoesUp);
            row[columnNameCandlesDown] = series.Candles
                .Count(candle => candle.GoesDown);
            table.Rows.Add(row);

            DataRow row2 = table.NewRow();
            row2[columnNameDescription] = BodyAverageLabel;
            row2[columnNameCandlesUp] = series.Candles
                .Where(candle => candle.GoesUp)
                .Average(candle => candle.Body)
                .ToString(format);
            row2[columnNameCandlesDown] = series.Candles
                .Where(candle => candle.GoesDown)
                .Average(candle => candle.Body)
                .ToString(format);
            table.Rows.Add(row2);

            DataRow row3 = table.NewRow();
            row3[columnNameDescription] = BodyStandardDeviationLabel;
            row3[columnNameCandlesUp] = series.Candles
                .Where(candle => candle.GoesUp)
                .Deviation(candle => candle.Body)
                .ToString(format);
            row3[columnNameCandlesDown] = series.Candles
                .Where(candle => candle.GoesDown)
                .Deviation(candle => candle.Body)
                .ToString(format);
            table.Rows.Add(row3);

            DataRow row4 = table.NewRow();
            row4[columnNameDescription] = RangeAverageLabel;
            row4[columnNameCandlesUp] = series.Candles
                .Where(candle => candle.GoesUp)
                .Average(candle => candle.Range)
                .ToString(format);
            row4[columnNameCandlesDown] = series.Candles
                .Where(candle => candle.GoesDown)
                .Average(candle => candle.Range)
                .ToString(format);
            table.Rows.Add(row4);

            DataRow row5 = table.NewRow();
            row5[columnNameDescription] = RangeStandardDeviationLabel;
            row5[columnNameCandlesUp] = series.Candles
                .Where(candle => candle.GoesUp)
                .Deviation(candle => candle.Range)
                .ToString(format);
            row5[columnNameCandlesDown] = series.Candles
                .Where(candle => candle.GoesDown)
                .Deviation(candle => candle.Range)
                .ToString(format);
            table.Rows.Add(row5);
            return table;
        }
    }
}