using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReMap : MonoBehaviour
{
    //地形生成
    // Start is called before the first frame update
    void Start()
    {
        GetInDate();
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

        print("本地图名字 "+GlobalSetDate.instance.CReMapName);



        if (GlobalSetDate.instance.CReMapName == "") return;





        //获取门信息
        menFXList = GetMenFXListByMapName(GlobalSetDate.instance.CReMapName);
        print("menFXList   "+ menFXList.Count);
        foreach (string m in menFXList) print(m);
        //这里要知道 从哪进来的 进入方向   保留一个门
        //-
        print("从哪个位置的门进来的  "+ GlobalSetDate.instance.DanqianMenweizhi);



        //判断 全局数据中 是否有 地图 有的话  取出来
        string mapDateMsg = GetMapMsgDateByName(GlobalSetDate.instance.CReMapName);
        if (mapDateMsg != "")
        {
            print("根据 数据 来生成地图");
            CreateMapByMapMsgDate(mapDateMsg);
            //获取 门信息

            //角色站位 和方向
            GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetPlayerPosByFX();
            return;
        }
        


        //判断是 哪种景色  洞外 洞内  相应的 景 图片是哪些






     



        //随机出每个 分支 的地图数量
        string type = GetTypeByMenFXList(menFXList);

        SetMapMsgDateInStr();
        //根据类型 生成 连接点

        //lr  平地 洞内   左右的 地形是最多的  有门的 打怪才能过的  有直线通道的
        //普通的直线  连接线 左右连接  向左  向右  向上  向下 4个方向 生成地图
        // 左右的 或者 2个门的 还有只要 碰到就下落的地板   上下移动   左右移动的地板
        // 2个门都适用   1.户外 2.洞内 3.户外洞内混合



        //lru
        //lrd

        //lud
        //rud

        //ru
        //rd
        //lu
        //ld
        //ud
        //lrud





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

                _goScreenName = s.Split('$')[1].Split('!')[0];
                _reMapName = s.Split('$')[1].Split('!')[1];
                _dangQianMenWeiZhi = s.Split('$')[1].Split('!')[2];
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
                int theSD = int.Parse(_sd);
                if (mapObj.GetComponent<DBBase>())
                {
                    mapObj.GetComponent<DBBase>().SetSD(theSD);
                }
                else
                {
                    mapObj.GetComponent<SpriteRenderer>().sortingOrder = theSD;
                }

                //如果是门数据
                if (mapObj.GetComponent<RMapMen>())
                {
                    GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetDoorInList(mapObj);
                    mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg2(_goScreenName, _dangQianMenWeiZhi, _reMapName);
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

        string str = GetStrByList(menFXList);
        print("str    "+str);
        //取中心连接点 地图 以及 随机出 每个 朝向的 模块数量    模块的最少数是多少？ 左右是1  上下是2（多一个直连+转弯） 然后连接门  门不算进数量 数量走完连接门
        //数元素 自带 落叶粒子
        // lr=l:mapR_1?3?pd?shu!r:map_r-2?3?dn?qiang
        
        string lianjie = str.Split('=')[0];

        CreateLJByName(lianjie);

        //str    lr=l:mapR_1!r:map_r-2
        //往哪个方向延伸地图
        CreateBianlu(str.Split('=')[1]);



        GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetPlayerPosByFX();

        return "";
    }

    public int MaxMapNums = 2;

    void CreateBianlu(string mapStrMsg)
    {
        List<string> strList = new List<string>(mapStrMsg.Split('!'));
        for (int i = 0; i < strList.Count; i++){
            string fx = strList[i].Split(':')[0];
            //获取 生成的 地图块 数量
            int n = 2 + GlobalTools.GetRandomNum(MaxMapNums);
            string _goScreenName = strList[i].Split(':')[1];
            print("门位置 "+ strList[i].Split(':')[0] + "  _goScreenName    "+ _goScreenName);
            CreateFZRoad(fx, n, _goScreenName);
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
                _sd = child.gameObject.GetComponent<SpriteRenderer>().sortingOrder.ToString();
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
    void CreateFZRoad(string fx,int mapNums,string goScreenName)
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
                    mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(fx, goScreenName);

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
                    mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(fx, goScreenName);

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
                        int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
                        string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
                        mapObj = GlobalTools.GetGameObjectByName(mapName);
                    }
                    else
                    {
                        string mapArrName = "db_dn_shu";
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

                if (i == 0)
                {
                    //  DiBanBase  LianJieDian  
                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetUpPos();
                    pos = new Vector2(LJDpos.x,LJDpos.y+ mapObj.GetComponent<DBBase>().GetHight());

                    //寻找连接点
                }
                else if (i == mapNums)
                {
                    //结束 创建门
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                    pos = LJDpos;//new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y);
                }else if (i == mapNums-1)
                {
                    //转弯
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetUpPos();
                    pos = LJDpos;
                }
                else
                {
                    //创建地板
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetUpPos();
                    pos = LJDpos;

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
                        int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
                        string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
                        mapObj = GlobalTools.GetGameObjectByName(mapName);
                    }
                    else
                    {
                        string mapArrName = "db_dn_shu";
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

        foreach(string m in CMapMsgArr)
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
