﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

	//スタートボタンを押した
	public void PushStartButton () {
		SceneManager.LoadScene ("StageSelectScene");	//ステージ選択シーンへ
	}
}
