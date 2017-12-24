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
using OxyPlot.Series;

namespace DashBoard
{
    public interface IStatisticsView
    {
        void LoadData(DataTable data);
        void LoadHistograms(IEnumerable<ColumnSeries> series);
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
            this.view.RefreshRequest += View_SelectedStatisticsChanged;
        }

        private void View_SelectedStatisticsChanged()
        {
            string statisticsLabel = view.SelectedStatisticsLabel;
            IEnumerable<ColumnSeries> histograms = !string.IsNullOrEmpty(statisticsLabel)
                ? GetHistogram(series, statisticsLabel)
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

        private IEnumerable<ColumnSeries> GetHistogram(CandleTimeSeries candleSeries, string statisticsLabel)
        {
            ColumnSeries candlesUp;
            ColumnSeries candlesDown;
            IEnumerable<Candle> upCandles = candleSeries.Candles
                .Where(candle => candle.GoesUp);
            IEnumerable<Candle> downCandles = candleSeries.Candles
                .Where(candle => candle.GoesDown);
            switch (statisticsLabel)
            {
                case CandlesQuantityLabel:
                case BodyAverageLabel:
                case BodyStandardDeviationLabel:
                    candlesUp = GetBodyHistogram(upCandles, "Candles Up", view.Groups, nameof(Candle.Body));
                    candlesDown = GetBodyHistogram(downCandles, "Candles Down", view.Groups, nameof(Candle.Body));
                    break;
                case RangeAverageLabel:
                case RangeStandardDeviationLabel:
                    candlesUp = GetBodyHistogram(upCandles, "Candles Up", view.Groups, nameof(Candle.Range));
                    candlesDown = GetBodyHistogram(downCandles, "Candles Down", view.Groups, nameof(Candle.Range));
                    break;
                default:
                    throw new NotImplementedException();
            }
            yield return candlesUp;
            yield return candlesDown;
        }

        private ColumnSeries GetBodyHistogram(IEnumerable<Candle> candles, string label, int groups, string propertyName)
        {
            ColumnSeries histogram = Plotter.GetDataPointSeries<ColumnSeries>(
                (nameof(ColumnSeries.Title), label),
                (nameof(ColumnSeries.ValueField), "Value")
            );
            PropertyInfo property = typeof(Candle).GetProperty(propertyName);
            double Function(Candle candle)
            {
                return (double)property.GetValue(candle);
            }
            List<Candle> list = candles
                .ToList();
            double max = list
                .Max(candle => Function(candle));
            double min = list
                .Min(candle => Function(candle));
            double amplitude = (max - min) / groups;
            histogram.ItemsSource = list
                .GroupBy(candle => (int)((Function(candle) - min) / amplitude))
                .OrderBy(grouping => grouping.Key)
                .Select(grouping =>
                {
                    double firstValue = amplitude * grouping.Key;
                    double lastValue = amplitude * (grouping.Key + 1);
                    string itemLabel = $"{firstValue:N2} - {lastValue:N2}";
                    return new { Label = itemLabel, Value = grouping.Count() };
                })
                .ToList();

            return histogram;
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