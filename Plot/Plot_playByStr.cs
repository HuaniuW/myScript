﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_playByStr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HideAlertBar();
        GetStart();
        CheckSaveDate();
        
    }


    public string TalkID = "talk01";

    // Update is called once per frame
    void Update()
    {
        if(!IsHitAlertControl)ButtonControl();
        if (IsHitInAir) HitKuaiInAir();
        if(IsStartPlot) GetStartPlotStr();
    }

    //是否已经 播放完  读取记录判断   ***新库类 根据位置等  读取记录信息
    void CheckSaveDate()
    {
        //剧情是否被播过？
        //GlobalSetDate.instance.TempZGuanKaStr;
        if (GlobalDateControl.IsHasDateByName(TalkID))
        {
            //有记录
            IsHitKuaiStop = false;
        }
        else
        {
            print("该剧情 没有 记录过！");
        }
    }


    //剧情角色 相对玩家位置的 转身
    //第二次触发 的剧情内容

    //自动播放第一条消息
    //播放完后 取消 碰撞就触发 并且记录  (剧情播完 才能记录)





    [Header("是否碰到 剧情块就 停止所有操作")]
    public bool IsHitKuaiStop = true;

    GameObject player;

    void HitKuaiStop()
    {
       

        //停止 动作
        Globals.isInPlot = true;

        player = GlobalTools.FindObjByName("player");

        //角色 头发变白
        player.GetComponent<PlayerGameBody>().Bianbai();

        //player.GetComponent<GameBody>().StopVSpeed();
        player.GetComponent<GameBody>().GetStand();

        if (player.GetComponent<GameBody>().isInAiring)
        {
            IsHitInAir = true;
        }
    }

    bool IsHitInAir = false;
    void HitKuaiInAir()
    {
        if (player.GetComponent<GameBody>().IsGround) {
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


    bool IsStartPlot = false;
    bool IsCanClickRemovePlotKuai = false;
    float startNums = 0;
    bool IsPlotOver = false;
    
    void GetStartPlotStr()
    {
        if (IsCanClickRemovePlotKuai) return;
        startNums += Time.deltaTime;
        if (startNums>2f)
        {
            startNums = 0;
            IsStartPlot = false;
            IsCanClickRemovePlotKuai = true;
        }
    }


    

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (GlobalDateControl.IsHasDateByName(TalkID)) return;
        if (!IsHitKuaiStop && !IsHitAlertControl && !isStarting) return;
        if (Coll.tag == "Player")
        {
            if (IsHitKuaiStop)
            {
                IsStartPlot = true;
                startNums = 0;
                playNums = 0;
                HitKuaiStop();
                if (GlobalTools.FindObjByName("player").GetComponent<GameBody>().IsGround) Globals.IsHitPlotKuai = true;
                print("IsHitKuaiStop!!!!!   剧情开始 ");
                GetStartPlotByTimes();
                //显示提示牌
                //ShowAlertBar();
                return;
            }













            //if (!IsHitAlertControl && !isStarting)
            //{
            //    //自动播放的
            //    isStarting = true;
            //    playNums = 0;
            //    //GetStart();
            //    GetStartPlotByTimes();
            //}
            //else
            //{
            //    //玩家点击交互的
            //    playNums = 0;
            //    //StopPlayerControl();
            //    if (GlobalTools.FindObjByName("player").GetComponent<GameBody>().IsGround) Globals.IsHitPlotKuai = true;
            //    print("hitplotKuai");
            //    //显示提示牌
            //    //ShowAlertBar();
            //}
        }
    }












    void ButtonControl()
    {

        //return;

        //if (!Globals.isInPlot) return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            print("-------------------------------------尼玛！！1");
            //对话 快速跳过
            if (talkBar) talkBar.GetComponent<UI_talkBar>().RemoveSelf();
            GetComponent<TheTimer>().End();
            if (playNums < maxNums) {
                
                GetStartPlotByTimes();
            }
            else
            {
                //结束剧情
                PlotOver();
            } 
            return;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            //做取消剧情相关的事
            print("----------------------按键跳过剧情！！");
            Globals.isInPlot = false;
        }
    }

    bool isStarting = false;
    //人为控制 对话   必须玩家点了 才能 进入下一条
    public bool IsPlayerControlPlotTalk = true;
    //全局记录是否播放过   有的是直接场景 跳好站位 在播剧情      是否需要 剧情？
    //  人物对话 可以用这套
    //开局 给boss一个 单独独白就可以了   

    //T0 进场  触发剧情   摄像机 位置     行动还是对话    谁说话 说什么 是否有动作 持续多久 是否可以快进（快进还原动作）     直接跳过
    //move  1.walk  2.run 
    //camera topos
    //talk text
    //ac acname
    //isCanJump


    //talk-2.8-B_yg-居然还有人类存在？哈哈哈哈！|talk-2.8-B_yg-弱小就不应该存在。乖乖的等我消灭吧 哈哈哈哈哈！啊哈哈哈哈哈哈哈！阿阿阿阿阿阿阿阿阿阿阿阿阿？哈哈哈哈！


    [Header("内容 为什么把说话的对象 放在数组内 应为 会切换对象 所以没有单独拿出来")]
    public string plotStr = "talk-2.8-B_yg-居然还有人类存在？哈哈哈哈！|talk-2.8-B_yg-弱小就不应该存在。乖乖的等我消灭吧 哈哈哈哈哈！啊哈哈哈哈哈哈哈！阿阿阿阿阿阿阿阿阿阿阿阿阿？哈哈哈哈！";
    [Header("是否停止角色的行动 这里 在角色确定打开才停止")]
    public bool IsStopPlayerControl = false;

    [Header("是否 碰撞 后提示操作 这里会停止角色的 行动 角色点击才能跳过对话")]
    public bool IsHitAlertControl = false;

    string _plotString;
    public void SetPlotString(string str)
    {
        _plotString = str;
    }


  

    void CanControl()
    {
        Globals.isInPlot = false;
    }



    List<string> plotList = new List<string> { };
    int maxNums = 0;
    int playNums = 0;
    void GetStart()
    {
        string[] strArr = plotStr.Split('|');
        plotList = new List<string>(strArr);//strArr.ToList();
        maxNums = plotList.Count;
        //playNums = 0;
        //print(plotList.Count+"   ------------------------   "+plotList[0]);
        //GetStartPlotByTimes();
    }

    float _time = 2;
    public void GetStartPlotByTimes()
    {
        print("?????? playNums:    " + playNums);
        if (IsHitAlertControl&& playNums == maxNums)
        {
            //对话完成 这里之后可以操作  
            if (talkBar) talkBar.GetComponent<UI_talkBar>().RemoveSelf();
            OutHitKuaiReSet();
            GetComponent<TheTimer>().TimesAdd(0.4f, TimeTalkBarCallBack2);
            //IsSHowTalkBar = false;
            playNums = 0;
            print("  对话框 结束 playNums 归零 " + playNums);
            print("---------------------------------------------------------------------------");
            if (IsStopPlayerControl) CanControl();
            
            //存档
            GlobalDateControl.SetMsgInCurrentGKDateAndSetInZGKDate(this.TalkID);
            IsPlotOver = true;
            return;
        }
        string str = plotList[playNums];
        string txtTop = str.Split('-')[0];
        _time = float.Parse(str.Split('-')[1]);
        switch (txtTop)
        {
            case "talk":
                string talkMen = str.Split('-')[2];
                string talkContent = str.Split('-')[3];
                GetTalk(talkContent, talkMen, _time);
                break;
            case "walk":
                break;
        }

        if(!IsHitAlertControl) GetComponent<TheTimer>().TimesAdd(_time, TimeCallBack);
        if (playNums <maxNums) {
            playNums++;
        }
       
        
    }


    GameObject talkBar;
    public GameObject talkPosObj;
    void GetTalk(string talkContent, string talkMen,float time = 2) {
        //print("??---------------->  "+ talkMen);
        //找到 说话角色  找到对话框的 位置点
        GameObject obj = GlobalTools.FindObjByName(talkMen);
        Vector2 talkPos = Vector2.zero;
        if(obj.GetComponent<GameBody>()!=null) talkPos = obj.GetComponent<GameBody>().GetTalkPos();

        if (IsHitAlertControl)
        {
            //延迟0.5秒显示
            if (talkBar) talkBar.GetComponent<UI_talkBar>().RemoveSelf();
            //隐藏掉 提示牌
            HideAlertBar();
            _talkContent = talkContent;
            //if(talkPos == null||talkPos == Vector2.zero)
            //{
                
            //    _talkPos =  new Vector2(talkPosObj.transform.position.x, talkPosObj.transform.position.y) ;
            //    print(" -- 显示位置: " + talkPos + "    >>???    "+talkPosObj.transform.position);
            //}
            _talkPos = new Vector2(talkPosObj.transform.position.x, talkPosObj.transform.position.y);
            _distime = time;
            GetComponent<TheTimer>().TimesAdd(0.2f, TimeTalkBarCallBack);
        }
        else
        {
            //创建对话框
            talkBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TalkBar") as GameObject);

            //这里时间减了 0.5f就是错开了 显示  UIBar 提前了0.5秒消失 0.5秒后 会再自动调用下一句
            talkBar.GetComponent<UI_talkBar>().ShowTalkText(talkContent, talkPos, time - 0.5f);
        }
    }



    string _talkContent;
    Vector2 _talkPos;
    float _distime;


    void TimeTalkBarCallBack(float nums)
    {
        if (IsStopPlayerControl) HitKuaiStop();
        talkBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TalkBar") as GameObject);
        talkBar.GetComponent<UI_talkBar>().ShowTalkText(_talkContent, _talkPos, _distime);
        IsSHowTalkBar = false;
        //GetComponent<TheTimer>().TimesAdd(1f, TimeTalkBarCallBack2);

    }

    void TimeTalkBarCallBack2(float num)
    {
        print("我靠  进来多块！！！！！");
        IsSHowTalkBar = false;
    }

    public void TimeCallBack(float nums)
    {
        //print("计时完成一个周期");
        //对比数组中数据长度 
        if (playNums < maxNums) { 
            GetStartPlotByTimes();
        }
        else
        {
            //结束 剧情  
           
            PlotOver();
        }
    }

    void PlotOver()
    {
        print("剧情结束！！！！！！！！！！！！！！！");
        Globals.isInPlot = false;
    }



  

    bool IsSHowTalkBar = false;
    [Header("提示显示的 对话按钮显示动画 提示玩家点击")]
    public GameObject AlertObj;
    private void OnTriggerStay2D(Collider2D Coll)
    {
        
        if (Coll.tag == "Player")
        {
            if (IsHitAlertControl)
            {
                //print("?????????  IsHitAlertControl "+ IsHitAlertControl);
                if (Input.GetKeyDown(KeyCode.J))
                {
                    if (IsPlotOver) return;
                    if (!GlobalTools.FindObjByName("player").GetComponent<GameBody>().IsGround) return;

                    print("startNums 计时器      " + startNums);

                    if (!IsCanClickRemovePlotKuai) return;
                    IsCanClickRemovePlotKuai = false;
                    IsStartPlot = true;

                    if (!IsSHowTalkBar)
                    {
                        IsSHowTalkBar = true;
                        GetStartPlotByTimes();

                        //print("点击切换 对话 " + playNums);
                    }

                }
            }
        }
    }


    void ShowAlertBar()
    {
        print("显示提示牌     "+AlertObj);
        if(AlertObj) AlertObj.SetActive(true);
    }

    void HideAlertBar()
    {
        print("隐藏提示牌");
        if (AlertObj) AlertObj.SetActive(false);
    }

   
    private void OnTriggerExit2D(Collider2D Coll)
    {
        print("离开");
        if (Coll.tag == "Player")
        {
            if (IsHitAlertControl)
            {
                //if(playNums<maxNums)PlotNext();
                OutHitKuaiReSet();
                
                playNums = 0;
                IsSHowTalkBar = false;
                if (talkBar) talkBar.GetComponent<UI_talkBar>().RemoveSelf();
            }
            Globals.IsHitPlotKuai = false;
        }


        
    }

    void OutHitKuaiReSet()
    {
        HideAlertBar();
        
        CanControl();
        print("执行 可以控制后   "+Globals.isInPlot);
        //IsSHowTalkBar = false;
    }


    


}
