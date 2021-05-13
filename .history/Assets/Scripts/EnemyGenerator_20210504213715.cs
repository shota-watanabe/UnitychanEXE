using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject Boxmion;
    public GameObject enemyController;
    
    float span = 1.0f;
    float delta = 0;
    int enemyCount = 0;

    void Update()
    {
        this.delta += Time.deltaTime;
        if(this.delta > this.span && enemyCount < 2)
        {
            EnemyGenerate();
            enemyCount ++;
        }
    }

    public void EnemyGenerate()
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

    (float x, float z)RandomNumGenerate()
    {
        var x = Random.Range(-2, 3);
        var z = Random.Range(4, 9);

        return (x, z);
    }
}
