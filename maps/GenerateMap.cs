using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//生成地图
public class GenerateMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
        _canva = GlobalTools.FindObjByName("MapUI");
        CreateCustomAllMap();

    }

    GameObject _canva;
	
	// Update is called once per frame
	void Update () {
		
	}

    //多支路的几率
    public string duozhiJL = "10-15";

    [Header("地图间隔距离")]
    public int mapjiangejuli = 10;

    GameObject currentMap;

    int curCreateMapMaxNums = 1;

    //创建一个大关卡的地图  map1_1,map1_2...
    public void CreateCustomAllMap() {
        //有几个小地图组成？ 最少10个 +随机
        int n = (int)UnityEngine.Random.Range(0, 4);
        int mapsNum = 8 + n;
        print("地图长度--------------------------------------------------->   "+mapsNum);
        //查找 是否有特别地图 以及特别地图位置编号
        string[] smapArr;
        if (GlobalMapDate.IsHasSpecialMap())
        {
            smapArr = GlobalMapDate.GetCSpeicalMapNameArr();
        }

        string mapName;

        for (int i = 0; i < mapsNum; i++)
        {
            mapName = "map_" + GlobalMapDate.CCustomNum.ToString();
            mapName += "-" + (i+1);
            if (i == 0)
            {
                //首个地图
                currentMap = GetGKImgAndPosition(mapName,400,200);
                mapZBArr.Add(mapName + "!1#1"+"!400#200");
                theMapArr.Add(currentMap);
                
                //创建分支
                CreateMapFenZhi(i+1,mapName);
            }
            else if (i == mapsNum - 1)
            {
                //最后一个地图
                print("最后是哪个地图！！！！！！！！！！！！！！！！！！！！！！"+(i+1));
            }
            else
            {
                
                currentMap = GetCurrentMapObjZBByName(mapName);

                print("------------>  " + mapName+"     ???    "+currentMap.name);
                if (mapName != currentMap.name) {
                    print("数据错误！！！！！！！！！！！！！！！");
                    return;
                } 
                CreateMapFenZhi(i + 1, mapName);
            }
        }
    }

    GameObject GetCurrentMapObjZBByName(string _mapName)
    {
        foreach(var mapObj in theMapArr)
        {
            if (mapObj.name == _mapName) return mapObj;
        }
        return null;
    }

    //map-1_1!1-1|map-1_2!2-1|map-1_3!1-2 1-0  用坐标代替碰撞找位置
    List<string> mapZBArr = new List<string> { };
    List<GameObject> theMapArr = new List<GameObject> { };

    //可以用的坐标  坐标上没有地图
    List<string> kyFXArr = new List<string> { };
   
    //创建地图的分支 上下左右
    void CreateMapFenZhi(int i,string cZXMapName)
    {
        List<string> tempKyFXArr = new List<string> { };
        bool isSepMap = false;
        isSepMap = GlobalMapDate.IsSpeMapByName(cZXMapName);
        //判断是否是特殊地图
        if (isSepMap)
        {
            print("特殊地图名字     "+ cZXMapName);
            tempKyFXArr = GlobalMapDate.GetFXListByName(cZXMapName);
            //判断各个方向是否有地图 有的话连接

        }
        else
        {
            //这个地图衍生几个方向 去探索 当i越大 多方向概率越小
            //10 15 20 25
            int fxNums = 1;
            int jl = (int)UnityEngine.Random.Range(0, 100);
            string[] jvArr = duozhiJL.Split('-');

            print("jl    " + jl);
            //扩展几个边界
            if (jl < int.Parse(jvArr[0]) * (100 - i) / 100)
            {
                fxNums = 3;
            }
            else if (jl < int.Parse(jvArr[1]) * (100 - i) / 100)
            {
                fxNums = 2;
            }

            print("扩展数量------->fxNums:   " + fxNums);

            //判断周围 看看是否坐标被占用 找出空位比较 空地
            List<string> fxArr = new List<string> { "l", "u", "d", "r" };
            int maxNum = 4;
            kyFXArr.Clear();
            for (int c = 0; c < fxArr.Count; c++)
            {
                //查询该坐标是否已经有地图
                if (IsHasMap(fxArr[c], cZXMapName))
                {
                    maxNum--;
                }
                else
                {
                    kyFXArr.Add(fxArr[c]);
                }
            }

            print("剩余方向数maxNum:  " + maxNum + "  扩展方向数fxNums: " + fxNums);

            foreach (var stu in kyFXArr)
            {
                print("  kyFXArr:   " + stu);
            }


            if (maxNum <= fxNums)
            {
                //如果最大的空位 小于扩展数  则直接用数组里面的位置来 生成地图
                tempKyFXArr = kyFXArr;
            }
            else
            {
                //方向的比重
                List<string> fxzhi = new List<string> { "l-5", "d-10", "u-15", "r-70" };
                //随机选取方向 生成方向数组  
                tempKyFXArr = ChoseFXArr(fxzhi, kyFXArr, fxNums);

                foreach (var stu2 in tempKyFXArr)
                {
                    print("  tempKyFXArr:   " + stu2);
                }
            }
        }

        //获取最大地图的名字
        int maxI = int.Parse(theMapArr[theMapArr.Count-1].name.Split('-')[1]);
        CreateMapByKyFXArr(tempKyFXArr, cZXMapName, maxI);
        return;

    }



    List<string> ChoseFXArr(List<string> fxzhi, List<string> kyFXArr,int canChoseNums)
    {
        // fxzhi  "l-5", "d-10", "u-15", "r-70"  方向几率值
        List<string> choseArr = new List<string> { };
        List<string> tempFXJLArr = new List<string> { };
        for (int i=0;i< canChoseNums; i++)
        {
            int jl = (int)UnityEngine.Random.Range(0, 100);
            float fenmu = 0;
            tempFXJLArr.Clear();
            //获取可用方向数组的 分母值
            for (int s = 0; s < kyFXArr.Count; s++)
            {
                for (int j=0;j< fxzhi.Count;j++)
                {
                    if (kyFXArr[s] == fxzhi[j].Split('-')[0]) {
                        fenmu += float.Parse(fxzhi[j].Split('-')[1]);
                        tempFXJLArr.Add(fxzhi[j]);
                    }
                }
            }


            for (int c =0; c < tempFXJLArr.Count ; c++)
            {
                if(jl<= int.Parse(tempFXJLArr[c].Split('-')[1])/ fenmu * 100){
                    choseArr.Add(tempFXJLArr[c].Split('-')[0]);
                    tempFXJLArr.Remove(tempFXJLArr[c]);
                    break;
                }
            }

            if (jl >= int.Parse(tempFXJLArr[tempFXJLArr.Count - 1].Split('-')[1]) / fenmu * 100)
            {
                choseArr.Add(tempFXJLArr[tempFXJLArr.Count - 1].Split('-')[0]);
                tempFXJLArr.Remove(tempFXJLArr[tempFXJLArr.Count - 1]);
            }

        }
        return choseArr;
    }


    void CreateMapByKyFXArr(List<string> tempKyFXArr,string cZXMapName,int maxI)
    {
        int createMapNums = maxI + 1;
        //判断 是否是 特殊地图  或者最后的地图
        for (int c = 0; c < tempKyFXArr.Count; c++)
        {
            createMapNums = maxI + 1 + c;
            //获得名字  先查找名字是否被占用
            string _newMapName = "map_" + GlobalMapDate.CCustomNum.ToString() + "-" + createMapNums;
            //创建地图 生成名字 和坐标 存入两个数组
            CreateMapImgByFX(cZXMapName, tempKyFXArr[c], _newMapName);
        }
    }


    //上 20  下10  左5  其他右
    [Header("扩展地图的朝向几率")]
    public string fxjlstr = "20-10-5";

    string ChoseFX()
    {
        string _fx = "r";
        string[] cxjl = fxjlstr.Split('-');
        int _cxjl = (int)UnityEngine.Random.Range(0, 100);
        if (_cxjl <int.Parse(cxjl[2]))
        {
            //左
            _fx = "l";
        }else if (_cxjl < int.Parse(cxjl[1]))
        {
            //上
            _fx = "d";
        }else if (_cxjl < int.Parse(cxjl[0]))
        {
            _fx = "u";
        }
        else
        {
            _fx = "r";
        }

        return _fx;
    }



    bool IsHasMap(string fx, string cZXMapName)
    {
        string _zb = GetZBByFX(fx, cZXMapName);
        for (int i=0;i< mapZBArr.Count;i++)
        {
            if (_zb == mapZBArr[i].Split('!')[1]) return true;
        }
        return false;
    }



    //-------------------------地图生成细节
    //主路骨干 和 怪物分布优先出  然后是 景
    //连线  找出孤立位置 放置宝箱啥的

    void GetInMapZBArr(string mapName,string fx,string cZXMapName,Vector2 pos)
    {
        string newMapZB = GetZBByFX(fx, cZXMapName);
        string mapZBAndName = mapName + "!" + newMapZB+"!"+pos.x+"#"+pos.y;
        mapZBArr.Add(mapZBAndName);
    }

    string GetZBByFX(string fx,string cZXMapName)
    {
        string currentMapZB = GetCurrentMapZBByName(cZXMapName);
        print("寻找坐标地图的名字   "+ cZXMapName+"  方向  "+fx);
        print("是否有地图坐标   currentMapZB "+ currentMapZB);
        string[] zb = currentMapZB.Split('#');
        print("zb--------------------------->    "+zb);
        string newMapZB = "";
        switch (fx)
        {
            case "u":
                newMapZB = zb[0] + "#" + (int.Parse(zb[1]) + 1);
                break;
            case "d":
                newMapZB = zb[0] + "#" + (int.Parse(zb[1]) - 1);
                break;
            case "l":
                newMapZB = (int.Parse(zb[0]) - 1) + "#" + zb[1];
                break;
            case "r":
                newMapZB = (int.Parse(zb[0]) + 1) + "#" + zb[1];
                break;
        }
        return newMapZB;
    }


    //通过当前地图的名字获取当前地图的坐标
    string GetCurrentMapZBByName(string cZXMapName)
    {
        string zb = "";
        for (int i=0;i<mapZBArr.Count;i++)
        {
            if(mapZBArr[i].Split('!')[0] == cZXMapName)
            {
                zb = mapZBArr[i].Split('!')[1];
                return zb;
            }
        }
        return zb;
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


    void CreateMapImgByFX(string theCZXMapName,string fx,string newMapName)
    {
        Vector2 pos = new Vector2(0,0);
        if(fx == "u")
        {
            pos = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y + currentMap.GetComponent<RectTransform>().rect.height + mapjiangejuli);
        }
        else if (fx == "d")
        {
            pos = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y - currentMap.GetComponent<RectTransform>().rect.height - mapjiangejuli);
        }
        else if (fx == "l")
        {
            pos = new Vector2(currentMap.transform.position.x - currentMap.GetComponent<RectTransform>().rect.width - mapjiangejuli, currentMap.transform.position.y );
        }
        else if (fx == "r")
        {
            pos = new Vector2(currentMap.transform.position.x + currentMap.GetComponent<RectTransform>().rect.width + mapjiangejuli, currentMap.transform.position.y);
        }

        GetInMapZBArr(newMapName, fx, theCZXMapName,pos);
        GameObject gkImg = GetGKImgAndPosition(newMapName, pos.x, pos.y);
        theMapArr.Add(gkImg);

        //连线
        LianXianMaps(currentMap, gkImg, fx);
    }

    void LianXianMaps(GameObject currentMap, GameObject gkImg,string fx)
    {
        GameObject xian = null;
        if (fx == "u")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian2");
            xian.transform.position = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y + currentMap.GetComponent<RectTransform>().rect.height*0.5f + xian.GetComponent<RectTransform>().rect.height*0.4f);
            
        }else if (fx == "d")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian2");
            xian.transform.position = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y - currentMap.GetComponent<RectTransform>().rect.height * 0.5f - xian.GetComponent<RectTransform>().rect.height * 0.4f);
        }
        else if(fx == "r")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian1");
            xian.transform.position = new Vector2(currentMap.transform.position.x + currentMap.GetComponent<RectTransform>().rect.width * 0.5f + xian.GetComponent<RectTransform>().rect.width * 0.4f, currentMap.transform.position.y);
        }else if (fx == "l")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian1");
            xian.transform.position = new Vector2(currentMap.transform.position.x - currentMap.GetComponent<RectTransform>().rect.width * 0.5f - xian.GetComponent<RectTransform>().rect.width * 0.4f, currentMap.transform.position.y);
        }

        xian.name = currentMap.name + "#" + fx;
        xian.transform.parent = _canva.transform;
        xian.GetComponent<GKMapXian>().LianJieMap(currentMap.name, gkImg.name);
    }


}
