using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaps : MonoBehaviour
{
    //多方向 可以多加3个分支 否则 一个地图 只有2个分支
    [Header("是否多方向")]
    public bool IsDuoFX = false;

    [Header("多分支的几率 <15 2 <30 3   这样")]
    public string DuoFenZhiJL = "30-60";

    [Header("生成的 地图 最大数量")]
    public int MaxMapNums = 70;

    //已经生成到的 最大 地图数
    int hasCreateMapMaxNums = 0;

    //生成的 地图信息 记录   map_1-1!0#0!r:map_1-2^d:map_1-3|map_1-2!l:map_1-1^r:map_1-4
    string MapListMsgStr = "";

    List<string> mapZBArr = new List<string> { };

    [Header("地图 分支 的比值")]
    public List<string> FXBiZhi = new List<string> { "l-5", "d-10", "u-15", "r-70" };


    string cMapNum = "1";
    string cMapName = "";


    public void SetMapCenterName(string str) {
        cMapNum = str;
        GlobalMapDate.CCustomStr = str;
    }

    string _qishiZB = "0#0";


    

    public void SetMapQishiName(string CMapStr,string qishiZB,int theMaxMapNums)
    {
        GlobalMapDate.CCustomStr = CMapStr;
        _qishiZB = qishiZB;
        MaxMapNums = theMaxMapNums;
    }



    // Start is called before the first frame update
    void Start()
    {
        //GetMaps();
    }

    public void Test()
    {
        print("CreateMaps!!!");
    }

    string baoliuFX = "l";

    public List<string> GetMaps()
    {


        for(var i = 1; i <= MaxMapNums; i++)
        {
            
            cMapName = "map_" + cMapNum + "-" + i;

            string cMapMsg = cMapName;
            if (i == 1)
            {
                cMapMsg = cMapName + "!"+_qishiZB;
                mapZBArr.Add(cMapMsg);

                //保留方向 入场  根据角色进场来 保留  如果角色进来是 右进 这里保留左
                CreateMapFenZhi(i, cMapName);

            }
            else
            {

                if (GlobalMapDate.IsSpeMapByName(cMapName))
                {
                    CreateSpeMapByName(cMapName);
                }
                else
                {

                    if (i == mapZBArr.Count)
                    {
                        CreateMapFenZhi(i, cMapName);
                    }

                    //要判断 地图数组里面 是否已经有了 地图i
                    if (i == MaxMapNums)
                    {
                        //最后一个 地图
                        print("生成最后一个 地图");

                        //MapListMsgStr += "|" + cMapMsg;
                    }
                    else
                    {

                        //MapListMsgStr += "|"+cMapMsg;
                    }
                }



            }
        }



        //多关卡 取到新的关卡参数 开始新一轮循环
        //当前关卡有几个 分支  分支的 主要方向
        //如果只有一个分支 就直接在最后的 地图上 num最大值  否则在中间开始找 两边随机 避开特殊地图    是否有指定    没有指定的 单独做个 数组****




        print(" mapZBArr  "+ mapZBArr.Count);

        for(var s=0;s< mapZBArr.Count; s++)
        {
            print(s+" :  "+mapZBArr[s]);
        }

        Globals.mapZBArr = mapZBArr;

        //生成地图
        ShengChengImgMap();

        return mapZBArr;
    }
    GameObject _canva;
    List<GameObject> imgMapObjArr = new List<GameObject> { };

    void ShengChengImgMap()
    {

        _canva = GlobalTools.FindObjByName("MapUI");
        for (var i= 0;i<mapZBArr.Count;i++)
        {
            //print(i);
            //print(i+ " ----->   "+ mapZBArr[i]);
            string _name = mapZBArr[i].Split('!')[0];
            float _x = 200+float.Parse(mapZBArr[i].Split('!')[1].Split('#')[0]) * 102;
            float _y = 200+float.Parse(mapZBArr[i].Split('!')[1].Split('#')[1]) * 42;
            GameObject map =  GetGKImgAndPosition(_name,_x,_y);

            string[] fzList = mapZBArr[i].Split('!')[2].Split('^');
            for (var j=0;j< fzList.Length;j++)
            {
                if (IsHasImgObjInList(fzList[j].Split(':')[1])) continue;


                LianXianMaps(map, fzList[j].Split(':')[0]);
            }

        }
    }



    bool IsMapArrHasSpeMapByName(string speMapName)
    {
        for (int i=0;i< mapZBArr.Count;i++)
        {
            if(mapZBArr[i].Split('!')[0] == speMapName)
            {
                return true;
            }
        }
        return false;
    }

    void LianXianMaps(GameObject currentMap, string fx)
    {
        GameObject xian = null;
        if (fx == "u")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian2");
            xian.transform.position = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y + currentMap.GetComponent<RectTransform>().rect.height * 0.5f + xian.GetComponent<RectTransform>().rect.height * 0.4f);

        }
        else if (fx == "d")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian2");
            xian.transform.position = new Vector2(currentMap.transform.position.x, currentMap.transform.position.y - currentMap.GetComponent<RectTransform>().rect.height * 0.5f - xian.GetComponent<RectTransform>().rect.height * 0.4f);
        }
        else if (fx == "r")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian1");
            xian.transform.position = new Vector2(currentMap.transform.position.x + currentMap.GetComponent<RectTransform>().rect.width * 0.5f + xian.GetComponent<RectTransform>().rect.width * 0.4f, currentMap.transform.position.y);
        }
        else if (fx == "l")
        {
            xian = GlobalTools.GetGameObjectByName("map_xian1");
            xian.transform.position = new Vector2(currentMap.transform.position.x - currentMap.GetComponent<RectTransform>().rect.width * 0.5f - xian.GetComponent<RectTransform>().rect.width * 0.4f, currentMap.transform.position.y);
        }

        xian.name = currentMap.name + "#" + fx;
        xian.transform.parent = _canva.transform;
        //xian.GetComponent<GKMapXian>().LianJieMap(currentMap.name, gkImg.name);
    }


    bool IsHasImgObjInList(string _mapName)
    {
        foreach(GameObject o in imgMapObjArr)
        {
            if (o.name == _mapName) return true;
        }
        return false;
    }


    GameObject GetGKImgAndPosition(string _name, float px = 0, float py = 0)
    {
        GameObject gkImg = GlobalTools.GetGameObjectByName("gkImg");
        gkImg.transform.parent = _canva.transform;
        gkImg.transform.position = new Vector2(px, py);

        gkImg.name = _name;

        string _zb = GetCurrentMapZBByName(_name);
        if (GlobalMapDate.IsSpeMapByName(_name))
        {
            gkImg.GetComponent<gkImgTextTest>().GetText("<color=#ff1111>speMap</color>   " + _zb);
        }
        else
        {
            gkImg.GetComponent<gkImgTextTest>().GetText("<color=#000000>speMap</color>   " + _zb);
        }

        


        imgMapObjArr.Add(gkImg);

        return gkImg;
    }


    //根据 分支几率  能衍生几个分支
    int MapFenZhiJL(int i)
    {
        //这个地图衍生几个方向 去探索 当i越大 多方向概率越小
        //10 15 20 25
        int fxNums = 1;


        int jl = (int)UnityEngine.Random.Range(0, 100);
        string[] jvArr = DuoFenZhiJL.Split('-');

        //扩展几个边界  
        if (jl < int.Parse(jvArr[0]) * (100 - i) / 100)
        {
            fxNums = 3;
        }
        else if (jl < int.Parse(jvArr[1]) * (100 - i) / 100)
        {
            fxNums = 2;
        }
        return fxNums;
    }






    void CreateMapFenZhi2(int i, string cZXMapName)
    {
        List<string> tempKyFXArr = new List<string> { };
        bool isSepMap = false;
        isSepMap = GlobalMapDate.IsSpeMapByName(cZXMapName);

        if (isSepMap)
        {
            print("特殊地图名字------------------------------------------------------------->     " + cZXMapName);
            //tempKyFXArr = GlobalMapDate.GetFXListByName(cZXMapName);
            //判断各个方向是否有地图 有的话连接
            tempKyFXArr = GlobalMapDate.GetFXListByName(cZXMapName);
            print(tempKyFXArr.Count);
            foreach (string fx in tempKyFXArr)
            {
                print(" 特殊地图fx  " + fx);
            }
        }
        else
        {
            if (i == 1)
            {
                //保留方向 和 出口方向
                tempKyFXArr.Add("r");
            }
            else
            {
                //这个地图衍生几个方向 去探索 当i越大 多方向概率越小
                //10 15 20 25
                int fxNums = MapFenZhiJL(i);


                //判断周围 看看是否坐标被占用 找出空位比较 空地
                List<string> fxArr = new List<string> { "l", "u", "d", "r" };
                //List<string> fxArr = new List<string> { "l", "r" };
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

                if (maxNum <= fxNums)
                {
                    //如果最大的空位 小于扩展数  则直接用数组里面的位置来 生成地图
                    tempKyFXArr = kyFXArr;
                }
                else
                {
                    //方向的比重
                    //List<string> fxzhi = new List<string> { "l-5", "d-10", "u-15", "r-70" };
                    //随机选取方向 生成方向数组  
                    tempKyFXArr = ChoseFXArr(FXBiZhi, kyFXArr, fxNums);
                }
            }
        }

        //获取最大地图的名字
        //maxI = theMapArr.Count;
        //maxI = int.Parse(theMapArr[theMapArr.Count-1].name.Split('-')[1]);
        CreateMapByKyFXArr(tempKyFXArr, cZXMapName);
        return;
    }





    //可以用的坐标  坐标上没有地图
    List<string> kyFXArr = new List<string> { };

    //创建地图的分支 上下左右
    void CreateMapFenZhi(int i, string cZXMapName)
    {
        List<string> tempKyFXArr = new List<string> { };
        bool isSepMap = false;
        isSepMap = GlobalMapDate.IsSpeMapByName(cZXMapName);
        //判断是否是特殊地图
        if (isSepMap)
        {
            print("特殊地图名字------------------------------------------------------------->     " + cZXMapName);
            //tempKyFXArr = GlobalMapDate.GetFXListByName(cZXMapName);
            //判断各个方向是否有地图 有的话连接
            kyFXArr.Clear();
            kyFXArr = GlobalMapDate.GetFXListByName(cZXMapName);
            print(kyFXArr.Count);
            foreach(string fx in kyFXArr)
            {
                print(" 特殊地图fx  "+fx);
            }

            //获取 特殊地图的可用 方向 
            for (int s = 0; s < kyFXArr.Count; s++)
            {
                if (!IsHasMap(kyFXArr[s], cZXMapName))
                {
                    tempKyFXArr.Add(kyFXArr[s]);
                }
            }


            if (tempKyFXArr.Count == 0) return;

        }
        else
        {

            if(i == 1)
            {
                //保留方向 和 出口方向
                tempKyFXArr.Add("r");
            }
            else
            {
                //这个地图衍生几个方向 去探索 当i越大 多方向概率越小
                //10 15 20 25
                int fxNums = MapFenZhiJL(i);


                //判断周围 看看是否坐标被占用 找出空位比较 空地
                List<string> fxArr = new List<string> { "l", "u", "d", "r" };
                //List<string> fxArr = new List<string> { "l", "r" };
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

                if (maxNum <= fxNums)
                {
                    //如果最大的空位 小于扩展数  则直接用数组里面的位置来 生成地图
                    tempKyFXArr = kyFXArr;
                }
                else
                {
                    //方向的比重
                    //List<string> fxzhi = new List<string> { "l-5", "d-10", "u-15", "r-70" };
                    //随机选取方向 生成方向数组  
                    tempKyFXArr = ChoseFXArr(FXBiZhi, kyFXArr, fxNums);
                }
            }





          

            
        }

        //获取最大地图的名字
        //maxI = theMapArr.Count;
        //maxI = int.Parse(theMapArr[theMapArr.Count-1].name.Split('-')[1]);
        CreateMapByKyFXArr(tempKyFXArr, cZXMapName);
        return;

    }



   

    [Header("是否让特殊 数组 更多的连在一条线")]
    public bool IsTeShuMapLianzaiyiqi = true;

    //通过 方向 来创建 地图img
    void CreateMapImgByFX(string cZXMapName, string fx, string newMapName)
    {
        //tempSpeMapList.Clear();
        //判断是否是特殊地图  并找到安防特殊地图的位置      --如果是特殊的地图 看能不能对向相连 否则就存入数组
        //newMapName = IsSpeMapAddMaxI(newMapName);
        //CreateOutMap(currentMap, fx, newMapName, theCZXMapName);
        //print("?????????????????????????????????????????????????????????   theCZXMapName  "+ theCZXMapName+ "   newMapName    "+ newMapName);

        //1.新地图的 信息   中心 地图
        string newMapZB = GetZBByFX(fx, cZXMapName);
        print("  zxMapName "+cZXMapName+" zb  "+newMapZB);
        for (var i=0; i< mapZBArr.Count; i++)
        {
            if(mapZBArr[i].Split('!')[0] == cZXMapName)
            {
                //map_s-2!1#0!l:map_s-1^u:map_s-4^d:map_s-5
                print("mapArrZB -------?   "+mapZBArr[i] + "   fx   "+fx+ "  newMapName   "+ newMapName);
                //map_s-1!0#0   fx   r  newMapName   map_s-2
                GXZXMapMsg(mapZBArr[i],fx,newMapName);
                //将新生成的 分支地图信息 加入 总的 地图信息数组
                TianjiaNewMapMsgInMapArr(cZXMapName, fx, newMapName, newMapZB);

                //TianjiaNewMapMsgInMapArr();

                break;
            }
        }


        //如果特殊地图数组有数据
        //if (!IsTeShuMapLianzaiyiqi && tempSpeMapList.Count > 0)
        //{
        //    CreateSpeMap();
        //}
    }

    //跟新中心 地图数据
    void GXZXMapMsg(string mapStrMsg,string fx,string newMapName) {
        string zxMapName = mapStrMsg.Split('!')[0];
        string zxMapZB = mapStrMsg.Split('!')[1];
        string fzMsg = "";
        if (mapStrMsg.Split('!').Length == 3)
        {

            fzMsg += mapStrMsg.Split('!')[2] + "^" + fx + ":" + newMapName;

        }
        else
        {
            fzMsg = fx + ":" + newMapName;
        }

        string mapMsg = zxMapName + "!" + zxMapZB + "!" + fzMsg;
        print(" 跟新中心地图 的 信息   "+mapMsg);
        mapZBArr.Remove(mapStrMsg);
        mapZBArr.Add(mapMsg);
    }


    void TianjiaNewMapMsgInMapArr(string cZXMapName,string fx,string newMapName,string newMapZB)
    {
        string ffx = "";
        if (fx == "l")
        {
            ffx = "r";
        }
        else if (fx == "r")
        {
            ffx = "l";
        }
        else if (fx == "u")
        {
            ffx = "d";
        }
        else if (fx == "d")
        {
            ffx = "u";
        }

        string newMapMsgStr = newMapName + "!" + newMapZB + "!" + ffx + ":" + cZXMapName;
        print(" 添加的 新地图信息  "+newMapMsgStr);
        mapZBArr.Add(newMapMsgStr);
    }



    string IsMapNameHasMapGetNewName(string _mapName)
    {

        for (int i = 0; i < mapZBArr.Count; i++)
        {
            if (_mapName == mapZBArr[i].Split('!')[0]||GlobalMapDate.IsSpeMapByName(_mapName))
            {
                _mapName = _mapName.Split('-')[0] + "-" + (int.Parse(_mapName.Split('-')[1]) + 1);
                return IsMapNameHasMapGetNewName(_mapName);
            }
        }

        return _mapName;
    }


   

    //这里是绕开 所有特殊地图名  是特殊地图的花 名字+1 
    string IsSpeMapAddMaxI(string newMapName)
    {
        //先判断 名字是否被占用
        newMapName = IsMapNameHasMapGetNewName(newMapName);
        if (GlobalMapDate.IsSpeMapByName(newMapName))
        {
            //要判断 数组中是否已经有该特殊数组了
            if (!tempSpeMapList.Contains(newMapName)) tempSpeMapList.Add(newMapName);
            print("特殊地图名字？ " + newMapName);
            newMapName = newMapName.Split('-')[0] + "-" + (int.Parse(newMapName.Split('-')[1]) + 1);
            print("newMapName 1     " + newMapName);
            if (GlobalMapDate.IsSpeMapByName(newMapName))
            {
                print("特殊地图  " + newMapName);
                return IsSpeMapAddMaxI(newMapName);
            }
        }
        //print("------生成的名字   "+newMapName);
        return newMapName;
    }





    bool IsHasMap(string fx, string cZXMapName)
    {
        string _zb = GetZBByFX(fx, cZXMapName);
        for (int i = 0; i < mapZBArr.Count; i++)
        {
            if (_zb == mapZBArr[i].Split('!')[1]) return true;
        }
        return false;
    }


    bool IsHasMapByZXMapZB(string fx,string _zb)
    {

        string zb = GetNewZBByFX(_zb,fx);

        for (int i = 0; i < mapZBArr.Count; i++)
        {
            print( "对比的坐标  "+_zb+ "检车地图 和坐标  " + mapZBArr[i].Split('!')[0]+"    坐标是  "+ mapZBArr[i].Split('!')[1]);
            if (zb == mapZBArr[i].Split('!')[1]) return true;
        }
        return false;
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



    // Update is called once per frame
    void Update()
    {
        
    }



    //还给一种 选择方向 大概率只选1个方向  有右选右 没有再在可以用的方向 随机



    /// <summary>
    /// 
    /// </summary>
    /// <param name="fxzhi">分支选中的几率值组</param>
    /// <param name="kyFXArr">可以选择的方向数组</param>
    /// <param name="canChoseNums">可以选择的方向数</param>
    /// <returns></returns>
    List<string> ChoseFXArr(List<string> fxzhi, List<string> kyFXArr, int canChoseNums)
    {
        // fxzhi  "l-5", "d-10", "u-15", "r-70"  方向几率值
        List<string> choseArr = new List<string> { };
        List<string> tempFXJLArr = new List<string> { };

        if (GlobalTools.GetRandomNum() > 30)
        {
            canChoseNums = 1;
        }


        for (int i = 0; i < canChoseNums; i++)
        {
            int jl = (int)UnityEngine.Random.Range(0, 100);
            float fenmu = 0;
            tempFXJLArr.Clear();
            //获取可用方向数组的 分母值
            //u,r,l
            for (int s = 0; s < kyFXArr.Count; s++)
            {
                for (int j = 0; j < fxzhi.Count; j++)
                {
                    if (kyFXArr[s] == fxzhi[j].Split('-')[0])
                    {
                        fenmu += float.Parse(fxzhi[j].Split('-')[1]);
                        tempFXJLArr.Add(fxzhi[j]);
                    }
                }
            }


            string choseFX = null;

            for (int c = 0; c < tempFXJLArr.Count; c++)
            {
                if (jl <= int.Parse(tempFXJLArr[c].Split('-')[1]) / fenmu * 100)
                {
                    choseFX = tempFXJLArr[c].Split('-')[0];
                    choseArr.Add(choseFX);
                    //tempFXJLArr.Remove(tempFXJLArr[c]);
                    for (int n = 0; n < kyFXArr.Count; n++)
                    {
                        if (kyFXArr[n] == tempFXJLArr[c].Split('-')[0])
                        {
                            kyFXArr.Remove(kyFXArr[n]);
                        }
                    }
                    break;
                }
            }

            if (choseFX == null)
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



    string GetZBByFX(string fx, string cZXMapName)
    {
        string currentMapZB = GetCurrentMapZBByName(cZXMapName);
        print(" currentMapZB  "+ currentMapZB);
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
        print(" ????????????????cZXMapName "+ cZXMapName);
        for (int i = 0; i < mapZBArr.Count; i++)
        {
            print(i+"  ?? "+ mapZBArr[i]);
            if (mapZBArr[i].Split('!')[0] == cZXMapName)
            {
                zb = mapZBArr[i].Split('!')[1];
                return zb;
            }
        }
        return zb;
    }







    //特殊地图--------------------------------------------------------------------------
    void CreateSpeMap()
    {
        for (int i = 0; i < tempSpeMapList.Count; i++)
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
        string mapName = FindLJSpeMapDeMap(SpeMapFXArr, speMapName);

    }



  

    string FindLJSpeMapDeMap(List<string> _SpeMapFXArr, string speMapName)
    {
        string findFX = "";
        List<string> SpeMapFXArr = new List<string> { };
        //string findZB = "";
        print("speMapName    "+ speMapName);
        for (int i = mapZBArr.Count - 1; i >= 0; i--)
        {
            SpeMapFXArr = _SpeMapFXArr;
            for (int j = SpeMapFXArr.Count-1; j >= 0; j--)
            {
                //寻找放置方向
                if (SpeMapFXArr[j] == "l")
                {
                    findFX = "r";
                }
                else if (SpeMapFXArr[j] == "d")
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

                //如果是特殊地图就跳出当前循环  特殊地图是不能增加连接的  因为 门数量固定了
                if (GlobalMapDate.IsSpeMapByName(cMapName)) break;

                //根据 findFX 找到 适合的 地图  1.该方向上有空位

                //1判断 该位置 是否有 地图了 2判断特殊 地图在该位置 各个方向 是否有地图 


                if (!IsHasMap(findFX, cMapName))
                {

                    //这里 要找  这个坐标 特殊地图 另外几个方向上 是否 有地图    有的话就 continue

                    


                    print("  特殊地图 找到的 可以连接的 中心地图 名字 " + cMapName);
                    //获取这个空位坐标
                    string _zb = GetZBByFX(findFX, cMapName); //这个坐标上面没有地图  这个坐标就是特殊地图 选的位置


                    //除开一个 fx
                    List<string> shengyuFXArr = SpeMapFXArr;
                    print("中心地图是 " + cMapName + "  找到的给特殊地图 " + speMapName + "可以用的 zb " + _zb + "    移除的方向是    " + SpeMapFXArr[j]);
                    shengyuFXArr.Remove(SpeMapFXArr[j]);
                    print("剩余 几个方向: "+ shengyuFXArr.Count);

                    foreach(string fx in shengyuFXArr)
                    {
                        print(fx);
                    }

                    if (shengyuFXArr.Count != 0 && IsZXMapFXHasMap(shengyuFXArr, _zb)) continue;

                   
                    List<string> speMapFXArr = GlobalMapDate.GetFXListByName(speMapName);
                    print(speMapName+"   分支数量  " +speMapFXArr.Count);

                    print(cMapName+" fx "+ findFX+"  j "+j+"  length  "+ shengyuFXArr.Count +"   ??特殊地图名字   "+ speMapName);
                    print("  ??  "+ speMapFXArr[j]);
                    //获取这个特殊地图已经连接的方向  这个方向要剔除
                    string hasLianJieFX = speMapFXArr[j];
                    //获取剩余方向数组  在上面创建分支 如果遇到特殊地图 就将特殊数组存进临时数组   （计数 如果在特殊数组后开始 就连接上了）
                    speMapFXArr.Remove(hasLianJieFX);


                    //获取中心地图信息
                    string zxMapMsg = GetMapMsgByName(cMapName);
                    //跟新 地图信息 中心地图 新加一个地图
                    GXZXMapMsg(zxMapMsg, findFX, speMapName);
                    //记录 特殊 地图信息
                    TianjiaNewMapMsgInMapArr(cMapName, findFX, speMapName, _zb);

                    //创建分支数据
                    CreateSpeMapFZByFXList(speMapFXArr, speMapName);

                    return null;

                }
            }

        }
        return null;
    }




    string GetMapMsgByName(string mapName)
    {
        foreach(string msg in mapZBArr)
        {
            if (msg.Split('!')[0] == mapName) return msg;
        }
        return "";
    }


    //特殊地图分支
    void CreateSpeMapFZByFXList(List<string> FXlist, string cZXMapName)
    {
        print("现在是哪个特殊地图的分支 ---    " + cZXMapName);
        int createMapNums;
        for (int c = 0; c < FXlist.Count; c++)
        {
            maxI = GetMapZBArrMaxNum(mapZBArr);
            //maxI  取mapZBArr中的名字最大值
            createMapNums = maxI + 1;
            int nums = GetNotUseNums(mapZBArr);
            if (nums != 1) createMapNums = nums;

            //获得名字  先查找名字是否被占用
            string _newMapName = "map_" + GlobalMapDate.CCustomStr + "-" + createMapNums;
            print("特殊地图 分支 名字1 " + _newMapName);
            //判断是否是特殊地图 是的话 名字加1 将该名字存进 特殊地图数组
            _newMapName = IsSpeMapNewFZMapNameIsSpeName(_newMapName);

            //判断 是否是 特殊地图  是的话判断是否能连
            //if (GlobalMapDate.IsSpeMapByName(_newMapName))
            //{
            //    print(" ********** 新创建的 分支 地图是 特殊地图 " + _newMapName);
            //    //获取 特殊 数组的 方向 数组
            //    //List<string> speMapFXList = GlobalMapDate.GetFXListByName(_newMapName);
            //    //只连特殊 数组了 可以 直接 return了
            //    //tempSpeMapList.Add(_newMapName);
            //    //CreateSpeMap();

            //    //GetSpeMapSetPos(speMapFXList, FXlist[c], cZXMapName, _newMapName);
            //    continue;
            //}


            string newMapZB = GetZBByFX(FXlist[c], cZXMapName);

            print("-----------------------------------------------------------特殊地图 " + cZXMapName + "    " + FXlist[c] + " 侧 分支" + _newMapName);
            //跟新中心地图
            GXZXMapMsg(GetMapMsgByName(cZXMapName), FXlist[c], _newMapName);
            //记录旧地图
            TianjiaNewMapMsgInMapArr(cZXMapName, FXlist[c], _newMapName, newMapZB);
        }
    }

    string IsSpeMapNewFZMapNameIsSpeName(string speFZMapName)
    {
        //判断名字是否被占用
        if (GlobalMapDate.IsSpeMapByName(speFZMapName))
        {
            if (!tempSpeMapList.Contains(speFZMapName)) tempSpeMapList.Add(speFZMapName);
            speFZMapName = speFZMapName.Split('-')[0] + "-" + (int.Parse(speFZMapName.Split('-')[1]) + 1);
            if (GlobalMapDate.IsSpeMapByName(speFZMapName))
            {
                IsSpeMapNewFZMapNameIsSpeName(speFZMapName);
            }
        }
        speFZMapName = IsMapNameHasMapGetNewName(speFZMapName);
        return speFZMapName;
    }




    int GetNotUseNums(List<string> _mapZBArrList)
    {
        //1 2 4 5 7
        int nums = 1;
        List<int> numsList = new List<int> { };
        for (int i = 0; i < _mapZBArrList.Count; i++)
        {
            string nameStr = _mapZBArrList[i].Split('!')[0];
            int tempNums = int.Parse(nameStr.Split('-')[1]);
            numsList.Add(tempNums);
            //if (tempNums > nums) nums = tempNums;
        }

        numsList.Sort();

        for(int j=0;j< numsList.Count; j++)
        {
            if (numsList[j] == nums) nums++;
        }

        return nums;
    }




    int GetMapZBArrMaxNum(List<string> _mapZBArrList)
    {
        int nums = 1;
        for(int i = 0; i < _mapZBArrList.Count; i++)
        {
            string nameStr = _mapZBArrList[i].Split('!')[0];
            int tempNums = int.Parse(nameStr.Split('-')[1]);

            if (tempNums > nums) nums = tempNums;
        }
        return nums;
    }




    int maxI = 0;
    List<string> tempSpeMapList = new List<string> { };
    void CreateMapByKyFXArr(List<string> tempKyFXArr, string cZXMapName)
    {
        tempSpeMapList.Clear();
        int createMapNums;
        for (int c = 0; c < tempKyFXArr.Count; c++)
        {
            maxI = GetMapZBArrMaxNum(mapZBArr);
            //maxI  取mapZBArr中的名字最大值
            createMapNums = maxI + 1;
            int nums = GetNotUseNums(mapZBArr);
            if (nums != 1) createMapNums = nums;
            print(" ----------->中心地图 名字    "+ cZXMapName+ "     创建了几个分支   "+ tempKyFXArr.Count+"     当前生成分支方向 "+ tempKyFXArr[c]);
            //如果大于最大的  并且中心地图不是特殊地图  数组数  直接跳出  没有分支了    特殊地图除外
            if (createMapNums > MaxMapNums && !GlobalMapDate.IsSpeMapByName(cZXMapName)) {
                return;
            }
            else
            {
                if(createMapNums > MaxMapNums) print("中心地图是特殊地图 " + cZXMapName + "  最大nums    " + MaxMapNums + " createMapNums    " + createMapNums);
            }
            //maxI = theMapArr.Count;
            //createMapNums = maxI+1;
            //获得名字  先查找名字是否被占用
            string _newMapName = "map_" + cMapNum + "-" + createMapNums;
            
            //判断名字是否被占用？？ 特殊地图会直接走完 不会超出数组长度 这里不需要判断 名字不会被占用？
            _newMapName = IsMapNameHasMapGetNewName(_newMapName);

            //如果是 特殊 数组 看看能不能连上 中心数组  不能的话怎么办？
            if (GlobalMapDate.IsSpeMapByName(_newMapName))
            {
                print(" ********** 新创建的 分支 地图是 特殊地图 " + _newMapName);
                //获取 特殊 数组的 方向 数组
                //List<string> speMapFXList = GlobalMapDate.GetFXListByName(_newMapName);

                //只连特殊 数组了 可以 直接 return了
                tempSpeMapList.Add(_newMapName);
                //CreateSpeMap();

                //GetSpeMapSetPos(speMapFXList, tempKyFXArr[c], cZXMapName, _newMapName);
                continue;
            }

            print("创建的 " + tempKyFXArr[c] + "分支 地图名字  " + _newMapName);
            //创建地图 生成名字 和坐标 存入两个数组
            CreateMapImgByFX(cZXMapName, tempKyFXArr[c], _newMapName);
        }

        //如果有特殊 数组  记录特殊数组 位置

        //if (IsTeShuMapLianzaiyiqi && tempSpeMapList.Count > 0)
        //{
        //    CreateSpeMap();
        //}

    }


    //其他方向 是否有地图
    bool IsOtherFXHasNoMap(List<string> speMapFXArr,string fx,string zb)
    {
        for(int i = 0; i < speMapFXArr.Count; i++)
        {
            if (speMapFXArr[i] != fx)
            {
                if (IsHasMapByZXMapZB(speMapFXArr[i], zb)) return false;
            }
        }
        return true;
    }


    void GetSpeMapSetPos(List<string> speMapFXArr, string CMapLJFX, string cZXMapName, string newMapName)
    {
        for(int i=0;i< speMapFXArr.Count; i++)
        {
            if (KeLianJieFX(speMapFXArr[i], CMapLJFX)!=null)
            {
                //string _zb = GetZBByFX(cZXMapName,CMapLJFX);
                ////判断 其他方向没有 地图
                //if (!IsOtherFXHasNoMap(speMapFXArr, speMapFXArr[i],_zb))
                //{
                //    return;
                //}
                CreateMapImgByFX(cZXMapName, CMapLJFX, newMapName);
                return;
            }
        }
        //如果没有地方安置特殊数组 
        maxI++;
        string _newMapName = "map_" + cMapNum + "-" + (maxI+1);
        if (GlobalMapDate.IsSpeMapByName(_newMapName))
        {
            List<string> speMapFXList = GlobalMapDate.GetFXListByName(_newMapName);
            GetSpeMapSetPos(speMapFXList, CMapLJFX, cZXMapName, _newMapName);
        }
        else
        {
            CreateMapImgByFX(cZXMapName, CMapLJFX, _newMapName);
        }

    }

    string KeLianJieFX(string fx,string CMapLJFX)
    {
        string ffx = "";
        if (fx == "l")
        {
            ffx = "r";
        }
        else if (fx == "r")
        {
            ffx = "l";
        }
        else if (fx == "u")
        {
            ffx = "d";
        }
        else if (fx == "d")
        {
            ffx = "u";
        }
        if(ffx == CMapLJFX)
        {
            return fx;
        }
        return null;
    }




    bool IsZXMapFXHasMap(List<string> FXArr, string zb)
    {
        for (var i = 0; i < FXArr.Count; i++)
        {
            print("i   "+i);
            if (IsHasMapByZXMapZB(FXArr[i], zb)) return true;
        }
        return false;
    }




}
