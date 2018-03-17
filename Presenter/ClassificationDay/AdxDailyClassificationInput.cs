﻿using Model.ClassificationDayMethods;
using System;

namespace Presenter.ClassificationDay
{
    public class AdxDailyClassificationInput : IDailyClassificationInput
    {
        public int DirectionalIndexPeriod { get; set; }
        public int MovingAveragePeriod { get; set; }
        public double AdxMinimumValue { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }

        public IDailyClassificationMethod GetMethod() => new AdxDailyClassificationMethod(DirectionalIndexPeriod,
                                                                                          MovingAveragePeriod,
                                                                                          AdxMinimumValue,
                                                                                          StartDay,
                                                                                          EndDay);
    }
}
