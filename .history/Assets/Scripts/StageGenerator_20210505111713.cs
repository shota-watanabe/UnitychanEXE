using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public GameObject boxmion;
    public GameObject[] stageChips; //ステージチッププレハブ配列

    public int startChipIndex; //自動生成開始インデックス
    int enemyCount = 2;
    public List <GameObject> generateStageList = new List<GameObject>(); //生成済みステージチップリスト
    public EnemyController target1, target2;

    void Start()
    {
        boxmion = GameObject.Find("Boxmion");
    }

    void OnDisable()
    {
        target1.OnDestroyed.RemoveAllListeners();
    }
    void OnEnable()
    {
        target1.OnDestroyed.AddListener(() =>
        {
            Debug.Log("target1がDestroyされました");
        });

        target2.OnDestroyed.AddListener(() =>
        {
            Debug.Log("target2がDestroyされました");
        });
        
    }

    void Update()
    {
        if(enemyCount == 1)
        {
            //敵をすべて倒したらステージ更新
            UpdateStage();
        }
    }

   void UpdateStage()
    {
        //ステージ生成
        GameObject stageObject = GenerateStage();

        //生成したステージチップを管理リストに追加
        generateStageList.Add(stageObject);

        //ステージ保持上限値(1)に達したら古いステージを削除
        if(generateStageList.Count >= 1)DestroyOldStage();

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
