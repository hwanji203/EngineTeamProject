namespace Member.Kimyongmin._02.Code.Enemy
{
    public interface IAgentable
    {
        public bool IsAttaking { get; }

        public bool GetDamage(float damage, PlayerAttackType attackType)
        {
            if (IsAttaking == true && attackType == PlayerAttackType.Dash)
            {
                CounterDamage(damage);
                return true;
            }
            else
            {
                DefaultDamage(damage);
                return false;
            }
        }

        protected void CounterDamage(float damage);

        protected void DefaultDamage(float damage);
    }
}
