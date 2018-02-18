namespace Model.Strategies
{
    public abstract class Strategy
    {
        public string Name { get; set; }

        protected Strategy(string name)
        {
            Name = name;
        }
    }

    public class StrategyVicente : Strategy
    {
        public StrategyVicente(string name) : base(name)
        {
        }
    }
    public class StrategyEduard : Strategy
    {
        public StrategyEduard(string name) : base(name)
        {
        }
    }
}