using System;
using System.Collections.Generic;

namespace Presenter
{
    public class SelectionMethodInputPresenter
    {
        private readonly IMethodSelectorView view;
        public event Action MethodChanged;
        public string MethodName => view.GetSelectedMethodName();

        public SelectionMethodInputPresenter(IMethodSelectorView view)
        {
            this.view = view;
            this.view.SelectedMethodChanged += () => MethodChanged?.Invoke();
            this.view.LoadMethodsNames(new List<string>() { UtilsPresenter.MovingAverageMethodName, UtilsPresenter.AdxMethodName });
        }
    }
}
