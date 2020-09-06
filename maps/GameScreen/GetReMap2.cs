using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReMap2 : GetReMap
{
    // Start is called before the first frame update
    void Start()
    {
        print("GetReMap 2!!");
        GetStart();
    }

    protected override void GetStart()
    {
        GetInDate();

        print("  进入 重写？ ");
       

        ////print("  @@--------这里是 完成地图后！ ");

        kuang = GlobalTools.FindObjByName("kuang");

        Ax = GlobalTools.FindObjByName("A_");



        GetGuaiControlMen();


        StartCoroutine(IEDestory2ByTime(0.2f));
    }

    public override void GetInDate()
    {

        //判断 是否 有这个地图的数据 有的话 直接按数据生成地图
        print("************************************************************************************************************************");

        maps = GlobalTools.FindObjByName("maps");

        if (GlobalSetDate.instance.CReMapName != "" && IsTiaoshi && CMapName != "")
        {
            GlobalSetDate.instance.CReMapName = CMapName;
            GlobalMapDate.CCustomStr = CMapName.Split('_')[1];
        }
        else
        {
            CMapName = GlobalSetDate.instance.CReMapName;
        }

        print("本地图名字 " + GlobalSetDate.instance.CReMapName);
        if (GlobalSetDate.instance.CReMapName == "") return;





        print(" 随机 生成地图 调试用！！   ");


        CreateMapByFX("lr");

        GetKuaiBianjie(mapObjArr);

        return;






        //获取门信息
        if (IsTiaoshi && ThisMenFXList.Count != 0)
        {
            //{l:map_r-2,r:map_r-3}
            menFXList = ThisMenFXList;
        }
        else
        {
            menFXList = GetMenFXListByMapName(GlobalSetDate.instance.CReMapName);
            ThisMenFXList = menFXList;
        }
        //print("menFXList   " + menFXList);

        //foreach (string m in menFXList) print("  menlist>////////////////////////:   "+m);
        //这里要知道 从哪进来的 进入方向   保留一个门
        //-

        //排序 门  获取 连接点 字符 lr /lru /lrd  等 来获取  连接点的 obj数组 来生成连接点
        string str = GetStrByList(menFXList);
        //print("menlist>///////////// -str :    " + str);

        string lianjie = str.Split('=')[0];
        _lianjiedibanType = lianjie;




        if (IsTiaoshi && CongNaGeMenjinru != "")
        {
            GlobalSetDate.instance.DanqianMenweizhi = CongNaGeMenjinru;
        }
        else
        {
            //从哪个门进入的  r
            CongNaGeMenjinru = GlobalSetDate.instance.DanqianMenweizhi;
        }
        print("从哪个位置的门进来的  " + GlobalSetDate.instance.DanqianMenweizhi);
        //判断 全局数据中 是否有 地图地形数据 有的话  取出来
        string mapDateMsg = GetMapMsgDateByName(GlobalSetDate.instance.CReMapName);
        if (mapDateMsg != "")
        {
            print("--------------------------> 根据 数据 来生成地图");
            GlobalSetDate.instance.IsCMapHasCreated = true;
            CreateMapByMapMsgDate(mapDateMsg);
            //获取 门信息

            //角色站位 和方向
            GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetPlayerPosByFX();

            //GetGuaiControlMen();

            return;
        }

        //判断是 哪种景色  洞外 洞内  相应的 景 图片是哪些

        //随机出每个 分支 的地图数量
        string type = GetTypeByMenFXList(menFXList);


        print("随机 出个 演示 地图？");

        //GetGuaiControlMen();
    }


    protected override void CreateLJByName(string lianjie)
    {
        print(" 连接数组长度  " + GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance()).Count);
        int nums = GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance()).Count;
        string ljdianName = GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
        print("连接点 名字   " + ljdianName);
        lianjiedian = GlobalTools.GetGameObjectByName(ljdianName);
        lianjiedian.transform.position = Vector3.zero;

        lianjiedian.transform.parent = maps.transform;
        mapObjArr.Add(lianjiedian);



        //if (lianjie.Split('_')[0] == "lr")
        //{
        //    if (!GlobalSetDate.instance.IsCMapHasCreated) ChuGuai(true);
        //}
       
    }

    string _lianjieFX = "lrud";
    void CreateMapByFX(string lianjieFX)
    {
        print("  ??? "+ lianjieFX);
        //_lianjieFX = lianjieFX;
        //创建 连接点
        CreateLJByName(lianjieFX);
        //创建 各个方向的 起始点地板


        char[] FXArr = _lianjieFX.ToCharArray();


        CMapName = "map_s-1";

        for (int i = 0; i < FXArr.Length; i++)
        {
            int xiaoDuanNums = 1 + GlobalTools.GetRandomNum(2);
            CreateFZRoad(FXArr[i].ToString(), xiaoDuanNums, "test");
        }


        //CreateFZRoad("u", 3, "test");


    }


    bool IsHasFX(string lianjieFX,string fx)
    {
        return lianjieFX.Contains(fx);
    }




    protected override void CreateFZRoad(string fx, int mapNums, string goScreenName, string danFX = "")
    {
        for (int i = 0; i <= mapNums; i++)
        {
            //这里要 小段 有几个地图组成
            int XiaoDiTuNums = 1 + GlobalTools.GetRandomNum(3);
            if (fx == "l" || fx == "d")
            {
                if (i != mapNums)
                {
                    CreateRoadDuanByFX(fx, XiaoDiTuNums, i);
                }
                else
                {
                    //********************************@***********************门 地图生成 控制
                    mapObj = GlobalTools.GetGameObjectByName("dt_men_l");
                    //判断是否是 特殊地图
                    if (GlobalMapDate.IsSpeMapByName(goScreenName))
                    {
                        goScreenName += "$p";
                    }
                    if (danFX != "")
                    {
                        //单门的方向
                        mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(danFX, goScreenName);
                    }
                    else
                    {
                        mapObj.GetComponent<RMapMen>().MenKuai.GetComponent<ScreenChange>().SetMenMsg(fx, goScreenName);
                    }
                    GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetDoorInList(mapObj);

                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                    pos = LJDpos;
                    GetMapObjPos(i);

                }
            }
            else if (fx == "r" || fx == "u")
            {
                if (i != mapNums)
                {
                    CreateRoadDuanByFX(fx, XiaoDiTuNums, i);
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
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                    pos = LJDpos;
                    GetMapObjPos(i);
                }
            }





            if (i != mapNums - 1)
            {
                ChuGuai();
            }
            else
            {
                ChuGuai(false, fx);
            }


        }
    }




    Vector2 LJDpos;
    Vector2 pos;
    int sd;
    GameObject mapObj;

    bool _isQishi0 = false;

    //生成小段的 地形
    protected void CreateRoadDuanByFX(string FX, int DuanNums, int isFirstNums,bool IsToMen = true)
    {
        //是什么 地形  洞外 洞内  跳跃  洞内直接上去的隧道（要给两边墙 标记是竖着上去的 布景和灯光需要）

        //1.平地 上升 和下降
        //2.跳跃 上升 下降
        //3.下落地板
        //4.左右移动地板
        //5.上下移动地板
        //6.倒立地形
        //7.是否有机关 地刺  齿轮
        //8.怪物分布
        //9.洞内 拱形分布 关门 门怪 或者 全灭怪---------洞内背景是否 封闭
        //10.洞内 高度低
        //11.洞内 高度 有小幅变化
        //12.是否有剧情 魂魄
        //13.是否有宝物  这里做好单边 只要 指定 方向就可以做 复杂地形 但是结束 没有门 IsToMen = false;  12 和13 在大地图中判断
        //14.房型 屋内 地图
        //特殊型 塔型 电梯 上楼
        //15.boss地图
        //16.浮动地板

        //标识物


        //是否有机关



        print("_isQishi0  " + _isQishi0 + " isFirstNums  " + isFirstNums + "    ");

        int nums = GlobalTools.GetRandomNum();
        for (int i = 0; i < DuanNums; i++)
        {
            mapObj = GetDiBanByName();
            if (FX == "l")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetLeftPos();
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                }
                //判断是否是 对接连接点的 起始路
                if (isFirstNums == 0 && IsHasFX(_lianjieFX, "d"))
                {
                    PuTongShangShengDX(i, FX, true);
                }
                else
                {
                    ShengChengDiBan(i, FX, nums);
                }

            }
            else if (FX == "d")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetLeftPos();
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                }

                if (isFirstNums == 0 && IsHasFX(_lianjieFX, "l"))
                {
                    PuTongXiaJiangDX(i, FX, true);
                }
                else
                {
                    ShengChengDiBan(i, FX,nums);
                    
                }
            }
            else if (FX == "r")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetRightPos();
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                }
                //判断是否是 对接连接点的 起始路
                if (isFirstNums == 0 && IsHasFX(_lianjieFX, "u"))
                {
                    PuTongXiaJiangDX(i, FX, true);
                }
                else
                {
                    ShengChengDiBan(i, FX, nums);
                }
            }
            else if (FX == "u")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    LJDpos = lianjiedian.GetComponent<LianJieDian>().GetRightPos();
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                }

                if (isFirstNums == 0 && IsHasFX(_lianjieFX, "r"))
                {
                    PuTongShangShengDX(i, FX, true);
                }
                else
                {
                    ShengChengDiBan(i, FX, nums);

                }
            }
            GetMapObjPos(i);
        }

        _isQishi0 = false;
    }

    void ShengChengDiBan(int i,string FX,int nums)
    {
        if (nums < 80)
        {
            PuTongPingDiDX(i, FX, false);
        }
        else if (nums < 50)
        {
            PuTongPingDiDX(i, FX, false);
        }
        else if (nums < 30)
        {
            if ((FX == "l" && IsHasFX(_lianjieFX, "d"))|| (FX == "u" && IsHasFX(_lianjieFX, "r")))
            {
                //如果包含 D 下路 就不能有下降 下降改为平地
                PuTongPingDiDX(i, FX, false);
            }
            else
            {
                PuTongXiaJiangDX(i, FX, false);
            }
        }
        else
        {

            if((FX== "d" && IsHasFX(_lianjieFX, "l"))|| (FX == "r" && IsHasFX(_lianjieFX, "u")))
            {
                //如果包含 L 左路 就不能上升 上升改为平地
                PuTongPingDiDX(i, FX, false);
            }
            else
            {
                PuTongShangShengDX(i, FX, false);
            }


        }
    }



    void GetMapObjPos(int i)
    {
        sd = 19 + i % 7;
        if (mapObj.transform.Find("diban")) mapObj.GetComponent<DBBase>().SetSD(sd);
        mapObj.transform.position = pos;
        mapObj.transform.parent = maps.transform;
        _cMapObj = mapObj;
        mapObjArr.Add(mapObj);
    }




    //普通 梯度上升 地形
    void PuTongShangShengDX(int i,string fx, bool IsQishi = false)
    {
        print("  i shi duoshao--------------->    "+i);

        if(fx == "l"|| fx == "d")
        {
            if (IsQishi)
            {
                if (i == 0)
                {
                    pos = new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth() - 4f - GlobalTools.GetRandomDistanceNums(1f), LJDpos.y + mapObj.GetComponent<DBBase>().GetHight() * 0.5f + 2 + GlobalTools.GetRandomDistanceNums(1));
                    return;
                }
            }

            pos = new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y + mapObj.GetComponent<DBBase>().GetHight() * 0.5f + GlobalTools.GetRandomDistanceNums(1));
        }
        else
        {
            if (IsQishi)
            {
                if (i == 0)
                {
                    pos = new Vector2(LJDpos.x  + 4f + GlobalTools.GetRandomDistanceNums(1f), LJDpos.y + mapObj.GetComponent<DBBase>().GetHight() * 0.5f + 2 + GlobalTools.GetRandomDistanceNums(1));
                    return;
                }
            }

            pos = new Vector2(LJDpos.x , LJDpos.y + mapObj.GetComponent<DBBase>().GetHight() * 0.5f + GlobalTools.GetRandomDistanceNums(1));
        }
    }


    //普通下降地形
    void PuTongXiaJiangDX(int i, string fx, bool IsQishi = false)
    {
        float xiajiang = 0;
        if (IsQishi&&i==0) xiajiang = 7;
        if (fx == "l"|| fx == "d")
        {
            pos = new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y - mapObj.GetComponent<DBBase>().GetHight() * 0.5f - GlobalTools.GetRandomDistanceNums(1)- xiajiang);
        }
        else
        {
            pos = new Vector2(LJDpos.x , LJDpos.y - mapObj.GetComponent<DBBase>().GetHight() * 0.5f - GlobalTools.GetRandomDistanceNums(1)- xiajiang);
        }
    }

    //普通平地地形
    void PuTongPingDiDX(int i, string fx, bool IsQishi = false)
    {
        if (fx == "l" || fx == "d")
        {
            pos = new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y);
        }
        else
        {
            pos = new Vector2(LJDpos.x , LJDpos.y);
        }
    }


   

    //斜边
    void XieBian(Vector2 posV2,float XuanZhuanDuShu)
    {
        GameObject xiebian = GetDiBanByName();
        Vector2 xiebianPos = new Vector2(posV2.x, posV2.y-3);
        xiebian.transform.position = xiebianPos;
        print("  rotation:   " + xiebian.transform.localRotation);
        xiebian.transform.name = "hahahahhah";
        xiebian.transform.localEulerAngles = new Vector3(0, 0, XuanZhuanDuShu);
    }



   




    //洞内直接上升
    void DongNeiZhiJieShangsheng(int i, string fx, int DuanNums, bool IsQishi = false)
    {
        //收尾  和 做斜边（斜边的 修饰 后期要传入地板参数）防止 跳跃上去
        float duibianKuangX = 0;
        if (fx == "l")
        {
            if (IsQishi)
            {
                if (i == 0)
                {
                    duibianKuangX = LJDpos.x;
                    pos = new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth() - 4f - GlobalTools.GetRandomDistanceNums(1f), LJDpos.y + mapObj.GetComponent<DBBase>().GetHight() * 0.5f + 2 + GlobalTools.GetRandomDistanceNums(1));
                    //做斜边 斜边点在 左下

                    //斜边 怎么存入 数据  怎么标记自己 和控制 修饰

                    if (i - DuanNums != -1)
                    {
                        print("创建 斜边！！！");
                        XieBian(pos, 135);
                    }


                }
                else
                {
                    pos = new Vector2(LJDpos.x, LJDpos.y + mapObj.GetComponent<DBBase>().GetHight());

                    //创建 洞内的 对边
                    GameObject duibian = GetDiBanByName();
                    duibian.transform.position = new Vector2(duibianKuangX, pos.y);


                    if (i - DuanNums == -1)
                    {
                        //斜边
                        //是否 左转延伸

                        GameObject duibian2 = GetDiBanByName();
                        duibian2.transform.position = new Vector2(duibianKuangX, pos.y + duibian2.GetComponent<DBBase>().GetHight());

                        XieBian(duibian2.GetComponent<DBBase>().GetLeftPos(), 135);
                    }

                }
            }
        }
        else
        {

        }




    }



}
