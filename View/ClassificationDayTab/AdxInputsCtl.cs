using Presenter.ClassificationDay;
using System;
using System.Windows.Forms;

namespace View.ClassificationDayTab
{
    public partial class AdxInputsCtl : UserControl, IAdxInputsView
    {
        private AdxInputsPresenter presenter;
        public event Action DoClassificationRequest;

        public AdxInputsCtl()
        {
            InitializeComponent();
            SetNumericUpDownsInitialValues();
            presenter = new AdxInputsPresenter(this);
            DoClassificationButton.MouseClick += (e, args) => DoClassificationRequest?.Invoke();
        }

        private void SetNumericUpDownsInitialValues()
        {
            dxPeriodNumericUpDown.Value = 13;
            dxPeriodNumericUpDown.Increment = 1;
            dxPeriodNumericUpDown.Minimum = 1;
            dxPeriodNumericUpDown.Maximum = 100;
            averagePeriodNumericUpDown.Value = 13;
            averagePeriodNumericUpDown.Increment = 1;
            averagePeriodNumericUpDown.Minimum = 1;
            averagePeriodNumericUpDown.Maximum = 100;
        }

        public int GetDxPeriod() => (int)dxPeriodNumericUpDown.Value;
        public int GetMovingAveragePeriod() => (int)averagePeriodNumericUpDown.Value;
    }
}