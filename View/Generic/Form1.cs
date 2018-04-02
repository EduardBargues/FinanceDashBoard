using Presenter.ClassificationDay;
using Presenter.Daily;
using Presenter.Generic;
using Presenter.History;
using System;
using System.Windows.Forms;
using View.ClassificationDayTab;
using View.DailyTab;
using View.HistoryTab;

namespace View.Generic
{
    public partial class Form1 : Form, IMainView
    {
        private readonly HistoryCtl historyCtl;
        private readonly DailyCtl dailyCtl;
        private readonly ClassificationDayCtl classificationDayCtl;

        public event Action DailyPageGotFocus;

        public Form1()
        {
            InitializeComponent();
            Controls.Clear();

            TabPage historyPage = new TabPage
            {
                Text = "HISTORY"
            };
            historyCtl = new HistoryCtl
            {
                Dock = DockStyle.Fill,
                Visible = true
            };
            historyPage.Controls.Add(historyCtl);

            TabPage dailyPage = new TabPage
            {
                Text = "DAILY"
            };
            dailyCtl = new DailyCtl
            {
                Dock = DockStyle.Fill,
                Visible = true
            };
            dailyPage.Controls.Add(dailyCtl);

            TabPage classificationPage = new TabPage
            {
                Text = "CATEGORIZATION"
            };
            classificationDayCtl = new ClassificationDayCtl()
            {
                Dock = DockStyle.Fill,
                Visible = true
            };
            classificationPage.Controls.Add(classificationDayCtl);

            TabControl tabControl = new TabControl { Dock = DockStyle.Fill };
            tabControl.TabPages.Add(historyPage);
            tabControl.TabPages.Add(dailyPage);
            tabControl.TabPages.Add(classificationPage);

            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            Controls.Add(tabControl);

            MainPresenter mainPresenter = new MainPresenter(this);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 1)
                DailyPageGotFocus?.Invoke();
        }


        public IHistoryView GetHistoryView() => historyCtl;
        public IDailyView GetDailyView() => dailyCtl;
        public IDailyClassificationView GetDailyClassificationView() => classificationDayCtl;
    }
}
