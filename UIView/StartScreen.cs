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


    public Image TxtD;

    // Use this for initialization
    void Start () {
        print("-----------> " + FileControl.GetInstance().GetValueByKey("yuyan"));
        string str = FileControl.GetInstance().GetValueByKey("yuyan");
        string isDebug = FileControl.GetInstance().GetValueByKey("debug");

        string isQuanping = FileControl.GetInstance().GetValueByKey("quanping");

        string isDebug2 = FileControl.GetInstance().GetValueByKey("debug2");
        Globals.isDebug2 = isDebug2 == "201710" ? true : false;

        Globals.isDebug = isDebug == "1999201710" ? true : false;
        print("  是否是 debug "+isDebug +"   ??? "+Globals.isDebug);
        

        if (str!="")
        {
            Globals.language = str;
            print("Globals.language    "+ Globals.language);
            GetLuanguage();
        }

        
        FindSaveDate();
        GetButton();

        if (isQuanping == "q")
        {
            Resolution[] resolutions = Screen.resolutions;
            //设置当前分辨率 
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
            //设置成全屏
            Screen.fullScreen = true;
        }

      

        //iTween.FadeTo(btn1.transform.gameObject, iTween.Hash("alpha", 0f, "time", 2f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fd"));
        //iTween.MoveFrom(this.gameObject, iTween.Hash("x", -10f, "time", 3f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fds"));
        //btn1.Component.CrossFadeAlpha
        //(btn1.GetComponentsInChildren<Component>()[0] as Graphic).CrossFadeAlpha(0, 1, true);
        //FileControl.GetInstance().CreateTxt();
        //FileControl.GetInstance().ReWriteTxt();
        //FileControl.GetInstance().AddTxtTextByFileInfo();
        //FileControl.GetInstance().ReadTxtSecond();


    }

    float speedX = 0.04f;
    float speedY = 0.034f;
    bool isStart = false;
    float distance = 16;
    Vector2 vp = Vector2.zero;
    //文本飘动
    void DPiaoDpnr()
    {
        if (!isStart)
        {
            isStart = true;
            vp = TxtD.transform.position;
        }

        float _x = TxtD.transform.position.x;
        float _y = TxtD.transform.position.y;
        _x += speedX;
        _y += speedY;
        TxtD.transform.position = new Vector2(_x, _y);
        if (TxtD.transform.position.x > vp.x + distance || TxtD.transform.position.x < vp.x - distance) speedX *= -1;
        if (TxtD.transform.position.y > vp.y + distance || TxtD.transform.position.y < vp.y - distance) speedY *= -1;



        
    }


    GameObject setUI;
    void GetSetUI()
    {
        if (GlobalSetDate.instance.IsChangeScreening|| IsChoseInGame) return;
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
        GlobalTools.PlayAudio("xd", this);
    }

    List<Button> btns = new List<Button>();

    void GetButton()
    {
        //btn3.interactable = false;
        //Button[] b = { btn1, btn2, btn3, btn4, btn5, btnX };
        Button[] b = {btn1,btn2,btn3,btn4};
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
            //这里要显示 第一次进游戏 设置面板
            GetSetUI();
        }

        //如果没有存档  显示新游戏  继续游戏按钮隐藏
        //如果有存档 点新游戏给提示

        btn1.onClick.AddListener(GameStart);
        btn2.onClick.AddListener(ContinueGame);
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
        else if (Globals.language == Globals.JAPAN)
        {
            btn1.GetComponentInChildren<Text>().text = "新しいゲーム";
            btn2.GetComponentInChildren<Text>().text = "ゲームを続ける";
            btn3.GetComponentInChildren<Text>().text = "設定";
            btn4.GetComponentInChildren<Text>().text = "ゲームを終了する";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
        else if (Globals.language == Globals.KOREAN)
        {
            btn1.GetComponentInChildren<Text>().text = "새로운 게임";
            btn2.GetComponentInChildren<Text>().text = "게임 계속하기";
            btn3.GetComponentInChildren<Text>().text = "설정";
            btn4.GetComponentInChildren<Text>().text = "게임을 종료";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
        else if (Globals.language == Globals.Portugal)
        {
            btn1.GetComponentInChildren<Text>().text = "novo jogo";
            btn2.GetComponentInChildren<Text>().text = "Continuar o jogo";
            btn3.GetComponentInChildren<Text>().text = "configuração";
            btn4.GetComponentInChildren<Text>().text = "sair do jogo";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
        else if (Globals.language == Globals.CHINESEF)
        {
            btn1.GetComponentInChildren<Text>().text = "新遊戲";
            btn2.GetComponentInChildren<Text>().text = "繼續遊戲";
            btn3.GetComponentInChildren<Text>().text = "遊戲設置";
            btn4.GetComponentInChildren<Text>().text = "退出遊戲";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
        if (Globals.language == Globals.German)
        {
            btn1.GetComponentInChildren<Text>().text = "neues Spiel";
            btn2.GetComponentInChildren<Text>().text = "Setzen Sie das Spiel fort";
            btn3.GetComponentInChildren<Text>().text = "Spieleinstellungen";
            btn4.GetComponentInChildren<Text>().text = "Beenden Sie das Spiel";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
        if (Globals.language == Globals.French)
        {
            btn1.GetComponentInChildren<Text>().text = "nouveau jeu";
            btn2.GetComponentInChildren<Text>().text = "Continuer le jeu";
            btn3.GetComponentInChildren<Text>().text = "Paramètres de jeu";
            btn4.GetComponentInChildren<Text>().text = "quitter le jeu";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
        if (Globals.language == Globals.Italy)
        {
            btn1.GetComponentInChildren<Text>().text = "nuovo gioco";
            btn2.GetComponentInChildren<Text>().text = "Continua il gioco";
            btn3.GetComponentInChildren<Text>().text = "Impostazioni di gioco";
            btn4.GetComponentInChildren<Text>().text = "uscire dal gioco";
            btn5.GetComponentInChildren<Text>().text = "战斗实验室";
        }
    }


    //查看是否有存档来显示 继续游戏按钮
    void FindSaveDate()
    {
        if (GameSaveDate.GetInstance().IsHasSaveDate())
        {
            print("有存档！！！！！！");
            btn2.interactable = true;
            kuang.transform.position = btn2.transform.position;
            getRQ = btn2;
        }
        else
        {
            btn2.interactable = false;
            kuang.transform.position = btn1.transform.position;
            getRQ = btn1;
            //btn2.gameObject.SetActive(false);
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
        if (GlobalSetDate.instance.IsChangeScreening|| IsChoseInGame) return;
        if (UI_Save) return;
        //SceneManager.LoadScene("loads");
        //如果有存档在这给提示 会删除之前的所有存档
        kuang.transform.position = btn1.transform.position;
        getRQ = btn1;
        KuangMoveStartSet();
        IsChoseInGame = true;
        GlobalTools.PlayAudio("xd", this);
        StartCoroutine(IStartByTime());
    }


    private void GameStartNoAduio()
    {
        if (GlobalSetDate.instance.IsChangeScreening) return;
        if (UI_Save) return;
        //SceneManager.LoadScene("loads");
        //如果有存档在这给提示 会删除之前的所有存档
        //kuang.transform.position = btn1.transform.position;
        //getRQ = btn1;
        //KuangMoveStartSet();
        //StartCoroutine(IStartByTime());
        GlobalSetDate.instance.GetGameDateStart();
    }


    public IEnumerator IStartByTime()
    {
        yield return new WaitForSeconds(1);
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
        if (GlobalSetDate.instance.IsChangeScreening|| IsChoseInGame) return;
        //print("退出游戏");
        if (UI_Save) return;
        kuang.transform.position = btn4.transform.position;
        getRQ = btn4;
        KuangMoveStartSet();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            //Debug.Log("key:" + e.keyCode);
        }
    }

    const string VERTICAL = "Vertical";
    float verticalDirection;
    bool IsFX = false;

    // Update is called once per frame
    void Update () {

        //DPiaoDpnr();

        if (UI_Save|| IsInGame) return;
        if (GlobalSetDate.instance.IsChangeScreening||IsChoseInGame) return;


        verticalDirection = Input.GetAxis(VERTICAL);

        if (verticalDirection>=-0.6f&& verticalDirection <= 0.6f)
        {
            IsFX = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W)|| (!IsFX && verticalDirection>0.6f))
        {
            IsFX = true;
            FindNearestQR("up");
            GlobalTools.PlayAudio("xz", this);
        }else  if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!IsFX && verticalDirection < -0.6f))
        {
            IsFX = true;
            FindNearestQR("down");
            //print("in------>");
            GlobalTools.PlayAudio("xz", this);
        }else if (Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            GetChoseObj();
        }

    }

    void KuangMoveStartSet()
    {
        GameObject.Find("/UI_Start").GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(150, 20, 164, 34,0.3f);
        GameObject.Find("/UI_Start").GetComponent<UITween>().GetUIImage(kuang).ImgAlphaStartSet(0, 0.2f);
    }

    bool IsInGame = false;

    void GetChoseObj()
    {
        if (IsChoseInGame) return;


        //print(getRQ.name+"  - "+getRQ.GetComponentInChildren<Text>().text);

        //print(kuang.color+"   ---    "+kuang.rectTransform.rect.width);

        GlobalTools.PlayAudio("xd", this);
        if (getRQ.name == "Button1")
        {
            //GameStart();
            //新游戏
            StartCoroutine(IStart2ByTime());
            IsInGame = true;
        }
        else if (getRQ.name == "Button2")
        {
            //GetSaveDateUI();
            //GetSaveDateUI();
            GlobalTools.PlayAudio("xd", this);
            StartCoroutine(IStart2ByTime());
            IsInGame = true;
            //继续游戏
        }
        else if (getRQ.name == "Button3")
        {
            //GlobalTools.PlayAudio("xd", this);
            //设置
            GetSetUI();

            
        }else if (getRQ.name == "Button4")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public IEnumerator IStart2ByTime()
    {
        yield return new WaitForSeconds(1);
        if (getRQ.name == "Button1")
        {
            GameStartNoAduio();
            //新游戏
        }
        else if (getRQ.name == "Button2")
        {
            //继续游戏
            ContinueGameNoAudio();
        }
    }


    void ImageAlphaTo(Image img,float toAlpha)
    {

    }

    GameObject UI_Save;

    //存取档界面
    void ContinueGame()
    {
        if (GlobalSetDate.instance.IsChangeScreening|| IsChoseInGame) return;

        //if (UI_Save) return;
        kuang.transform.position = btn2.transform.position;
        getRQ = btn2;
        GlobalTools.PlayAudio("xd", this);
        //GlobalSetDate.instance.CurrentUserDate = GameSaveDate.GetInstance().GetSaveDateByName(GlobalSetDate.instance.saveDateName);
        //UserDate t = GlobalSetDate.instance.CurrentUserDate;
        //GlobalSetDate.instance.isInFromSave = true;
        GlobalSetDate.instance.isInFromSave = true;
        IsChoseInGame = true;
        //调用进入游戏
        //GlobalSetDate.instance.GetGameDateStart();
        //GlobalSetDate.instance.GetGameDateStart();
        //if (!UI_Save) UI_Save = GlobalTools.GetGameObjectByName("UI_Save");


        StartCoroutine(IStartContineByTime());

    }

    bool IsChoseInGame = false;

    public IEnumerator IStartContineByTime()
    {
        yield return new WaitForSeconds(1);
        GlobalSetDate.instance.GetGameDateStart();
    }




    void ContinueGameNoAudio()
    {
        if (GlobalSetDate.instance.IsChangeScreening) return;
        GlobalSetDate.instance.isInFromSave = true;
        GlobalSetDate.instance.GetGameDateStart();
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
                print(rq.name+"   rq.y    "+rq.transform.position.y+"  -------  "+kuang.transform.position.y+ "  interactable   "+rq.interactable);
                if (rq.interactable == true && (int)rq.transform.position.y > (int)kuang.transform.position.y+1)
                {
                    print("在我上面    "+rq.name);
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "down")
        {
            foreach (var rq in btns)
            {
                if (rq.interactable == true && (int)rq.transform.position.y < (int)kuang.transform.position.y-1)
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


        if (getRQ != null&& getRQ.gameObject.activeSelf) {
            print("????????????????:   "+getRQ.name);
            //这里面额外给个缓动动画
            kuang.transform.position = getRQ.transform.position;
            KuangMoveStartSet();
        }
        



        //置空临时列表
        tempList = null;

    }

    
}
