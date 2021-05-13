using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    int enemyCount = 2;
    int currentChipIndex;

    public GameObject[] stageChips; //ステージチッププレハブ配列
    public int startChipIndex; //自動生成開始インデックス
    public List <GameObject> generateStageList = new List<GameObject>(); //生成済みステージチップリスト

    void Start()
    {
        
    }

    void Update()
    {
        if(enemyCount == 0)
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
