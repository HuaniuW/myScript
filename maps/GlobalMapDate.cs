using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapDate : MonoBehaviour {

    // Use this for initialization

    //当前关卡 第几关
    public static int CCustomNum = 1;

    //判断是否已经有了 数据 有的话去取  没有的话 生成
    public static bool IsHasDateCCustom = false;



    public static List<string> SpecialMapNameAndNumArr = new List<string>{"5!u-r!jiguan|9!r-d!jiguan","", "6!n!boss|8!n!cundang" };



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
            mapName1 = "map_" + CCustomNum + "-" + speMapArr[i].Split('!')[0];
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
            mapName1 = "map_" + CCustomNum + "-" + speMapArr[i].Split('!')[0];
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
            mapName1 = "map_" + CCustomNum + "-" + speMapArr[i].Split('!')[0];
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
        string[] speMapArr = GetCSpeicalMapNameArr();
        string mapName1;
        string speMapMsg;
        for (int i = 0; i < speMapArr.Length; i++)
        {
            mapName1 = "map_" + CCustomNum + "-" + speMapArr[i].Split('!')[0];
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
        if (SpecialMapNameAndNumArr[CCustomNum-1] != "") return true;
        return false;
    }

    //获取当前关卡的 特殊关卡的数组
    public static string[] GetCSpeicalMapNameArr()
    {
        return SpecialMapNameAndNumArr[CCustomNum-1].Split('|');
    }

    //获取当前关卡的 特殊关卡的字符串
    public static string GetCSpeicalMapStr()
    {
        return SpecialMapNameAndNumArr[CCustomNum-1];
    }

    


    public static bool IsHasGKBYId(int num)
    {

        return false;
    }
}
