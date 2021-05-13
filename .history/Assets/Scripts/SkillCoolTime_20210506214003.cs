using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public Button punchButton;
    // 何秒でボタンが再アクティブになるか
    const int COUNT = 1;
    int countTime;
    float timer;

    void Awake()
    {

    }
    public void OnClickButton()
    {
        punchButton.interactable = false;
        countTime = COUNT;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // 毎秒処理
        if (timer > 1f)
        {
            if (punchButton.interactable == false)
            {
                countTime--;
            }
            else if (countTime <= 0)
            {
                punchButton.interactable = true;
            }
        }

        Debug.Log(countTime);
    }
}
