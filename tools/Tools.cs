using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tools : MonoBehaviour {
    //UNITY  帧率 跟龙骨帧率同步就OK   1:1    目前unity默认是60帧  只要龙骨是60帧 就可以1:1取到帧数
    // Use this for initialization
    void Start () {
        
    }

    //输出系统时间 可用于玩家数量统计
    public static void timeData()
    {
        DateTime dt = new DateTime();
        dt = System.DateTime.Now;
        //此处的大小写必须完全按照如下才能输出长日期长时间，时间为24小时制式，hh:mm:ss格式输出12小时制式时间
        string strFu = dt.ToString("yyyy-MM-dd HH:mm:ss");
        print("strFu:" + strFu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //限制帧率  没什么卵用
    //Application.targetFrameRate = 30;
    //-1 代表不限定帧率
    //Application.targetFrameRate = -1;
    //Application.targetFrameRate 是用来让游戏以指定的帧率运行，如果设置为 -1 就让游戏以最快的速度运行。
    //但是 这个 设定会 垂直同步 影响。
    //如果设置了垂直同步，那么 就会抛弃这个设定 而根据 屏幕硬件的刷新速度来运行。
    //如果设置了垂直同步为1，那么就是 60 帧。
    //如果设置了为2 ，那么就是 30 帧。
    // print(Application.targetFrameRate);  获取帧率 默认-1

    //**  测试可用  float.Parse(str),
    //或者float.TryParse(str,out value);
    //或者Convert.ToFloat（string）




}
