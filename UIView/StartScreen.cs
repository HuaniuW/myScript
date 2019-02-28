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
    public Button btn4;
    public Image kuang;
    public Image kuang2;

    // Use this for initialization
    void Start () {
        FindSaveDate();
        GetButton();

       
        //iTween.FadeTo(btn1.transform.gameObject, iTween.Hash("alpha", 0f, "time", 2f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fd"));
        //iTween.MoveFrom(this.gameObject, iTween.Hash("x", -10f, "time", 3f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fds"));
        //btn1.Component.CrossFadeAlpha
        //(btn1.GetComponentsInChildren<Component>()[0] as Graphic).CrossFadeAlpha(0, 1, true);

    }

    List<Button> btns = new List<Button>();

    void GetButton()
    {
        btn3.interactable = false;
        Button[] b = {btn1,btn2,btn3,btn4};
        btns.AddRange(b);
        
        btn1.onClick.AddListener(GameStart);
        btn2.onClick.AddListener(GetSaveDateUI);
        btn3.onClick.AddListener(OnSet);
        btn4.onClick.AddListener(OutGame);

        
    }

   
    //查看是否有存档来显示 继续游戏按钮
    void FindSaveDate()
    {
        if (GameSaveDate.GetInstance().IsHasSaveDate())
        {
            btn2.interactable = true;
            kuang.transform.position = btn2.transform.position;
            getRQ = btn2;
        }
        else
        {
            btn2.interactable = false;
            kuang.transform.position = btn1.transform.position;
            getRQ = btn1;
        }
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
        if (UI_Save) return;
        kuang.transform.position = btn3.transform.position;
        getRQ = btn3;
    }

    private void GameStart()
    {
        if (UI_Save) return;
        //SceneManager.LoadScene("loads");
        GlobalSetDate.instance.GetGameDateStart();
        kuang.transform.position = btn1.transform.position;
        getRQ = btn1;

    }

    private void OutGame()
    {
        print("退出游戏");
        if (UI_Save) return;
        kuang.transform.position = btn4.transform.position;
        getRQ = btn4;
    }



    // Update is called once per frame
    void Update () {
        if (UI_Save) return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindNearestQR("up");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindNearestQR("down");
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //print("enter");
            GetChoseObj();
        }
    }

    void GetChoseObj()
    {
        print(getRQ.name);
        if (getRQ.name == "Button2") {
            GetSaveDateUI();
        }
    }

    GameObject UI_Save;

    //存取档界面
    void GetSaveDateUI()
    {
        if (UI_Save) return;
        kuang.transform.position = btn2.transform.position;
        getRQ = btn2;
        if(!UI_Save) UI_Save = GlobalTools.GetGameObjectByName("UI_Save");
    }


    Button getRQ = null;
    void FindNearestQR(string fx)
    {
        if (btns.Count == 0) return;
        float jl = 0;
        bool hasValue = false;
        getRQ = null;
        List<Button> tempList = new List<Button>();

        if (fx == "up")
        {
            //获取在我上方的容器list
            foreach (var rq in btns)
            {
                if (rq.interactable == true && rq.transform.position.y > kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "down")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true && rq.transform.position.y < kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "right")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true&&rq.transform.position.x > kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "left")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true && rq.transform.position.x < kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }

        if (tempList.Count > 0)
        {
            foreach (var rq2 in tempList)
            {
                float jl2 = Vector2.Distance(rq2.transform.position, kuang.transform.position);
                if (hasValue)
                {
                    if (jl2 < jl)
                    {
                        jl = jl2;
                        getRQ = rq2;
                    }

                }
                else
                {
                    jl = jl2;
                    getRQ = rq2;
                    hasValue = true;
                }
            }
        }

        if (getRQ != null) kuang.transform.position = getRQ.transform.position;
    
    }
}
