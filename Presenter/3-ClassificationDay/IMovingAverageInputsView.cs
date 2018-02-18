namespace Presenter
{
    public interface IMovingAverageInputsView
    {
        int GetSlowMovingAveragePeriod();
        int GetMediumMovingAveragePeriod();
        int GetFastMovingAveragePeriod();
    }
}
