using Model.ClassificationDayMethods;
using System;

namespace Presenter.ClassificationDay
{
    public class MovingAverageDailyClassificationInput : IDailyClassificationInput
    {
        public int SlowMovingAveragePeriod { get; set; }
        public int MediumMovingAveragePeriod { get; set; }
        public int FastMovingAveragePeriod { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public IDailyClassificationMethod GetMethod() => new MovingAveragesDailyClassificationMethod(SlowMovingAveragePeriod,
                                                                                                     MediumMovingAveragePeriod,
                                                                                                     FastMovingAveragePeriod,
                                                                                                     StartDay,
                                                                                                     EndDay);
    }
}
