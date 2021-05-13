using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int stageNo;
    public GameObject textGameOver;
    public GameObject textClear;
    public GameObject buttons;

    public AudioClip gameoverVoice;
    public AudioClip gameclearVoice;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(gameoverVoice);
        textGameOver.SetActive(true);
        buttons.SetActive(false);

        Invoke("GoBackStageSelect", 2.5f);
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
        Invoke("GoBackStageSelect", 2.5f);
    }

    void GoBackStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
