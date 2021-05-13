using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public Button punchButton;
    int countTime;
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
        if (timer > 0.5f)
        {
            timer = 0f;
            if (countTime > 0)
            {
                countTime--;
            }
            else
            {
                punchButton.interactable = true;
            }
        }

        Debug.Log(countTime);
    }
}
