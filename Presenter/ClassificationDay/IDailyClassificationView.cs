﻿using Presenter.Generic;
using System;
using System.Collections.Generic;

namespace Presenter.ClassificationDay
{
    public interface IDailyClassificationView
    {
        void LoadClassifications(IEnumerable<DailyClassification> classifications);
        IAdxInputsView GetAdxInputsView();
        IMovingAverageInputsView GetMovingAverageInputView();
        DateTime GetStartDay();
        DateTime GetEndDay();
        ISeriesIndicatorView GetSeriesIndicatorView();
        event Action SelectedDayChanged;
        DateTime SelectedDay { get; }
        IParameterListView GetClassificationStatisticsView();
    }
}
