using Model.ClassificationDayMethods;
using System;

namespace Presenter.ClassificationDay
{
    public class DailyClassificationInputPresenter
    {
        private readonly IDailyClassificationMethodSelectorView view;
        public event Action<IDailyClassificationMethod> DoAnalysisRequest;

        public DailyClassificationInputPresenter(IDailyClassificationMethodSelectorView view)
        {
            this.view = view;
            AdxInputsPresenter adxPresenter = new AdxInputsPresenter(view.GetAdxInputsView());
            adxPresenter.DoClassificationRequest += OnDoClassificationRequest;
            MovingAverageInputsPresenter movingAveragePresenter = new MovingAverageInputsPresenter(view.GetMovingAverageView());
            movingAveragePresenter.DoClassificationRequest += OnDoClassificationRequest;
        }

        public void OnDoClassificationRequest(IDailyClassificationInput input)
        {
            IDailyClassificationMethod method = input.GetMethod();
            DoAnalysisRequest?.Invoke(method);
        }
    }
}