using System.Collections.Generic;

namespace Presenter.ClassificationDay
{
    public interface IDailyClassificationView
    {
        IDailyClassificationMethodSelectorView GetMethodSelectorView();
        void LoadClassifications(IEnumerable<DailyClassification> classifications);
    }
}
