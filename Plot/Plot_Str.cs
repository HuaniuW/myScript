﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_Str : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("剧情id")]
    public string TalkID = "talk01";

    [Header("对话框弹出点")]
    public GameObject TalkPosObj;

    [Header("CameraZ Z 移动位置")]
    public float CameraZ = -9;

    //acType:talk-talkObjName:player-msg:你好 哈哈哈！-choseType:dan-id:3-nextId:4
    //[Header("内容 为什么把说话的对象 放在数组内 应为 会切换对象 所以没有单独拿出来")]    类型（talk 对话  ac 动作 ----   对象名  ----  内容   ----   是不是多选1是单 2是给玩家多个选择    ----        ）
    //string plotStr = "acType:talk-talkObjName:B_yg-msg:hi 欢迎来到 约束之地！-choseType:dan-id:1-nextId:2|acType:talk-talkObjName:B_yg-msg:你好啊 哈哈哈哈哈哈哈哈a ggg哦哦哦哦哦哦哦哦哦哦哦gg！-choseType:dan-id:2-nextId:3|" +
    //    "acType:talk-talkObjName:player-msg:你好！哈哈哈。哈哈哈哈哈哦哦哦哦哦哦哦哦哦哦哦g哈哈哈哈哈哦哦哦哦哦哦哦哦哦哦哦g哈哈哈哈哈a哦哦哦哦哦哦哦哦哦哦哦g-choseType:dan-id:3-nextId:4|acType:talk-talkObjName:n-msg:有信心 通过考验么！-choseType:dan-id:4-nextId:5|" +
    //    "acType:talk-talkObjName:player-msg:。。。。。。！#6@必须得！#7@。。。。没！#8-choseType:duo-id:5-nextId:6|" +
    //    "acType:talk-talkObjName:B_yg-msg:哦哦哦 明白你的意思了！-choseType:dan-id:6-nextId:9|" +
    //    "acType:talk-talkObjName:B_yg-msg:我竟然无言以对！-choseType:dan-id:7-nextId:10|" +
    //    "acType:talk-talkObjName:B_yg-msg:你走吧！！-choseType:dan-id:9-nextId:11|" +
    //    "acType:talk-talkObjName:B_yg-msg:啦啦啦啦 啦啦啦啦啦。-choseType:dan-id:10-nextId:11|";

    [Header("是否 在谈话结束 自动记录")]
    public bool IsNeedRecordByOver = true;
    string plotStr = "acType:talk-talkObjName:B_dlws-msg:我等你好久了，要现在就和我战斗吗？-choseType:dan-id:1-nextId:2|" +
        "acType:talk-talkObjName:player-msg:来吧！#4^event$test@不要，不想做无谓的战斗。#3-choseType:多-id:2-nextId:3|" +
        "acType:talk-talkObjName:B_dlws-msg:哼！等你变强再来找我吧 。^event$over-choseType:dan-id:3-nextId:6|" +
        "acType:talk-talkObjName:B_dlws-msg:哈哈！正合我意，我上了。^event$startfight-choseType:dan-id:4-nextId:5";


    private void OnEnable()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.NEXT_ID, GetNextId);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHOSE_EVENT, GetChoseEvent);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.PLOT_KILL_PLAYER, KillPlayerOver);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.NEXT_ID, GetNextId);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHOSE_EVENT, GetChoseEvent);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.PLOT_KILL_PLAYER, KillPlayerOver);
    }

    void GetNextId(UEvent e)
    {
        int id = int.Parse(e.eventParams.ToString());
        ShowPlotStr(id);
    }


    string ChoseEventStr = "";
    void GetChoseEvent(UEvent e)
    {
        ChoseEventStr = e.eventParams.ToString();
        //print(" ------ choseEventName    "+e.eventParams.ToString());
    }


    GameObject player;
    bool IsHitInAir = false;
    //玩家碰到后 停下来
    void PlayerHitStop() {
        //停止 动作
        Globals.isInPlot = true;
        player = GlobalTools.FindObjByName("player");
        //角色 头发变白
        player.GetComponent<PlayerGameBody>().Bianbai();
        //player.GetComponent<GameBody>().StopVSpeed();
        player.GetComponent<GameBody>().GetStand();
        player.GetComponent<GameBody>().StopYanMu();
        if (player.GetComponent<GameBody>().isInAiring)
        {
            IsHitInAir = true;
        }
    }

    void HitKuaiInAir()
    {
        if (player.GetComponent<GameBody>().IsGround)
        {
            IsHitInAir = false;
            player.GetComponent<GameBody>().GetStand();
            player.GetComponent<GameBody>().GetDB().animation.Play();
        }
        else
        {
            player.GetComponent<GameBody>().GetDB().animation.GotoAndPlayByFrame(player.GetComponent<GameBody>().GetJumpDownACName());
            player.GetComponent<GameBody>().GetDB().animation.Stop();
        }
    }


    //判断 剧情是否 播过
    bool IsPlotHasPlayed()
    {
        return GlobalDateControl.IsHasDateByName(TalkID);
    }






    bool IsCanBeHit = true;
    bool IsStartPlot = false;
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (!IsCanBeHit) return;
        if (Coll.tag == "Player")
        {
            //判断 剧情是否已经播过 播过的话就 不进入了
            if (IsPlotHasPlayed()) return;
            IsStartPlot = true;
            PlayerHitStop();
            GetStr();
            //ShowPlotStr();
        }
    }


    private void OnTriggerStay2D(Collider2D Coll)
    {
        if (!IsCanBeHit) return;
        if (Coll.tag == "Player")
        {
            //print("?");
            //出文本了
            
        }
    }

    private void OnTriggerExit2D(Collider2D Coll)
    {
        if (!IsCanBeHit) return;
        print("离开");
        if (Coll.tag == "Player")
        {
            if (_cBar) _cBar.GetComponent<UI_talkBar>().RemoveSelf();
        }
    }



    List<string> plotStrList = new List<string>() { };
    void GetStr()
    {
        string[] strArr = plotStr.Split('|');
        plotStrList = new List<string>(strArr);//strArr.ToList();
    }


    int _nextId = 1;
   
    string _acType = "";
    string _talkObjName = "";
    string _msg = "";



    Vector2 _talkPos = Vector2.zero;
    float times = 0;


    //1是单个 谈话框  2是多选
    string _choseType = "dan";


    void GetStrMsg(string str)
    {
        string[] strArr = str.Split('-');
        //print("  ???>>str    "+str);
        for (int i = 0;i<strArr.Length;i++)
        {
            string key = strArr[i].Split(':')[0];
            string theMsg = strArr[i].Split(':')[1];
            if (key == "acType") _acType = theMsg;
            if (key == "talkObjName") _talkObjName = theMsg;
            //print(" --------- _talkObjName    "+ _talkObjName);
            if (key == "msg") _msg = theMsg;
            if (key == "choseType") _choseType = theMsg;
            if (key == "nextId") _nextId = int.Parse(theMsg);
        }
    }



    GameObject _cBar;
    void GetTalkBar()
    {
        if (_cBar)
        {
            _cBar.GetComponent<UI_talkBar>().RemoveSelf();
        }

        _cBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TalkBar") as GameObject);

        //print("--->_talkObjName  ?   "+ _talkObjName);

        GameObject talkObj = GlobalTools.FindObjByName(_talkObjName);

        if (talkObj)
        {
            _talkPos = talkObj.GetComponent<GameBody>().GetTalkPos();
        }
        else
        {
            _talkPos = this.TalkPosObj.transform.position;
        }

        _cBar.GetComponent<UI_talkBar>().ShowTalkText(_msg, _talkPos, times, _talkObjName);
        _clickJishiSatrt = true;

    }





    void ShowPlotStr(int strId = 1)
    {
        //根据 nextId获取 相应的 对话内容
        string _msgNR = GetStrByStrId(strId);

        if (_msgNR == "")
        {
            PlotOver();
            return;
        }


        //根据 内容 获取 各个键值
        GetStrMsg(_msgNR);


        if(_choseType == "dan")
        {
            //找到 对话框  哪个
            GetTalkBar();
        }
        else{
            //做多选
            _cBar.GetComponent<UI_talkBar>().StopCanClick();
            //_cBar.GetComponent<UI_talkBar>().BgImgColorChange();
            //显示 选择题

            GameObject ui_choseBar = GlobalTools.GetGameObjectByName("UI_talkChoseBar");
            ui_choseBar.transform.position = new Vector2(player.transform.position.x+3.6f* player.transform.localScale.x, player.transform.position.y);      //player.transform.position;
            ui_choseBar.GetComponent<UI_talkChose>().GetStrMsg(_msg);

        }


    }


    void PlotOver()
    {
        //print("没有文本了！！！！");
        //结束剧情
        ClickJishiReSet();
        if (IsNeedRecordByOver)
        {
            GlobalDateControl.SetMsgInCurrentGKDateAndSetInZGKDate(this.TalkID,IsNeedRecordByOver);
        }
        HuanYuanCamera();
        if (_cBar) _cBar.GetComponent<UI_talkBar>().RemoveSelf();

        StartCoroutine(SetPlotFalse(0.4f));
    }

    public IEnumerator SetPlotFalse(float time)
    {
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        Globals.isInPlot = false;

       

        print("是否有 CHOSE_EVENT 事件 "+ ChoseEventStr);
        if (ChoseEventStr == "over")
        {
            //存档 数据 存入哪些东西
            PlotOverSave();
        }

        IsCanBeHit = false;
        //Destroy(this.gameObject);
    }

    [Header("对话结束 需要存入的数组吧")]
    public string[] OverCunRuShuJuArr;
    void PlotOverSave()
    {
        for (int i = 0;i< OverCunRuShuJuArr.Length;i++)
        {
            GlobalDateControl.SetMsgInCurrentGKDateAndSetInZGKDate(OverCunRuShuJuArr[i]);
        }

        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.OPEN_DOOR, OpenDoor);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "Men_2-0"), this);
        if (IsNeedRecordByOver)
        {
            GlobalDateControl.SaveMapDate();
        }
    }


    void KillPlayerOver(UEvent e)
    {
        PlotOverSave();
    }



    string GetStrByStrId(int id)
    {
        for (int i=0;i< plotStrList.Count;i++)
        {
            if (plotStrList[i] == "") return "";
            string strMag = plotStrList[i];
            string[] strArr = strMag.Split('-');

            for (int j=0;j< strArr.Length; j++)
            {
                string key = strArr[j].Split(':')[0];
                string theMsg = strArr[j].Split(':')[1];
                if (key == "id")
                {
                    if(int.Parse(theMsg) == id)
                    {
                        return plotStrList[i];
                    }
                }
            }
        }
        return "";
    }



    void ClickJishiReSet()
    {
        _clickJishiNum = 0;
        _clickJishiSatrt = false;
        _isCanClickJ = false;
    }

    bool _clickJishiSatrt = false;
    float _clickJishiNum = 0;
    bool _isCanClickJ = false;
    float _canClickNum = 1f;

    void IsCanClickJ()
    {
        if (_clickJishiSatrt)
        {
            _clickJishiNum += Time.deltaTime;
            if(_clickJishiNum>= _canClickNum)
            {
                if (!_isCanClickJ) {
                    _isCanClickJ = true;
                    _cBar.GetComponent<UI_talkBar>().CanClick();


                    if (_choseType == "dan")
                    {
                        //显示 可以点
                        //print("对话显示类型1 可以点了！ ");
                    }
                    else if (_choseType == "duo")
                    {
                        //print(" 类型2 多选！ ");
                    }
                } 
            }
        }
    }




    void ButtonControl()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //做取消剧情相关的事
            //print("----------------------按键跳过剧情！！");
            Globals.isInPlot = false;
        }



        if (!Globals.isInPlot) return;
        
        
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            //print("  _isCanClickJ  "+ _isCanClickJ);
            if (!_isCanClickJ) return;
            ClickJishiReSet();
            //print("-------------------------------------尼玛！！  _nextId  "+_nextId);
            ShowPlotStr(_nextId);
            _cBar.GetComponent<UI_talkBar>().SetOutEvent();
            return;
        }
    }







    bool IsPlotStarting = false;
    void StartPlot()
    {
        if (IsStartPlot&&!IsPlotStarting && !IsHitInAir)
        {
            IsPlotStarting = true;
            //拉近镜头
            LaJinCamera();

            //显示 文本  协程来运行
            // public IEnumerator IEDestory(GameObject gameObject, float time)  协程 伪进程
            //StartCoroutine(ObjectPools.GetInstance().IEDestory(hitBar,2f));  执行 协程IEnumerator

            StartCoroutine(IPoltPlay(0.4f));

        }
    }

    public IEnumerator IPoltPlay(float time)
    {
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        ShowPlotStr();
    }



    GameObject cm;
    Vector3 cameraPosition = Vector3.zero;
    void LaJinCamera()
    {
        cm = GlobalTools.FindObjByName("MainCamera");
        cameraPosition = cm.transform.position;
        cm.GetComponent<CameraController>().SetNewPosition(new Vector3(cm.transform.position.x, player.transform.position.y + 2, CameraZ));
    }


    void HuanYuanCamera()
    {
        cm.GetComponent<CameraController>().SetNewPosition(cameraPosition);
    }



    // Update is called once per frame
    void Update()
    {
        if (IsHitInAir) HitKuaiInAir();
        StartPlot();
        IsCanClickJ();
        ButtonControl();
    }
}