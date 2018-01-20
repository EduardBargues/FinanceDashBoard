using Presenter;
using System;
using System.Windows.Forms;

namespace View
{
    public partial class Form1 : Form, IMainView
    {
        private MainPresenter mainPresenter;
        private readonly HistoryCtl historyCtl;
        private readonly DailyCtl dailyCtl;
        private readonly CategorizationCtl categorizationCtl;

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

            TabPage categorizationPage = new TabPage
            {
                Text = "CATEGORIZATION"
            };
            categorizationCtl = new CategorizationCtl()
            {
                Dock = DockStyle.Fill,
                Visible = true
            };
            categorizationPage.Controls.Add(categorizationCtl);

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
        public ICategorizationView GetCategorizationView()
        {
            return categorizationCtl;
        }
    }
}
