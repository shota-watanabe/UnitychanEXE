using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public EnemyController target;
    GameObject boxmion;

    float span = 2.0f;
    float delta = 0;
    int enemyCount = 0;

    void Update()
    {
        if (enemyCount < 2) EnemyGenerate();
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
            EnemyGenerate();
        });
    }

    public void EnemyGenerate()
    {
        GameObject origin = GameObject.Find("Boxmion"); //元になるHogeを探す
        GameObject hogeGameObject = new GameObject(origin); //Hogeを複製
        /*
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

        enemyCount ++; */
    }

    (float x, float z) RandomNumGenerate()
    {
        var x = Random.Range(-2, 3);
        var z = Random.Range(4, 9);

        return (x, z);
    }
}
