using Presenter.ClassificationDay;
using Presenter.Daily;
using Presenter.History;

namespace Presenter.Generic
{
    public class MainPresenter
    {
        private readonly IMainView view;

        public MainPresenter(IMainView view)
        {
            this.view = view;

            DailyPresenter dailyPresenter = new DailyPresenter(view.GetDailyView());
            this.view.DailyPageGotFocus += () =>
            {
                dailyPresenter.LoadDays();
                dailyPresenter.LoadStrategies();
            };
            HistoryPresenter historyPresenter = new HistoryPresenter(view.GetHistoryView());
            DailyClassificationPresenter dailyClasiiClassificationPresenter = new DailyClassificationPresenter(view.GetDailyClassificationView());
        }
    }
}
