namespace Presenter.ClassificationDay
{
    public interface IAdxInputsView : ITrigger
    {
        int GetDxPeriod();
        int GetMovingAveragePeriod();
    }
}
