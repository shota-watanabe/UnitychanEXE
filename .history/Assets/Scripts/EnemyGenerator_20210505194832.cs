using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject Boxmion;
    public EnemyController target;

    int enemyCount = 0;

    void Update()
    {
        if(enemyCount < 1)EnemyGenarate();
        Debug.Log(enemyCount);
    }

    void OnDisable()
    {
        target.OnDestroyed.RemoveAllListeners();
    }
    void OnEnable()
    {
        target.OnDestroyed.AddListener(() =>
        {
            Debug.Log("targetがDestroyされました");
            enemyCount--;
        });
    }

    public void EnemyGenarate()
    {
        //Boxmion = new GameObject("Boxmion");

        Boxmion = Instantiate(Boxmion) as GameObject;

        var m = RandomNumGenerate();

        if (m.x % 2 == 0 && m.z % 2 == 0)
        {
            Boxmion.transform.position = new Vector3(m.x, 0, m.z);
        }
        else
        {
            RandomNumGenerate();
        }

        Debug.Log("生成した");
        enemyCount++;
    }

    (float x, float z) RandomNumGenerate()
    {
        var x = Random.Range(-2, 3);
        var z = Random.Range(4, 9);

        return (x, z);
    }
}
