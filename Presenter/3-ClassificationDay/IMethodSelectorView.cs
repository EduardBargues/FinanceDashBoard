using System;
using System.Collections.Generic;

namespace Presenter
{
    public interface IMethodSelectorView
    {
        string GetSelectedMethodName();
        event Action SelectedMethodChanged;
        void LoadMethodsNames(IEnumerable<string> methodsNames);
    }
}