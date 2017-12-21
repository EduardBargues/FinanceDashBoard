using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard
{
    public interface IMainView
    {
        IHistoryView GetHistoryView();
        IDailyView GetDailyView();
        event Action DailyPageGotFocus;
    }

    public class MainPresenter
    {
        private readonly IMainView view;
        private readonly DailyPresenter dailyPresenter;
        private readonly HistoryPresenter historyPresenter;

        public MainPresenter(IMainView view)
        {
            this.view = view;
            dailyPresenter = new DailyPresenter(view.GetDailyView());
            historyPresenter = new HistoryPresenter(view.GetHistoryView());
            this.view.DailyPageGotFocus += () => dailyPresenter.LoadDays();
        }
    }
}
