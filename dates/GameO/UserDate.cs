using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDate{

    public string userName;         //用户名
    public int onlyId;
    //public Dictionary<string, string> atk_2 = new Dictionary<string, string> { { "atkName", "atk_2" }, { "xF", "300" }, { "yF", "100" }, { "showTXFrame", "9" }, { "txName", "dg_002" }, { "ox", "10" }, { "oy", "8" }, { "yanchi", "20" }, { "atkDistance", "6" } };
    //玩家位置
    public string playerPosition;
    public string cameraPosition;
    //背包数据记录
    public string bagDate = "huizhang1_0-huizhang2_2";
    //记录血瓶数量
    public int xp_nums = 0;
    //装备栏记录  id_num
    //public string zblIds;
    //生命
    public string curLive;
    //蓝
    public string curLan;
    //关卡记录  关卡名guan1_1  是否开门 men_1-0(1)  控制数组是否使用 csz_1-0(1)  宝箱是否打开bx_1-0  隐藏点是否探索 ts_1-0
    // guan1_1:men_1-0,men_2-0,csz_1-0,bx_1-0,ts_1-0=guan1_2:men_1-0,csz_1-0|
    public string guankajilu;
    //小地图数据
    public string mapDate;
    //存档时间
    public string saveTime;
    //是否是最新存档
    //public bool isNewSaveDate = false;
    //地图名字
    public string screenName;
   
    public UserDate()
    {
        
    }
}
