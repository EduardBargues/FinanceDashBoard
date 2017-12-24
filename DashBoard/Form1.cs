using System;
using System.Windows.Forms;

namespace DashBoard
{
    public partial class Form : System.Windows.Forms.Form, IMainView
    {
        private MainPresenter mainPresenter;
        private readonly HistoryCtl historyCtl;
        private readonly DailyCtl dailyCtl;

        public event Action DailyPageGotFocus;

        public Form()
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

            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.TabPages.Add(historyPage);
            tabControl.TabPages.Add(dailyPage);
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            Controls.Add(tabControl);

            mainPresenter = new MainPresenter(this);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 1)
                DailyPageGotFocus?.Invoke();
        }


        public IHistoryView GetHistoryView()
        {
            return historyCtl;
        }
        public IDailyView GetDailyView()
        {
            return dailyCtl;
        }
    }
}
