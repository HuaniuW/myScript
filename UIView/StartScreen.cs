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
    public Button btn5;
    public Button btnX;
    //public Button btn_SYS;
    public Image kuang;
    public Image kuang2;

    public AudioSource xz;
    public AudioSource xd;

    // Use this for initialization
    void Start () {
        FindSaveDate();
        GetButton();

       
        //iTween.FadeTo(btn1.transform.gameObject, iTween.Hash("alpha", 0f, "time", 2f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fd"));
        //iTween.MoveFrom(this.gameObject, iTween.Hash("x", -10f, "time", 3f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fds"));
        //btn1.Component.CrossFadeAlpha
        //(btn1.GetComponentsInChildren<Component>()[0] as Graphic).CrossFadeAlpha(0, 1, true);

    }


    GameObject setUI;
    void GetSetUI()
    {
        if (GlobalSetDate.instance.IsChangeScreening) return;
        kuang.transform.position = btn3.transform.position;
        getRQ = btn3;
        KuangMoveStartSet();
        if (setUI == null)
        {
            setUI = GlobalTools.GetGameObjectByName("UI_set");
            GlobalSetDate.instance.IsChangeScreening = true;
        }
        else
        {
            GlobalSetDate.instance.IsChangeScreening = false;
            DestroyImmediate(setUI, true);
        }
    }

    List<Button> btns = new List<Button>();

    void GetButton()
    {
        //btn3.interactable = false;
        Button[] b = {btn1,btn2,btn3,btn4,btn5,btnX};
        btns.AddRange(b);

        //获取存档 如果有存档 显示继续游戏  新游戏放到最下
        //print("查找是否有存档"+GameSaveDate.GetInstance().IsHasSaveDate());
        if (GameSaveDate.GetInstance().IsHasSaveDate()) {
            Vector2 p = btn2.transform.position;
            btn2.transform.position = btn1.transform.position;
            //btn1.gameObject.SetActive(false);
            btn1.transform.position = p;
            kuang.transform.position = btn2.transform.position;
        }
        else
        {
            btn2.gameObject.SetActive(false);
        }

        //如果没有存档  显示新游戏  继续游戏按钮隐藏
        //如果有存档 点新游戏给提示

        btn1.onClick.AddListener(GameStart);
        btn2.onClick.AddListener(GetSaveDateUI);
        btn3.onClick.AddListener(GetSetUI);
        btn4.onClick.AddListener(OutGame);
        btn5.onClick.AddListener(InSYS);
        btnX.onClick.AddListener(ClearSave);
        GetLuanguage();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_LANGUAGE, GetLanguageChange);

    }

    void GetLanguageChange(UEvent e)
    {
        print(e.eventParams);
        GetLuanguage();
    }

    void ClearSave()
    {

    }

    void GetLuanguage()
    {
        if (Globals.language == Globals.CHINESE)
        {
            btn1.GetComponentInChildren<Text>().text = "新游戏";
            btn2.GetComponentInChildren<Text>().text = "继续游戏";
            btn3.GetComponentInChildren<Text>().text = "游戏设置";
            btn4.GetComponentInChildren<Text>().text = "退出游戏";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
        else if (Globals.language == Globals.ENGLISH)
        {
            btn1.GetComponentInChildren<Text>().text = "new Game";
            btn2.GetComponentInChildren<Text>().text = "play";
            btn3.GetComponentInChildren<Text>().text = "set";
            btn4.GetComponentInChildren<Text>().text = "out";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
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
        if (GlobalSetDate.instance.IsChangeScreening) return;
        print("设置");
        if (UI_Save) return;
        kuang.transform.position = btn3.transform.position;
        getRQ = btn3;
    }

    private void GameStart()
    {
        if (GlobalSetDate.instance.IsChangeScreening) return;
        if (UI_Save) return;
        //SceneManager.LoadScene("loads");
        //如果有存档在这给提示 会删除之前的所有存档
        kuang.transform.position = btn1.transform.position;
        getRQ = btn1;
        KuangMoveStartSet();
        GlobalSetDate.instance.GetGameDateStart();
        
       
    }

    private void InSYS()
    {
        if (GlobalSetDate.instance.IsChangeScreening) return;
        //print("进入游戏实验室");
        if (UI_Save) return;

        GlobalSetDate.instance.GetGameDateStart(true);

        kuang.transform.position = btn5.transform.position;
        getRQ = btn5;
        KuangMoveStartSet();
    }




    private void OutGame()
    {
        if (GlobalSetDate.instance.IsChangeScreening) return;
        //print("退出游戏");
        if (UI_Save) return;
        kuang.transform.position = btn4.transform.position;
        getRQ = btn4;
        KuangMoveStartSet();
    }

   



    // Update is called once per frame
    void Update () {
        if (UI_Save) return;
        if (GlobalSetDate.instance.IsChangeScreening) return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindNearestQR("up");
            GlobalTools.PlayAudio("xz", this);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindNearestQR("down");
            print("in------>");
            GlobalTools.PlayAudio("xz", this);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GetChoseObj();
        }

    }

    void KuangMoveStartSet()
    {
        GameObject.Find("/UI_Start").GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(150, 20, 164, 34,0.3f);
        GameObject.Find("/UI_Start").GetComponent<UITween>().GetUIImage(kuang).ImgAlphaStartSet(0, 0.2f);
    }



    void GetChoseObj()
    {
        print(getRQ.name+"  - "+getRQ.GetComponentInChildren<Text>().text);

        print(kuang.color+"   ---    "+kuang.rectTransform.rect.width);

        GlobalTools.PlayAudio("xd", this);
        if (getRQ.name == "Button1")
        {
           /* GameStart();*/
           //新游戏
        }
        else if (getRQ.name == "Button2")
        {
            //GetSaveDateUI();
            //继续游戏
        }else if (getRQ.name == "Button3")
        {
            //设置
            GetSetUI();
        }
    }


    void ImageAlphaTo(Image img,float toAlpha)
    {

    }

    GameObject UI_Save;

    //存取档界面
    void GetSaveDateUI()
    {
        if (GlobalSetDate.instance.IsChangeScreening) return;

        //if (UI_Save) return;
        kuang.transform.position = btn2.transform.position;
        getRQ = btn2;

        //GlobalSetDate.instance.CurrentUserDate = GameSaveDate.GetInstance().GetSaveDateByName(GlobalSetDate.instance.saveDateName);
        //UserDate t = GlobalSetDate.instance.CurrentUserDate;
        //GlobalSetDate.instance.isInFromSave = true;
        GlobalSetDate.instance.isInFromSave = true;
        //调用进入游戏
        GlobalSetDate.instance.GetGameDateStart();



        
        //GlobalSetDate.instance.GetGameDateStart();
        //if (!UI_Save) UI_Save = GlobalTools.GetGameObjectByName("UI_Save");
    }

    void OnDisable()
    {
        //print("移除");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_LANGUAGE, GetLanguageChange);

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
                if (rq.interactable == true && (int)rq.transform.position.y > (int)kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "down")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true && (int)rq.transform.position.y < (int)kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "right")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true&& (int)rq.transform.position.x > (int)kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "left")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true && (int)rq.transform.position.x < (int)kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }

        if (tempList.Count > 0)
        {
            //寻找临时列表最小距离
            foreach (var rq2 in tempList)
            {
                //print("tempList>    "+ tempList.Count+"   rq2 "+rq2.name);
                float jl2 = Vector2.Distance(rq2.transform.position, kuang.transform.position);
                if (hasValue)
                {
                    if ((int)jl2 < (int)jl)
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


        if (getRQ != null) {
            //这里面额外给个缓动动画
            kuang.transform.position = getRQ.transform.position;
            KuangMoveStartSet();
        }
        



        //置空临时列表
        tempList = null;

    }

    
}
