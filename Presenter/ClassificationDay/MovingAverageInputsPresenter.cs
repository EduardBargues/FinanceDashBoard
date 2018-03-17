using System;

namespace Presenter.ClassificationDay
{
    public class MovingAverageInputsPresenter
    {
        private readonly IMovingAverageInputsView view;
        public event Action<MovingAverageDailyClassificationInput> DoClassificationRequest;

        public MovingAverageInputsPresenter(IMovingAverageInputsView view)
        {
            this.view = view;
            this.view.DoClassificationRequest += DoClassificationAnalysis;
        }

        private void DoClassificationAnalysis()
        {
            MovingAverageDailyClassificationInput input = new MovingAverageDailyClassificationInput()
            {
                FastMovingAveragePeriod = view.GetFastMovingAveragePeriod(),
                MediumMovingAveragePeriod = view.GetMediumMovingAveragePeriod(),
                SlowMovingAveragePeriod = view.GetSlowMovingAveragePeriod()
            };
            DoClassificationRequest?.Invoke(input);
        }
    }
}
