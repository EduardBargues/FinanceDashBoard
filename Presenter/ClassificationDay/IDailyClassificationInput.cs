using Model.ClassificationDayMethods;

namespace Presenter.ClassificationDay
{
    public interface IDailyClassificationInput
    {
        IDailyClassificationMethod GetMethod();
    }
}
