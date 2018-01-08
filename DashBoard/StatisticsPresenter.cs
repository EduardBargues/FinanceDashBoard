using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using CandleTimeSeriesAnalysis;
using CandleTimeSeriesAnalysis.Indicators;
using CommonUtils;
using MoreLinq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using TimeSeriesAnalysis;

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
        private TimeSeries diPlus;
        private TimeSeries diMinus;
        private TimeSeries adx;
        public void LoadData(CandleTimeSeries candleSeries, IEnumerable<TimeSeries> indicators)
        {
            Dictionary<string, TimeSeries> inds = indicators
                .ToDictionary(i => i.Name);
            diPlus = inds[UtilsPresenter.DiPlusIndicatorName];
            diMinus = inds[UtilsPresenter.DiMinusIndicatorName];
            adx = inds[UtilsPresenter.AdxIndicatorName];
            series = candleSeries;

            DataTable table = GetStatistics();
            view.LoadData(table);
        }

        private PlotView GetHistograms(CandleTimeSeries candleSeries, string statisticsLabel)
        {
            string title;
            string xlabel;
            Func<CandleTimeSeries, List<double>> upFunction;
            Func<CandleTimeSeries, List<double>> downFunction;
            switch (statisticsLabel)
            {
                case RangeAverageLabel:
                case RangeStandardDeviationLabel:
                    title = "Candles Range";
                    xlabel = "$";
                    (upFunction, downFunction) = GetRangeFunctions();
                    break;
                case AdxSustainedSlopeLabel:
                    title = "Patches";
                    xlabel = "#";
                    (upFunction, downFunction) = GetPatchesFunctions();
                    break;
                case DirectionalIndicatorMaxValueLabel:
                    title = "Directional Indicator";
                    xlabel = "";
                    (upFunction, downFunction) = GetDirectionalIndicatorFunctions();
                    break;
                default:
                    title = "Candles Body";
                    xlabel = "$";
                    (upFunction, downFunction) = GetBodyFunctions();
                    break;
            }

            PlotView plotView = GetHistograms(candleSeries
                , view.Groups
                , upFunction
                , downFunction
                , title
                , xlabel);
            return plotView;
        }

        private (Func<CandleTimeSeries, List<double>> upFunction
            , Func<CandleTimeSeries, List<double>> downFunction) GetDirectionalIndicatorFunctions()
        {
            return (
                s => s.Candles
                    .Select(candle => diPlus[candle.Start])
                    .ToList(),
                s => s.Candles
                    .Select(candle => diMinus[candle.Start])
                    .ToList()
                );
        }
        private (Func<CandleTimeSeries, List<double>> upFunction
            , Func<CandleTimeSeries, List<double>> downFunction) GetBodyFunctions()
        {
            return (
                s => s.Candles
                    .Where(candle => candle.GoesUp)
                    .Select(candle => candle.Body)
                    .ToList(),
                s => s.Candles
                    .Where(candle => candle.GoesDown)
                    .Select(candle => candle.Body)
                    .ToList());
        }
        private (Func<CandleTimeSeries, List<double>> upFunction
            , Func<CandleTimeSeries, List<double>> downFunction) GetRangeFunctions()
        {
            return (
                s => s.Candles
                    .Where(candle => candle.GoesUp)
                    .Select(candle => candle.Range)
                    .ToList(),
                s => s.Candles
                    .Where(candle => candle.GoesDown)
                    .Select(candle => candle.Range)
                    .ToList());
        }

        private PlotView GetHistograms(CandleTimeSeries candlesSeries
            , int groups
            , Func<CandleTimeSeries, List<double>> upFunction
            , Func<CandleTimeSeries, List<double>> downFunction
            , string title
            , string xlabel)
        {
            List<double> upValues = upFunction(candlesSeries);
            List<double> downValues = downFunction(candlesSeries);
            List<double> allNumbers = upValues.ToList();
            allNumbers.AddRange(downValues);
            double min = allNumbers.Min();
            double max = allNumbers.Max();
            double increment = (max - min) / groups;

            ColumnSeries seriesCandlesUp = new ColumnSeries { Title = "Up", FillColor = OxyColors.Black, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            ColumnSeries seriesCandleDown = new ColumnSeries { Title = "Down", FillColor = OxyColors.Red, StrokeColor = OxyColors.Red, StrokeThickness = 1 };
            LinearAxis valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0, Title = xlabel};
            CategoryAxis categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            for (int groupIndex = 0; groupIndex < groups; groupIndex++)
            {
                string label = $"{min + groupIndex * increment:N2}/{min + (groupIndex + 1) * increment:N2}";
                categoryAxis.Labels.Add(label);
            }
            Dictionary<int, int> upGroups = upValues
                .GroupBy(value =>
                {
                    int groupIndex = (int)((value - min) / increment);
                    if (groupIndex == groups)
                        groupIndex--;
                    return groupIndex;
                })
                .ToDictionary(grouping => grouping.Key,
                              grouping => grouping.Count());
            Dictionary<int, int> downGroups = downValues
                .GroupBy(value =>
                {
                    int groupIndex = (int)((value - min) / increment);
                    if (groupIndex == groups)
                        groupIndex--;
                    return groupIndex;
                })
                .ToDictionary(grouping => grouping.Key,
                              grouping => grouping.Count());
            upGroups.Keys
                .Union(downGroups.Keys)
                .OrderBy(groupIndex => groupIndex)
                .ForEach(groupIndex =>
                {
                    int upNum = upGroups.ContainsKey(groupIndex)
                        ? upGroups[groupIndex]
                        : 0;
                    int downNum = downGroups.ContainsKey(groupIndex)
                        ? downGroups[groupIndex]
                        : 0;
                    seriesCandlesUp.Items.Add(new ColumnItem(upNum, groupIndex));
                    seriesCandleDown.Items.Add(new ColumnItem(downNum, groupIndex));
                });

            PlotModel model = new PlotModel
            {
                LegendPlacement = LegendPlacement.Inside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                Title = title
            };
            model.Series.Add(seriesCandlesUp);
            model.Series.Add(seriesCandleDown);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);
            return new PlotView { Model = model };
        }

        private (Func<CandleTimeSeries, List<double>>, Func<CandleTimeSeries, List<double>>) GetPatchesFunctions()
        {
            (List<IGrouping<int, DateValue>> upGroups
                , List<IGrouping<int, DateValue>> downGroups) = GetGroupedPatches(adx, diPlus, diMinus);

            List<double> UpFunction(CandleTimeSeries s)
            {
                return upGroups
                    .Select(values => (double)values.Count())
                    .ToList();
            }
            List<double> DownFunction(CandleTimeSeries s)
            {
                return downGroups
                    .Select(values => (double)values.Count())
                    .ToList();
            }

            return (UpFunction, DownFunction);
        }

        private (List<IGrouping<int, DateValue>>, List<IGrouping<int, DateValue>>) GetGroupedPatches(
            TimeSeries adx
            , TimeSeries diPlus
            , TimeSeries diMinus)
        {
            List<IGrouping<int, DateValue>> strongTendencyGroups = adx.Values
                .Where((dv, index) => index > 0 &&
                                      index < adx.Count() - 1)
                .GroupAdjacent(dv => Math.Sign(adx.GetDerivative(dv.Date)))
                .Where(group => group.Key > 0)
                .ToList();
            List<IGrouping<int, DateValue>> upGroups = new List<IGrouping<int, DateValue>>();
            List<IGrouping<int, DateValue>> downGroups = new List<IGrouping<int, DateValue>>();
            foreach (IGrouping<int, DateValue> group in strongTendencyGroups)
            {
                int upCandles = group.Count(dv => diPlus[dv.Date] > diMinus[dv.Date]);
                int downCandles = group.Count(dv => diPlus[dv.Date] < diMinus[dv.Date]);
                if (upCandles > downCandles) upGroups.Add(group);
                else downGroups.Add(group);
            }
            return (upGroups, downGroups);
        }
        private const string CandlesQuantityLabel = "#";
        private const string BodyAverageLabel = "Body Average";
        private const string BodyStandardDeviationLabel = "Body St. Dev.";
        private const string RangeAverageLabel = "Range Average";
        private const string RangeStandardDeviationLabel = "Range St. Dev.";
        private const string CandlesQuantityDmPositiveNegativeLabel = "# Candles DM+ vs. DM-";
        private const string ColumnNameDescription = "Description";
        private const string ColumnNameCandlesUp = "Candles UP";
        private const string ColumnNameCandlesDown = "Candles DOWN";
        private const string DirectionalIndicatorMaxValueLabel = "DI max. value";
        private const string AdxSustainedSlopeLabel = "# Candles ADX sustained slope";
        private const string Format = "N2";
        private DataTable GetStatistics()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn(ColumnNameDescription, typeof(string)));
            table.Columns.Add(new DataColumn(ColumnNameCandlesUp, typeof(string)));
            table.Columns.Add(new DataColumn(ColumnNameCandlesDown, typeof(string)));

            AddQuantityRow(series, table);
            AddAverageBodyRow(series, table);
            AddStDevBodyRow(series, table);
            AddAverageRangeRow(series, table);
            AddStDevRangeRow(series, table);
            AddDiCandlesQuantityRow(diPlus, diMinus, table);
            AddIndicatorMaxValueRow(table, diPlus, diMinus);
            AddPositiveSlopeAdxRow(table, adx, diPlus, diMinus);

            return table;
        }
        private void AddPositiveSlopeAdxRow(DataTable table, TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            DataRow sustainedSlopeAdx = table.NewRow();
            (List<IGrouping<int, DateValue>> upGroups
                , List<IGrouping<int, DateValue>> downGroups) = GetGroupedPatches(adx, diPlus, diMinus);
            sustainedSlopeAdx[ColumnNameDescription] = AdxSustainedSlopeLabel;
            sustainedSlopeAdx[ColumnNameCandlesUp] = upGroups.Any()
                ? upGroups.Max(group => group.Count())
                : 0;
            sustainedSlopeAdx[ColumnNameCandlesDown] = downGroups.Any()
                ? downGroups.Max(group => group.Count())
                : 0;
            table.Rows.Add(sustainedSlopeAdx);
        }
        private static void AddIndicatorMaxValueRow(DataTable table, TimeSeries diPlus, TimeSeries diMinus)
        {
            DataRow maxValueIndicatorRow = table.NewRow();
            maxValueIndicatorRow[ColumnNameDescription] = DirectionalIndicatorMaxValueLabel;
            maxValueIndicatorRow[ColumnNameCandlesUp] = diPlus.Values.Max(dv => dv.Value).ToString(Format);
            maxValueIndicatorRow[ColumnNameCandlesDown] = diMinus.Values.Max(dv => dv.Value).ToString(Format);
            table.Rows.Add(maxValueIndicatorRow);
        }
        private static void AddDiCandlesQuantityRow(TimeSeries diPlus, TimeSeries diMinus, DataTable table)
        {
            DataRow candlesDmiQuantity = table.NewRow();
            candlesDmiQuantity[ColumnNameDescription] = CandlesQuantityDmPositiveNegativeLabel;
            candlesDmiQuantity[ColumnNameCandlesUp] = diPlus.Values.Count(dv => dv.Value > diMinus[dv.Date]);
            candlesDmiQuantity[ColumnNameCandlesDown] = diPlus.Values.Count(dv => dv.Value < diMinus[dv.Date]);
            table.Rows.Add(candlesDmiQuantity);
        }
        private static void AddStDevRangeRow(CandleTimeSeries series, DataTable table)
        {
            DataRow rangeStDev = table.NewRow();
            rangeStDev[ColumnNameDescription] = RangeStandardDeviationLabel;
            rangeStDev[ColumnNameCandlesUp] = series.Candles
                .Where(candle => candle.GoesUp)
                .Deviation(candle => candle.Range)
                .ToString(Format);
            rangeStDev[ColumnNameCandlesDown] = series.Candles
                .Where(candle => candle.GoesDown)
                .Deviation(candle => candle.Range)
                .ToString(Format);
            table.Rows.Add(rangeStDev);
        }
        private static void AddAverageRangeRow(CandleTimeSeries series, DataTable table)
        {
            DataRow rangeAverage = table.NewRow();
            rangeAverage[ColumnNameDescription] = RangeAverageLabel;
            rangeAverage[ColumnNameCandlesUp] = series.Candles
                .Where(candle => candle.GoesUp)
                .Average(candle => candle.Range)
                .ToString(Format);
            rangeAverage[ColumnNameCandlesDown] = series.Candles
                .Where(candle => candle.GoesDown)
                .Average(candle => candle.Range)
                .ToString(Format);
            table.Rows.Add(rangeAverage);
        }
        private static void AddStDevBodyRow(CandleTimeSeries series, DataTable table)
        {
            DataRow bodyStDev = table.NewRow();
            bodyStDev[ColumnNameDescription] = BodyStandardDeviationLabel;
            bodyStDev[ColumnNameCandlesUp] = series.Candles
                .Where(candle => candle.GoesUp)
                .Deviation(candle => candle.Body)
                .ToString(Format);
            bodyStDev[ColumnNameCandlesDown] = series.Candles
                .Where(candle => candle.GoesDown)
                .Deviation(candle => candle.Body)
                .ToString(Format);
            table.Rows.Add(bodyStDev);
        }
        private static void AddAverageBodyRow(CandleTimeSeries series, DataTable table)
        {
            DataRow bodyAverage = table.NewRow();
            bodyAverage[ColumnNameDescription] = BodyAverageLabel;
            bodyAverage[ColumnNameCandlesUp] = series.Candles
                .Where(candle => candle.GoesUp)
                .Average(candle => candle.Body)
                .ToString(Format);
            bodyAverage[ColumnNameCandlesDown] = series.Candles
                .Where(candle => candle.GoesDown)
                .Average(candle => candle.Body)
                .ToString(Format);
            table.Rows.Add(bodyAverage);
        }
        private static void AddQuantityRow(CandleTimeSeries series, DataTable table)
        {
            DataRow candlesQuantity = table.NewRow();
            candlesQuantity[ColumnNameDescription] = CandlesQuantityLabel;
            candlesQuantity[ColumnNameCandlesUp] = series.Candles
                .Count(candle => candle.GoesUp);
            candlesQuantity[ColumnNameCandlesDown] = series.Candles
                .Count(candle => candle.GoesDown);
            table.Rows.Add(candlesQuantity);
        }
    }
}