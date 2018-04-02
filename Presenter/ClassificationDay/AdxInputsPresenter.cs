using System;

namespace Presenter.ClassificationDay
{
    public class AdxInputsPresenter
    {
        private readonly IAdxInputsView view;
        public event Action<AdxDailyClassificationInput> DoClassificationRequest;

        public AdxInputsPresenter(IAdxInputsView view)
        {
            this.view = view;
            this.view.DoClassificationRequest += DoClassificationAnalysis;
        }

        private void DoClassificationAnalysis()
        {
            AdxDailyClassificationInput input = new AdxDailyClassificationInput()
            {
                MovingAveragePeriod = view.GetMovingAveragePeriod(),
                DirectionalIndexPeriod = view.GetDxPeriod(),
            };
            DoClassificationRequest?.Invoke(input);
        }

        public int GetDxPeriod()
        {
            return view.GetDxPeriod();
        }

        public int GetMovingAveragePeriod()
        {
            return view.GetMovingAveragePeriod();
        }
    }
}
