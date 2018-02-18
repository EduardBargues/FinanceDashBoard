using Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace View
{
    public partial class SelectionMethodInputCtl : UserControl, IMethodSelectorView
    {
        public event Action SelectedMethodChanged;
        private SelectionMethodInputPresenter presenter;

        public SelectionMethodInputCtl()
        {
            InitializeComponent();

            methodsComboBox.SelectedValueChanged += MethodsComboBox_SelectedValueChanged;
            presenter = new SelectionMethodInputPresenter(this);
        }

        private void MethodsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        public string GetSelectedMethodName()
        {
            return methodsComboBox.SelectedText;
        }

        public void LoadMethodsNames(IEnumerable<string> methodsNames)
        {
            methodsComboBox.DataSource = methodsNames.ToList();
        }

        private UserControl GetMethodInputsControl(string methodName)
        {
            switch (methodName)
            {
                case UtilsPresenter.AdxMethodName:
                    return new AdxInputsCtl();
                case UtilsPresenter.MovingAverageMethodName:
                    return new MovingAverageInputsCtl();
                default:
                    return null;
            }
        }
    }
}
