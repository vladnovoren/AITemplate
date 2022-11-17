namespace AI.Chasing
{
    public class OppositeDecision : IDecision
    {
        public OppositeDecision(IDecision baseDecision)
        {
            _baseDecision = baseDecision;
        }

        public bool Decide()
        {
            return !_baseDecision.Decide();
        }

        private IDecision _baseDecision;
    }
}
