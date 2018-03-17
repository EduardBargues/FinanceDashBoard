namespace Presenter.ClassificationDay
{
    public interface IMovingAverageInputsView : ITrigger
    {
        int GetSlowMovingAveragePeriod();
        int GetMediumMovingAveragePeriod();
        int GetFastMovingAveragePeriod();
    }
}
