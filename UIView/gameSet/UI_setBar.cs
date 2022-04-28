using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_setBar : MonoBehaviour
{
    [Header("*********语言 选项 按钮")]
    public Button textYY1;
    public Button textYY2;
    public Button textYY3;
    public Button textYY4;
    public Button textYY5;

    //繁体 中文
    public Button textYY6;
    //德
    public Button textYY7;
    //法
    public Button textYY8;
    //意大利
    public Button textYY9;


    [Header("*********手柄")]
    public Button textCZ1;
    [Header("*********键盘")]
    public Button textCZ2;


    [Header("*********窗口模式")]
    public Button Chuangkou;
    [Header("*********全屏")]
    public Button Quanpin;


    //设置标题1
    public Text text1;
    //设置标题2
    public Text text2;

    [Header("****操作 标题")]
    public Text TextCZ;
    //声音条
    public Slider sl1;

    public Image kuang;

    public Button btn_close;

    public Button btn_inStartScreen;

    public Button btn_outGame;

    [Header("****手柄操作说明")]
    public Image Img_shoubing;
    [Header("****键盘操作说明")]
    public Image Img_jianpan;



    List<GameObject> labels = new List<GameObject>();
    // Use this for initialization
    float kuangX;

    void Start()
    {
        AddEvenr();
        //this.sl1.value = GlobalSetDate.instance.GetSoundEffectValue();
        kuang.transform.position = new Vector2(kuang.transform.position.x, textYY1.transform.position.y);


        xzUI = "yingliang";
        kuangX = kuang.transform.position.x;
        GlobalSetDate.instance.IsChangeScreening = true;
        GameObject[] b = { textYY1.gameObject, textYY2.gameObject, textYY3.gameObject, textYY4.gameObject, textYY5.gameObject,textYY6.gameObject,textYY7.gameObject,textYY8.gameObject,textYY9.gameObject,
            textCZ1.gameObject, textCZ2.gameObject,Chuangkou.gameObject,Quanpin.gameObject, btn_close.gameObject, btn_inStartScreen.gameObject, btn_outGame.gameObject };
        labels.AddRange(b);

        GetLuanguage();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_LANGUAGE, GetLanguageChange);
        print("start");
    }

    void Awake()
    {
        Globals.IsInSetBar = true;
        print("Awake");
        if (SceneManager.GetActiveScene().name == "startScreen")
        {
            btn_inStartScreen.gameObject.SetActive(false);
            btn_outGame.gameObject.SetActive(false);
        }
        else
        {
            btn_inStartScreen.gameObject.SetActive(true);
            btn_outGame.gameObject.SetActive(false);
        }

    }

    void GetLuanguage()
    {
        if (Globals.language == Globals.CHINESE)
        {
            text1.text = "音量";
            text2.text = "语言";
            TextCZ.text = "操作说明";
            textCZ1.GetComponentInChildren<Text>().text = "手柄";
            textCZ2.GetComponentInChildren<Text>().text = "键盘";

            Chuangkou.GetComponentInChildren<Text>().text = "窗口模式";
            Quanpin.GetComponentInChildren<Text>().text = "全屏";

            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "开始界面";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "退出游戏";
            }
        }
        else if (Globals.language == Globals.ENGLISH)
        {
            text1.text = "volume";
            text2.text = "language";
            TextCZ.text = "Instructions";
            textCZ1.GetComponentInChildren<Text>().text = "handle";
            textCZ2.GetComponentInChildren<Text>().text = "keyboard";

            Chuangkou.GetComponentInChildren<Text>().text = "windowed";
            Quanpin.GetComponentInChildren<Text>().text = "full screen";

            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "startScreen";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "outGame";
            }
        }
        else if (Globals.language == Globals.JAPAN)
        {
            text1.text = "音量";
            text2.text = "言語";
            TextCZ.text = "手順";
            textCZ1.GetComponentInChildren<Text>().text = "取り持つ";
            textCZ2.GetComponentInChildren<Text>().text = "キーボード";

            Chuangkou.GetComponentInChildren<Text>().text = "窓";
            Quanpin.GetComponentInChildren<Text>().text = "全画面表示";
            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "開始インターフェース";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "ゲームを終了する";
            }
        }
        else if (Globals.language == Globals.Portugal)
        {
            text1.text = "volume";
            text2.text = "Língua";
            TextCZ.text = "Instrucciones";
            textCZ1.GetComponentInChildren<Text>().text = "encargarse de";
            textCZ2.GetComponentInChildren<Text>().text = "teclado";

            Chuangkou.GetComponentInChildren<Text>().text = "ventana";
            Quanpin.GetComponentInChildren<Text>().text = "pantalla completa";
            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "iniciar interface";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "salir del juego";
            }
        }
        else if (Globals.language == Globals.KOREAN)
        {
            text1.text = "용량";
            text2.text = "언어";
            TextCZ.text = "지침";
            textCZ1.GetComponentInChildren<Text>().text = "핸들";
            textCZ2.GetComponentInChildren<Text>().text = "건반";

            Chuangkou.GetComponentInChildren<Text>().text = "창문";
            Quanpin.GetComponentInChildren<Text>().text = "전체 화면";
            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "시작 인터페이스";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "게임을 종료";
            }
        }
        else if (Globals.language == Globals.CHINESEF)
        {
            text1.text = "音量";
            text2.text = "語言";
            TextCZ.text = "操作說明";
            textCZ1.GetComponentInChildren<Text>().text = "手柄";
            textCZ2.GetComponentInChildren<Text>().text = "鍵盤";

            Chuangkou.GetComponentInChildren<Text>().text = "窗口模式";
            Quanpin.GetComponentInChildren<Text>().text = "全屏";
            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "開始界面";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "退出遊戲";
            }
        }
        else if (Globals.language == Globals.German)
        {
            text1.text = "Volumen";
            text2.text = "Sprache";
            TextCZ.text = "Anweisungen";
            textCZ1.GetComponentInChildren<Text>().text = "handhaben";
            textCZ2.GetComponentInChildren<Text>().text = "Klaviatur";

            Chuangkou.GetComponentInChildren<Text>().text = "Fenstermodus";
            Quanpin.GetComponentInChildren<Text>().text = "ganzer Bildschirm";
            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "Schnittstelle starten";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "Beenden Sie das Spiel";
            }
        }
        else if (Globals.language == Globals.French)
        {
            text1.text = "le volume";
            text2.text = "Langue";
            TextCZ.text = "Des instructions";
            textCZ1.GetComponentInChildren<Text>().text = "manipuler";
            textCZ2.GetComponentInChildren<Text>().text = "clavier";

            Chuangkou.GetComponentInChildren<Text>().text = "mode fenêtré";
            Quanpin.GetComponentInChildren<Text>().text = "plein écran";
            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "interface de démarrage";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "quitter le jeu";
            }
        }
        else if (Globals.language == Globals.Italy)
        {
            text1.text = "volume";
            text2.text = "linguaggio";
            TextCZ.text = "Istruzioni";
            textCZ1.GetComponentInChildren<Text>().text = "maniglia";
            textCZ2.GetComponentInChildren<Text>().text = "tastiera";

            Chuangkou.GetComponentInChildren<Text>().text = "modalità finestra";
            Quanpin.GetComponentInChildren<Text>().text = "a schermo intero";
            if (btn_inStartScreen.isActiveAndEnabled)
            {
                btn_inStartScreen.GetComponentInChildren<Text>().text = "interfaccia di avvio";
            }

            if (btn_outGame.isActiveAndEnabled)
            {
                btn_outGame.GetComponentInChildren<Text>().text = "uscire dal gioco";
            }
        }
    }


    void GetLanguageChange(UEvent e)
    {
        print("   触发了吗？？？ " + e.eventParams);
        GetLuanguage();
    }

    void AddEvenr()
    {
        sl1.onValueChanged.AddListener(SLChange);

        textYY1.onClick.AddListener(Yuyan1);
        textYY2.onClick.AddListener(Yuyan2);
        textYY3.onClick.AddListener(Yuyan3);
        textYY4.onClick.AddListener(Yuyan4);
        textYY5.onClick.AddListener(Yuyan5);
        textYY6.onClick.AddListener(Yuyan6);
        textYY7.onClick.AddListener(Yuyan7);
        textYY8.onClick.AddListener(Yuyan8);
        textYY9.onClick.AddListener(Yuyan9);

        textCZ1.onClick.AddListener(Shoubing);
        textCZ2.onClick.AddListener(Jianpan);

        btn_close.onClick.AddListener(RemoveSelf);
        btn_inStartScreen.onClick.AddListener(ToStartScreen);


        Chuangkou.onClick.AddListener(TheChuangKou);
        Quanpin.onClick.AddListener(TheQuanpin);

        //Chuangkou.GetComponentInChildren<Text>().text = "창문";
        //Quanpin.GetComponentInChildren<Text>().text = "전체 화면";

    }


    void TheChuangKou()
    {
        //Screen.SetResolution(960, 540, false);
        Screen.SetResolution(1340, 810, false);
        FileControl.GetInstance().WriteInByKey("quanping","c");
        getRQ = Chuangkou.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 24, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;

    }

    void TheQuanpin()
    {
        FileControl.GetInstance().WriteInByKey("quanping", "q");
        Resolution[] resolutions = Screen.resolutions;
        //设置当前分辨率 
        Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
        //设置成全屏
        Screen.fullScreen = true;


        getRQ = Quanpin.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 24, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
    }


    string SHOUBING = "shoubing";
    void Shoubing()
    {
        print(" 手柄 ");
        Img_shoubing.GetComponent<UI_shoubinSM>().GetStart();

        getRQ = textCZ1.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 24, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming("s");
    }

    string JIANPAN = "jianban";
    void Jianpan()
    {
        print(" 键盘操作 ");
        //PlayVadioByName("xd");
        Img_jianpan.GetComponent<UI_JianpanSM>().GetStart();

        getRQ = textCZ2.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 24, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming("k");
    }




    void Yuyan1()
    {
        GetZhongWen();
    }

    void Yuyan2()
    {
        GetYingWen();
    }

    void Yuyan3()
    {
        GetJapan();
    }

    void Yuyan4()
    {
        GetPutaoya();
    }

    void Yuyan5()
    {
        GetHanyu();
    }

    void Yuyan6()
    {
        GetChineseF();
    }
    void Yuyan7()
    {
        GetDe();
    }
    void Yuyan8()
    {
        GetFa();
    }
    void Yuyan9()
    {
        GetYi();
    }

    void ToStartScreen()
    {
        print("回到开始界面！！！");
        SceneManager.LoadScene("startScreen");
        RemoveSelf();
    }








    const string VERTICAL = "Vertical";
    float verticalDirection;
    bool IsFX = false;

    // Update is called once per frame
    void Update()
    {

        verticalDirection = Input.GetAxis(VERTICAL);

        if (verticalDirection >= -0.6f && verticalDirection <= 0.6f)
        {
            IsFX = false;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!IsFX && verticalDirection > 0.6f))
        {
            IsFX = true;
            FindNearestQR("up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!IsFX && verticalDirection < -0.6f))
        {
            IsFX = true;
            FindNearestQR("down");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FindNearestQR("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FindNearestQR("right");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            //print(" ****************** enter!!!!!!  ");
            //在游戏内 return键 冲突 无法使用
            GetChoseObj();
        }
        else
        {
            GetChoseBtn2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("   111111 ");
        }
    }

    //PS4手柄
    void GetChoseBtn1()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            RemoveSelf();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            //print(" ****************** enter!!!!!!  ");
            GetChoseObj();
        }
    }

    //360手柄
    void GetChoseBtn2()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            RemoveSelf();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            //print(" ****************** enter!!!!!!  ");
            GetChoseObj();
        }
    }



    void GetChoseObj()
    {
        print(" xzUI*************************************************   " + xzUI);
        
        if (xzUI == Globals.JAPAN)
        {
            GetJapan();
        }
        else if (xzUI == Globals.ENGLISH)
        {
            GetYingWen();
        }
        else if (xzUI == Globals.CHINESE)
        {
            GetZhongWen();
        }
        else if (xzUI == Globals.Portugal)
        {
            GetPutaoya();
        }
        else if (xzUI == Globals.KOREAN)
        {
            GetHanyu();
        }
        else if (xzUI == Globals.CHINESEF)
        {
            GetChineseF();
        }
        else if (xzUI == Globals.German)
        {
            GetDe();
        }
        else if (xzUI == Globals.French)
        {
            GetFa();
        }
        else if (xzUI == Globals.Italy)
        {
            GetYi();
        }
        else if (xzUI == SHOUBING)
        {
            Shoubing();
        }
        else if (xzUI == JIANPAN)
        {
            Jianpan();
        }
        else if (xzUI == "close")
        {
            RemoveSelf();
        }
        else if (xzUI == "startScreen")
        {
            ToStartScreen();
        }
        else if (xzUI == "outGame")
        {

        }
        else if (xzUI == "Chuangkou")
        {
            TheChuangKou();
            
        }
        else if (xzUI == "Quanpin")
        {
            TheQuanpin();
        }

    }




    void GetZhongWen()
    {
        PlayVadioByName("xd");
        Globals.language = "chinese";
        Globals.languageType = "";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.CHINESE);
        //print("Globals.language    "+ Globals.language);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY1.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();
    }

    void GetYingWen()
    {
        PlayVadioByName("xd");
        Globals.language = "english";
        Globals.languageType = "e";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.ENGLISH);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY2.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();
    }

    void GetJapan()
    {
        PlayVadioByName("xd");
        Globals.language = Globals.JAPAN;
        Globals.languageType = "r";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.JAPAN);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY3.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();

    }

    void GetHanyu()
    {
        PlayVadioByName("xd");
        Globals.language = Globals.KOREAN;
        Globals.languageType = "h";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.KOREAN);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY5.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();
    }

    void GetPutaoya()
    {
        PlayVadioByName("xd");
        Globals.language = Globals.Portugal;
        Globals.languageType = "p";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.Portugal);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY4.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();
    }

    void GetChineseF()
    {
        PlayVadioByName("xd");
        Globals.language = Globals.CHINESEF;
        Globals.languageType = "zf";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.CHINESEF);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY6.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();
    }

    void GetDe()
    {
        PlayVadioByName("xd");
        Globals.language = Globals.German;
        Globals.languageType = "d";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.German);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY7.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();
    }

    void GetFa()
    {
        PlayVadioByName("xd");
        Globals.language = Globals.French;
        Globals.languageType = "f";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.French);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY8.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();
    }

    void GetYi()
    {
        PlayVadioByName("xd");
        Globals.language = Globals.Italy;
        Globals.languageType = "y";
        FileControl.GetInstance().WriteInByKey("yuyan", Globals.Italy);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);

        getRQ = textYY9.gameObject;
        GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
        kuang.transform.position = getRQ.transform.position;
        HideShuoming();
    }




    void RemoveSelf()
    {
        Globals.IsInSetBar = false;
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_LANGUAGE, GetLanguageChange);
        GlobalSetDate.instance.IsChangeScreening = false;
        DestroyImmediate(this.gameObject, true);
    }

    void SLChange(float value)
    {
        GlobalSetDate.instance.SoundEffect = value;
        PlayVadioByName("xz");
    }



    GameObject getRQ = null;
    void FindNearestQR(string fx)
    {
        if (labels.Count == 0) return;
        float jl = 0;
        bool hasValue = false;
        getRQ = null;
        List<GameObject> tempList = new List<GameObject>();

        if (fx == "up")
        {
            //获取在我上方的容器list
            foreach (var rq in labels)
            {
                if (rq.transform.position.y > kuang.transform.position.y + 1)
                {
                    if (rq.activeSelf) tempList.Add(rq);
                }
            }
        }
        else if (fx == "down")
        {
            foreach (var rq in labels)
            {
                //print("  rq name "+rq.name+"   ");
                if (rq.transform.position.y < kuang.transform.position.y - 1)
                {
                    if (rq.activeSelf)
                    {
                        tempList.Add(rq);
                        //print("  rq -------------name " + rq.name + "   ");
                    }

                }
            }
        }
        else if (fx == "right")
        {
            foreach (var rq in labels)
            {
                if (rq.transform.position.y == kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "left")
        {
            foreach (var rq in labels)
            {
                if (rq.transform.position.y == kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }


        //print("rq  tempList.Count " + tempList.Count);
        if (tempList.Count > 0)
        {
            //寻找临时列表最小距离
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


        if (getRQ != null)
        {
            KuangMoveStartSet(getRQ.name, fx);
            PlayVadioByName("xz");
        }
        //置空临时列表
        tempList = null;

    }

    [Header("***选择 音效")]
    public AudioSource Audio_xuanze;
    [Header("***选定 音效")]
    public AudioSource Audio_xuanding;

    //播放选择声音
    void PlayVadioByName(string _name)
    {
        //GameObject UI_start = GameObject.Find("/UI_Start");
        //if(UI_start!=null) GlobalTools.PlayAudio(_name, UI_start.GetComponent<StartScreen>());
        if (_name == "xz")
        {
            Audio_xuanze.Play();
        }
        else if (_name == "xd")
        {
            Audio_xuanding.Play();
        }
    }



    void HideShuoming(string type = "")
    {
        Img_shoubing.gameObject.SetActive(false);
        Img_jianpan.gameObject.SetActive(false);
        if (type == "s")
        {
            Img_shoubing.gameObject.SetActive(true);
        }
        else if (type == "k")
        {
            Img_jianpan.gameObject.SetActive(true);
           
        }

    }


    string xzUI = "";

    void KuangMoveStartSet(string _txtName, string zy = "")
    {
        print("sha a "+zy+"  txtName  "+_txtName);
        if (zy == "down" || zy == "up") zy = "";

        if (_txtName == "ButtonYY1")
        {
            //简体中文
            xzUI = Globals.CHINESE;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();

        }
        else if (_txtName == "ButtonYY2")
        {
            //英文
            xzUI = Globals.ENGLISH;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();
        }
        else if (_txtName == "ButtonYY3")
        {
            //日语
            xzUI = Globals.JAPAN;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();
        }
        else if (_txtName == "ButtonYY4")
        {
            //葡萄牙
            xzUI = Globals.Portugal;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();
        }
        else if (_txtName == "ButtonYY5")
        {
            //韩国
            xzUI = Globals.KOREAN;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();
        }
        else if (_txtName == "ButtonYY6")
        {
            //繁体
            xzUI = Globals.CHINESEF;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();
        }
        else if (_txtName == "ButtonYY7")
        {
            //德国
            xzUI = Globals.German;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();
        }
        else if (_txtName == "ButtonYY8")
        {
            //法国
            xzUI = Globals.French;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();
        }
        else if (_txtName == "ButtonYY9")
        {
            //意大利
            xzUI = Globals.Italy;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 18, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming();
        }

        else if (_txtName == "ButtonCZ1")
        {
            //手柄操作
            xzUI = SHOUBING;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 24, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming("s");
            Shoubing();
        }
        else if (_txtName == "ButtonCZ2")
        {
            //键盘操作
            xzUI = JIANPAN;
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 24, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            HideShuoming("k");
            Jianpan();
        }
        else if (_txtName == "BtnQuanpin")
        {
            //全屏
            xzUI = "Quanpin";
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 24, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            //HideShuoming("s");
        }
        else if (_txtName == "BtnChuangkou")
        {
            //窗口
            xzUI = "Chuangkou";
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(186, 24, 190, 29, 0.3f);
            kuang.transform.position = getRQ.transform.position;
            //HideShuoming("s");
        }
        else if (_txtName == "Text2")
        {
            //选择的是文字 中文
            xzUI = "zhongwen";

            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(166, 20, 152, 34, 0.3f);
            if (zy == "")
            {
                kuang.transform.position = new Vector2(kuangX - 100, getRQ.transform.position.y);
            }
            else
            {
                if (zy == "left")
                {
                    //选择的是文字 中文
                    xzUI = "zhongwen";
                    kuang.transform.position = new Vector2(kuangX - 100, getRQ.transform.position.y);
                }
                else if (zy == "right")
                {
                    //选择的是文字 英文
                    xzUI = "yingwen";
                    kuang.transform.position = new Vector2(kuangX + 100, getRQ.transform.position.y);
                }
            }
        }
        else if (_txtName == "Text1")
        {
            //选中的事音量
            xzUI = "yingliang";
            if (zy == "")
            {

            }
            else
            {
                if (zy == "left")
                {
                    GlobalSetDate.instance.GetDownSoundEffectValue();
                    this.sl1.value = GlobalSetDate.instance.GetSoundEffectValue();
                }
                else if (zy == "right")
                {
                    GlobalSetDate.instance.GetUpSoundEffectValue();
                    this.sl1.value = GlobalSetDate.instance.GetSoundEffectValue();
                }
            }
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(336, 20, 350, 34, 0.3f);
            kuang.transform.position = new Vector2(kuangX, getRQ.transform.position.y);
        }
        else if (_txtName == "btn_close")
        {
            xzUI = "close";
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(30, 30, 40, 40, 0.3f);
            kuang.transform.position = getRQ.transform.position;

        }
        else if (_txtName == "btn_startScreen")
        {
            xzUI = "startScreen";
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(180, 26, 190, 36, 0.3f);
            kuang.transform.position = getRQ.transform.position;
        }
        else if (_txtName == "btn_outGame")
        {
            xzUI = "outGame";
            GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(180, 26, 190, 36, 0.3f);
            kuang.transform.position = getRQ.transform.position;
        }

        GetComponent<UITween>().GetUIImage(kuang).ImgAlphaStartSet(0, 0.2f);
    }
}
