using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesAnalysis;

namespace Model.Strategies
{
    public class DxAndAdxPatchSelector : IPatchSelector
    {
        private double toleranceDx;

        public DxAndAdxPatchSelector(double toleranceDx)
        {
            this.toleranceDx = toleranceDx;
        }

        public IEnumerable<IEnumerable<DateTime>> GetGoodPatches(TimeSeries dx, TimeSeries adx, TimeSeries diPlus, TimeSeries diMinus)
        {
            List<DateTime> currentPatch = new List<DateTime>();
            foreach (DateValue dv in dx.Values.Where((dv, index) => index > 0))
            {
                double derivative = adx.GetDerivative(dv.Date, DifferentiationMode.Left, TimeScale.Minutes);
                bool startPatch = derivative >= 0;// 0.3 / 60;
                if (startPatch)
                    currentPatch.Add(dv.Date);
                else
                {
                    bool currentPatchUnderCreation = currentPatch.Any();
                    if (currentPatchUnderCreation)
                    {
                        currentPatch.Add(dv.Date);
                        bool goesUp = Math.Sign(derivative) > 0;
                        if (!goesUp)
                        {
                            yield return currentPatch;
                            currentPatch = new List<DateTime>();
                        }
                    }
                }
            }
            if (currentPatch.Any())
                yield return currentPatch;
        }
    }
}