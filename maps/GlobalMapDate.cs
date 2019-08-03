using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapDate : MonoBehaviour {

    // Use this for initialization

    //当前关卡 第几关
    public static int CCustomNum = 1;

    //判断是否已经有了 数据 有的话去取  没有的话 生成
    public static bool IsHasDateCCustom = false;



    public static List<string> SpecialMapNameAndNumArr = new List<string>{"testMap1_1-4|testMap1_2-9","","testMap3_1-1"};



    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool IsHasSpecialMap()
    {
        if (SpecialMapNameAndNumArr[CCustomNum] != "") return true;
        return false;
    }

    //获取当前关卡的 特殊关卡的数组
    public static string[] GetCSpeicalMapNameArr()
    {
        return SpecialMapNameAndNumArr[CCustomNum].Split('|');
    }

    //获取当前关卡的 特殊关卡的字符串
    public static string GetCSpeicalMapStr()
    {
        return SpecialMapNameAndNumArr[CCustomNum];
    }
}
