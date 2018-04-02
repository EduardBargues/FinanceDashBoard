using Model.ClassificationDayMethods;

namespace Presenter.ClassificationDay
{
    public class MovingAverageDailyClassificationInput : IDailyClassificationInput
    {
        public int SlowMovingAveragePeriod { get; set; }
        public int MediumMovingAveragePeriod { get; set; }
        public int FastMovingAveragePeriod { get; set; }

        public IDailyClassificationMethod GetMethod() => new MovingAveragesDailyClassificationMethod(SlowMovingAveragePeriod,
                                                                                                     MediumMovingAveragePeriod,
                                                                                                     FastMovingAveragePeriod);
    }
}
