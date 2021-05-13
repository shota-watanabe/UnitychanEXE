using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    GameObject punchButton;
    Image imgButton;
    Button btnButton;
    // 何秒でボタンが再アクティブになるか
    const int COUNT = 1;
    int countTime;

    // ただのタイマー
    float timer;

    private void Awake() {
        imgButton = punchButton.GetComponent<Image>();
        btnButton = punchButton.GetComponent<Button>();
    }

    /// <summary>
    /// ボタンを押した時の処理
    /// </summary>
    public void OnClickButton() {
        countTime = COUNT;
    }

    private void Update() {
        timer += Time.deltaTime;
        // 毎秒処理
        if(timer > 1f) {
            timer = 0f;
            if(countTime > 0) {
                countTime--;
                imgButton.fillAmount = 1 - (float)countTime / (float)COUNT;
                btnButton.interactable = false;
            } else {
                btnButton.interactable = true;
            }
        }
    }
}
