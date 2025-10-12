using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float moveSpeed;
    public float detectDelay;
}
