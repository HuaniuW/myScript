using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {
    public Button btn1;
    public Button btn2;
    public Button btn3;

    // Use this for initialization
    void Start () {
        //btn1.onClick.AddListener(delegate () { gameStart(btn1.gameObject); });
        btn1.onClick.AddListener(GameStart);
        btn2.onClick.AddListener(OnSet);
        btn3.onClick.AddListener(OutGame);

    }

    private void OnSet()
    {
        print("设置");
    }

    private void GameStart()
    {
        SceneManager.LoadScene("loadScreen");
        
    }

    private void OutGame()
    {
        print("退出游戏");
    }



    // Update is called once per frame
    void Update () {
		
	}
}
