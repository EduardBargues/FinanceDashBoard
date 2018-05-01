using System.Collections.Generic;

namespace Presenter.Generic
{
    public interface IParameterListView
    {
        void LoadData(IEnumerable<ParameterVisual> parameters);
    }
}
