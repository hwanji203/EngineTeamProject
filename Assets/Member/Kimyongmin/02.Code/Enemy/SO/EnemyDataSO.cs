using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.SO
{
    public enum EnemyType
    {
        NotAggressive,
        Neutral,
        Aggressive
    }

    [CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EnemyData")]
    public class EnemyDataSo : ScriptableObject
    {
        public EnemyType EnemyType;

        public float attackDelay;
        public float idleSpeed;
        public float moveSpeed;
        public float detectDelay;
    }
}