using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public Button punchButton;
    // 何秒でボタンが再アクティブになるか
    const int COUNT = 10;
    int countTime;
    float timer;

    public void OnClickButton()
    {
        countTime = COUNT;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // 毎秒処理
        if (timer > 10f)
        {
            timer = 0f;
            if (countTime < 10)
            {
                countTime--;
                punchButton.interactable = false;
            }
            else
            {
                punchButton.interactable = true;
            }
        }
        Debug.Log(countTime);
    }
}
