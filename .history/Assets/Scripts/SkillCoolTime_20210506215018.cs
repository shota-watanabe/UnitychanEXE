using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public Button punchButton;
    float countTime;
    float timer;

    void Awake()
    {

    }
    public void OnClickButton()
    {
        punchButton.interactable = false;
        countTime = 1;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // 毎秒処理
        if (timer > 1f)
        {
            timer = 0f;
            if (countTime > 0.5)
            {
                countTime -= Time.deltaTime;
            }
            else
            {
                punchButton.interactable = true;
            }
        }

        Debug.Log(countTime);
    }
}
