using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("hello!  createMap!");
        GetChildMsg("screenC");
        init();
	}
    //先把左右型搞好
    // 1.简单的单一型 主路只有一个模块的  中间多几个凸起的小模块   还可以额外安插几个其他的小模块
    //上下门 是左还是右

    string theMapMsg = "map_1-3!l,r";
    void init()
    {
        // map_1-3!l,r,u
        GetMapDate();

        // 地图 类型  第几大关的类型 根据类型 决定拼接元素
        //查找地图是否有 保存信息   （新游戏是否删除此信息？）  ******地图VO存什么 主路模块 主路景色杂物  前景  远景******
        //如果没有本地保存信息  先获取 地图基本信息  几个门  方向在哪  （地图几 知道从那边进的地图-上下左右门）  --- 地图建完后保存地图信息
        //是否生成简单1型地图
        //生成主角 主角位置  
        //生成怪物


       

    }


    string saveMapName;

    void GetMapDate()
    {
        saveMapName = this.theMapMsg.Split('!')[0];
        UserDate mapDate = GameSaveDate.GetInstance().GetSaveDateByName(saveMapName);
        if (mapDate == null)
        {
            //说明没有存档 要新生成地图
            //GenerateNewMap();

            GetTest("jing_cao_2");
        }
        else
        {
            //根据mapDate 创建地图



        }
    }

    void GetTest(string objName,float _x = 0,float _y =0)
    {
        GameObject obj = GlobalTools.GetGameObjectByName(objName);
        if(obj!=null)obj.transform.position = new Vector3(_x,_y,obj.transform.position.z);
        //print("_w "+obj.GetComponent<MeshFilter>().mesh.bounds.size.x);
        SpriteRenderer objRen = obj.GetComponent<SpriteRenderer>();
        print("w> "+ objRen.bounds.size+"  order  "+ objRen.sortingOrder);
        objRen.sortingOrder = 28;
        //objRen.bounds.size = new Vector3(60, 60, 0.2f);
        print("w2> " + objRen.bounds.size + "  order  " + objRen.sortingOrder);



        GameObject diban = GlobalTools.FindObjByName("diban1");
        print(">>>>   "+diban.GetComponent<Terrain>());

        GameObject jing = GlobalTools.FindObjByName("jing_cao_2");
        print("jing   "+jing);
        print("-------------->   "+GlobalTools.GetJingW(jing));

        
        print(GlobalTools.GetPointWorldPosByName(GlobalTools.FindObjByName("yj_shu_1"),"tl"));
    }

    void GenerateNewMap()
    {
        //几门 主路  简单1型   (基础型 1 2 3 4 5  还有随机型 )
        string[] menArr = this.theMapMsg.Split('!')[1].Split(',');
        if (menArr.Length == 2)
        {

            if ((menArr[0] == "l" && menArr[1] == "r") || (menArr[0] == "r" && menArr[1] == "l"))
            {
                //左右通道型
                //生成主干道
                Zhugandao();
            }
            else if ((menArr[0] == "u" && menArr[1] == "d") || (menArr[0] == "d" && menArr[1] == "u"))
            {
                //上下通道型
            }
        }




        if (menArr.Length == 1)
        {
            //只有一个门
        }

        //1 门
        //2门
        // r l
        // 


        //GameObject obj = GlobalTools.FindObjByName("screenC/diban_1_1/cao_1");
        //print("cao   " + obj.transform.position);
    }

    void Zhugandao()
    {
        //简单型 1 2 3 
        //GameObject zhugandao = ObjectPools.GetInstance().SwpanObject2(); //GlobalTools.GetGameObjectByName("diban_1_1");
        GameObject zhugandao = GlobalTools.GetGameObjectByName("diban_1_1");
        zhugandao.transform.position = Vector3.zero;
        //做还是不做哦 好烦躁啊
    }



    void GetChildMsg(string transformName)
    {
        GameObject screenC = GlobalTools.FindObjByName(transformName);
        if(screenC == null)
        {
            print("无此对象！");
            return;
        }
        foreach (Transform child in screenC.transform)
        {
            print(child.gameObject.name+"  -  "+child.gameObject.transform.position);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
