using System.Collections;
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
    public static bool isDebug2 = false;

    public static bool isPC = true;

    //防止虚拟按键 和键盘按键 冲突 导致虚拟按键跑动出问题
    public static bool isXNBtn = false;

    public static bool isGameOver = false;

    public static bool isPlayerDie = false;

    //语言 english
    public static string language = "chinese";

    public const string CHINESE = "chinese";
    public const string CHINESEF = "chineseF";
    public const string ENGLISH = "english";
    public const string JAPAN = "japan";
    public const string KOREAN = "korean";
    public const string Portugal = "portugal";

    public const string German = "German";
    public const string French = "French";

    public const string Italy = "Italy";


    // Use this for initialization

    public static string languageType = "";


    public static bool isKeyUp = false;
    public static bool isKeyDown = false;

    //记录飞刀位置 和丢出的 FX
    public static string feidaoFX = "";

    public static GameObject feidao = null;

    public static bool isShouFD = false;

    public static bool cameraIsFeidaoGS = false;

    public static bool IsHitPlotKuai = false;

    public static bool IsCanControl = true;

    public static bool IsNeedSetJingInMaps = true;


    internal static string mapType;
    //控制 地图 基本类型的 参数  （使用的 地板 景会变化）
    public static int mapTypeNums = 1;
    public static List<string> mapZBArr;


    //角色进门 停止
    public static bool IsHitDoorStop = false;

    public static UI_ShowPanel Get_UI_ShowPanel()
    {
        return null;
    }


    //是否进入战斗状态
    public static bool IsInFighting = false;

    //是否在 摄像机 碰撞块内
    public static bool IsInCameraKuai = false;
    public static bool IsInJijia = false;
    public static bool IsInJijiaGK = false;

    public static bool IsInSetBar = false;

    //是否是 取档进入游戏 在神树旁边出现
    public static bool ISLOADINGAME = false;


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
