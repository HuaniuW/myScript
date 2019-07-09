using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_setBar : MonoBehaviour {
    public Toggle tog1;
    public Toggle tog2;
    
    //设置标题1
    public Text text1;
    //设置标题2
    public Text text2;
    //声音条
    public Slider sl1;

    public Image kuang;

    public Button btn_close;

    List<GameObject> labels = new List<GameObject>();
    // Use this for initialization
    float kuangX;

    void Start () {
        AddEvenr();
        this.sl1.value = GlobalSetDate.instance.GetSoundEffectValue();
        kuang.transform.position = new Vector2(kuang.transform.position.x, text1.transform.position.y);


        xzUI = "yingliang";
        kuangX = kuang.transform.position.x;
        GlobalSetDate.instance.IsChangeScreening = true;
        GameObject[] b = { text1.gameObject, text2.gameObject , btn_close.gameObject};
        labels.AddRange(b);

        GetLuanguage();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_LANGUAGE, GetLanguageChange);
    }


    void GetLuanguage()
    {
        if(Globals.language == Globals.CHINESE)
        {
            text1.text = "音量";
            text2.text = "语言";
            tog1.GetComponentInChildren<Text>().text = "中文";
            tog2.GetComponentInChildren<Text>().text = "英文";


        }
        else if(Globals.language == Globals.ENGLISH)
        {
            text1.text = "volume";
            text2.text = "language";
            tog1.GetComponentInChildren<Text>().text = "chinese";
            tog2.GetComponentInChildren<Text>().text = "english";
        }
    }


    void GetLanguageChange(UEvent e)
    {
        print(e.eventParams);
        GetLuanguage();
    }

    void AddEvenr()
    {
        tog1.onValueChanged.AddListener(GetTog1);
        tog2.onValueChanged.AddListener(GetTog2);
        sl1.onValueChanged.AddListener(SLChange);

        btn_close.onClick.AddListener(RemoveSelf);
    }

    // Update is called once per frame
    void Update () {
       
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindNearestQR("up");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindNearestQR("down");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FindNearestQR("left");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FindNearestQR("right");
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GetChoseObj();
        }
    }

    void GetChoseObj()
    {
        if(xzUI == "zhongwen")
        {
            if (!tog1.isOn)
            {
                //GetZhongWen();
                tog1.isOn = true;
            }
        }
        else if(xzUI == "yingwen")
        {
            if (!tog2.isOn)
            {
                //GetYingWen();
                tog2.isOn = true;
            }
        }else if (xzUI == "close")
        {
            RemoveSelf();
        }
       
    }

    void GetZhongWen()
    {
        PlayVadioByName("xd");
        Globals.language = "chinese";
        //print("Globals.language    "+ Globals.language);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);
    }

    void GetYingWen()
    {
        PlayVadioByName("xd");
        Globals.language = "english";
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_LANGUAGE, Globals.language), this);
    }




    void RemoveSelf()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_LANGUAGE, GetLanguageChange);
        GlobalSetDate.instance.IsChangeScreening = false;
        DestroyImmediate(this.gameObject, true);
    }

    void SLChange(float value)
    {
        GlobalSetDate.instance.SoundEffect = value;
        PlayVadioByName("xz");
    }

    void GetTog1(bool check)
    {
        if (tog1.isOn) {
            GetZhongWen();
        }
    }

    void GetTog2(bool check)
    {
        if (tog2.isOn) GetYingWen();
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
                if (rq.transform.position.y > kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "down")
        {
            foreach (var rq in labels)
            {
                if (rq.transform.position.y < kuang.transform.position.y)
                {
                    tempList.Add(rq);
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
            KuangMoveStartSet(getRQ.name,fx);
            PlayVadioByName("xz");
        }
        //置空临时列表
        tempList = null;

    }

    //播放选择声音
    void PlayVadioByName(string _name)
    {
        GameObject UI_start = GameObject.Find("/UI_Start");
        GlobalTools.PlayAudio(_name, UI_start.GetComponent<StartScreen>());
    }

    string xzUI = "";

    void KuangMoveStartSet(string _txtName,string zy = "")
    {
        print("sha a "+zy+"  txtName  "+_txtName);
        if (zy == "down" || zy == "up") zy = "";
        if(_txtName == "Text2")
        {
            //选择的是文字 中文
            xzUI = "zhongwen";

            GameObject.Find("/UI_Start").GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(166, 20, 152, 34, 0.3f);
            if (zy == "")
            {
                kuang.transform.position = new Vector2(kuangX - 100, getRQ.transform.position.y);
            }
            else
            {
                if(zy == "left")
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
        else if(_txtName == "Text1")
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
            GameObject.Find("/UI_Start").GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(336, 20, 350, 34, 0.3f);
            kuang.transform.position = new Vector2(kuangX, getRQ.transform.position.y);
        }
        else if (_txtName == "btn_close")
        {
            xzUI = "close";
            GameObject.Find("/UI_Start").GetComponent<UITween>().GetUIImage(kuang).ImgChangeStartSet(60, 60, 80, 80, 0.3f);
            kuang.transform.position = getRQ.transform.position;

        }
        
        GameObject.Find("/UI_Start").GetComponent<UITween>().GetUIImage(kuang).ImgAlphaStartSet(0, 0.2f);
    }
}
