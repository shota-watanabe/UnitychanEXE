using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public GameObject boxmion;
    public GameObject[] stageChips; //ステージチッププレハブ配列

    public int startChipIndex; //自動生成開始インデックス
    int enemyCount = 2;
    int enemyDefeatCount = 0;
    public List<GameObject> generateStageList = new List<GameObject>(); //生成済みステージチップリスト
    public EnemyController target1, target2;
    public GameController gameController;

    void OnEnable()
    {
        //ゲーム開始時に登場する敵に死亡時のイベントを追加
        target1.OnDestroyed.AddListener(() =>
        {
            enemyCount--;
            enemyDefeatCount++;
        });

        target2.OnDestroyed.AddListener(() =>
        {
            enemyCount--;
            enemyDefeatCount++;
        });

    }
    public void SetEnemies(GameObject stageObject)
    {
        //シーン上のすべての敵を取得する
        EnemyController[] enemies = stageObject.GetComponentsInChildren<EnemyController>();
        //敵の数を設定
        enemyCount = enemies.Length;
        foreach (EnemyController enemy in enemies)
        {
            //それぞれの敵に対して死亡時の処理を登録する
            enemy.OnDestroyed.AddListener(() =>
            {
                enemyCount--;
                enemyDefeatCount++;
            });
        }
    }

    void Update()
    {
        //ステージ上の敵をすべて倒したらステージ更新
        if (enemyCount == 0 && enemyDefeatCount != 6)
        {
            UpdateStage();
            enemyCount = 2;
        }
        //敵を一定数倒したらゲームクリア
        else if(enemyDefeatCount == 6)
        {
            enabled = false;
            gameController.GetComponent<GameController>().GameClear();
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

    //指定のインデックス位置にStageオブジェクトをランダムに生成
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

    //古いステージを削除
    void DestroyOldStage()
    {
        GameObject oldStage = generateStageList[0];
        generateStageList.RemoveAt(0);
        Destroy(oldStage);
    }

}
