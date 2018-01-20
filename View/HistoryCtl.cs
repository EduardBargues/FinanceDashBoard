﻿using CandleTimeSeriesAnalysis;
using Presenter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TimeSeriesAnalysis;

namespace View
{
    public partial class HistoryCtl : UserControl, IHistoryView
    {
        public event Action LoadDataRequest;

        public HistoryCtl()
        {
            InitializeComponent();
            dateTimePickerFrom.Value = DateTime.Today.AddMonths(-3);
            buttonRefresh.Click += (a, b) => LoadDataRequest?.Invoke();
            numericUpDownDxPeriod.Value = 14;
            numericUpDownAdxSmoothingPeriod.Value = 14;
        }

        public DateTime StartDate => dateTimePickerFrom.Value;
        public DateTime EndDate => dateTimePickerTo.Value;
        public int IndicatorPeriod => (int)numericUpDownDxPeriod.Value;
        public int SmoothingPeriod => (int)numericUpDownAdxSmoothingPeriod.Value;

        public void LoadData(CandleTimeSeries series, IEnumerable<(TimeSeries, Color)> indicators)
        {
            seriesIndicatorCtl.LoadData(series, null, null, indicators);
        }
    }
}