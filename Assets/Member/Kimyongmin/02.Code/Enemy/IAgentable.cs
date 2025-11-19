namespace Member.Kimyongmin._02.Code.Enemy
{
    public interface IAgentable
    {
        public bool IsAttaking { get; }

        public void GetDamage(float damage, PlayerAttackType attackType)
        {
            if (IsAttaking == true && attackType == PlayerAttackType.Dash)
            {
                CounterDamage(damage);
            }
            else
            {
                DefaultDamage(damage);
            }
        }

        protected void CounterDamage(float damage);

        protected void DefaultDamage(float damage);
    }
}
