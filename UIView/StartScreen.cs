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
        //DontDestroyOnLoad(this);
        //this.transform.gameObject.SetActive(false);
        //btn1.transform.gameObject.SetActive(false);
        //test1();

        btn1.onClick.AddListener(GameStart);
        btn2.onClick.AddListener(OnSet);
        btn3.onClick.AddListener(OutGame);

       

        GlobalSetDate.instance.DoSomeThings();
        GlobalSetDate.instance.screenName = "test_1";
        GlobalSetDate.instance.playerPosition = "14_-2";
        //iTween.FadeTo(btn1.transform.gameObject, iTween.Hash("alpha", 0f, "time", 2f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fd"));
        //iTween.MoveFrom(this.gameObject, iTween.Hash("x", -10f, "time", 3f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fds"));
        //btn1.Component.CrossFadeAlpha
        //(btn1.GetComponentsInChildren<Component>()[0] as Graphic).CrossFadeAlpha(0, 1, true);

    }

    void test1() {
        Component[] comps = GetComponentsInChildren<Component>();
        print(comps.Length);
        foreach (Component c in comps)
        {
            if (c is Graphic)
            {
                (c as Graphic).CrossFadeAlpha(0, 3, true);
            }
        }
        
    }

    public void Fds()
    {
        print("diaoyong!");
    }

    private void OnSet()
    {
        print("设置");
    }

    private void GameStart()
    {
        
        SceneManager.LoadScene("loads");
        
    }

    private void OutGame()
    {
        print("退出游戏");
    }



    // Update is called once per frame
    void Update () {
       
	}
}
