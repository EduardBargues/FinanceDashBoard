using CandleTimeSeriesAnalysis;
using Presenter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TimeSeriesAnalysis;

namespace View
{
    public partial class DailyCtl : UserControl, IDailyView
    {
        private readonly StatisticsCtl statisticsCtl;

        public DateTime? SelectedDay => gridDays.CurrentRow?.DataBoundItem as DateTime?;
        public event Action RefreshRequest;
        public bool DaysLoaded { get; set; }
        public int Period => (int)periodIndicator.Value;
        public int SmoothingPeriod => (int)smoothingPeriod.Value;

        public bool ComputeCandlesByDuration => candlesByDurationRadioButton.Checked;
        public bool ComputeCandlesByTicks => candlesByTicksRadioButton.Checked;
        public bool ComputeCandlesByVolume => candlesByVolumeRadioButton.Checked;

        public TimeSpan CandlesDuration => TimeSpan.FromMinutes((int)candlesDurationUpDown.Value);
        public int CandlesTicks => (int)candlesTicksUpDown.Value;
        public double CandlesVolume => Convert.ToDouble(candlesVolumeUpDown.Value);

        public DailyCtl()
        {
            InitializeComponent();
            candlesByDurationRadioButton.Checked = true;
            candlesDurationUpDown.Value = 10;
            periodIndicator.Value = 14;
            smoothingPeriod.Value = 14;
            refreshButton.Click += (a, b) => RefreshRequest?.Invoke();
            gridDays.SelectionChanged += (a, b) => RefreshRequest?.Invoke();

            statisticsCtl = new StatisticsCtl
            {
                Dock = DockStyle.Fill,
                Visible = true,
            };
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(statisticsCtl);
        }

        public IStatisticsView GetStatisticsView()
        {
            return statisticsCtl;
        }

        public void LoadDays(IEnumerable<DateTime> days)
        {
            DaysLoaded = true;

            gridDays.DataSource = days
                .OrderBy(day => day)
                .ToList();
            gridDays.Columns.Clear();
            gridDays.Columns.Add(nameof(DateTime.Date), "DAY");
            gridDays.Columns[0].DataPropertyName = nameof(DateTime.Date);
            gridDays.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridDays.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            int rowCount = gridDays.RowCount;
            if (rowCount > 1)
                gridDays.Rows[rowCount - 1].Selected = true;
            gridDays.Refresh();
        }

        public void LoadStrategies(IEnumerable<string> strategiesNames)
        {
            strategiesCombobox.DataSource = strategiesNames.ToList();
            strategiesCombobox.Refresh();
        }
        public void LoadData(CandleTimeSeries series, TimeSeries buySeries, TimeSeries sellSeries, IEnumerable<(TimeSeries, Color)> indicators)
        {
            seriesIndicatorCtl.LoadData(series, buySeries, sellSeries, indicators);
        }
    }
}
