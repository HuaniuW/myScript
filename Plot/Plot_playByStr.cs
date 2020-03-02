using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_playByStr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonControl();
    }


    void ButtonControl()
    {
        //if (!Globals.isInPlot) return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            //print("-------------------------------------尼玛！！1");
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

    string testStr = "talk-2.8-B_yg-居然还有人类存在？哈哈哈哈！|talk-2.8-B_yg-弱小就不应该存在。乖乖的等我消灭吧 哈哈哈哈哈！啊哈哈哈哈哈哈哈！阿阿阿阿阿阿阿阿阿阿阿阿阿？哈哈哈哈！";
    string _plotString;
    public void SetPlotString(string str)
    {
        _plotString = str;
    }



    List<string> plotList = new List<string> { };
    int maxNums = 0;
    int playNums = 0;
    void GetStart()
    {
        string[] strArr = testStr.Split('|');
        plotList = new List<string>(strArr);//strArr.ToList();
        maxNums = plotList.Count;
        playNums = 0;
        //print(plotList.Count+"   ------------------------   "+plotList[0]);
        GetStartPlotByTimes();
    }


    public void GetStartPlotByTimes()
    {
        
        string str = plotList[playNums];
        string txtTop = str.Split('-')[0];
        float time = float.Parse(str.Split('-')[1]);
        switch (txtTop)
        {
            case "talk":
                string talkMen = str.Split('-')[2];
                string talkContent = str.Split('-')[3];
                GetTalk(talkContent, talkMen, time);
                break;
            case "walk":
                break;
        }
        GetComponent<TheTimer>().TimesAdd(time, TimeCallBack);
        playNums++;
    }

    GameObject talkBar;
    void GetTalk(string talkContent, string talkMen,float time = 2) {
        //找到 说话角色  找到对话框的 位置点
        GameObject obj = GlobalTools.FindObjByName(talkMen);
        Vector2 talkPos = obj.GetComponent<GameBody>().GetTalkPos();
        //创建对话框
        talkBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TalkBar") as GameObject);
        /*print("talkBar   "+ talkBar);
        print("talkPos   "+ talkPos);
        print("talkContent   " + talkContent);
        print("talkBar   " + (talkBar==null));*/

        talkBar.GetComponent<UI_talkBar>().ShowTalkText(talkContent, talkPos, time-0.5f);
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


    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (!isStarting&&Coll.tag == "Player")
        {
            isStarting = true;
            GetStart();
        }
    }

}
