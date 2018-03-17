namespace Presenter.ClassificationDay
{
    public interface IDailyClassificationMethodSelectorView
    {
        IMovingAverageInputsView GetMovingAverageView();
        IAdxInputsView GetAdxInputsView();
    }
}