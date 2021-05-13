using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public GameObject boxmion;
    public GameObject[] stageChips; //ステージチッププレハブ配列

    public int startChipIndex; //自動生成開始インデックス
    int enemyCount = 2;
    public List<GameObject> generateStageList = new List<GameObject>(); //生成済みステージチップリスト
    public EnemyController target1, target2;
    void Start()
    {
    }

    void OnEnable()
    {
        //boxmion = GameObject.Find("Boxmion");

        target1.OnDestroyed.AddListener(() =>
        {
            enemyCount--;
        });

        target2.OnDestroyed.AddListener(() =>
        {
            enemyCount--;
        });

    }
    public void SetEnemies(GameObject stageObject)
    {
        //すべての敵を取得する（敵はステージの子オブジェクトなのでGetComponentsInChildren<>が使える
        EnemyController[] enemies = stageObject.GetComponentsInChildren<EnemyController>();
        //敵の数を設定
        enemyCount = enemies.Length;
        foreach (EnemyController enemy in enemies)
        {
            //それぞれの敵に対して死亡時の処理を登録する
            enemy.OnDestroyed.AddListener(() =>
            {
                //Debug.Log(enemy.name + "がDestroyされました");
                enemyCount--;
            });
        }
    }

    void Update()
    {

        if (enemyCount == 0)
        {
            //敵をすべて倒したらステージ更新
            UpdateStage();
            enemyCount = 2;
        }
    }

    void UpdateStage()
    {
        //ステージ生成
        GameObject stageObject = GenerateStage();
        //敵の数と敵の死亡時コールバックを設定
        SetEnemies(stageObject);

        //生成したステージチップを管理リストに追加
        generateStageList.Add(stageObject);

        //ステージ保持上限値(1)に達したら古いステージを削除
        if (generateStageList.Count >= 1) DestroyOldStage();

    }

    GameObject GenerateStage()
    {

        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject = (GameObject)Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, 6),
            Quaternion.identity
            );

        return stageObject;
    }

    void DestroyOldStage()
    {
        GameObject oldStage = generateStageList[0];
        generateStageList.RemoveAt(0);
        Destroy(oldStage);
    }

}
