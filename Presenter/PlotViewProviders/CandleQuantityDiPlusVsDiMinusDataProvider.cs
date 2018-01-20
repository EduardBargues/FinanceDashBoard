using CandleTimeSeriesAnalysis;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace Presenter.PlotViewProviders
{
    public class CandleQuantityDiPlusVsDiMinusDataProvider : I1DimensionDataProvider
    {
        private readonly CandleTimeSeries series;
        private readonly TimeSeries diPlus;
        private readonly TimeSeries diMinus;

        public CandleQuantityDiPlusVsDiMinusDataProvider(CandleTimeSeries series, TimeSeries diPlus, TimeSeries diMinus)
        {
            this.series = series;
            this.diPlus = diPlus;
            this.diMinus = diMinus;
        }

        public IEnumerable<double> GetUpData()
        {
            return series.Candles
                .Where(candle => diPlus[candle.Start] > diMinus[candle.Start])
                .Select(candle => diPlus[candle.Start] - diMinus[candle.Start]);
        }

        public IEnumerable<double> GetDownData()
        {
            return series.Candles
                .Where(candle => diPlus[candle.Start] < diMinus[candle.Start])
                .Select(candle => diMinus[candle.Start] - diPlus[candle.Start]);
        }
    }
}
