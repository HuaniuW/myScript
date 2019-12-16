using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ScreenDate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static ScreenDate _instance;
	public static ScreenDate GetInstance(){
		if (_instance == null) _instance = new ScreenDate();
		return _instance;
	}


    //总的map地图  游戏开始要在这个数据里面 根据地图名称 找相应的 地图场景数据
    //数据结构命名规则 避免 - 
	//数据结构什么样子？？？  map_1-1:men1~1`1`1~0`0`0~0~29|map_1-2:men1~1`1`1~0`0`0~0~0
    static string mapScreenDateZ = "";

	//当前关卡的 地图场景数据
	static string cScreenDate = "";

    //根据当前场景名字 获取场景数据 map_1-1
	static string GetScreenDateByName(string screenName){
		string[] mapScreenDateArr = mapScreenDateZ.Split('|');
		string screenDate = "";
		for (var i = 0; i < mapScreenDateArr.Length;i++){
			if(mapScreenDateArr[i].Split(':')[0] == screenName){
				screenDate = mapScreenDateArr[i];
				break;
			}
		}
		return screenDate;
	}

    //将生成的关卡数据 写到临时总关卡数据
	static void GetInZScreenDate(string screenDate){
		mapScreenDateZ += screenDate;
	}

	//地图数据储存到本地  根据存档名 来储存

	//地图数据的 读取 根据存档名来读取



    //获取关卡元素景的 名字头  jjd_1_1  中间就是关卡名   这里要获得 是用第几关的 数据

	public static int GetGKNum(){
		return 1;
	}


    //根据名字头 和 关卡  获取 景的list数据
	public List<string> GetListByFstNameAndGKNums(string nameFst){		
		string strName = nameFst  + GetGKNum();
		print("?????      "+strName);
		print("?obj   "+GetDateByName.GetInstance().GetListByName(strName, ScreenDate.GetInstance()));
		return GetDateByName.GetInstance().GetListByName(strName,ScreenDate.GetInstance());
    }



    //获取关卡景的元素数据


	//地图元素数据名字 数组

	//下层近景 近景 下 jin jing  down =jjd
	public static List<string> jjd_1 = new List<string> { "jjd_1_1", "jjd_1_2" };
	public static List<string> shu_1 = new List<string> { "shu_1_1", "shu_1_2", "shu_1_3", "shu_1_4", "shu_1_5", "shu_1_6", "shu_1_8" };

    //门 下方的近景
	public static List<string> men_jjd_1 = new List<string> { "jjd_1_1", "jjd_1_2" };

    //门 上方的近景 
	public static List<string> men_jju_1 = new List<string> { "jju_1_1", "jju_1_2", "jju_1_3", "jju_1_4" };

    //门 下方的 前景
	public static List<string> men_qjd_1 = new List<string> { "qjd_1_3", "qjd_1_4", "qjd_1_5", "qjd_1_6" };
    //前景 下方
	public static List<string> qjd_1 = new List<string> { "qjd_1_3", "qjd_1_4", "qjd_1_6", "qjd_1_7", "qjd_1_8", "qjd_1_5" };
    //较大的 前景 w>13 h>4
	public static List<string> qjdd_1 = new List<string> { "qyj_1_1" };

    //public static List<string> qjdd_1 = new List<string> { "1", "2", "10" };

    public static List<string> qyj_1 = new List<string> { "qyj_1_1" };
    //近远景
    public static List<string> jyj_1 = new List<string> { "jyj_1_1", "jyj_1_2", "jyj_1_3", "jyj_1_4", "jyj_1_5", "jyj_1_6" };
    //中远景
    public static List<string> zyj_1 = new List<string> { "zyj_1_1", "zyj_1_2", "zyj_1_3", "zyj_1_4", "zyj_1_5", "zyj_1_6", "zyj_1_7" };
    //粒子落叶
    public static List<string> liziLY_1 = new List<string> { "liziLY_1_1" };
    //粒子 雾
    public static List<string> liziWu_1 = new List<string> { "liziWu_1_1" };

    //跳跃地板
    public static List<string> tiaoyuediban_1 = new List<string> {"xdiban_1_10", "xdiban_1_11", "xdiban_1_12", "xdiban_1_13", "xdiban_1_14", "xdiban_1_15", "xdiban_1_16", "xdiban_1_17", "xdiban_1_18", "xdiban_1_19" };
    //移动的地板
    public static List<string> mdiban_1 = new List<string> { "mdiban_1_1", "mdiban_1_2"};


}
