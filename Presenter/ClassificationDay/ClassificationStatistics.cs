using Presenter.Generic;
using System.Collections.Generic;

namespace Presenter.ClassificationDay
{
    public class ClassificationStatistics
    {
        public int NumberOfSuccesses { get; set; }
        public int NumberOfFails { get; set; }
        public int NumberOfConsecutiveSuccesses { get; set; }
        public int NumberOfConsecutiveFails { get; set; }

        public IEnumerable<ParameterVisual> GetParameters()
        {
            yield return new ParameterVisual()
            {
                Value = NumberOfSuccesses,
                Description = "Successes:"
            };
            yield return new ParameterVisual()
            {
                Value = NumberOfFails,
                Description = "Fails:"
            };
            yield return new ParameterVisual()
            {
                Value = NumberOfConsecutiveSuccesses,
                Description = "Consecutive successes:"
            };
            yield return new ParameterVisual()
            {
                Value = NumberOfConsecutiveFails,
                Description = "Consecutive Fails:"
            };
        }
    }
}
