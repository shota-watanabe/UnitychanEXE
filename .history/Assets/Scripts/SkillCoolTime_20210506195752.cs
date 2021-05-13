using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public GameObject punch;
    public Button punchButton;
    // 何秒でボタンが再アクティブになるか
    const int COUNT = 1;
    int countTime;
    float timer;

    private void Awake()
    {
        punchButton = punch.GetComponent<Button>();
    }

    public void OnClickButton()
    {
        countTime = COUNT;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // 毎秒処理
        if (timer > 1f)
        {
            timer = 0f;
            if (countTime > 0)
            {
                countTime--;
                punchButton.interactable = false;
            }
            else
            {
                punchButton.interactable = true;
            }
        }
    }
}
