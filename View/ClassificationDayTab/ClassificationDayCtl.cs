using Presenter.ClassificationDay;
using Presenter.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace View.ClassificationDayTab
{
    public partial class ClassificationDayCtl : UserControl, IDailyClassificationView
    {
        public ClassificationDayCtl()
        {
            InitializeComponent();
            classificationGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            classificationGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            classificationGrid.SelectionChanged += (sender, args) => SelectedDayChanged?.Invoke();
            startDateCtl.Value = DateTime.Today.AddMonths(-3);
            endDayCtl.Value = DateTime.Today;
        }

        public void LoadClassifications(IEnumerable<DailyClassification> classifications)
        {
            classificationGrid.DataSource = classifications.ToList();
        }

        public IAdxInputsView GetAdxInputsView()
        {
            return dailyClassificationMethodInputCtl.GetAdxInputsView();
        }

        public IMovingAverageInputsView GetMovingAverageInputView()
        {
            return dailyClassificationMethodInputCtl.GetMovingAverageView();
        }

        public DateTime GetStartDay()
        {
            return startDateCtl.Value.Date;
        }

        public DateTime GetEndDay()
        {
            return endDayCtl.Value.Date;
        }

        public ISeriesIndicatorView GetSeriesIndicatorView()
        {
            return seriesIndicatorCtl;
        }

        public event Action SelectedDayChanged;
        public DateTime SelectedDay => classificationGrid.SelectedRows.Count > 0
            ? ((DailyClassification)classificationGrid.SelectedRows[0].DataBoundItem).Day
            : DateTime.MinValue;
    }
}
