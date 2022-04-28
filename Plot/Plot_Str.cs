using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_Str : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IsHasOverAndNeedDel();
        CheckNeedGetCiPlot();
    }

    [Header("是否在对话完成后 删除自己")]
    public bool IsOverMoveSelf = false;

    [Header("在对话结束 后 删除的 对象")]
    public GameObject OverDelObj;

   

    void OverDelSelf()
    {
        if (OverDelObj) {
            OverDelObj.SetActive(false);
        } 
    }

    void IsHasOverAndNeedDel()
    {
        //开始 来看 是否需要删除
        if (IsPlotHasPlayed() && IsOverMoveSelf) OverDelSelf();

        string value = GlobalSetDate.instance.GetOtherDateValueByKey(CheckOtherKey);
        print(CheckOtherKey+ "数据  ------------------------ ！！！！    " + value);
        if (value == "0" && PlotID == "talk02")
        {
            OverDelSelf();
        }

    }


    //怎么 移除 自己    有的是 在第一次谈话就移除  有的是 在选择后 出现是否移除
    [Header("在对话结束 是否需要移除 自身")]
    public bool IsOverNeedRemoveSelf = true;

    [Header("备用 对话内容")]
    public string TalkMsg2 = "";
    //根据上次对话记录 来获取 第二次对话内容



    string Lizi_Xiaoshi = "Lizi_Xiaoshi";
    string Audio_xiaoshi = "Audio_xiu";


    [Header("出现物品的 位置")]
    public Transform OutObjPos;

    [Header("是否 对话结束 出现东西 默认出现 不出现的时候 看选择是什么")]
    public bool IsOverOutObj = true;

    [Header("对话结束 给予玩家的 徽章物品")]
    public string OverOutObjName = "";

    void OverOutObj()
    {
        if (!OutObjPos) return;
        if (IsOverNeedRemoveSelf)
        {
            GameObject shanguang = GlobalTools.GetGameObjectByName("TX_HZchuxianXingXing");
            shanguang.transform.position = OutObjPos.position;
            shanguang.transform.parent = this.transform.parent.parent;


            Vector2 pos = new Vector2(OutObjPos.position.x, OutObjPos.position.y - 1);

            GameObject lizixiaoshi = GlobalTools.GetGameObjectByName("Lizi_Xiaoshi");
            lizixiaoshi.transform.position = pos;
            lizixiaoshi.transform.parent = this.transform.parent.parent;
            lizixiaoshi.GetComponent<ParticleSystem>().Play();

            GameObject Audio_xiu = GlobalTools.GetGameObjectByName("Audio_xiu");
            Audio_xiu.transform.position = pos;
            Audio_xiu.transform.parent = this.transform.parent.parent;
            Audio_xiu.GetComponent<AudioSource>().Play();
        }


        if (!IsOverOutObj || OverOutObjName == "") {
            return;
        }
        //播声音

        print("OverOutObjName     "+ OverOutObjName);
        GameObject outObj = GlobalTools.GetGameObjectByName(OverOutObjName);
        if (outObj == null) return;
        outObj.transform.position = OutObjPos.position;
        print("   ------obj "+outObj.name);
        outObj.transform.parent = this.transform.parent.parent;
    }




    [Header("剧情id")]
    public string PlotID = "talk01";

    [Header("对话框弹出 位置 点")]
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
    //string plotStr = "acType:talk-talkObjName:B_dlws-msg:我等你好久了，要现在就和我战斗吗？-choseType:dan-id:1-nextId:2|" +
    //    "acType:talk-talkObjName:player-msg:来吧！#4^event$test@不想做无谓的战斗。#3-choseType:duo-id:2-nextId:3|" +
    //    "acType:talk-talkObjName:B_dlws-msg:哼！等你变强再来找我吧 。^event$over-choseType:dan-id:3-nextId:6|" +
    //    "acType:talk-talkObjName:B_dlws-msg:哈哈！正合我意，我上了。^event$startfight-choseType:dan-id:4-nextId:5";



    /**
     G2 和斗笠武士的对
    id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:我等你好久了，你准备好挑战我了吗？|id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:来吧！#4@不想做无谓的战斗。#3|id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:哼！等你变强再来找我吧 。^event$openDoor|id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:哈哈！正合我意，我上了。^event$startfight|
     
     */





    //string plotStr = "acType:talk-talkObjName:B_dlws-msg:只有意志坚定的人才能使用我的力量，你觉得你的意志坚定吗？-choseType:dan-id:1-nextId:2|" +
    //    "acType:talk-talkObjName:player-msg:当然！#4@我不喜欢费神。#3-choseType:duo-id:2-nextId:3|" +
    //    "acType:talk-talkObjName:B_dlws-msg:好吧，不出我所料，你和大部分人一样。^event$over-choseType:dan-id:3-nextId:6|" +
    //    "acType:talk-talkObjName:B_dlws-msg:你确定吗？你确定能坚定的一口气闯过这里所有的障碍吗。-choseType:dan-id:4-nextId:5|" +
    //    "acType:talk-talkObjName:player-msg:当然确定。#6^event$jianding*1@开玩笑的，哈哈！#7^event$over-choseType:duo-id:5-nextId:6|" +
    //    "acType:talk-talkObjName:B_dlws-msg:终于有一个有坚定品质的人了，我的力量就在终点口，到时候就会给与你我的力量。-choseType:dan-id:6-nextId:8";

    //string plotStr = "acType:talk-talkObjName:B_dlws-msg:。。。没有人爱我！-choseType:dan-id:1-nextId:2|" +
    //   "acType:talk-talkObjName:player-msg:。。。。我也是！#4@总会有爱你的人出现的！。#3-choseType:duo-id:2-nextId:3|" +
    //   "acType:talk-talkObjName:B_dlws-msg:好吧，不出我所料，你和大部分人一样。^event$over-choseType:dan-id:3-nextId:6|" +
    //   "acType:talk-talkObjName:B_dlws-msg:你确定吗？你确定能坚定的一口气闯过这里所有的障碍吗。-choseType:dan-id:4-nextId:5|" +
    //   "acType:talk-talkObjName:player-msg:当然确定。#6^event$jianding*1@开玩笑的，哈哈！#7^event$over-choseType:duo-id:5-nextId:6|" +
    //   "acType:talk-talkObjName:B_dlws-msg:终于有一个有坚定品质的人了，我的力量就在终点口，到时候就会给与你我的力量。-choseType:dan-id:6-nextId:8";


    public string plotStr = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:你喜欢花吗 ？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:喜欢#4@不喜欢#3|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:我以前也不喜欢 后来发现只是因为花太少。|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:是吧 谁会不喜欢花呢。|" +
       "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:有机会我会为你种满 漫山遍野的鲜花。|" +
       "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
       "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:后面的 花之盾 送你了。";

    public string PLOTSTR
    {
        //GET访问器，可以理解成另类的方法，返回已经被赋了值的私有变量a
        get { return plotStr; }
        //SET访问器，将我们打入的值赋给私有变量money
        set
        {
            if (PlotID== "talk01")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        plotStr = plot_id_1;
                        break;
                    case Globals.JAPAN:
                        plotStr = plot_id_1r;
                        break;
                    case Globals.ENGLISH:
                        plotStr = plot_id_1y;
                        break;
                    case Globals.Portugal:
                        plotStr = plot_id_1p;
                        break;
                    case Globals.KOREAN:
                        plotStr = plot_id_1h;
                        break;
                    case Globals.CHINESEF:
                        plotStr = plot_id_1zf;
                        break;
                    case Globals.German:
                        plotStr = plot_id_1d;
                        break;
                    case Globals.French:
                        plotStr = plot_id_1f;
                        break;
                    case Globals.Italy:
                        plotStr = plot_id_1i;
                        break;
                }
            }else if (PlotID == "talk02")
            {
                //坚定 对话
                string _value = GlobalSetDate.instance.GetOtherDateValueByKey(CheckOtherKey);
                if (_value == "1" && PlotID == "talk02")
                {
                    switch (Globals.language)
                    {
                        case Globals.CHINESE:
                            plotStr = ci2PlotStr1;
                            break;
                        case Globals.JAPAN:
                            plotStr = ci2PlotStr1r;
                            break;
                        case Globals.ENGLISH:
                            plotStr = ci2PlotStr1y;
                            break;
                        case Globals.Portugal:
                            plotStr = ci2PlotStr1x;
                            break;
                        case Globals.KOREAN:
                            plotStr = ci2PlotStr1h;
                            break;
                        case Globals.CHINESEF:
                            plotStr = ci2PlotStr1zf;
                            break;
                        case Globals.German:
                            plotStr = ci2PlotStr1d;
                            break;
                        case Globals.French:
                            plotStr = ci2PlotStr1f;
                            break;
                        case Globals.Italy:
                            plotStr = ci2PlotStr1i;
                            break;
                    }
                }
                else
                {
                    switch (Globals.language)
                    {
                        case Globals.CHINESE:
                            plotStr = plot_id_2;
                            break;
                        case Globals.JAPAN:
                            plotStr = plot_id_2r;
                            break;
                        case Globals.ENGLISH:
                            plotStr = plot_id_2y;
                            break;
                        case Globals.Portugal:
                            plotStr = plot_id_2x;
                            break;
                        case Globals.KOREAN:
                            plotStr = plot_id_2h;
                            break;
                        case Globals.CHINESEF:
                            plotStr = plot_id_2zf;
                            break;
                        case Globals.German:
                            plotStr = plot_id_2d;
                            break;
                        case Globals.French:
                            plotStr = plot_id_2f;
                            break;
                        case Globals.Italy:
                            plotStr = plot_id_2i;
                            break;
                    }
                }
            }
            else if (PlotID == "talk02a")
            {
                //和 神之手 第一次 遇见
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        plotStr = plot_id_2a;
                        break;
                    case Globals.JAPAN:
                        plotStr = plot_id_2ar;
                        break;
                    case Globals.ENGLISH:
                        plotStr = plot_id_2ay;
                        break;
                    case Globals.Portugal:
                        plotStr = plot_id_2ax;
                        break;
                    case Globals.KOREAN:
                        plotStr = plot_id_2ah;
                        break;
                    case Globals.CHINESEF:
                        plotStr = plot_id_2azf;
                        break;
                    case Globals.German:
                        plotStr = plot_id_2ad;
                        break;
                    case Globals.French:
                        plotStr = plot_id_2af;
                        break;
                    case Globals.Italy:
                        plotStr = plot_id_2ai;
                        break;
                }
            }
            else if (PlotID == "talk02b")
            {
                //和 神之手 第一次 遇见
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        plotStr = plot_id_2b;
                        break;
                    case Globals.JAPAN:
                        plotStr = plot_id_2br;
                        break;
                    case Globals.ENGLISH:
                        plotStr = plot_id_2by;
                        break;
                    case Globals.Portugal:
                        plotStr = plot_id_2bx;
                        break;
                    case Globals.KOREAN:
                        plotStr = plot_id_2bh;
                        break;
                    case Globals.CHINESEF:
                        plotStr = plot_id_2bzf;
                        break;
                    case Globals.German:
                        plotStr = plot_id_2bd;
                        break;
                    case Globals.French:
                        plotStr = plot_id_2bf;
                        break;
                    case Globals.Italy:
                        plotStr = plot_id_2bi;
                        break;
                }
            }
            else if (PlotID == "talk03")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        plotStr = plot_id_3;
                        break;
                    case Globals.JAPAN:
                        plotStr = plot_id_3r;
                        break;
                    case Globals.ENGLISH:
                        plotStr = plot_id_3y;
                        break;
                    case Globals.Portugal:
                        plotStr = plot_id_3x;
                        break;
                    case Globals.KOREAN:
                        plotStr = plot_id_3h;
                        break;
                    case Globals.CHINESEF:
                        plotStr = plot_id_3zf;
                        break;
                    case Globals.German:
                        plotStr = plot_id_3d;
                        break;
                    case Globals.French:
                        plotStr = plot_id_3f;
                        break;
                    case Globals.Italy:
                        plotStr = plot_id_3i;
                        break;
                }
            }
            else if (PlotID == "talk06")
            {
                //和 神之手 第一次 遇见
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        plotStr = plot_id_6;
                        break;
                    case Globals.JAPAN:
                        plotStr = plot_id_6r;
                        break;
                    case Globals.ENGLISH:
                        plotStr = plot_id_6y;
                        break;
                    case Globals.Portugal:
                        plotStr = plot_id_6x;
                        break;
                    case Globals.KOREAN:
                        plotStr = plot_id_6h;
                        break;
                    case Globals.CHINESEF:
                        plotStr = plot_id_6zf;
                        break;
                    case Globals.German:
                        plotStr = plot_id_6d;
                        break;
                    case Globals.French:
                        plotStr = plot_id_6f;
                        break;
                    case Globals.Italy:
                        plotStr = plot_id_6i;
                        break;
                }
            }
            else if (PlotID == "talk06a")
            {
                //和 神之手 第一次 遇见
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        plotStr = plot_id_6a;
                        break;
                    case Globals.JAPAN:
                        plotStr = plot_id_6ar;
                        break;
                    case Globals.ENGLISH:
                        plotStr = plot_id_6ay;
                        break;
                    case Globals.Portugal:
                        plotStr = plot_id_6ax;
                        break;
                    case Globals.KOREAN:
                        plotStr = plot_id_6ah;
                        break;
                    case Globals.CHINESEF:
                        plotStr = plot_id_6azf;
                        break;
                    case Globals.German:
                        plotStr = plot_id_6ad;
                        break;
                    case Globals.French:
                        plotStr = plot_id_6af;
                        break;
                    case Globals.Italy:
                        plotStr = plot_id_6ai;
                        break;
                }
            }
        }
    }








    string plot_id_1 = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:你喜欢花吗？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:喜欢#4@不喜欢#3|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:我以前也不喜欢 后来发现只是因为花太少。|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:是吧 谁会不喜欢花呢。|" +
       "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:不过我已经种满漫山遍野的鲜花了。|" +
       "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
       "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:后面的 花之盾 送你了。";

    string plot_id_1r = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:花が好きですか？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:お気に入り#4@嫌い#3|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:以前は気に入らなかったのですが、花が少なすぎたからだとわかりました。|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:ええ、花が嫌いな人。|" +
       "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:しかし、私はすでに山や畑のいたるところに花を植えました。|" +
       "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
       "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:後ろのフラワーシールドはあなたのためです。";

    string plot_id_1y = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:Do you like flowers?|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Like #4@dislike#3|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:I didn't like it before and found out that it was just because there were too few flowers.|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:Yeah, who doesn't like flowers.|" +
       "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:But I have already planted flowers all over the mountains and fields.|" +
       "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
       "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:The flower shield at the back is for you.";

    string plot_id_1p = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:¿Te gustan las flores?|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Me gusta #4@no me gusta#3|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:No me gustaba antes y descubrí que era solo porque había muy pocas flores.|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:Sí, a quién no le gustan las flores.|" +
      "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:Pero ya he plantado flores por todas las montañas y campos.|" +
      "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
      "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:El escudo de flores en la parte posterior es para ti.";

    string plot_id_1h = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:꽃을 좋아하세요?|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:좋아요 #4@싫어요#3|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:나는 전에 그것을 좋아하지 않았고 그것이 단지 꽃이 너무 적기 때문이라는 것을 알았습니다.|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:예, 꽃을 좋아하지 않는 사람.|" +
      "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:그러나 나는 이미 산과 들에 꽃을 심었습니다.|" +
      "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
      "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:뒤에 있는 꽃 방패는 당신을 위한 것입니다.";

    string plot_id_1zf = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:你喜歡花嗎？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:喜歡#4@不喜歡#3|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:我以前也不喜歡 後來發現只是因為花太少。|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:是吧 誰會不喜歡花呢。|" +
       "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:不過我已經種滿漫山遍野的鮮花了。|" +
       "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
       "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:後面的 花之盾 送你了。";
    string plot_id_1d = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:magst du Blumen？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:wie#4@nicht mögen#3|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:Ich mochte es vorher nicht und fand heraus, dass es nur daran lag, dass es zu wenige Blumen gab.|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:Ja, wer mag keine Blumen.|" +
       "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:Aber ich habe schon überall auf den Bergen und Feldern Blumen gepflanzt.|" +
       "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
       "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:Das Blumenschild auf der Rückseite ist für Sie.";
    string plot_id_1f = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:aimes-tu les fleurs？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:aimer#4@Ne pas aimer#3|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:Je n'aimais pas ça avant et j'ai découvert que c'était juste parce qu'il y avait trop peu de fleurs.|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:Ouais, qui n'aime pas les fleurs.|" +
       "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:Mais j'ai déjà planté des fleurs partout dans les montagnes et les champs.|" +
       "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
       "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:Le bouclier de fleurs à l'arrière est fait pour vous.";
    string plot_id_1i = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:Hun_npc-msg:ti piacciono i fiori？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:come#4@antipatia#3|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:Non mi piaceva prima e ho scoperto che era solo perché c'erano troppo pochi fiori.|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:Hun_npc-msg:Sì, a chi non piacciono i fiori.|" +
       "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Hun_npc-msg:Ma ho già piantato fiori in tutte le montagne e nei campi.|" +
       "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:player-msg:。。。。。。。。。。" +
       "id:7-choseType:dan-nextId:8-acType:talk-talkObjName:Hun_npc-msg:Lo scudo floreale sul retro è per te.";







    /**
     id:1-choseType:dan-nextId:2-acType:talk-talkObjName:player-msg:当时你为什么躲在树后面不出来 ？|id:2-choseType:dan-nextId:3-acType:talk-talkObjName:B_dlws-msg:当时你是天使，我是恶魔啊，我也想让自己变成天使再出来。|id:3-choseType:dan-nextId:4-acType:talk-talkObjName:player-msg:。。。。。。。。 |id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:现在我开始接受我是恶魔了。|
     
     
     
     */


    //碑文？？
    //起始 -- 已经不记得是多少次了，世界又毁灭了。人类总是在自我毁灭中循环。一次又一次，又一次。

    //G1--世界不在 我还在。可惜的是为什么我还在。

    //G2--我是恶魔 神创造的恶魔，让我扰乱人间，防止人间沉沦。我给人类的是诱惑，是人类自己想要的，跟我又有什么关系呢？为什么所有人都能重新开始，只有我不能。

    //G6 有死无生

    //G6打完武士后-- 人类的 战争机器 冷酷又高效。其实我很开心 每次世界毁灭 七月都会出现。

    [Header("用来 检测 剧情的内容 比如 jianding 等")]
    public string CheckOtherKey = "0";

    void CheckNeedGetCiPlot()
    {
        string value = GlobalSetDate.instance.GetOtherDateValueByKey(CheckOtherKey);
        print("数据  CheckOtherKey:    " + CheckOtherKey + "  检测 是否选用第二个剧情文本 ！！！！    " +value);
        if (value == "0"&&PlotID == "talk02b")
        {
            OverDelSelf();
            return;
        }


        if (value == "1"&&PlotID == "talk02")
        {
            plotStr = ci2PlotStr1;
        }
    }

    //加上 你意志坚定吗？
    //id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:我的能量只能在徽章和神树二选一，你是要徽章还是点亮神树。|id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:徽章#4^event$jianding*1@点亮神树#3^event$jianding*0|id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:好吧。那到时候我帮你点亮神树|id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:那我就不给神树冲能了，路上你要自己注意了,没有神树就靠你的意志了，路上要小心。|id:5-choseType:dan-nextId:6-acType:talk-talkObjName:Bll-msg:记得，不要和剑圣战斗


    //G2和 斗笠武士的对话



    string plot_id_2 = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:我的力量现在只能在徽章和神树二选一，你是要徽章还是点亮神树。|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:徽章#4^event$jianding*1@点亮神树#3^event$jianding*0|" +
        "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:好吧。那到时候我帮你点亮神树|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:那我就不给神树冲能了，路上你要自己注意了,没有神树就靠你的意志了，路上要小心。";

    string plot_id_2r = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:私の力はバッジと神の木の間でのみ選択できます。バッジが欲しいですか、それとも神の木に火をつけますか？|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:バッジ#4^event$jianding*1@神聖な木を照らします#3^event$jianding*0|" +
        "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:わかった。 その時、私はあなたが神の木を照らすのを手伝います|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:そうすれば、私は神の木にエネルギーを与えません。あなたは道で自分自身に注意を払わなければなりません。神の木がなければ、それはあなたの意志に依存します。道に注意してください。";

    string plot_id_2y = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:My power can only be chosen between the badge and the god tree. Do you want the badge or light the god tree?|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:badge#4^event$jianding*1@Light up the sacred tree#3^event$jianding*0|" +
        "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:All right. At that time, I will help you light up the divine tree|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Then I will not give energy to the divine tree. You have to pay attention to yourself on the road. Without the divine tree, it depends on your will. Be careful on the road.";

    string plot_id_2x = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Mi poder solo se puede elegir entre la insignia y el árbol de dioses. ¿Quieres la insignia o enciendes el árbol de dioses?|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Insignia#4^event$jianding*1@Ilumina el árbol sagrado#3^event$jianding*0|" +
        "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Todo bien. En ese momento, te ayudaré a encender el árbol divino.|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Entonces no le daré energía al árbol divino. Tienes que prestarte atención en el camino. Sin el árbol divino, depende de tu voluntad. Ten cuidado en el camino.";

    string plot_id_2h = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:내 힘은 뱃지와 신나무 중에서만 선택할 수 있습니다. 뱃지를 원하십니까, 신나무에 불을 붙이시겠습니까?|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:배지#4^event$jianding*1@신성한 나무를 밝혀라#3^event$jianding*0|" +
        "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:괜찮아. 그 때 나는 당신이 신성한 나무에 불을 붙이도록 도울 것입니다.|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:그러면 나는 신의 나무에 에너지를주지 않을 것입니다. 당신은 길에서주의를 기울여야합니다. 신의 나무가 없으면 당신의 의지에 달려 있습니다. 길에서 조심하십시오.";


    string plot_id_2zf = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:我的力量現在只能在徽章和神樹二選一，你是要徽章還是點亮神樹。|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:徽章#4^event$jianding*1@点亮神樹#3^event$jianding*0|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:好吧。那到時候我幫你點亮神樹|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:那我就不給神樹衝能了，路上你要自己注意了,沒有神樹就靠你的意誌了，路上要小心。";
    string plot_id_2d = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Meine Macht kann nur zwischen dem Abzeichen und dem Götterbaum gewählt werden. Willst du das Abzeichen oder den Götterbaum anzünden?|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Abzeichen#4^event$jianding*1@Erleuchte den heiligen Baum#3^event$jianding*0|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:In Ordnung. Zu dieser Zeit werde ich dir helfen, den göttlichen Baum zu erleuchten|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Dann werde ich dem göttlichen Baum keine Energie geben. Du musst auf dem Weg auf dich selbst achten. Ohne den göttlichen Baum hängt es von deinem Willen ab. Sei vorsichtig auf dem Weg.";
    string plot_id_2f = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Mon pouvoir ne peut être choisi qu'entre l'insigne et l'arbre divin. Voulez-vous l'insigne ou allumer l'arbre divin ?|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:badge#4^event$jianding*1@Allume l'arbre sacré#3^event$jianding*0|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:D'accord. À ce moment-là, je t'aiderai à allumer l'arbre divin|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Alors je ne donnerai pas d'énergie à l'arbre divin. Vous devez faire attention à vous-même sur la route. Sans l'arbre divin, cela dépend de votre volonté. Soyez prudent sur la route.";
    string plot_id_2i = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Il mio potere può essere scelto solo tra il distintivo e l'albero di Dio. Vuoi il distintivo o accendi l'albero di Dio?|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:distintivo#4^event$jianding*1@Illumina l'albero sacro#3^event$jianding*0|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Ok. In quel momento, ti aiuterò ad illuminare l'albero divino|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Allora non darò energia all'albero divino. Devi prestare attenzione a te stesso sulla strada. Senza l'albero divino, dipende dalla tua volontà. Stai attento sulla strada.";















    string ci2PlotStr1 = "acType:talk-talkObjName:B_dlws-msg:怎么了-choseType:dan-id:1-nextId:2|" +
        "acType:talk-talkObjName:player-msg:没事#9^event$dontRemove*0@我需要点亮神树#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
        "acType:talk-talkObjName:B_dlws-msg:好吧。。。^event$over-choseType:dan-id:3-nextId:6";

    string ci2PlotStr1r = "acType:talk-talkObjName:B_dlws-msg:どうしたの-choseType:dan-id:1-nextId:2|" +
       "acType:talk-talkObjName:player-msg:大丈夫#9^event$dontRemove*0@神の木を照らす必要があります#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
       "acType:talk-talkObjName:B_dlws-msg:わかった。 。 。^event$over-choseType:dan-id:3-nextId:6";

    string ci2PlotStr1y = "acType:talk-talkObjName:B_dlws-msg:What's up-choseType:dan-id:1-nextId:2|" +
      "acType:talk-talkObjName:player-msg:fine#9^event$dontRemove*0@I need to light up the god tree#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
      "acType:talk-talkObjName:B_dlws-msg:All right. . .^event$over-choseType:dan-id:3-nextId:6";

    string ci2PlotStr1x = "acType:talk-talkObjName:B_dlws-msg:Que pasa-choseType:dan-id:1-nextId:2|" +
      "acType:talk-talkObjName:player-msg:multa#9^event$dontRemove*0@Necesito encender el árbol de dios#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
      "acType:talk-talkObjName:B_dlws-msg:Todo bien. . .^event$over-choseType:dan-id:3-nextId:6";

    string ci2PlotStr1h = "acType:talk-talkObjName:B_dlws-msg:무슨 일이야-choseType:dan-id:1-nextId:2|" +
      "acType:talk-talkObjName:player-msg:좋아#9^event$dontRemove*0@신의 나무에 불을 켜야 해요#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
      "acType:talk-talkObjName:B_dlws-msg:괜찮아. . .^event$over-choseType:dan-id:3-nextId:6";

    string ci2PlotStr1zf = "acType:talk-talkObjName:B_dlws-msg:怎麼了-choseType:dan-id:1-nextId:2|" +
       "acType:talk-talkObjName:player-msg:沒事#9^event$dontRemove*0@我需要點亮神樹#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
       "acType:talk-talkObjName:B_dlws-msg:好吧。。。^event$over-choseType:dan-id:3-nextId:6";
    string ci2PlotStr1d = "acType:talk-talkObjName:B_dlws-msg:Was ist falsch-choseType:dan-id:1-nextId:2|" +
       "acType:talk-talkObjName:player-msg:fein#9^event$dontRemove*0@Ich muss den Götterbaum anzünden#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
       "acType:talk-talkObjName:B_dlws-msg:In Ordnung。。。^event$over-choseType:dan-id:3-nextId:6";
    string ci2PlotStr1f = "acType:talk-talkObjName:B_dlws-msg:Qu'est ce qui ne va pas-choseType:dan-id:1-nextId:2|" +
       "acType:talk-talkObjName:player-msg:amende#9^event$dontRemove*0@J'ai besoin d'allumer l'arbre divin#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
       "acType:talk-talkObjName:B_dlws-msg:D'accord。。。^event$over-choseType:dan-id:3-nextId:6";
    string ci2PlotStr1i = "acType:talk-talkObjName:B_dlws-msg:Cosa c'è che non va-choseType:dan-id:1-nextId:2|" +
       "acType:talk-talkObjName:player-msg:bene#9^event$dontRemove*0@Ho bisogno di illuminare l'albero di Dio#3^event$jianding*0-choseType:duo-id:2-nextId:3|" +
       "acType:talk-talkObjName:B_dlws-msg:Ok。。。^event$over-choseType:dan-id:3-nextId:6";




    //id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:我等你好久了，你准备好挑战我了吗？|id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:来吧！#4@不想做无谓的战斗。#3|id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:哼！等你变强再来找我吧 。^event$openDoor|id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:哈哈！正合我意，我上了。^event$startfight|

    string plot_id_2a = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:我等你好久了，你准备好挑战我了吗？|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:不要挡我的路！#4@不想做无谓的战斗。#3|" +
        "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:等你变强再来找我吧 。^event$openDoor|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:正合我意，我上了。^event$startfight|";

    string plot_id_2ar = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:私は長い間あなたを待っていました、あなたは私に挑戦する準備ができていますか？|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:邪魔しないで！#4@無意味な戦いはしたくない。#3|" +
        "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:あなたが強くなったときに私に戻ってきてください。^event$openDoor|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:まさに私が欲しいもの、私は上にいます。^event$startfight|";

    string plot_id_2ay = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:I've been waiting for you for a long time, are you ready to challenge me?|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Don't get in my way!#4@I don't want to do pointless battles.#3|" +
        "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:Come back to me when you get stronger.^event$openDoor|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Exactly what I want, I'm on.^event$startfight|";

    string plot_id_2ax = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Te he estado esperando durante mucho tiempo, ¿estás listo para desafiarme?|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:¡No te interpongas en mi camino!#4@No quiero hacer batallas sin sentido.#3|" +
        "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:Vuelve a mí cuando seas más fuerte.^event$openDoor|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Exactamente lo que quiero, estoy en.^event$startfight|";

    string plot_id_2ah = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:나는 오랫동안 당신을 기다리고 있었는데, 당신은 나에게 도전 할 준비가 되셨습니까?|" +
        "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:방해하지마!#4@무의미한 싸움은 하고 싶지 않습니다.#3|" +
        "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:더 강해지면 내게 돌아와.^event$openDoor|" +
        "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:내가 원하는 바로 그거야.^event$startfight|";

    string plot_id_2azf = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:我等你好久了，你準備好挑戰我了嗎？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:不要擋我的路！#4@不想做無謂的戰鬥。#3|" +
       "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:等你變強再來找我吧 。^event$openDoor|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:正合我意，我上了。^event$startfight|";
    string plot_id_2ad = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Ich habe lange auf dich gewartet, bist du bereit, mich herauszufordern?|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:komm mir nicht in die Quere！#4@Ich will keinen sinnlosen Kampf führen。#3|" +
       "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:Komm zu mir, wenn du stärker wirst 。^event$openDoor|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Genau das, was ich will, ich bin oben。^event$startfight|";
    string plot_id_2af = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Je t'attendais depuis longtemps, es tu prêt à me défier ?|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:ne te mets pas en travers de mon chemin！#4@Je ne veux pas faire un combat inutile。#3|" +
       "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:Viens à moi quand tu deviens plus fort 。^event$openDoor|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Exactement ce que je veux, je suis debout。^event$startfight|";
    string plot_id_2ai = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Ti aspetto da tanto tempo, sei pronto a sfidarmi?|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:non intralciarmi！#4@Non voglio fare una lotta inutile。#3|" +
       "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:B_dlws-msg:Vieni da me quando diventerai più forte 。^event$openDoor|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:B_dlws-msg:Esattamente quello che voglio, sono sveglio。^event$startfight|";












    //id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:力量已经汇聚完成了，流刃之火。好好利用。

    string plot_id_2b = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:这是流刃若火 好好利用";
    string plot_id_2br = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:それは火のようです、それを上手に使ってください";
    string plot_id_2by = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:It's like fire, use it well";
    string plot_id_2bx = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Es como el fuego, úsalo bien.";
    string plot_id_2bh = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:불 같아 잘 활용해";

    string plot_id_2bzf = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:這是流刃若火 好好利用";
    string plot_id_2bd = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:Es ist wie Feuer, benutze es gut";
    string plot_id_2bf = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:C'est comme le feu, utilise le bien";
    string plot_id_2bi = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:B_dlws-msg:È come il fuoco, usalo bene";




    //G3给乱刃
    //真的 羡慕你每次回来 设么都不记得了
    // q  。。。。。。
    //这是 乱刃之舞 给你吧
    //每次我都把最好的给你  怎么样 见到我开心吗
    //q 。。。。。   开心吧   开心
    //    。。。。   好吧    是吗，后面再见。

    string plot_id_3 = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:羡慕你什么都不记得了|" +
        "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
        "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:这是 乱刃之舞 给你";

    string plot_id_3r = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:戻ってくるたびに本当にうらやましいです何も覚えていません|" +
        "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
        "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:これはあなたのためのブレードのダンスです";

    string plot_id_3y = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:I really envy you every time you come back, you don't remember anything|" +
        "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
        "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:This is Dance of the Blades for you";

    string plot_id_3x = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Realmente te envidio cada vez que vuelves, no recuerdas nada.|" +
        "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
        "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:Esto es Dance of the Blades para ti";

    string plot_id_3h = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:네가 돌아올 때마다 정말 부럽다 넌 아무것도 기억하지 못해|" +
        "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
        "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:이것은 당신을 위한 칼날의 춤입니다";

    string plot_id_3zf = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:羨慕你什麼都不記得了|" +
       "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
       "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:這是 亂刃之舞 給你";
    string plot_id_3d = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Ich beneide dich, dass du dich an nichts erinnerst|" +
       "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
       "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:Das ist Tanz der Klingen für Sie";
    string plot_id_3f = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Je t'envie de ne te souvenir de rien|" +
       "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
       "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:C'est la danse des lames pour vous";
    string plot_id_3i = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Ti invidio per non ricordare nulla|" +
       "id:2-choseType:dan-nextId:3-acType:talk-talkObjName:player-msg:。。。。。。|" +
       "id:3-choseType:dan-nextId:9-acType:talk-talkObjName:H_np-msg:Questa è la Danza delle Lame per te";








    //G6 给护盾
    //徘徊在这里是神之手，有信心打败神之手吗？
    //q有 没有
    //是吗，你总是那么自信     不像你
    //这个徽章可以很好的对抗 神之手
    string plot_id_6 = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:徘徊在这里是神之手，有信心打败神之手吗？|" +
       "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:有#3@没有#4|" +
       "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:是吗，你总是那么自信|" +
       "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:不像你。。。|" +
       "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:这个徽章给你，希望能助你一臂之力|";

    string plot_id_6r = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:ここをさまようのは神の手です、あなたは神の手を打ち負かす自信がありますか？|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:持ってる#3@いいえ#4|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:あなたはいつもとても自信がありますか|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:あなたのようではない。 。 。 。|" +
      "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:このバッジはあなたのためのものです、私はそれがあなたを助けることができることを願っています|";

    string plot_id_6y = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Wandering here is the hand of God, do you have the confidence to defeat the hand of God?|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Have confidence#3@no#4|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:are you always so confident|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:Not like you. . . .|" +
      "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:This badge is for you, I hope it can help you|";

    string plot_id_6x = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Aquí vagando está la mano de Dios, ¿tienes la confianza para vencer la mano de Dios?|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Ten confianza#3@no#4|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:¿Siempre estás tan seguro?|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:No como tu. . . .|" +
      "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:Esta insignia es para ti, espero que te pueda ayudar|";

    string plot_id_6h = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:여기 방황하는 것은 신의 손인데, 신의 손을 이길 자신이 있습니까?|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:자신감을 가지고#3@아니요#4|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:당신은 항상 그렇게 자신감이 있습니까|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:당신처럼하지 않습니다. . . .|" +
      "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:이 배지는 당신을 위한 것입니다. 도움이 되었으면 합니다.|";

    string plot_id_6zf = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:徘徊在這裡是神之手，有信心打敗神之手嗎？|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:有#3@沒有#4|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:是嗎，你總是那麼自信|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:不像你。。。|" +
      "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:這個徽章給你，希望能助你一臂之力|";
    string plot_id_6d = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Hier wandert die Hand Gottes, hast du das Vertrauen, die Hand Gottes zu besiegen?|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Haben#3@Nein#4|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:Bist du immer so zuversichtlich|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:Nicht wie du. . .|" +
      "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:Dieses Abzeichen ist für Sie, ich hoffe, es kann Ihnen helfen|";
    string plot_id_6f = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Errant ici est la main de Dieu, avez-vous la confiance nécessaire pour vaincre la main de Dieu ?|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Avoir#3@Non#4|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:es tu toujours aussi confiant|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:Pas comme vous. . .|" +
      "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:Ce badge est pour vous, j'espère qu'il pourra vous aider|";
    string plot_id_6i = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Vagare qui è la mano di Dio, hai la fiducia per sconfiggere la mano di Dio?|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Avere#3@No#4|" +
      "id:3-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:sei sempre così sicuro di te?|" +
      "id:4-choseType:dan-nextId:5-acType:talk-talkObjName:H_np-msg:Non come te. . .|" +
      "id:5-choseType:dan-nextId:8-acType:talk-talkObjName:H_np-msg:Questo badge è per te, spero che possa aiutarti|";








    //G6 机甲前的对话
    //后面是人类的战争遗产，不知道你喜不喜欢，不过还蛮好玩的。
    //你每次都要来重启世界，人类却一次又一次自我毁灭。值得吗。
    //Q 这就是我的使命
    //好吧，好像了解为什么你什么都不会记得的原因了。
    //但是为什么神明不让我也从新来过呢？
    //Q。。。。。。。。
    //你飞过去后，估计就见不到我了，你会想我么。
    //Q会的      。。。。。。
    //真的吗    。。。。。。。
    //Q真的
    //真好 我不会忘记你的。再过一万年我还会等你
    //。。。。

    //如果有机会重来一次，你觉得会不一样吗？
    //会 不会 不知道
    //是吗 我也觉得 好吧
    //后面是人类的战争遗产,喜欢的话下次跟你多留一点




    string plot_id_6a = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:如果有机会重来一次，你觉得会不一样吗？|" +
      "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:会不一样#3@就算重来也不会改变什么的#4@不知道#5|" +
      "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:是吗。|" +
      "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:我也觉得|" +
      "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:好吧|"+
      "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:后面的机甲是人类的战争遗产,喜欢的话下次跟你多留一点|";

    string plot_id_6ar = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:もう一度やり直す機会があったら、違うと思いますか？|" +
     "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:異なります#3@戻っても何も変わらない#4@わかりません#5|" +
     "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:はい？|" +
     "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:私もそう感じます|" +
     "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:Ok|" +
     "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:後ろのメカは人類の戦争遺産です。よろしければ、次回はもう少しお任せします。|";

    string plot_id_6ay = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:If you had the chance to do it all over again, do you think it would be different?|" +
     "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:will be different#3@It won't change anything even if it comes back#4@do not know#5|" +
     "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:yes?|" +
     "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:I feel so too|" +
     "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:Ok|" +
     "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:The mecha at the back is the war legacy of mankind. If you like it, I will leave a little more with you next time.|";

    string plot_id_6ax = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Si tuvieras la oportunidad de hacerlo todo de nuevo, ¿crees que sería diferente?|" +
     "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:Será diferente#3@No cambiará nada aunque vuelva#4@No lo sé#5|" +
     "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:¿sí?|" +
     "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:yo también lo siento|" +
     "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:De acuerdo|" +
     "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:El mecha en la parte posterior es el legado de guerra de la humanidad. Si te gusta, te dejaré un poco más la próxima vez.|";

    string plot_id_6ah = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:다시 할 수 있는 기회가 주어진다면 다를 것 같나요?|" +
     "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:다를 것이다#3@돌아와도 달라지는 건 없어#4@몰라요#5|" +
     "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:네?|" +
     "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:나도 그렇게 느낀다|" +
     "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:확인|" +
     "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:뒤에 있는 메카는 인류의 전쟁 유산인데 마음에 드셨다면 다음번에는 조금 더 남기고 가겠습니다.|";

    string plot_id_6azf = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:如果有機會重來一次，你覺得會不一樣嗎？|" +
    "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:會不一樣#3@就算重來也不會改變什麼的#4@不知道#5|" +
    "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:是嗎。|" +
    "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:我也覺得|" +
    "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:好吧|" +
    "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:後面的機甲是人類的戰爭遺產,喜歡的話下次跟你多留一點|";

    string plot_id_6ad = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Wenn du die Möglichkeit hättest, alles noch einmal zu machen, denkst du, es wäre anders?|" +
    "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:wird anders sein#3@Es wird nichts ändern, auch wenn es wiederkommt#4@weiß nicht#5|" +
    "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:ja?|" +
    "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:Ich fühle mich auch so|" +
    "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:In Ordnung|" +
    "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:Der Mecha auf der Rückseite ist das Kriegserbe der Menschheit. Wenn es dir gefällt, lasse ich beim nächsten Mal ein bisschen mehr bei dir.|";

    string plot_id_6af = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Si vous aviez la chance de tout recommencer, pensez vous que ce serait différent ?|" +
     "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:sera différent#3@Ça ne changera rien même si ça revient#4@ne sait pas#5|" +
     "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:oui?|" +
     "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:je me sens aussi|" +
     "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:D'accord|" +
     "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:Le mecha à l'arrière est l'héritage de guerre de l'humanité. Si vous l'aimez, je vous en laisserai un peu plus la prochaine fois.|";

    string plot_id_6ai = "id:1-choseType:dan-nextId:2-acType:talk-talkObjName:H_np-msg:Se avessi la possibilità di rifare tutto da capo, pensi che sarebbe diverso?|" +
     "id:2-choseType:duo-nextId:3-acType:talk-talkObjName:player-msg:sarà diverso#3@Non cambierà nulla anche se ritorna#4@non lo so#5|" +
     "id:3-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:sì?|" +
     "id:4-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:Lo sento anch'io|" +
     "id:5-choseType:dan-nextId:6-acType:talk-talkObjName:H_np-msg:Ok|" +
     "id:6-choseType:dan-nextId:7-acType:talk-talkObjName:H_np-msg:Il mecha sul retro è l'eredità bellica dell'umanità. Se ti piace, la prossima volta lascerò un po' di più con te.|";





    //最后的剧情是什么
    //打完龙之后 湮灭？   进入湮灭对话
    //终于 一切都结束了



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
        print("数据 ------ choseEventName    " + e.eventParams.ToString());

        //加一个 不删除 自身的 事件
        string eventName = ChoseEventStr.Split('*')[0];


        if (eventName == "openDoor")
        {
            if (NPC) NPC.GetComponent<RoleDate>().isCanBeHit = true;
        }

        if (eventName == "startfight")
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEW_OPEN_DOOR, "close"), this);
            if (NPC) {
                NPC.GetComponent<RoleDate>().isCanBeHit = true;
                if (NPC.GetComponent<Plot_enemy>())
                {
                    NPC.GetComponent<Plot_enemy>().ShowHitKuai();
                }
            }
        }


        //event$dontRemove
        if (eventName == "dontRemove")
        {
            IsOverMoveSelf = false;
            IsCanBeHit = false;
            IsNeedRecordByOver = false;
            return;
        }

        //记录关卡 剧情 记录的 事件
        if (eventName == "jianding")
        {
            if(ChoseEventStr.Split('*')[1] == "1")
            {
                //二次不会碰撞 不能消失
                IsOverMoveSelf = false;
                IsCanBeHit = false;
                IsNeedRecordByOver = false;
                //选择的 是坚定
                //存入全局数据 是坚定  无法显示 跳跃关卡 存档点

                
                print("数据 选择的 是 坚定！！！！！！   选的 是徽章 ");
            }
            else
            {
                //选择的是 不坚定  现在 谈话结束后 可以消失
                print("数据 选择的是 不 坚定！！   选择神树！！！");
            }
            GlobalSetDate.instance.SaveInOtherDate(ChoseEventStr);
        }
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
        print("数据 检测剧情是否已经播过！！！！   " + PlotID);
        return GlobalDateControl.IsHasDateByName(PlotID);
    }

    //NPC朝向
    public GameObject NPC;
    void NPCChaoxiang()
    {
        if (NPC == null) return;
        if (NPC.transform.position.x >= player.transform.position.x)
        {
            NPC.transform.localScale = new Vector2(NPC.transform.localScale.x, NPC.transform.localScale.y);
        }
        else
        {
            NPC.transform.localScale = new Vector2(-NPC.transform.localScale.x, NPC.transform.localScale.y);
        }
    }




    bool IsCanBeHit = true;
    bool IsStartPlot = false;
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (!IsCanBeHit) return;
        if (Coll.tag == "Player")
        {
            print(" 玩家 碰撞到 剧情块！！！ ");
            player = Coll.gameObject;
            //判断 剧情是否已经播过 播过的话就 不进入了
            //if (IsPlotHasPlayed()) return;

            NPCChaoxiang();
            PlayerHitStop();
            GetStr();
            //ShowPlotStr();

            IsStartPlot = true;
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
        PLOTSTR = "";
        //print("剧情内容>>>>>>>>: "+ plotStr);
        string[] strArr = PLOTSTR.Split('|');
        plotStrList = new List<string>(strArr);//strArr.ToList();
    }

    void OnGUI()
    {
        if (Globals.isDebug)
        {
            GUI.TextArea(new Rect(0, 20, 250, 40), "PLOTSTR  ID : " + PlotID);//使用GUI在屏幕上面实时打印当前按下的按键
            //Zhenshu();
        }

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
            //print("  内容 "+_msg);
            if (key == "choseType") _choseType = theMsg;
            if (key == "nextId") _nextId = int.Parse(theMsg);
        }
    }


    public bool IsBarGensui = false;
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

        Transform talkPos;
        if (talkObj)
        {
            print("talkObj name   "+ talkObj.name);
            _talkPos = talkObj.GetComponent<GameBody>().GetTalkPos();
            talkPos = talkObj.GetComponent<GameBody>().TalkPos.transform;
        }
        else
        {
            _talkPos = this.TalkPosObj.transform.position;
            talkPos = this.TalkPosObj.transform;
        }

        if (IsBarGensui)
        {
            _cBar.GetComponent<UI_talkBar>().ShowTalkText(_msg, _talkPos, times, _talkObjName, talkPos);
        }
        else
        {
            _cBar.GetComponent<UI_talkBar>().ShowTalkText(_msg, _talkPos, times, _talkObjName);
        }

        
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
        print("剧情 没有文本了！！！！ 结束剧情");
        //结束剧情
        ClickJishiReSet();
        if (IsNeedRecordByOver)
        {
            GlobalDateControl.SetMsgInCurrentGKDateAndSetInZGKDate(this.PlotID,IsNeedRecordByOver);
        }
        HuanYuanCamera();
        if (_cBar) _cBar.GetComponent<UI_talkBar>().RemoveSelf();


        if (!IsOverMoveSelf) {
            StartCoroutine(SetPlotFalse(0.4f,true));
            return;
        }
        


        StartCoroutine(SetPlotFalse(0.4f));
    }


  




    public IEnumerator SetPlotFalse(float time,bool IsDontRemoveSelf = false)
    {
        print("yanchijin ru duihua  jieshu  de !! "+ ChoseEventStr);
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        print(" ChoseEventStr  "+ ChoseEventStr);
        if (IsDontRemoveSelf)
        {
            print("---------------------------------------------------------->>>>>>>不能够移除自身 ");
            Globals.isInPlot = false;
            IsCanBeHit = false;
        }
        else
        {
            Globals.isInPlot = false;
            print("    ??????? ");
            OverOutObj();
            print("是否有 CHOSE_EVENT 事件 " + ChoseEventStr);
            if (ChoseEventStr == "over"|| ChoseEventStr == "openDoor" || ChoseEventStr == "startfight")
            {
                //存档 数据 存入哪些东西
                PlotOverSave();
            }

            IsCanBeHit = false;

            if (IsOverMoveSelf)
            {
                OverDelSelf();
            }
        }


       

        //Destroy(this.gameObject);
    }





    [Header("对话结束 需要存入的数组吧")]
    public string[] OverCunRuShuJuArr;
    void PlotOverSave()
    {

        print("对话 存档是否 被调用 ！！！！！！！！！！！！");

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


        //是否出现啥 徽章  或者爆炸 等


        //if (IsOverMoveSelf)
        //{
        //    OverDelSelf();
        //}

        //能不能延迟 才能使玩家 行动

    }


    void KillPlayerOver(UEvent e)
    {
        print("  KillPlayerOver 这里进来了吗？？？？？？？？   ");
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
            //Globals.isInPlot = false;
        }



        if (!Globals.isInPlot) return;
        
        
        
        if (Input.GetKeyDown(KeyCode.J)|| Input.GetKeyDown(KeyCode.Joystick1Button1))
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
    //拉近摄像机
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
