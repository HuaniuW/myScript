﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

    //是否进入情节 plot（情节）
    public static bool isInPlot = false;
    /// <summary>
    /// 开启状态菜单
    /// </summary>
    public const string OPEN_ZTCD = "p";

    /// <summary>
    /// 使用血瓶
    /// </summary>
    public const string USE_XP = "z";

    public static bool isDebug = true;

    public static bool isPC = true;

    //防止虚拟按键 和键盘按键 冲突 导致虚拟按键跑动出问题
    public static bool isXNBtn = false;

    public static bool isGameOver = false;

    //语言 english
    public static string language = "chinese";

    public const string CHINESE = "chinese";
    public const string ENGLISH = "english";
    // Use this for initialization

    public static bool isKeyUp = false;
    public static bool isKeyDown = false;

    //记录飞刀位置 和丢出的 FX
    public static string feidaoFX = "";

    public static GameObject feidao = null;

    public static bool isShouFD = false;







    public static UI_ShowPanel Get_UI_ShowPanel()
    {
        return null;
    }


   

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
