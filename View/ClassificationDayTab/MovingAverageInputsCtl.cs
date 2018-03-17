using Presenter.ClassificationDay;
using System;
using System.Windows.Forms;

namespace View.ClassificationDayTab
{
    public partial class MovingAverageInputsCtl : UserControl, IMovingAverageInputsView
    {
        private MovingAverageInputsPresenter presenter;
        public event Action DoClassificationRequest;
        public MovingAverageInputsCtl()
        {
            InitializeComponent();
            SetNumericUpDownsInitialValues();
            presenter = new MovingAverageInputsPresenter(this);
            doAnalysisButton.MouseClick += (e, args) => DoClassificationRequest?.Invoke();
        }

        private void SetNumericUpDownsInitialValues()
        {
            slowMovingAverageNumericUpDown.Value = 21;
            slowMovingAverageNumericUpDown.Increment = 1;
            slowMovingAverageNumericUpDown.Minimum = 1;
            slowMovingAverageNumericUpDown.Maximum = 100;

            mediumMovingAverageNumericUpDown.Value = 13;
            mediumMovingAverageNumericUpDown.Increment = 1;
            mediumMovingAverageNumericUpDown.Minimum = 1;
            mediumMovingAverageNumericUpDown.Maximum = 100;

            fastMovingAverageNumericUpDown.Value = 8;
            fastMovingAverageNumericUpDown.Increment = 1;
            fastMovingAverageNumericUpDown.Minimum = 1;
            fastMovingAverageNumericUpDown.Maximum = 100;
        }

        public int GetSlowMovingAveragePeriod() => (int)slowMovingAverageNumericUpDown.Value;
        public int GetMediumMovingAveragePeriod() => (int)mediumMovingAverageNumericUpDown.Value;
        public int GetFastMovingAveragePeriod() => (int)fastMovingAverageNumericUpDown.Value;
    }
}
