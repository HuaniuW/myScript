using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//生成地图
public class GenerateMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
        _canva = GlobalTools.FindObjByName("MapUI");
        createCustomAllMap();

    }

    GameObject _canva;
	
	// Update is called once per frame
	void Update () {
		
	}

    //多支路的几率
    public string duozhiJL = "10-15";

    GameObject currentMap;

    //创建一个大关卡的地图  map1_1,map1_2...
    public void createCustomAllMap() {
        //有几个小地图组成？ 最少10个 +随机
        int n = (int)UnityEngine.Random.Range(0, 6);
        int mapsNum = 10 + n;

        //查找 是否有特别地图 以及特别地图位置编号
        string[] smapArr;
        if (GlobalMapDate.IsHasSpecialMap())
        {
            smapArr = GlobalMapDate.GetCSpeicalMapNameArr();
        }

        //先画地图 后填地图

        //GetGKImgAndPosition("test");

        string mapName = "map_"+GlobalMapDate.CCustomNum.ToString();

        string[] fx;

        for (int i = 0; i < mapsNum; i++)
        {
            mapName += "-" + i;
            if (i == 0)
            {
                //首个地图
                currentMap = GetGKImgAndPosition(mapName,200,200);



                CreateMap(i);





            }
            else if (i == mapsNum - 1)
            {
                //最后一个地图
            }
            else
            {

            }
        }

    }



    void CreateMap(int i)
    {
        //这个地图衍生几个方向 去探索 当i越大 多方向概率越小
        //10 15 20 25

        int fxNums = 1;
        int jv = (int)UnityEngine.Random.Range(0, 6);
        string[] jvArr = duozhiJL.Split('-');
        if (jv < int.Parse(jvArr[0]) * (100 - i))
        {
            fxNums = 3;
        }
        else if (jv < int.Parse(jvArr[1]) * (100 - i))
        {
            fxNums = 2;
        }
        string mapName = "map_" + GlobalMapDate.CCustomNum.ToString();

        if (fxNums == 1)
        {
            mapName += (i + 1);
            GetGKImgAndPosition(mapName, currentMap.transform.position.x+currentMap.GetComponent<RectTransform>().rect.width+10, currentMap.transform.position.y);
            return;
        }


        string[] _fxArr;

        for(int s = 0; s < fxNums; s++)
        {
            //判断下一个位置 上 下 左 右

        }

        //

        //衍生名字

        //map_1-1:l,map_1-2|r,map_1-3

        //主路骨干 和 怪物分布优先出  然后是 景

        //连线  找出孤立位置 放置宝箱啥的
    }

    void NextMapFX(string[] fxArr,int s)
    {

    }


    GameObject GetGKImgAndPosition(string _name,float px = 0,float py = 0)
    {
        GameObject gkImg = GlobalTools.GetGameObjectByName("gkImg");
        gkImg.transform.parent = _canva.transform;
        //print(gkImg.transform.position);
        gkImg.transform.position = new Vector2(px,py);
        //print(gkImg.transform.position);
        
        //gkImg.GetComponent<RectTransform>().position = new Vector2(px,py);
        gkImg.name = _name;
        //print(gkImg.name);
        return gkImg;
        //GameObject o = GlobalTools.FindObjByName("MapUI/gkImg");
        //print(o + "   ?  " + o.transform.position);
        //gkImg.transform.position = new Vector3(o.transform.position.x+o.GetComponent<RectTransform>().rect.width + 100,o.transform.position.y+200,o.transform.position.z);
    }


}
