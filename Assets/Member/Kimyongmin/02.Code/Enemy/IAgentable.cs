namespace Member.Kimyongmin._02.Code.Enemy
{
    public interface IAgentable
    {
        public bool IsAttaking { get; }
        public bool IsInvincibility { get; }

        public AttackReturnType GetDamage(float damage, PlayerAttackType attackType)
        {
            if (IsAttaking == true && attackType == PlayerAttackType.Dash)
            {
                CounterDamage(damage);
                return AttackReturnType.Counter;
            }
            else
            {
                if (IsInvincibility == true) return AttackReturnType.None;

                DefaultDamage(damage);
                return AttackReturnType.Default;
            }
        }

        protected void CounterDamage(float damage);

        protected void DefaultDamage(float damage);
    }
}
