using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int stageNo;					// ステージナンバー
    public GameObject textGameOver;		// 「ゲームオーバー」テキスト
    public GameObject textClear;		// 「クリア」テキスト
    public GameObject buttons;			// 操作ボタン

    public AudioClip gameoverVoice;
    public AudioClip gameclearVoice;
    private AudioSource audioSource;	// オーディオソース

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(gameoverVoice);
        textGameOver.SetActive(true);
        buttons.SetActive(false);

        Invoke("GoBackStageSelect", 2.0f);
    }

    public void GameClear()
    {
        audioSource.PlayOneShot(gameclearVoice);
        textClear.SetActive(true);
        buttons.SetActive(false);

        //セーブデータ更新
        if(PlayerPrefs.GetInt("CLEAR", 0) < stageNo)
        {
            PlayerPrefs.SetInt("CLEAR", stageNo);
        }
        Invoke("GoBackStageSelect", 2.0f);
    }

    void GoBackStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
