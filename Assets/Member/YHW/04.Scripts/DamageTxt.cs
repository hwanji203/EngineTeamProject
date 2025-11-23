using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class DamageTxt : MonoSingleton<DamageTxt>
{
    [SerializeField]private GameObject _Textprefab;
    private int _poolCount;
    [SerializeField]private Transform _spawn;
    GameObject[] obj;
    [SerializeField] private float _spawnRadius = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        _poolCount = 10;

        obj = new GameObject[_poolCount];
        for (int i = 0; i < _poolCount; i++)
        {
            obj[i] = Instantiate(_Textprefab, transform);
            obj[i].SetActive(false);
        }
    }

    

    public void ShowDamageTxt(float damage)
    {
        StartCoroutine(ShowDamageCoroutine(damage));
        
    }

    private IEnumerator ShowDamageCoroutine(float damage)
    {
        GameObject crntObj = null;
        for (int i = 0; i < _poolCount; i++)
        {
            if (obj[i].activeSelf == false)
            {
                crntObj = obj[i];
                obj[i].transform.position = _spawn.position;
                obj[i].SetActive(true);
                obj[i].GetComponent<TextMeshPro>().text = "-" + damage.ToString();
                Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;

                Vector3 spawnPos = _spawn.position + new Vector3(randomCircle.x, randomCircle.y, 0);

                obj[i].transform.position = spawnPos;
                break;
            }
        }
        yield return new WaitForSeconds(1f);
        crntObj.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_spawn.position, _spawnRadius);
    }
}
