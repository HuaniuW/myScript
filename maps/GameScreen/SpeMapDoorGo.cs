﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeMapDoorGo : MonoBehaviour
{
    //根据 地图的 信息 获取 门 要去的方向
    // Start is called before the first frame update
    void Start()
    {
        GetDoorSet();
    }

    public string ThisMapName = "";

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> DoorList = new List<GameObject> { };

    public void GetDoorSet()
    {
        print("设置门的 信息");
        //在特殊数据中找到自己
        print(GlobalSetDate.instance.CReMapName);
        //配置 各个方向的 地图
        //map_r+map_r-1!0#0!r:map_r-2^u:map_r-3|map_r-2!1#0!r:map_r-4@map_u+map_u-1!0#0!r:map_u-2|map_u-2!1#0!map_u-3
        print(GlobalSetDate.instance.gameMapDate.BigMapDate);

        //map_r-1!0#0!r:map_r-2^u:map_r-3
        string mapMsgStr = GetMapStr();
        string[] mapStrArr = mapMsgStr.Split('!')[2].Split('^');


        //r:map_r-2
        foreach (string s in mapStrArr)
        {
            foreach(GameObject o in DoorList)
            {
                print("地图门的位置-------------------------------------------------------->>>>>>  "+ o.GetComponent<ScreenChange>().DangQianMenWeizhi+"   数据中门的位置：？？  "+ s.Split(':')[0]+"   s "+s);
                if(o.GetComponent<ScreenChange>().DangQianMenWeizhi == s.Split(':')[0])
                {
                    o.GetComponent<ScreenChange>().SetMenMsg(s.Split(':')[0], s.Split(':')[1]);
                }
            }
        }

        string InDoorFX = "";
        //玩家位置设定
        foreach (GameObject o in DoorList)
        {
            if(GlobalSetDate.instance.DanqianMenweizhi == "l")
            {
                InDoorFX = "r";
            }
            else if (GlobalSetDate.instance.DanqianMenweizhi == "r")
            {
                InDoorFX = "l";
            }else if (GlobalSetDate.instance.DanqianMenweizhi == "u")
            {
                InDoorFX = "d";
            }else if (GlobalSetDate.instance.DanqianMenweizhi == "d")
            {
                InDoorFX = "u";
            }



            if (o.GetComponent<ScreenChange>().DangQianMenWeizhi == InDoorFX)
            {
                print("  特殊地图中处理玩家位置  ");
                GlobalTools.FindObjByName("player").transform.position = o.GetComponent<ScreenChange>().OutPosition.position;
                GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().SetPlayerPos();
            }
        }

    }


    string GetMapStr() {
        //map_r+map_r-1!0#0!r:map_r-2^u:map_r-3|map_r-2!1#0!r:map_r-4@map_u+map_u-1!0#0!r:map_u-2|map_u-2!1#0!map_u-3
        string mapTou = ThisMapName.Split('-')[0];
        print("当前的 关卡 名字是  "+ ThisMapName);
        print("总的 关卡 地图信息   "+ GlobalSetDate.instance.gameMapDate.BigMapDate);


        print("全局记录的 当前门位置  "+GlobalSetDate.instance.DanqianMenweizhi);

        string[] DaMapArrStr = GlobalSetDate.instance.gameMapDate.BigMapDate.Split('@');
        string CurrentDaMapStr = "";
        foreach(string st in DaMapArrStr)
        {
            print(" 总地图内容  "+st);
            if (st.Split('+')[0] == mapTou) CurrentDaMapStr = st;
        }

        print("关卡的 地图信息 "+ CurrentDaMapStr);

        string str = CurrentDaMapStr;

        string[] strArr = str.Split('+')[1].Split('|');
        foreach(string s in strArr)
        {
            if (s.Split('!')[0] == ThisMapName) return s;
        }

        return "";
    }
    
}