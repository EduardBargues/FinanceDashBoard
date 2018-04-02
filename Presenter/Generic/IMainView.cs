using Presenter.ClassificationDay;
using Presenter.Daily;
using Presenter.History;
using System;

namespace Presenter.Generic
{
    public interface IMainView
    {
        IHistoryView GetHistoryView();
        IDailyView GetDailyView();
        event Action DailyPageGotFocus;
        IDailyClassificationView GetDailyClassificationView();
    }
}
