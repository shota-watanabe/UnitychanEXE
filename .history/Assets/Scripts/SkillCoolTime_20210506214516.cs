using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public Button punchButton;
    float timer;

    void Awake()
    {

    }
    public void OnClickButton()
    {
        punchButton.interactable = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // 毎秒処理
        if (timer > 1f)punchButton.interactable = true;
    }
}
