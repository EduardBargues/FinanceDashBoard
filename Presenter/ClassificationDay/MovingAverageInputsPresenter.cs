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

        public int SlowMovingAveragePeriod => view.GetSlowMovingAveragePeriod();
        public int MediumMovingAveragePeriod => view.GetMediumMovingAveragePeriod();
        public int FastMovingAveragePeriod => view.GetFastMovingAveragePeriod();

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
