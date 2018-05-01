using System.Collections.Generic;

namespace Presenter.Generic
{
    public class ParameterListPresenter
    {
        private IParameterListView view;

        public ParameterListPresenter(IParameterListView view)
        {
            this.view = view;
        }

        public void LoadData(IEnumerable<ParameterVisual> parameters)
        {
            view.LoadData(parameters);
        }
    }
}
