using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUtils;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace DashBoard
{
    public partial class StatisticsCtl : UserControl, IStatisticsView
    {
        public event Action RefreshRequest;
        public string SelectedStatisticsLabel
        {
            get
            {
                string label = null;
                DataGridViewSelectedRowCollection selectedRows = grid.SelectedRows;
                if (selectedRows.Count > 0)
                {
                    DataGridViewRow row = selectedRows[0];
                    DataRow dataRow = ((DataRowView)row.DataBoundItem).Row;
                    label = dataRow[0].ToString();
                }
                return label;
            }
        }
        public int Groups => (int)groups.Value;

        public StatisticsCtl()
        {
            InitializeComponent();
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            groups.Value = 10;
        }

        public void LoadData(DataTable data)
        {
            grid.DataSource = data;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            grid.Refresh();
        }
        public void LoadHistograms(PlotView histograms)
        {
            groupBox.Controls.Clear();
            if (histograms != null)
            {
                groupBox.Controls.Add(histograms);
                histograms.Dock = DockStyle.Fill;
                histograms.Visible = true;
            }
            groupBox.Refresh();
        }
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshRequest?.Invoke();
        }
    }
}
