using MoreLinq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Collections.Generic;
using System.Linq;

namespace Presenter.PlotViewProviders
{
    public class HistogramPlotViewProvider : IPlotViewProvider
    {
        private readonly I1DimensionDataProvider dataProvider;
        private readonly string title;
        private readonly string xLabel;
        private readonly int groups;

        public HistogramPlotViewProvider(I1DimensionDataProvider dataProvider, string title, string xLabel, int groups)
        {
            this.dataProvider = dataProvider;
            this.title = title;
            this.xLabel = xLabel;
            this.groups = groups;
        }

        public PlotView GetPlotView()
        {
            List<double> upValues = dataProvider.GetUpData().ToList();
            List<double> downValues = dataProvider.GetDownData().ToList();
            List<double> allNumbers = upValues.ToList();
            allNumbers.AddRange(downValues);
            double min = allNumbers.Min();
            double max = allNumbers.Max();
            double increment = (max - min) / groups;

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
            ColumnSeries seriesCandlesUp = new ColumnSeries { Title = "UP", FillColor = OxyColors.Black, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            ColumnSeries seriesCandleDown = new ColumnSeries { Title = "DOWN", FillColor = OxyColors.Red, StrokeColor = OxyColors.Red, StrokeThickness = 1 };
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

            CategoryAxis categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom, Title = xLabel };
            for (int groupIndex = 0; groupIndex < groups; groupIndex++)
            {
                string label = $"{min + groupIndex * increment:N2}|{min + (groupIndex + 1) * increment:N2}";
                categoryAxis.Labels.Add(label);
            }
            model.Axes.Add(categoryAxis);
            LinearAxis valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0, Title = "#" };
            model.Axes.Add(valueAxis);
            return new PlotView { Model = model };
        }
    }
}
