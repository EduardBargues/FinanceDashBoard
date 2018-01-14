using CommonUtils;
using MoreLinq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Linq;

namespace DashBoard.PlotViewProviders
{
    public class ScatterPlotViewProvider : IPlotViewProvider
    {
        private readonly I2DimensionsDataProvider dataProvider;
        private readonly string title;
        private readonly string ylabel;
        private readonly string xlabel;

        public ScatterPlotViewProvider(I2DimensionsDataProvider dataProvider, string title, string ylabel, string xlabel)
        {
            this.dataProvider = dataProvider;
            this.title = title;
            this.ylabel = ylabel;
            this.xlabel = xlabel;
        }
        public PlotView GetPlotView()
        {
            ScatterSeries upSeries = Plotter.GetDataPointSeries<ScatterSeries>(
                (nameof(ScatterSeries.Title), "UP")
                , (nameof(ScatterSeries.ActualMarkerFillColor), OxyColors.Black)
                , (nameof(ScatterSeries.MarkerFill), OxyColors.Black)
                );
            dataProvider.GetUpData()
                .Select(data => new ScatterPoint(data.Item1, data.Item2))
                .ForEach(p => upSeries.Points.Add(p));
            PlotModel model = new PlotModel
            {
                LegendPlacement = LegendPlacement.Inside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                Title = title
            };
            model.Series.Add(upSeries);
            ScatterSeries downSeries = Plotter.GetDataPointSeries<ScatterSeries>(
                (nameof(ScatterSeries.Title), "DOWN")
                , (nameof(ScatterSeries.ActualMarkerFillColor), OxyColors.Red)
                , (nameof(ScatterSeries.MarkerFill), OxyColors.Red)
                );
            dataProvider.GetDownData()
                .Select(data => new ScatterPoint(data.Item1, data.Item2))
                .ForEach(p => downSeries.Points.Add(p));
            model.Series.Add(downSeries);
            //model.Axes.Clear();
            //model.Axes.Add(new LinearAxis() { Title = xlabel });
            //model.Axes.Add(new LinearAxis() { Title = ylabel });
            return new PlotView { Model = model };
        }
    }
}
