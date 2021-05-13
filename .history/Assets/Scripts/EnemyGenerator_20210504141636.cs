using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject Boxmion;
    public GameObject Dragon;
    public EnemyController enemyController;

    int enemyCount;
    int enemyDefeatCount;
    void Update()
    {
        enemyController.DestroyEvent += delegate { enemyCount--; };
        enemyController.DestroyEvent += delegate { enemyDefeatCount++; };

        if (enemyCount < 1 && enemyDefeatCount <= 1)
        {
            EnemyGenarate();
            enemyCount++;
        }

        Debug.Log(enemyCount);
        Debug.Log(enemyDefeatCount);
    }

    public void EnemyGenarate()
    {
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
    }

    public void BossGenerate()
    {
        Dragon = Instantiate(Dragon) as GameObject;
        Dragon.transform.position = new Vector3(0, 0, 8);
    }

    (float x, float z) RandomNumGenerate()
    {
        var x = Random.Range(-2, 3);
        var z = Random.Range(4, 9);

        return (x, z);
    }
}
