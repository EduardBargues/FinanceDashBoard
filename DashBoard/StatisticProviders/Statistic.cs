namespace DashBoard.StatisticProviders
{
    public class Statistic
    {
        public string Description { get; }
        public double UpValue { get; }
        public double DownValue { get; }

        public Statistic(string desc, double uv, double dv)
        {
            Description = desc;
            UpValue = uv;
            DownValue = dv;
        }
    }
}
