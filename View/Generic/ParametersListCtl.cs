using Presenter.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace View.Generic
{
    public partial class ParametersListCtl : UserControl, IParameterListView
    {
        public ParametersListCtl()
        {
            InitializeComponent();
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public void LoadData(IEnumerable<ParameterVisual> parameters)
        {
            dataGridView.DataSource = parameters.ToList();
        }
    }
}
