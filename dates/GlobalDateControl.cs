using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDateControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //哪几种 情况要存档  1.捡到徽章 2.战胜boss  3.门怪打死后  4.存档点    最简单的办法啊就是价格存档点



    //static string saveDateName = "myGame";
    //static string TempZGuanKaStr = "";


    //匹配存档中的全局关卡记录
    public static void GetGameAllCunstomStr()
    {
        //获取当前关卡的数据

        //是否有存档
        if (GameSaveDate.GetInstance().IsHasMapSaveDate())
        {
            //if (Globals.isDebug) print("!!!!!!!!!!!!!!!!!!有地图数据存档记录");
            //找到总的关卡记录
            GlobalSetDate.instance.CurrentUserDate = GameSaveDate.GetInstance().GetSaveDateByName(GlobalSetDate.instance.saveDateName);
            //print("  背包数据:  "+ GlobalSetDate.instance.CurrentUserDate.bagDate);
            
            if (GameSaveDate.GetInstance().IsHasSaveDateByName(GlobalSetDate.instance.saveDateName + "Map"))
            {
                //print(GameSaveDate.GetInstance().GetSaveDateByName(CurrentSaveDateName));
                GlobalSetDate.instance.CurrentMapMsgDate = GameSaveDate.GetInstance().GetSaveDateByName(GlobalSetDate.instance.saveDateName + "Map");
                GlobalSetDate.instance.TempZGuanKaStr = GlobalSetDate.instance.CurrentMapMsgDate.mapDate;
                print("TempZGuanKaStr  " + GlobalSetDate.instance.TempZGuanKaStr);
                //print(" map数据中bag数据 "+ GlobalSetDate.instance.CurrentMapMsgDate.bagDate);
                //print("  当前场景背包数据：  "+ GlobalSetDate.instance.bagDate);
                //if(GlobalSetDate.instance.TempZGuanKaStr!="") GlobalSetDate.instance.HowToInGame = "LoadGame";
            }
            else
            {
                //print("没有当前存档的记录");
            }

        }
        else
        {
            if (Globals.isDebug) print("没有地图数据存档记录");
            //记录当前关卡的记录  这里一般是新开游戏
            //TempZGuanKaStr = "";
        }
        //获取存档的关卡记录
    }





    //对话对全局 影响

    //获取 当前关卡场景 的名字
    public static string GetCGKName()
    {
        string guankaName = SceneManager.GetActiveScene().name;
        if(guankaName == "RMap_1"|| guankaName == "RMap_2")
        {
            guankaName = "R@"+GlobalSetDate.instance.CReMapName;
        }

        return guankaName;
    }

    


    // 关卡和关卡 “|” 分割    关卡内的内容 “，”分割
    //GKName ng0_ - 2  TempZGuanKaStr ng1_-1:,WP_huizhang12 - 0 | ng1_ - 1:,WP_huizhang12 - 0 | ng1_ - 1:,WP_huizhang12 - 0 | ng1_ - 2:,G_jydj | ng1_ - 2:,G_jydj | ng1_ - 2:,G_jydj,WP_huizhang4 - 1@21.09539#-4.326621|ng1_-2:,G_jydj,WP_huizhang4-1@21.09539#-4.326621|ng1_-2:,G_jydj,WP_huizhang4-0|ng1_-2:,G_jydj,WP_huizhang4-0|ng0_-2:|
    //获取全局数据
    public static string GetZGKDate()
    {
        return GlobalSetDate.instance.TempZGuanKaStr;
        //return GameSaveDate.GetInstance().GetSaveDateByName(GlobalSetDate.instance.saveDateName).mapDate; ;
    }


    //根据  查询的  keyName 来查询  当前关卡数据  中  是否有  相应key的数据
    public static bool IsHasDateByName(string KeyName)
    {
        string mapMsg = GetCurrentGKDate();
        //print(" mapMsg "+ (mapMsg == null)+"    ????? "+mapMsg+"   查询的 key是什么  "+KeyName);
        //if (mapMsg != null && mapMsg == "") mapMsg = GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetTempCGKDate();
        if (mapMsg == null|| mapMsg == "") return false;

        string GKName = mapMsg.Split(':')[0];
        string GKMsg = mapMsg.Split(':')[1];


        string CGKDateMsg = mapMsg.Split(':')[1];
        string[] dateArr = CGKDateMsg.Split(',');

        for(int i=0;i< dateArr.Length; i++)
        {
            string checkName = dateArr[i].Split('-')[0];
            //print("zhidui----->  " + checkName);
            if (checkName == KeyName) return true;
        }

        return false;
    }


    // ng1_ - 1:,WP_huizhang12 - 0 | 
    //获取 当前关卡的 全局数据
    public static string GetCurrentGKDate()
    {
        string CurrentGKDate = "";
        CurrentGKDate = GetCGKName() + ":" + GetGKDateByName(GetCGKName());
        //print("  获得 当前 关卡数据  " + CurrentGKDate);
        return CurrentGKDate;
    }

    public static string GetGKDateByName(string GKName)
    {
        if (GlobalSetDate.instance.TempZGuanKaStr == null) {
            GlobalSetDate.instance.TempZGuanKaStr = GKName + ":|";
            return "";
        }
        
        string currentGKDate = null;
        string[] arr = GlobalSetDate.instance.TempZGuanKaStr.Split('|');

        for (var i = 0; i < arr.Length; i++)
        {
            string[] arr2 = arr[i].Split(':');
            string curGKName = arr2[0];
            //if (Globals.isDebug) print("curGKName  "+ curGKName);
            if (curGKName == GKName)
            {
                currentGKDate = arr2[1];
                return currentGKDate;
                //if (Globals.isDebug) print("当前关卡的数据  "+ currentGKDate);
            }
        }
        return "";
    }


    //存入数据 先写入 当前关卡数据  再存入 全局数据
    public static void SetMsgInCurrentGKDateAndSetInZGKDate(string msg,bool IsNeedSave = false,bool IsNeedSaveAllDate = false)
    {
        //获取当前 关卡 数据信息
        string CGKMsg = GetCurrentGKDate();
        //print("-CurrentGKMsg------------>   " + CGKMsg);
        //print("要存入的信息-msg------------>   " + msg+"  是否需要保存！ "+ IsNeedSave);
        if (CGKMsg == null) return;
        //将要写入的 信息 写入   name-zt1@zt2@zt3  名字-状态1@状态2@状态3
        //CGKMsg += ","+msg;
        //信息比对 规则？？
        string msgName = msg.Split('-')[0];
        //print("msgName "+ msgName);//qishi


        string CGKDateName = CGKMsg.Split(':')[0];
        string CGKDateMsg = CGKMsg.Split(':')[1];

        string[] dateArr = CGKDateMsg.Split(',');
        string newCurrentDateMsg = "";
        bool IsHasMsg = false;
        for(int i = 0; i < dateArr.Length; i++)
        {
            //print("进来过没  i   "+i);
            if(dateArr[i].Split('-')[0] == msgName)
            {
                if (i != 0)
                {
                    newCurrentDateMsg += "," + msg;
                }
                else
                {
                    newCurrentDateMsg += msg;
                }
                IsHasMsg = true;
            }
            else
            {
                if (i != 0)
                {
                    newCurrentDateMsg += "," + dateArr[i];
                }
                else
                {
                    newCurrentDateMsg +=  dateArr[i];
                }
            }
        }

        //print("newCurrentDateMsg   "+ newCurrentDateMsg);

        if (newCurrentDateMsg == "") {
            newCurrentDateMsg = msg;
        }
        else
        {
            if (!IsHasMsg) newCurrentDateMsg += "," + msg;
        }
        

        CGKMsg = CGKDateName + ":" + newCurrentDateMsg;
        //print("CGKMsg 写入数据是什么？ >>>     "+ CGKMsg);

        //GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().SetTempCurrentGKDate(newDateMsg);

        //写入全局数据
        SetCurrentGKDateInZGKTempDate(CGKMsg);
        //这里要写入 总数据


        if(IsNeedSave) SaveMapDate();
        if (IsNeedSaveAllDate) GlobalSetDate.instance.GetSave();

        //print(" 写入 全局数据后的  全局数据  "+ GlobalSetDate.instance.TempZGuanKaStr);

    }



    //将当前关卡数据 加到总关卡数据中
    public static void SetCurrentGKDateInZGKTempDate(string CurrentGKDate)
    {
        //print(" 传入的 参数CurrentGKDate    "+ CurrentGKDate);
        RemoveCurrentGKDateFromZGKDateByCurrentGKName(CurrentGKDate.Split(':')[0]);
        //print("************************************移除记录后的 数据   TempZGuanKaStr "+ GlobalSetDate.instance.TempZGuanKaStr);

        GlobalSetDate.instance.TempZGuanKaStr += CurrentGKDate + "|";

        //if (Globals.isDebug) print("将当前关卡数据 加入到总关卡数据---  加完后的全局数据！！！ " + GlobalSetDate.instance.TempZGuanKaStr);
    }



    //获取关卡名的关卡数据 并且在原数据中删除
    public static void RemoveCurrentGKDateFromZGKDateByCurrentGKName(string CurrentGKName)
    {
        if (GlobalSetDate.instance.TempZGuanKaStr == null|| GlobalSetDate.instance.TempZGuanKaStr == "") return;
        //GKName ng0_ - 2  TempZGuanKaStr ng1_-1:,WP_huizhang12 - 0 | ng1_ - 1:,WP_huizhang12 - 0 | ng1_ - 1:,WP_huizhang12 - 0 | ng1_ - 2:,G_jydj | ng1_ - 2:,G_jydj | ng1_ - 2:,G_jydj,WP_huizhang4 - 1@21.09539#-4.326621|ng1_-2:,G_jydj,WP_huizhang4-1@21.09539#-4.326621|ng1_-2:,G_jydj,WP_huizhang4-0|ng1_-2:,G_jydj,WP_huizhang4-0|ng0_-2:|
        // |g1_1:men_1-1,men_2-1|
        string gkStr = "";
        string[] arr = GlobalSetDate.instance.TempZGuanKaStr.Split('|');
        for (var i = 0; i < arr.Length; i++)
        {
            string[] arr2 = arr[i].Split(':');
            string curGKName = arr2[0];
            //if (Globals.isDebug) print("curGKName  "+ curGKName);
            if (curGKName != CurrentGKName)
            {
                if (arr[i] != "") gkStr += arr[i] + '|';
            }
        }

        GlobalSetDate.instance.TempZGuanKaStr = gkStr;
    }


    public static void SaveMapDate()
    {
        //print("SaveMapDate! ");
        if (GlobalSetDate.instance.CurrentMapMsgDate == null) GlobalSetDate.instance.CurrentMapMsgDate = new UserDate();
        GlobalSetDate.instance.CurrentMapMsgDate.mapDate = GetZGKDate();
        if (GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1") != null) {
            GlobalSetDate.instance.CurrentMapMsgDate.bagDate = GlobalTools.FindObjByName("UI_Bag(Clone)/mianban1").GetComponent<Mianban1>().HZSaveDate();
            //print("存入背包的数据 :   " + GlobalSetDate.instance.CurrentMapMsgDate.bagDate);
        }
        
        //print(" SaveMapDate :     "+ GlobalSetDate.instance.CurrentMapMsgDate.mapDate);
        
        GameSaveDate.GetInstance().SaveDateByURLName(GlobalSetDate.instance.saveDateName + "Map", GlobalSetDate.instance.CurrentMapMsgDate);

    }



}
