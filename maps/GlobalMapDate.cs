using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapDate : MonoBehaviour {

    // Use this for initialization


    //boss平地
    public const string BOSS_PINGDI = "bossPingdi";
    //存档平地
    public const string CUNDANG_PINGDI = "cundangPingdi";
    //平地
    public const string PINGDI = "pingdi";
    //跳跃
    public const string TIAOYUE = "tiaoyue";
    //洞内
    public const string DONGNEI = "dongnei";
    //一般
    public const string YIBAN = "yiban";
    //剧情平地
    public const string JUQING_PINGDI = "juqingPingdi";
    //精英怪 平地
    public const string JINGYING_PINGDI = "jingyingPingdi";


    //多怪 精英 平地 会关门
    public const string DUOGUAI_JINGYING_PINGDI = "duoguaiJingyingPingdi";

    //多怪 警示鱼 平地
    public const string DUOGUAI_JSY_PINGDI = "duoguaiJSYPingdi";




    //当前关卡 第几关
    public static string CCustomStr = "s";

    //判断是否已经有了 数据 有的话去取  没有的话 生成
    public static bool IsHasDateCCustom = false;

    //例子 进门 特殊地图是 map_s-1   s 就是 取特殊地图数组的 标记
    //“s:7!l-r!jiguan|9!l-r!boss”
    public static List<string> SpecialMapNameAndNumArr = new List<string>{ "", "", "s:2!l-r|4!l-r" };

    //特殊生成地图 列表 （用来匹配 可以 自动生成的 特殊地图）
    public static List<string> TeShuShengchengDiTuList = new List<string>
    {
        "s:4!cundangPingdi^1",
        "s:2!jingying^jingyingPingdi^1^G_jydj!db^1,bg^1,jyj^1,yj1^1,yj2^1,qj1^1,qj2^1,xs1^1,xs2^1",
        "s:3!boss^bossPingdi^1^name!db^1,bg^1,jyj^1,yj1^1,yj2^1,qj1^1,qj2^1,xs1^1,xs2^1",
        "s:5!dongnei"
    };


    public static string CurrentSpelMapName = "";
    //boss    jingying  juqing cundang  dongne 等
    public static string CurrentSpelMapType = "";
    //地图组成信息
    public static string CurrentSpeMapDiXingMsg = "";

    public static void ClearGlobalCurrentMapMsg()
    {
        CurrentSpelMapName = "";
        CurrentSpelMapType = "";
        CurrentSpeMapDiXingMsg = "";
    }


    //这里暂时 不能用 要做存档取档处理
    // 阶次  在列表的前 几个 随机获取
    public static string JieCiRandom(List<string> stringList,int QianNums = 3)
    {
        if (stringList.Count == 0) return "";
        int i = QianNums*(Globals.mapTypeNums-1) + GlobalTools.GetRandomNum(QianNums);
        if (stringList.Count <= i) {
            i = stringList.Count-QianNums+ GlobalTools.GetRandomNum(QianNums)-1;
            if (i < 0) i = 0;
        } 
        string _getName = stringList[i];
        //stringList.Remove(_getName);
        return _getName;
    }

    //*****boss随机*******
    public static List<string> BossName_1 = new List<string> { };
    public static string GetCanRandomUSEBossName()
    {
        return JieCiRandom(BossName_1);
    }

  

    //****获得物品随机*******






    //关卡分支数组 起始方向
    public static List<string> GKFenZhiArr = new List<string> { "1:fz-1|fx-r", "2:fz-2|fx-r", "3:fz-0", "4:1", "5:2" };

    //当前是 哪一关

    //是否有分支
    bool IsThisCustomHasFZ(string guankaNums)
    {

        return false;
    }


    //创建分支 取到分支 起始点


    //分支 是否结束


    //大关卡数组  关卡分支连接在这里面取 如果用了 就移出数组



    //1.关卡有多少地图
    //4.特殊关卡 名字
    //有多少分支关卡  0 1 2；
    //存档点关卡
    //有奖励物关卡
    //精英怪关卡
    public static List<string> gk_1 = new List<string> { };

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //判断 是否是特殊地图
    public static bool IsSpeMapByName(string mapName)
    {
        if (!IsHasSpecialMap()) return false;
        string[] speMapArr = GetCSpeicalMapNameArr();
        string mapName1;
        for (int i = 0; i < speMapArr.Length; i++)
        {
            mapName1 = "map_" + CCustomStr + "-" + speMapArr[i].Split('!')[0];
            if (mapName1 == mapName) return true;
        }
        return false;
    }

    //获取特殊地图名字 List
    public static List<string> GetSpeMapNameArr()
    {
        if (!IsHasSpecialMap()) return null;
        List<string> tempMapNameList = new List<string> { };
        string[] speMapArr = GetCSpeicalMapNameArr();
        string mapName1;
        for (int i = 0; i < speMapArr.Length; i++)
        {
            mapName1 = "map_" + CCustomStr + "-" + speMapArr[i].Split('!')[0];
            tempMapNameList.Add(mapName1);
        }
        if (tempMapNameList.Count >= 1) return tempMapNameList;
        return null;
    }


    public static string GetSpeMsgByName(string mapName) {
        if (!IsHasSpecialMap()) return null;
        string[] speMapArr = GetCSpeicalMapNameArr();
        string mapName1;
        string speMsg;
        for (int i = 0; i < speMapArr.Length; i++)
        {
            mapName1 = "map_" + CCustomStr + "-" + speMapArr[i].Split('!')[0];
            if (mapName1 == mapName)
            {
                speMsg = speMapArr[i].Split('!')[2];
                return speMsg;
            }
        }
        return null;
    }


    //根据名字 获取方向列表
    public static List<string> GetFXListByName(string mapName) {
        if (!IsHasSpecialMap()) return null;
        //print("mapName   "+mapName);
        string[] speMapArr = GetCSpeicalMapNameArr();
        string mapName1;
        string speMapMsg;
        for (int i = 0; i < speMapArr.Length; i++)
        {
            mapName1 = "map_" + CCustomStr + "-" + speMapArr[i].Split('!')[0];
            if (mapName1 == mapName) {
                speMapMsg = speMapArr[i];
                return SGKFXArr(speMapMsg);
            }
        }
        return null;
    }

    /// <summary>
    /// 获取关卡的方向List
    /// </summary>
    /// <param name="gkStr"></param>
    /// <returns></returns>
    public static List<string> SGKFXArr(string gkStr)
    {
        string fxStr = gkStr.Split('!')[1];
        List<string> FXList = new List<string> { };
        if (fxStr != "n")
        {
            string[] fxArr = fxStr.Split('-');
            for (int i = 0; i < fxArr.Length; i++)
            {
                FXList.Add(fxArr[i]);
            }
            return FXList;
        }
        return null;
    }


    public static bool IsHasSpecialMap()
    {
        if (CCustomStr == "") return false;
        foreach (string s in SpecialMapNameAndNumArr)
        {
            if (s.Split(':')[0] == CCustomStr) return true;
        }
        return false;
    }

    //获取当前关卡的 特殊关卡的数组
    public static string[] GetCSpeicalMapNameArr()
    {
        if (CCustomStr == "") return null;
        foreach (string s in SpecialMapNameAndNumArr)
        {
            //print(s + "  ---  " + CCustomStr);
            if (s.Split(':')[0] == CCustomStr)
            {
                return s.Split(':')[1].Split('|');
            }
        }
        return null;
    }

    //获取当前关卡的 特殊关卡的字符串
    public static string GetCSpeicalMapStr()
    {
        foreach (string s in SpecialMapNameAndNumArr)
        {
            if (s.Split(':')[0] == CCustomStr)
            {
                return s.Split(':')[1];
            }
        }
        return null;
    }

    


    public static bool IsHasGKBYId(int num)
    {

        return false;
    }
}
