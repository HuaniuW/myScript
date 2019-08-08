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
                if (currentMap==null) continue;
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
            //print("特殊地图名字------------------------------------------------------------->     "+ cZXMapName);
            //tempKyFXArr = GlobalMapDate.GetFXListByName(cZXMapName);
            //判断各个方向是否有地图 有的话连接
            return;
        }
        else
        {
            //这个地图衍生几个方向 去探索 当i越大 多方向概率越小
            //10 15 20 25
            int fxNums = 1;
            int jl = (int)UnityEngine.Random.Range(0, 100);
            string[] jvArr = duozhiJL.Split('-');

            //扩展几个边界
            if (jl < int.Parse(jvArr[0]) * (100 - i) / 100)
            {
                fxNums = 3;
            }
            else if (jl < int.Parse(jvArr[1]) * (100 - i) / 100)
            {
                fxNums = 2;
            }

           

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

            //print("剩余方向数maxNum:  " + maxNum + "  扩展方向数fxNums: " + fxNums);
            print("当前中心地图名字 " + cZXMapName);
            print("扩展数量------->fxNums:   " + fxNums);
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
            }


            
            foreach (var stu2 in tempKyFXArr)
            {
                print("  可以扩展的方向:   " + stu2);
            }
        }

        //获取最大地图的名字
        maxI = theMapArr.Count;
        //maxI = int.Parse(theMapArr[theMapArr.Count-1].name.Split('-')[1]);
        CreateMapByKyFXArr(tempKyFXArr, cZXMapName);
        return;

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="fxzhi">分支选中的几率值组</param>
    /// <param name="kyFXArr">可以选择的方向数组</param>
    /// <param name="canChoseNums">可以选择的方向数</param>
    /// <returns></returns>
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
            //u,r,l
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


            string choseFX = null;

            for (int c =0; c < tempFXJLArr.Count ; c++)
            {
                if(jl<= int.Parse(tempFXJLArr[c].Split('-')[1])/ fenmu * 100){
                    choseFX = tempFXJLArr[c].Split('-')[0];
                    choseArr.Add(choseFX);
                    //tempFXJLArr.Remove(tempFXJLArr[c]);
                    for (int n=0;n< kyFXArr.Count;n++)
                    {
                        if(kyFXArr[n]== tempFXJLArr[c].Split('-')[0])
                        {
                            kyFXArr.Remove(kyFXArr[n]);
                        }
                    }
                    break;
                }
            }

            if(choseFX == null)
            {
                if (jl >= int.Parse(tempFXJLArr[tempFXJLArr.Count - 1].Split('-')[1]) / fenmu * 100)
                {
                    choseArr.Add(tempFXJLArr[tempFXJLArr.Count - 1].Split('-')[0]);
                    //tempFXJLArr.Remove(tempFXJLArr[tempFXJLArr.Count - 1]);

                    for (int n = 0; n < kyFXArr.Count; n++)
                    {
                        if (kyFXArr[n] == tempFXJLArr[tempFXJLArr.Count - 1].Split('-')[0])
                        {
                            kyFXArr.Remove(kyFXArr[n]);
                        }
                    }
                }
            }
           

        }
        return choseArr;
    }


    int maxI = 0;
    List<string> tempSpeMapList = new List<string> { };

    void CreateMapByKyFXArr(List<string> tempKyFXArr,string cZXMapName)
    {
        tempSpeMapList.Clear();
        int createMapNums = maxI + 1;
        for (int c = 0; c < tempKyFXArr.Count; c++)
        {
            maxI = theMapArr.Count;
            createMapNums = maxI + 1;
            //maxI = theMapArr.Count;
            //createMapNums = maxI+1;
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
        string[] zb = currentMapZB.Split('#');
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
        gkImg.transform.position = new Vector2(px,py);
        
        gkImg.name = _name;
        return gkImg;
    }


    List<string> tempFindSpeMapList = new List<string> { };
    //根据坐标和方向 获取改位置的 地图名字(用于 判断该方向上是否有地图)  返回特殊地图的名字和信息 还有方位
    string GetMapNameAndMsgByZBAndFX(string zb,string fx)
    {
        string getMapName = "";
        string _zb = GetNewZBByFX(zb,fx);
        for(int i = 0; i < mapZBArr.Count; i++)
        {
            if(mapZBArr[i].Split('!')[1] == _zb)
            {
                //判断 如果是特殊地图
                if (GlobalMapDate.IsSpeMapByName(mapZBArr[i].Split('!')[0])) return  mapZBArr[i]+"="+fx;
            }
        }
        return null;
    }

    string GetMapNameByZB(string zb)
    {
        for (int i = 0; i < mapZBArr.Count; i++)
        {
            if (mapZBArr[i].Split('!')[1] == zb)
            {
                //判断 如果是特殊地图
                if (GlobalMapDate.IsSpeMapByName(mapZBArr[i].Split('!')[0])) return mapZBArr[i].Split('!')[0];
                return mapZBArr[i].Split('!')[0];
            }
        }
        return null;
    }

    string GetNewZBByFX(string zb, string fx)
    {
        string newZB = "";
        string[] _zb = zb.Split('#');
        switch (fx)
        {
            case "u":
                newZB = _zb[0] + "#" + (int.Parse(_zb[1]) + 1);
                break;
            case "d":
                newZB = _zb[0] + "#" + (int.Parse(_zb[1]) - 1);
                break;
            case "l":
                newZB = (int.Parse(_zb[0]) - 1) + "#" + _zb[1];
                break;
            case "r":
                newZB = (int.Parse(_zb[0]) + 1) + "#" + _zb[1];
                break;

        }
        if (newZB != "") return newZB;
        return null;
    }

    int ss = 0;
    bool IsFXHasSpeMap(string zb,List<string> FXList)
    {
        tempFindSpeMapList.Clear();
        for (int i = 0;i<FXList.Count;i++)
        {
            ss++;
            //print("计算次数 哇塞    "+ss);
            //判断改方向上是否有地图
            string speMapMsg = GetMapNameAndMsgByZBAndFX(zb, FXList[i]);
            //判断该地图是否是特殊地图
            if (speMapMsg != null) tempFindSpeMapList.Add(speMapMsg);
        }
        if (tempFindSpeMapList.Count != 0) return true;
        return false;
    }

    void CreateMapImgByFX(string theCZXMapName,string fx,string newMapName)
    {
        tempSpeMapList.Clear();
        //判断是否是特殊地图  并找到安防特殊地图的位置      --如果是特殊的地图 看能不能对向相连 否则就存入数组
        newMapName = IsSpeMapAddMaxI(newMapName);
        print("newMapName    "+ newMapName);
        CreateOutMap(currentMap, fx, newMapName, theCZXMapName);
        //print("?????????????????????????????????????????????????????????   theCZXMapName  "+ theCZXMapName+ "   newMapName    "+ newMapName);
        
        //如果特殊地图数组有数据
        CreateSpeMap();
    }


    string IsSpeMapAddMaxI(string newMapName)
    {
        if (GlobalMapDate.IsSpeMapByName(newMapName))
        {
            tempSpeMapList.Add(newMapName);
            maxI++;
            print(newMapName+  "  ---  maxI -----   " +maxI);
            newMapName = newMapName.Split('-')[0] + "-" + (maxI+1);
            if (GlobalMapDate.IsSpeMapByName(newMapName))
            {
                return IsSpeMapAddMaxI(newMapName);
            }
        }
        print("生成的名字   "+newMapName);
        return newMapName;
    }

    GameObject CreateOutMap(GameObject _currentMap, string fx, string newMapName, string currentZXMapName)
    {
        Vector2 pos = GetMapPos(_currentMap, fx);
        GetInMapZBArr(newMapName, fx, currentZXMapName, pos);
        GameObject gkImg = GetGKImgAndPosition(newMapName, pos.x, pos.y);
        theMapArr.Add(gkImg);
        //连线
        LianXianMaps(_currentMap, gkImg, fx);
        return gkImg;
    }

    Vector2 GetMapPos(GameObject currentMap, string fx)
    {
        Vector2 pos = new Vector2(0, 0);
        if (fx == "u")
        {
            pos = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y + currentMap.GetComponent<RectTransform>().rect.height + mapjiangejuli);
        }
        else if (fx == "d")
        {
            pos = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y - currentMap.GetComponent<RectTransform>().rect.height - mapjiangejuli);
        }
        else if (fx == "l")
        {
            pos = new Vector2(currentMap.transform.position.x - currentMap.GetComponent<RectTransform>().rect.width - mapjiangejuli, currentMap.transform.position.y);
        }
        else if (fx == "r")
        {
            pos = new Vector2(currentMap.transform.position.x + currentMap.GetComponent<RectTransform>().rect.width + mapjiangejuli, currentMap.transform.position.y);
        }
        return pos;
    }
    


    void CreateSpeMap()
    {
        for (int i=0;i< tempSpeMapList.Count;i++)
        {
            CreateSpeMapByName(tempSpeMapList[i]);
        }
    }


    void CreateSpeMapByName(string speMapName)
    {
        //寻找适合位置
        //获取特殊地图 方向数组
        List<string> SpeMapFXArr = GlobalMapDate.GetFXListByName(speMapName);
        //1该方向上 有空位  2该空位 特殊地图的连接线可以连（先找特殊地图  普通地图 直接连   特殊地图的话看看是否能连 能连直接连  不能连就换位置）
        string mapName =  FindLJSpeMapDeMap(SpeMapFXArr, speMapName);
        
    }

    string FindLJSpeMapDeMap(List<string> SpeMapFXArr,string speMapName)
    {
        string findFX = "";
        string findZB = "";
        for(int i = mapZBArr.Count - 1; i >= 0; i--)
        {
            for (int j=0;j< SpeMapFXArr.Count;j++) {
                //寻找放置方向
                if (SpeMapFXArr[j] == "l")
                {
                    findFX = "r";
                }else if (SpeMapFXArr[j] == "d")
                {
                    findFX = "u";
                }
                else if (SpeMapFXArr[j] == "u")
                {
                    findFX = "d";
                }
                else if (SpeMapFXArr[j] == "r")
                {
                    findFX = "l";
                }

                string cMapName = mapZBArr[i].Split('!')[0];
                string _cMapZB = mapZBArr[i].Split('!')[1];

                //如果是特殊地图就跳出当前循环
                if (GlobalMapDate.IsSpeMapByName(cMapName)) break;
                //根据 findFX 找到 适合的 地图  1.该方向上有空位  
                if (!IsHasMap(findFX, cMapName))
                {
                    //获取这个空位坐标
                    string _zb = GetZBByFX(findFX, cMapName); //这个坐标上面没有地图  这个坐标就是特殊地图 选的位置
                    //2.空位上 特殊地图的方向 没有特殊地图
                    //特殊地图的方向数组
                    List<string> SpeMapFXList = GlobalMapDate.GetFXListByName(speMapName);
                    //查找方向上是否有    特殊地图 数组 有的话就跳过
                    if (IsFXHasSpeMap(_zb, SpeMapFXList)) continue;//这里不能用break 
                    //---------------------------------------------------------------找到可以放特殊地图的位置坐标


                    //找到院地图的OBJECY


                    //如果 周围 连线路上 没有其他特殊地图  就安插这个特殊地图

                    //获取当前地图对象 _currentMap  根据坐标获取 地图块对象
                    GameObject _currentMap =    GetMapImgObjByZB(_cMapZB);
                    //创建地图的特殊地图块
                    GameObject speMapObj =  CreateOutMap(_currentMap, findFX, speMapName, cMapName);
                    speMapObj.GetComponent<gkImgTextTest>().GetText("老子是特殊关卡");
                    //链接特殊地图块的各个方向



                    return null;

                }
            }

        }
        return null;
    }

    void CreateSpeMapFZ(string speMapName,List<string> fxArr,string speMapZB)
    {
        GameObject speMapObj =  GetMapImgObjByZB(speMapZB);
        //maxI = int.Parse(theMapArr[theMapArr.Count - 1].name.Split('-')[1]);
        maxI = theMapArr.Count;
        for (int i = 0; i < fxArr.Count; i++)
        {
            //检查 方向坐标上 是否有地图了 有的话连线
            if (IsHasMap(fxArr[i],speMapName))
            {
                string _zb = GetZBByFX(fxArr[i], speMapName);
                LianXianMaps(speMapObj, GetMapImgObjByZB(_zb), fxArr[i]);
            }
            else
            {
                int createMapNums = maxI + 1 + i;
                //获得名字  先查找名字是否被占用
                string _newMapName = "map_" + GlobalMapDate.CCustomNum.ToString() + "-" + createMapNums;
                if (GlobalMapDate.IsSpeMapByName(_newMapName))
                {
                    tempSpeMapList.Add(_newMapName);
                    createMapNums = maxI + 2 + i;
                }
                //方向上没有 地图 就生成一个地图
                CreateOutMap(speMapObj, fxArr[i], _newMapName, speMapObj.name);
            }
        }
    }

   
    GameObject GetMapImgObjByZB(string zb)
    {
        //根据坐标获取 地图块名字
        string mapName = GetMapNameByZB(zb);
        for(int i = 0; i < theMapArr.Count; i++)
        {
            if (theMapArr[i].name == mapName) return theMapArr[i];
        }
        return null;
    }


    //通过特殊地图坐标 判断该地图各个链接线上是否有特殊地图
    bool IsHasSpeMapInFXBySpeMapZB(string speMapZB,List<string> SpeMapFXList)
    {

        return false;
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
