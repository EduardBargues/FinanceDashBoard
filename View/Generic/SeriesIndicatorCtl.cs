﻿using CandleTimeSeriesAnalysis;
using CommonUtils;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using Presenter.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TimeSeriesAnalysis;

namespace View.Generic
{
    public partial class SeriesIndicatorCtl : UserControl, ISeriesIndicatorView
    {
        public SeriesIndicatorCtl()
        {
            InitializeComponent();
        }

        public void LoadData(
            CandleTimeSeries series,
            IEnumerable<(TimeSeries, Color)> indicators,
            TimeSeries buySeries = null,
            TimeSeries sellSeries = null,
            TimeSeries slowMovingAverage = null,
            TimeSeries mediumMovingAverage = null,
            TimeSeries fastMovingAverage = null)
        {
            IEnumerable<TimeSeriesPlotInfo> infos = indicators
                .Select(item => TimeSeriesPlotInfo.Create(series: item.Item1, color: item.Item2));
            PlotView indicatorPlotView = TimeSeries.GetPlotView(infos);
            LinearAxis ya = new LinearAxis();
            ya.MajorStep = 0.25;
            ya.MajorGridlineStyle = LineStyle.LongDash;
            ya.MajorGridlineColor = OxyColors.Gray;
            indicatorPlotView.Model.Axes.Add(ya);

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(indicatorPlotView);

            void onAxisChanged(object a, AxisChangedEventArgs args)
            {
                DateTimeAxis axis = (DateTimeAxis)a;
                Axis xaxis = indicatorPlotView.Model.Axes.FirstOrDefault();
                Axis yAxis = indicatorPlotView.Model.Axes[1];
                if (xaxis != null)
                {
                    xaxis.Reset();
                    xaxis.Minimum = axis.ActualMinimum;
                    xaxis.Maximum = axis.ActualMaximum;
                    List<DataPoint> points = indicatorPlotView.Model.Series
                        .Where(s => s is DataPointSeries)
                        .Cast<DataPointSeries>()
                        .SelectMany(s => s.Points.Where(p => p.X >= axis.ActualMinimum && p.X <= axis.ActualMaximum))
                        .ToList();
                    bool any = points.Any();
                    if (any)
                    {
                        double min = points
                            .Min(p => p.Y);
                        double max = points
                            .Max(p => p.Y);
                        yAxis.Reset();
                        yAxis.Minimum = min;
                        yAxis.Maximum = max;
                    }
                    indicatorPlotView.Model.InvalidatePlot(true);
                }
            }

            PlotView seriesPlotView = CandleTimeSeries.GetPlotView(CandleTimeSeriesPlotInfo.Create(series), onAxisChanged);
            if (buySeries != null)
                AddTimeSeries(buySeries, seriesPlotView, LineStyle.None, OxyColors.Blue, OxyColors.Blue, MarkerType.Circle);
            if (sellSeries != null)
                AddTimeSeries(sellSeries, seriesPlotView, LineStyle.None, OxyColors.GreenYellow, OxyColors.GreenYellow, MarkerType.Triangle);
            if (slowMovingAverage != null)
                AddTimeSeries(slowMovingAverage, seriesPlotView, LineStyle.Automatic, OxyColors.Gray, OxyColors.Gray, MarkerType.None);
            if (mediumMovingAverage != null)
                AddTimeSeries(mediumMovingAverage, seriesPlotView, LineStyle.Automatic, OxyColors.YellowGreen, OxyColors.YellowGreen, MarkerType.None);
            if (fastMovingAverage != null)
                AddTimeSeries(fastMovingAverage, seriesPlotView, LineStyle.Automatic, OxyColors.LightBlue, OxyColors.DarkBlue, MarkerType.None);
            splitContainer.Panel1.Controls.Clear();
            splitContainer.Panel1.Controls.Add(seriesPlotView);
        }

        private static void AddTimeSeries(
            TimeSeries series,
            PlotView seriesPlotView,
            LineStyle lineStyle,
            OxyColor lineColor,
            OxyColor markerColor,
            MarkerType marker)
        {
            LineSeries lineSeries = Plotter.GetDataPointSeries<LineSeries>(
                (nameof(LineSeries.Title), series.Name)
                , (nameof(LineSeries.MarkerType), marker)
                , (nameof(LineSeries.MarkerFill), markerColor)
                , (nameof(LineSeries.MarkerSize), 5)
                , (nameof(LineSeries.LineStyle), lineStyle)
                , (nameof(LineSeries.Color), lineColor)
            );
            foreach (DateTime date in series.Dates)
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), series[date]));
            seriesPlotView.Model.Series.Add(lineSeries);
        }
    }
}
