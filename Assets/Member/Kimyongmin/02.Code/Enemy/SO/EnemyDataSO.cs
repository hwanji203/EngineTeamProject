using UnityEngine;

public enum EnemyType
{
    NotAggressive,
    Neutral,
    Aggressive
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public EnemyType EnemyType;
    
    public float moveSpeed;
    public float detectDelay;
}
