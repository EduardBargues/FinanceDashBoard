using Presenter.ClassificationDay;
using System;
using System.Windows.Forms;

namespace View.ClassificationDayTab
{
    public partial class SelectionDailyClassificationMethodInputCtl : UserControl, IDailyClassificationMethodSelectorView
    {
        private MovingAverageInputsCtl movingAverageInputsCtl;
        private AdxInputsCtl adxInputsCtl;
        private DailyClassificationInputPresenter presenter;

        public event Action SelectedMethodChanged;

        public SelectionDailyClassificationMethodInputCtl()
        {
            InitializeComponent();
            SetControls();
            presenter = new DailyClassificationInputPresenter(this);
        }

        private void SetControls()
        {
            Controls.Clear();
            TabControl tabControl = new TabControl { Dock = DockStyle.Fill };
            Controls.Add(tabControl);

            tabControl.TabPages.Clear();

            TabPage threeMovingAveragesTab = new TabPage("Moving Averages classifier");
            movingAverageInputsCtl = new MovingAverageInputsCtl { Dock = DockStyle.Fill };
            threeMovingAveragesTab.Controls.Add(movingAverageInputsCtl);
            tabControl.TabPages.Add(threeMovingAveragesTab);

            TabPage adxTab = new TabPage("Adx classifier");
            adxInputsCtl = new AdxInputsCtl() { Dock = DockStyle.Fill };
            adxTab.Controls.Add(adxInputsCtl);
            tabControl.TabPages.Add(adxTab);
        }

        public IMovingAverageInputsView GetMovingAverageView() => movingAverageInputsCtl;

        public IAdxInputsView GetAdxInputsView() => adxInputsCtl;
    }
}
