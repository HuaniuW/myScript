﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReMap : MonoBehaviour
{
    [Header("连接点类型")]
    public string LianjiandianType = "";

    [Header("地板类型")]
    public string DibanType = "";
    [Header("前景1")]
    public string QianJingType_1 = "";
    [Header("前景2")]
    public string QianJingType_2 = "";

    [Header("背景1")]
    public string BejingType_1 = "";

    [Header("背近远景1")]
    public string BeiJinYuanjingType_1 = "";
    [Header("背近远景2")]
    public string BeiJinYuanjingType_2 = "";

    [Header("背远景1")]
    public string BeiYuanjingType_1 = "";

    [Header("超大背景")]
    public string CDBg = "";

    [Header("修饰粒子")]
    public string XiushiLizi_1 = "";

    [Header("雾")]
    public string WuType_1 = "";


    [Header("最大 地图 分支 衍生的数量")]
    public int MaxMapNums = 2;

    //---------------------调试 单个地图 直接生成 的 模块 不用的时候 都清空  目前只用作测试--------------------------

    [Header("是否是调试 调试的话用到下面的 信息")]
    public bool IsTiaoshi = false;

    [Header("当前 关卡名字")]
    public string CMapName = "";

    //{l:map_r-2,r:map_r-3}
    [Header("当前 门 方向列表")]
    public List<string> ThisMenFXList = new List<string>() { };
    [Header("从哪个门进入的 注意是反的 如果是 l 出来是r位置的门  ")]
    public string CongNaGeMenjinru = "";



    //地形生成
    // Start is called before the first frame update
    void Start()
    {
        GetInDate();


        //设置寻路
        //GlobalTools.FindObjByName("A_").GetComponent<>;
        //GlobalTools.FindObjByName("A_").GetComponent<AstarPath>().;
        //GlobalTools.FindObjByName("A_").transform.position = GlobalTools.FindObjByName("kuang").transform.position;

        //GlobalTools.FindObjByName("A_").GetComponent<AstarPath>().Scan();
        

        kuang = GlobalTools.FindObjByName("kuang");

        Ax = GlobalTools.FindObjByName("A_");

        StartCoroutine(IEDestory2ByTime(0.1f));
    }

    bool IsDuiqiAx = false;
    GameObject kuang;
    GameObject Ax;

    // Update is called once per frame
    void Update()
    {
        if (IsDuiqiAx)
        {
            if(Ax.transform.position!= kuang.transform.position)
            {
                Ax.transform.position = kuang.transform.position;
                //Ax.GetComponent<AstarPath>()
                Ax.GetComponent<AstarPath>().Scan();
                //GlobalTools.FindObjByName("A_").transform.position = GlobalTools.FindObjByName("kuang").transform.position;
                //GlobalTools.FindObjByName("A_").GetComponent<AstarPath>().Scan();

                IsDuiqiAx = false;
            }
        }
    }


    public IEnumerator IEDestory2ByTime(float time)
    {
        yield return new WaitForSeconds(time);
        //yield return new WaitForFixedUpdate();
        //Debug.Log(">>   "+ gameObject);
        print("寻路位置对齐！！！！！");
        IsDuiqiAx = true;

        //GlobalSetDate.instance.IsCMapHasCreated = true;

        if (!GlobalSetDate.instance.IsCMapHasCreated)
        {
            SetMapMsgDateInStr();
        }
        //GlobalTools.FindObjByName("A_").transform.position = GlobalTools.FindObjByName("kuang").transform.position;
        //GlobalTools.FindObjByName("A_").GetComponent<AstarPath>().Scan();
    }


    //对比 当前地图内部的 地图 名字 是否就是需要生成的名字  用在 从非随机地图 进入 随机地图    这样可以不用生成   从随机地图 进入 非随机地图的时候 记录一下 是哪个Rmap_1 还是2 出来的

    //记录 关卡名字   获取地图的门信息

    //取数据 看看有没有关卡 的数据 有的话 直接 取数据生成地图  没有的话 生成地图

    //几个门 怎么连接  生成的 地图地形连接数

    //生成地图 判断 生成类型
    //户外 还是 洞内 还是混合
    //地板 是 石头地板 还是草皮地板  还是 茂盛的植被地板 等
    //景  前景 是什么  后景是什么
    //粒子效果
    //

    GameObject maps;

    //当前地图 信息  eg->   map_1-1!0#0!r:map_1-2  
    string theMapMsg = "";
    public void GetInDate()
    {

        //判断 是否 有这个地图的数据 有的话 直接按数据生成地图
        print("************************************************************************************************************************");

        maps = GlobalTools.FindObjByName("maps");

        if (IsTiaoshi&& CMapName != "")
        {
            GlobalSetDate.instance.CReMapName = CMapName;
            GlobalMapDate.CCustomStr = CMapName.Split('_')[1];
        }
        else
        {
            CMapName = GlobalSetDate.instance.CReMapName;
        }

        print("本地图名字 "+GlobalSetDate.instance.CReMapName);
        if (GlobalSetDate.instance.CReMapName == "") return;



        //获取门信息
        if (IsTiaoshi && ThisMenFXList.Count!=0) {
            //{l:map_r-2,r:map_r-3}
            menFXList = ThisMenFXList;
        }
        else
        {
            menFXList = GetMenFXListByMapName(GlobalSetDate.instance.CReMapName);
            ThisMenFXList = menFXList;
        }
        print("menFXList   " + menFXList);

        //foreach (string m in menFXList) print(m);
        //这里要知道 从哪进来的 进入方向   保留一个门
        //-
        if (IsTiaoshi && CongNaGeMenjinru != "")
        {
            GlobalSetDate.instance.DanqianMenweizhi = CongNaGeMenjinru;
        }
        else
        {
            CongNaGeMenjinru = GlobalSetDate.instance.DanqianMenweizhi;
        }
        print("从哪个位置的门进来的  "+ GlobalSetDate.instance.DanqianMenweizhi);
        //判断 全局数据中 是否有 地图 有的话  取出来
        string mapDateMsg = GetMapMsgDateByName(GlobalSetDate.instance.CReMapName);
        if (mapDateMsg != "")
        {
            print("根据 数据 来生成地图");
            GlobalSetDate.instance.IsCMapHasCreated = true;
            CreateMapByMapMsgDate(mapDateMsg);
            //获取 门信息

            //角色站位 和方向
            GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetPlayerPosByFX();
            return;
        }
        
        //判断是 哪种景色  洞外 洞内  相应的 景 图片是哪些

        //随机出每个 分支 的地图数量
        string type = GetTypeByMenFXList(menFXList);
    }


    void CreateMapByMapMsgDate(string mapMsgDate)
    {
        print("->根据地图数据 生成地图 mapMsgDate "+ mapMsgDate);
        GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().ClearListDoor();
        string[] mapMsgArr = mapMsgDate.Split('|');
        foreach(string s in mapMsgArr)
        {
            string _name = s.Split('!')[0];
            string _pos = s.Split('!')[1];
            string _sd = s.Split('!')[2];

            string _goScreenName = "";
            string _reMapName = "";
            string _dangQianMenWeiZhi = "";

            if (s.Split('$').Length > 1)
            {
                _name = s.Split('$')[0].Split('!')[0];
                _pos = s.Split('$')[0].Split('!')[1];
                _sd = s.Split('$')[0].Split('!')[2];

                if (_name.Split('_')[0] == "wu")
                {

                }
                else
                {
                    _goScreenName = s.Split('$')[1].Split('!')[0];
                    _reMapName = s.Split('$')[1].Split('!')[1];
                    _dangQianMenWeiZhi = s.Split('$')[1].Split('!')[2];
                }
            }


            if (_name == "kuang")
            {
                GameObject kuang = GlobalTools.FindObjByName("kuang");
                Vector2 kuangPos = new Vector2(float.Parse(_pos.Split('#')[0]), float.Parse(_pos.Split('#')[1]));
                kuang.transform.position = kuangPos;
                Vector2 _size = new Vector2(float.Parse(_sd.Split('#')[0]), float.Parse(_sd.Split('#')[1]));
                kuang.GetComponent<BoxCollider2D>().size = _size;
                GlobalTools.FindObjByName("MainCamera").GetComponent<CameraController>().GetBounds(kuang.GetComponent<BoxCollider2D>());
            }
            else
            {
                GameObject mapObj = GlobalTools.GetGameObjectByName(_name);
                mapObj.transform.parent = maps.transform;
                mapObj.transform.position = new Vector3( float.Parse(_pos.Split('#')[0]), float.Parse(_pos.Split('#')[1]), float.Parse(_pos.Split('#')[2]));
                int theSD = 0;
                if (_sd!="") theSD = int.Parse(_sd);
                print(_name+" position "+ mapObj.transform.position+"   sd  "+_sd);

                if (mapObj.GetComponent<DBBase>())
                {
                    mapObj.GetComponent<DBBase>().SetSD(theSD);
                }
                else
                {
                    if(mapObj.GetComponent<SpriteRenderer>()) mapObj.GetComponent<SpriteRenderer>().sortingOrder = theSD;
                }

                //如果是门数据
                if (mapObj.GetComponent<RMapMen>())
                {
                    GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetDoorInList(mapObj);
                    mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg2(_goScreenName, _dangQianMenWeiZhi, _reMapName);
                }

                if(_name.Split('_')[0] == "wu")
                {
                    Vector3 sf = GlobalTools.VParse(s.Split('$')[1].Split('!')[0]);
                    Color _color = GlobalTools.ColorParse(s.Split('$')[1].Split('!')[1]);
                    mapObj.transform.localScale = sf;
                    mapObj.GetComponent<SpriteRenderer>().color = _color;
                }
                
            }
        }
    }



    string GetMapMsgDateByName(string mapName)
    {
        string[] mapMsgDateArr = GlobalSetDate.instance.gameMapDate.MapDate.Split('@');
        foreach(string s in mapMsgDateArr)
        {
            if (s.Split('=')[0] == mapName) return s.Split('=')[1];
        }
        return "";
    }




    string GetTypeByMenFXList(List<string> menFXList)
    {

        GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().ClearListDoor();

        //这里 做了排序     lr=l:mapR_1$p!r:map_r-3
        string str = GetStrByList(menFXList);
        print("str    "+str);
        //取中心连接点 地图 以及 随机出 每个 朝向的 模块数量    模块的最少数是多少？ 左右是1  上下是2（多一个直连+转弯） 然后连接门  门不算进数量 数量走完连接门
        //数元素 自带 落叶粒子
        // lr=l:mapR_1?3?pd?shu!r:map_r-2?3?dn?qiang
        
        string lianjie = str.Split('=')[0];
        if (LianjiandianType != "") lianjie = lianjie + "_" + LianjiandianType;
        CreateLJByName(lianjie);

        //str    lr=l:mapR_1!r:map_r-2
        //往哪个方向延伸地图
        CreateBianlu(str.Split('=')[1]);



        GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetPlayerPosByFX();

        return "";
    }

    

    void CreateBianlu(string mapStrMsg)
    {
        List<string> strList = new List<string>(mapStrMsg.Split('!'));
        for (int i = 0; i < strList.Count; i++){
            string fx = strList[i].Split(':')[0];
            //获取 生成的 地图块 数量
            int n = 1 + GlobalTools.GetRandomNum(MaxMapNums);
            string _goScreenName = strList[i].Split(':')[1];
            print("门位置 "+ strList[i].Split(':')[0] + "  _goScreenName    "+ _goScreenName);
            if(strList.Count == 1)
            {
                if(fx == "d")
                {
                    CreateFZRoad("l", n, _goScreenName,fx);
                }
                else if(fx == "u")
                {
                    CreateFZRoad("r", n, _goScreenName,fx);
                }
                else
                {
                    CreateFZRoad(fx, n, _goScreenName);
                }
            }
            else
            {
                CreateFZRoad(fx, n, _goScreenName);
            }
            
        }

        //计算边界 设置 CameraKuai  和 A*
        GetKuaiBianjie(mapObjArr);




        //foreach (Transform child in maps.transform)
        //{
        //    //print("name "+ child.gameObject.name);
        //    Debug.Log("组成地图对象>  name: "+child.gameObject.name+"  pos: "+ child.position+"   sd: "+ child.GetComponent<DBBase>().GetSD());
        //    string _name = child.gameObject.name.Split('(')[0];
        //    string _pos = child.position.x + "#" + child.position.y + "#" + child.position.z;
        //    string _sd = "";
        //    if (child.GetComponent<DBBase>()) {
        //        _sd = child.GetComponent<DBBase>().GetSD().ToString();
        //    }
        //    else
        //    {
        //        _sd = child.gameObject.GetComponent<SpriteRenderer>().sortingOrder.ToString();
        //    }
        //    string gkMapMsg = _name + "!" + _pos + "!" + _sd;

        //}

        
    }


    string MapMsgStr = "";
    //存入生成的地图数据  手动存入 也是 调用这个方法
    public void SetMapMsgDateInStr(bool IsShouDong = false)
    {
        print("储存 地图地形 数据！！！！！！！！！！！");
        MapMsgStr = "";
        for (int i = 0; i < maps.transform.childCount; i++)
        {
            Transform child = maps.transform.GetChild(i);
            string _name = child.gameObject.name.Split('(')[0];
            string _pos = child.position.x + "#" + child.position.y + "#" + child.position.z;
            string _sd = "";
            if (child.GetComponent<DBBase>())
            {
                _sd = child.GetComponent<DBBase>().GetSD().ToString();
            }
            else
            {
                if(child.gameObject.GetComponent<SpriteRenderer>()) _sd = child.gameObject.GetComponent<SpriteRenderer>().sortingOrder.ToString();
            }

            string gkMapMsg = _name + "!" + _pos + "!" + _sd;
            if (child.GetComponent<RMapMen>())
            {
                //如果是门 记录一下门信息
                string _goScreenName = child.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().GoScreenName;
                string _reMapName= child.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().ReMapName;
                string _dangQianMenWeiZhi = child.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().DangQianMenWeizhi;
                gkMapMsg += "$" + _goScreenName + "!" + _reMapName + "!" + _dangQianMenWeiZhi;
            }

            if(_name.Split('_')[0] == "wu")
            {
                //雾  记录缩放 颜色
                string sf = child.transform.localScale.ToString();
                string colorStr = child.GetComponent<SpriteRenderer>().color.ToString();
                print("---------------------------> 缩放  "+sf+"  颜色  "+colorStr);

                gkMapMsg += "$" + sf + "!" + colorStr;
            }


            if (i == 0)
            {
                MapMsgStr += gkMapMsg;
            }
            else
            {
                MapMsgStr += "|" + gkMapMsg;
            }
        }

        //加入 kuang 数据信息
        GameObject kuang = GlobalTools.FindObjByName("kuang");
        string kuangMsg = "kuang!"+ kuang.transform.position.x+"#"+ kuang.transform.position.y+"!"+kuang.GetComponent<BoxCollider2D>().size.x + "#" + kuang.GetComponent<BoxCollider2D>().size.y;

        MapMsgStr = GlobalSetDate.instance.CReMapName + "=" + MapMsgStr+"|"+kuangMsg;
        print(" 地图信息  " + MapMsgStr);
        

        if (IsShouDong) {
            print("修改前 数据  "+ GlobalSetDate.instance.gameMapDate.MapDate);
            ReplaceMapDate(GlobalSetDate.instance.CReMapName, MapMsgStr);
        }
        else
        {
            //将本地图信息 存入 全局 数据
            GlobalSetDate.instance.gameMapDate.MapDate += MapMsgStr + "@";
        }
    }


    void ReplaceMapDate(string mapName,string mapDateMsg)
    {
        List<string> strArr = new List<string>(GlobalSetDate.instance.gameMapDate.MapDate.Split('@'));
        
        print("  mapName "+mapName+"    count  "+strArr.Count);

        foreach (string s in strArr)
        {
            if (s != "")
            {
                if (s.Split('=')[0] == mapName)
                {
                    //print("  ????  进来没   " + s.Split('=')[0] + "   --->  " + mapName);
                    strArr.Remove(s);
                    print(" mapDateMsg  "+ mapDateMsg);
                    strArr.Add(mapDateMsg);
                    break;
                    //GlobalSetDate.instance.gameMapDate.MapDate += mapDateMsg + "@";
                }
            }
        }

       


        GlobalSetDate.instance.gameMapDate.MapDate = "";
        for (int i=0;i< strArr.Count; i++)
        {
            if (i == 0)
            {
                GlobalSetDate.instance.gameMapDate.MapDate = strArr[i];
            }
            else
            {
                GlobalSetDate.instance.gameMapDate.MapDate += "@" + strArr[i];
            }
        }
        print("修改后>>>>> 数据  " + GlobalSetDate.instance.gameMapDate.MapDate);

    }


    


    GameObject _cMapObj;
    List<GameObject> mapObjArr = new List<GameObject>() { };
    //参数    danFX单方向修正  
    void CreateFZRoad(string fx,int mapNums,string goScreenName,string danFX = "")
    {
        Vector2 LJDpos;
        Vector2 pos;
        int sd;
        GameObject mapObj;
        
        for (int i=0;i<= mapNums; i++)
        {
            if (fx == "l")
            {
                if(i!= mapNums)
                {
                    string mapArrName = "db_pd";
                    if (DibanType != "") mapArrName += "_" + DibanType;
                    int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
                    string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
                    mapObj = GlobalTools.GetGameObjectByName(mapName);
                }
                else
                {
                    mapObj = GlobalTools.GetGameObjectByName("dt_men_l");
                    //判断是否是 特殊地图
                    if (GlobalMapDate.IsSpeMapByName(goScreenName))
                    {
                        goScreenName += "$p";
                    }
                    if (danFX != "")
                    {
                        mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(danFX, goScreenName);
                    }
                    else
                    {
                        mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(fx, goScreenName);
                    }
                    GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetDoorInList(mapObj);
                }
               
                //控制 深度
                sd = 19 + i % 7;
                //GlobalTools.SetMapObjOrder(lu, sd);
                //print("lu-name--------   " + lu.transform.Find("diban").name);
                //print("lu------------->>>>>>>>>   " + lu.transform.Find("diban").GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder);
                //if(mapObj.transform.Find("diban")) mapObj.transform.Find("diban").GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd;
                if (mapObj.transform.Find("diban")) mapObj.GetComponent<DBBase>().SetSD(sd);
                //控制灯光


                if (i == 0)
                {
                    //  DiBanBase  LianJieDian
                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetLeftPos();

                    pos = new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y);
                  
                    //寻找连接点
                }
                else if (i== mapNums) {
                    //结束 创建门
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                    pos = LJDpos;//new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y);
                }
                else
                {
                    //创建地板
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                    pos = new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y);
                    
                }
                mapObj.transform.position = pos;
                mapObj.transform.parent = maps.transform;
                _cMapObj = mapObj;
                mapObjArr.Add(mapObj);
            }
            else if (fx == "r")
            {
                if (i != mapNums)
                {
                    string mapArrName = "db_pd";
                    if (DibanType != "") mapArrName += "_" + DibanType;
                    int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
                    string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
                    mapObj = GlobalTools.GetGameObjectByName(mapName);
                }
                else
                {
                    mapObj = GlobalTools.GetGameObjectByName("dt_men_r");
                    if (GlobalMapDate.IsSpeMapByName(goScreenName))
                    {
                        goScreenName += "$p";
                    }

                    if (danFX != "")
                    {
                        mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(danFX, goScreenName);
                    }
                    else
                    {
                        mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(fx, goScreenName);
                    }

                    

                    GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetDoorInList(mapObj);
                }

                sd = 19 + i % 7;
                //GlobalTools.SetMapObjOrder(lu, sd);
                //print("lu-name--------   " + lu.transform.Find("diban").name);
                //print("lu------------->>>>>>>>>   " + lu.transform.Find("diban").GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder);
                //if (mapObj.transform.Find("diban")) mapObj.transform.Find("diban").GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd;
                if (mapObj.transform.Find("diban")) mapObj.GetComponent<DBBase>().SetSD(sd);
                //控制灯光


                if (i == 0)
                {
                    //  DiBanBase  LianJieDian
                    //这里要先 拿到连接点位置

                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetRightPos();
                    pos = LJDpos;

                    //寻找连接点
                }
                else if (i == mapNums)
                {
                    //结束 创建门
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                    pos = LJDpos;//new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y);
                }
                else
                {
                    //创建地板
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                    pos = LJDpos;

                }
                mapObj.transform.position = pos;
                mapObj.transform.parent = maps.transform;
                _cMapObj = mapObj;
                mapObjArr.Add(mapObj);

            }
            else if (fx == "u")
            {
                if (i != mapNums)
                {

                    if(i == mapNums - 1)
                    {
                        string mapArrName = "db_rd";
                        if (DibanType != "") mapArrName += "_" + DibanType;

                        int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
                        string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
                        mapObj = GlobalTools.GetGameObjectByName(mapName);
                    }
                    else
                    {
                        string mapArrName = "db_dn_shu";
                        if (DibanType != "") mapArrName += "_" + DibanType;
                        int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
                        string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
                        mapObj = GlobalTools.GetGameObjectByName(mapName);
                    }

                   
                }
                else
                {
                  
                    mapObj = GlobalTools.GetGameObjectByName("dt_men_r");
                    if (GlobalMapDate.IsSpeMapByName(goScreenName))
                    {
                        goScreenName += "$p";
                    }
                    mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(fx, goScreenName);

                    GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetDoorInList(mapObj);
                }

                sd = 19 + i % 7;
                if (mapObj.transform.Find("diban")) mapObj.GetComponent<DBBase>().SetSD(sd);

                if (i == mapNums-1)
                {
                    //转弯
                    if (i == 0)
                    {
                        LJDpos = lianjiedian.GetComponent<DBBase>().GetUpPos();
                    }
                    else
                    {
                        LJDpos = _cMapObj.GetComponent<DBBase>().GetUpPos();
                    }
                    
                    pos = LJDpos;
                }else if (i == 0)
                {
                    //  DiBanBase  LianJieDian  
                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetUpPos();
                    pos = new Vector2(LJDpos.x, LJDpos.y + mapObj.GetComponent<DBBase>().GetHight());

                    //寻找连接点
                }
                else if (i == mapNums)
                {
                    //结束 创建门
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                    pos = LJDpos;//new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y);
                }
                else
                {
                    //创建地板
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetUpPos();
                    pos = new Vector2(LJDpos.x, LJDpos.y + _cMapObj.GetComponent<DBBase>().GetHight());

                }
                mapObj.transform.position = pos;
                mapObj.transform.parent = maps.transform;
                _cMapObj = mapObj;
                mapObjArr.Add(mapObj);
            }
            else if (fx == "d")
            {
                if (i != mapNums)
                {

                    if (i == mapNums - 1)
                    {
                        string mapArrName = "db_lu";
                        if (DibanType != "") mapArrName += "_" + DibanType;
                        int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
                        string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
                        mapObj = GlobalTools.GetGameObjectByName(mapName);
                    }
                    else
                    {
                        string mapArrName = "db_dn_shu";
                        if (DibanType != "") mapArrName += "_" + DibanType;
                        int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
                        string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
                        mapObj = GlobalTools.GetGameObjectByName(mapName);
                    }


                }
                else
                {
                   
                    mapObj = GlobalTools.GetGameObjectByName("dt_men_l");
                    if (GlobalMapDate.IsSpeMapByName(goScreenName))
                    {
                        goScreenName += "$p";
                    }
                    mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(fx, goScreenName);

                    GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetDoorInList(mapObj);
                }

                sd = 19 + i % 7;
                if (mapObj.transform.Find("diban")) mapObj.GetComponent<DBBase>().SetSD(sd);


             
                if (i == 0)
                {
                    //  DiBanBase  LianJieDian  
                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetDownPos();
                    pos = LJDpos;//new Vector2(LJDpos.x, LJDpos.y + mapObj.GetComponent<DBBase>().GetHight());

                    //寻找连接点
                }
                else if (i == mapNums)
                {
                    //结束 创建门
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                    pos = LJDpos;//new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y);
                }
                else if (i == mapNums - 1)
                {
                    //转弯
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetUpPos();
                    pos = new Vector2(LJDpos.x, LJDpos.y - _cMapObj.GetComponent<DBBase>().GetHight()); //LJDpos;
                }
                else
                {
                    //创建地板
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetUpPos();
                    pos = new Vector2(LJDpos.x, LJDpos.y - _cMapObj.GetComponent<DBBase>().GetHight());

                }
                mapObj.transform.position = pos;
                mapObj.transform.parent = maps.transform;
                _cMapObj = mapObj;
                mapObjArr.Add(mapObj);
            }
            
        }

        
    }


    float u;
    float d;
    float l;
    float r;

    void GetKuaiBianjie(List<GameObject> mapObjList)
    {
        for(int i= 0; i < mapObjList.Count; i++)
        {
            if (i == 0)
            {
                u = mapObjList[i].transform.position.y;
                d = mapObjList[i].transform.position.y;
                l = mapObjList[i].transform.position.x;
                r = mapObjList[i].transform.position.x;
            }
            else
            {
                if (mapObjList[i].transform.position.y > u) u = mapObjList[i].transform.position.y;
                if (mapObjList[i].transform.position.y < d) d = mapObjList[i].transform.position.y;
                if (mapObjList[i].transform.position.x < l) l = mapObjList[i].transform.position.x;
                if (mapObjList[i].transform.position.x > r) r = mapObjList[i].transform.position.x;
            }
        }

        //CameraKuai 的范围计算

        print("-------------------------------------计算摄像头块的 大小 ！！！！！！！！！！");
        print(" 地图组成数量    "+ mapObjList.Count);
        //边界延伸值
        float bianjieyansheng = 15;
        Vector2 lu = new Vector2(l- bianjieyansheng, u+ bianjieyansheng);
        Vector2 rd = new Vector2(r+ bianjieyansheng, d- bianjieyansheng);

        print("左上角位置  "+lu+"   右下角位置  "+rd);

        GameObject kuang = GlobalTools.FindObjByName("kuang");

        print("位置   "+kuang.transform.position+"   大小 "+ kuang.GetComponent<BoxCollider2D>().size);

        Vector2 zhongxindian = new Vector2((r+l)*0.5f,(u+d)*0.5f);
        float w = Mathf.Abs(r - l + 2* bianjieyansheng);
        float h = Mathf.Abs(u - d + 2* bianjieyansheng) < kuang.GetComponent<BoxCollider2D>().size.y ? kuang.GetComponent<BoxCollider2D>().size.y : Mathf.Abs(u - d + 2 * bianjieyansheng);
        kuang.transform.position = zhongxindian;
        kuang.GetComponent<BoxCollider2D>().size = new Vector2(w,h);

        print("切换后 位置 和 大小   "+ zhongxindian+"  大小  "+ kuang.GetComponent<BoxCollider2D>().size);

        GlobalTools.FindObjByName("MainCamera").GetComponent<CameraController>().GetBounds(kuang.GetComponent<BoxCollider2D>());



      
        
    }



    GameObject lianjiedian;
    void CreateLJByName(string lianjie)
    {
        print(" 连接数组长度  " + GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance()).Count);
        int nums = GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance()).Count;
        string ljdianName = GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
        print("连接点 名字   "+ ljdianName);
        lianjiedian = GlobalTools.GetGameObjectByName(ljdianName);
        lianjiedian.transform.position = Vector3.zero;

        lianjiedian.transform.parent = maps.transform;
        mapObjArr.Add(lianjiedian);
        //GameObject player = GlobalTools.FindObjByName("player");

        //player.transform.position = new Vector3(lianjiedian.transform.position.x + 2, lianjiedian.transform.position.y + 2, lianjiedian.transform.position.z);


        //print(lianjiedian.transform.position  +"    player   "+ GlobalTools.FindObjByName("player").transform.position);


        //GlobalTools.FindObjByName("MainCamera").transform.position = new Vector3(player.transform.position.x, player.transform.position.y, GlobalTools.FindObjByName("MainCamera").transform.position.z);  //GlobalTools.FindObjByName("player").transform.position;
    }

    // lr=l:mapR_1$p!r:map_r-3
    string GetStrByList(List<string> menFXList)
    {
        string str = "";

        string arrNums = "1-l:2-r:3-u:4-d";

        string[] arrs = arrNums.Split(':');

        List<string> tempList = new List<string> { };

        for (int i = 0;i< menFXList.Count; i++)
        {
            string s = menFXList[i].Split(':')[0];
            for(var j=0;j< arrs.Length; j++)
            {
                if (s == arrs[j].Split('-')[1])
                {
                    tempList.Add(arrs[j].Split('-')[0]+"*"+ menFXList[i]);
                }
            }
        }


        tempList.Sort();

        string zhongjianziduan = "";
        string neirong = "";

        for (int n =0;n< tempList.Count;n++)
        {
            zhongjianziduan += tempList[n].Split('*')[1].Split(':')[0];
            if (n == 0)
            {
                neirong += tempList[n].Split('*')[1];
            }
            else
            {
                neirong += "!"+tempList[n].Split('*')[1];
            }
            

            //print(n+" - "+ tempList[n]);
        }

        str = zhongjianziduan + "=" + neirong;

        return str;
    }



    //生成门 按顺序找门 左 右 上 下  门后面连接的数量
    //数据类型  l:map_1-2$4^r:map_r-2$3




    //门方向列表 {"l","r"}
    List<string> menFXList = new List<string> { };
    List<string> GetMenFXListByMapName(string mapName)
    {
        //BigMapDate->         map_r+map_r-1!0#0!r:map_r-2^u:map_r-3|map_r-2!1#0!r:map_r-4@map_u+map_u-1!0#0!r:map_u-2|map_u-2!1#0!map_u-3
        string[] mapArr1 = GlobalSetDate.instance.gameMapDate.BigMapDate.Split('@');
        string mapMsg = "";
        foreach(string s in mapArr1)
        {
            if (s.Split('+')[0] == GlobalSetDate.instance.CMapTou)
            {
                mapMsg = s.Split('+')[1];
                break;
            }
        }

        string[] CMapMsgArr = mapMsg.Split('|');
        //map_r-1!0#0!r:map_r-2^u:map_r-3
        foreach (string m in CMapMsgArr)
        {
            if(m.Split('!')[0] == GlobalSetDate.instance.CReMapName)
            {
                theMapMsg = m;
                break;
            }
        }

        string[] TheMapMsgArr = theMapMsg.Split('!')[2].Split('^');
        for(var i=0;i< TheMapMsgArr.Length; i++)
        {
            menFXList.Add(TheMapMsgArr[i]);
        }
        return menFXList;
    }


  
}