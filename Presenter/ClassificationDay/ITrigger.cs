using System;

namespace Presenter.ClassificationDay
{
    public interface ITrigger
    {
        event Action DoClassificationRequest;
    }
}
