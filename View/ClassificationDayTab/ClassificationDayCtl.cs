using Presenter.ClassificationDay;
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
        }

        public IDailyClassificationMethodSelectorView GetMethodSelectorView() => dailyClassificationMethodInputCtl;
        public void LoadClassifications(IEnumerable<DailyClassification> classifications) => classificationGrid.DataSource = classifications.ToList();
    }
}
