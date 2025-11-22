using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Battle/Enemy Database")]
public class EnemyDatabase : ScriptableObject
{
    [Header("Enemy Prefabs")]
    public EnemyPrefabData[] enemyPrefabs;
    
    private Dictionary<BattleEnemyType, GameObject> prefabDict;
    
    public GameObject GetPrefab(BattleEnemyType type)
    {
        if (prefabDict == null)
        {
            InitializeDictionary();
        }
        
        if (prefabDict.TryGetValue(type, out GameObject prefab))
        {
            return prefab;
        }
        
        Debug.LogError($"Enemy type {type} not found");
        return null;
    }
    
    private void InitializeDictionary()
    {
        prefabDict = new Dictionary<BattleEnemyType, GameObject>();
        
        foreach (var data in enemyPrefabs)
        {
            if (!prefabDict.ContainsKey(data.enemyType))
            {
                prefabDict.Add(data.enemyType, data.prefab);
            }
            else
            {
                Debug.LogWarning($"Duplicate enemy type: {data.enemyType}");
            }
        }
    }
}