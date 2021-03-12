using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReMap2 : GetReMap
{
    // Start is called before the first frame update
    void Start()
    {
        //print("GetReMap 2!!");
        GetStart();
    }

    protected override void GetStart()
    {
        GetInDate();

        //print("  进入 重写？ ");
       

        ////print("  @@--------这里是 完成地图后！ ");

        kuang = GlobalTools.FindObjByName("kuang");

        Ax = GlobalTools.FindObjByName("A_");



        GetGuaiControlMen();


        StartCoroutine(IEDestory2ByTime(0.2f));
    }


    protected void IsAllDieCheck(UEvent e)
    {
        //print("GuaiList.Count------------------------->     " + GuaiList.Count);
        if(GuaiList.Count == 0)
        {
            //开门
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "open"), this);
        }
    }


    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ALLDIE_OPEN_DOOR, this.IsAllDieCheck);
    }

    public override void GetInDate()
    {

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ALLDIE_OPEN_DOOR,this.IsAllDieCheck);


        //判断 是否 有这个地图的数据 有的话 直接按数据生成地图
        print("************************************************************************************************************************");
        _dixingkuozhanNums = 0;
        maps = GlobalTools.FindObjByName("maps");

        
        if (GlobalSetDate.instance.CReMapName != "" && IsTiaoshi && CMapName != "")
        {
            print(" 本地图名字   "+CMapName+"   本地*有*地图信息  地图的 信息数据是   " + GlobalSetDate.instance.CReMapName);
            GlobalSetDate.instance.CReMapName = CMapName;
            GlobalMapDate.CCustomStr = CMapName.Split('_')[1];
        }
        else
        {
            CMapName = GlobalSetDate.instance.CReMapName;
            print(" 本地图名字   " + CMapName + " ********** 没有 本地 地图信息  直接生成新的地图：   " + GlobalSetDate.instance.CReMapName);
        }

        print("本地图名字 " + GlobalSetDate.instance.CReMapName);
        CMapName = GlobalSetDate.instance.CReMapName;
        if (GlobalSetDate.instance.CReMapName == "") return;





        //print(" 随机 生成地图 调试用！！   ");


       

        //return;






        //获取门信息
        if (IsTiaoshi && ThisMenFXList.Count != 0)
        {
            //{l:map_r-2,r:map_r-3}
            menFXList = ThisMenFXList;
            print("注意 这里是调试 门");
            print("注意 这里是调试 门");
            print("注意 这里是调试 门");
            print("注意 这里是调试 门");
            print("注意 这里是调试 门");
        }
        else
        {
            menFXList = GetMenFXListByMapName(GlobalSetDate.instance.CReMapName);
            ThisMenFXList = menFXList;
        }


        

        print("menFXList   " + menFXList.ToString());

        foreach (string m in menFXList) print("  menlist>////////////////////////:   "+m);
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
        //print("从哪个位置的门进来的  " + GlobalSetDate.instance.DanqianMenweizhi);
        //判断 全局数据中 是否有 地图地形数据 有的话  取出来
        string mapDateMsg = GetMapMsgDateByName(GlobalSetDate.instance.CReMapName);
        //print("  *** mapDateMsg??  "+ mapDateMsg);
        if (mapDateMsg != "")
        {
            //print("--------------------------> 根据 数据 来生成地图");
            GlobalSetDate.instance.IsCMapHasCreated = true;

            //获取 地形是否是特殊地形
            if (GlobalMapDate.CurrentSpeMapDiXingMsg!="")
            {
                _dixingType = GlobalMapDate.CurrentSpelMapType.Split('^')[1];
                //print(" 获取了 地形的 type "+_dixingType);
            }


            CreateMapByMapMsgDate(mapDateMsg);
            //获取 门信息

            //角色站位 和方向
            GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetPlayerPosByFX();

            //GetGuaiControlMen();

            return;
        }




        CreateMapByFX(_lianjiedibanType);

        GetKuaiBianjie(mapObjArr);

        GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GetPlayerPosByFX();

        return;

        //判断是 哪种景色  洞外 洞内  相应的 景 图片是哪些

        //随机出每个 分支 的地图数量
        string type = GetTypeByMenFXList(menFXList);


        print("随机 出个 演示 地图？ _lianjiedibanType  "+ _lianjiedibanType);

        //GetGuaiControlMen();
    }


    GameObject lianjiedianY;

    protected override void CreateLJByName(string lianjie,bool IsYanZhan = false)
    {
        //print(" lianjie "+ lianjie);
        //print(" 连接数组长度  " + GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance()).Count);
        
        int nums = GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance()).Count;
        string ljdianName = GetDateByName.GetInstance().GetListByName(lianjie, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
        //print("连接点 名字   " + ljdianName);
        lianjiedian = GlobalTools.GetGameObjectByName(ljdianName);
        lianjiedian.transform.position = Vector3.zero;

        lianjiedian.transform.parent = maps.transform;
        _cMapObj = lianjiedian;
        mapObjArr.Add(lianjiedian);

        //地形扩展
        if (_dixingkuozhanNums != 0) return;
        if(IsYanZhan)
        {
            lianjiedianY = null;

            int YanZhanDibANNums = 1 + GlobalTools.GetRandomNum(2);
            int _DuanNums = 1 + GlobalTools.GetRandomNum(2);
            if (!IsHasFX(_lianjieFX, "l") && !IsHasFX(_lianjieFX, "d"))
            {
                //左封路
                for (int i = 0; i < _DuanNums; i++)
                {
                    YanShengDiBan("l", YanZhanDibANNums, i, true);
                }

            }
            else if (!IsHasFX(_lianjieFX, "r") && !IsHasFX(_lianjieFX, "u"))
            {
                //右封路
                for (int i = 0; i < _DuanNums; i++)
                {
                    YanShengDiBan("r", YanZhanDibANNums, i, true);
                }
            }
            else
            {
                if (GlobalTools.GetRandomNum() >= 5)
                {
                    //print("创建 右边 延展的 连接点");
                    YanShengDiBan("r", YanZhanDibANNums, 0);
                }
            }
        }



        //if (lianjie.Split('_')[0] == "lr")
        //{
        //    if (!GlobalSetDate.instance.IsCMapHasCreated) ChuGuai(true);
        //}

    }



    string _dixingType = "yiban";
    int _dixingkuozhanNums = 0;
    string _JYGuaiName = "";



    string _lianjieFX = "lr";
    int mapTypeNums = 1;
    void CreateMapByFX(string lianjieFX)
    {
        //print("  ??? "+ lianjieFX);
        _lianjieFX = lianjieFX;
        if (IsTiaoshi)
        {
            CMapName = "map_s-1";
        }
        

        //*********** 这里开始 从上 往下 写   这里要判断  这个 关卡地形是什么地形  有没有精英怪 boss 存档点 或者 机关 等  再开始根据需求 生成 存档点
        //寻找地图坐标 根据坐标 和地图nums来 判断 地图景 和 地板类型   灯光颜色的改变
        //print(" ********* 本地图的 坐标是多少  " + _mapZB);

        Globals.mapType = _dixingType;

        //几个门

        //纯粹的 1.平地   2.跳跃  3.洞内    4.洞内+跳跃   5.单边的精英怪 平地    6.一般地形 一般地形里面又分 是否有洞内 跳跃 等

        //判断 是否是左右地形  是的话才能 做专有地形（计算专有地形的比例)

        if (_lianjieFX =="lr")
        {
            //这里重新计算 各种地形的几率（洞内 跳跃  boss  机关等）
            //这里要根据 地图的 坐标 来判断 地形  是否是精英怪 是否是存档点
            //大型的特殊 场景 比如桥什么的  可以在后面插入加

           


            int rnums = GlobalTools.GetRandomNum();

            if (Globals.mapTypeNums == 1)
            {
                rnums = 90;
            }

            _JYGuaiName = "";

            //这里要注意 后期要根据数据来 获取 地图类型的几率
            print("这里要注意 后期要根据数据来 获取 地图类型的几率********* o(*￣︶￣*)o");

            if (rnums < 1)
            {
                //跳跃
                _dixingType = GlobalMapDate.TIAOYUE;
            }
            else if (rnums < 5)
            {
                //洞内  
                _dixingType = GlobalMapDate.DONGNEI;
            }else if (rnums < 10)
            {
                //多怪 精英 平地
                _dixingType = GlobalMapDate.DUOGUAI_JINGYING_PINGDI;
            }
            else
            {
                //一般
                _dixingType = GlobalMapDate.YIBAN;
            }


            string GlobalCMapType = GlobalMapDate.CurrentSpelMapType.Split('^')[0];
            //print("GlobalMapDate.CurrentSpelMapType   "+ GlobalMapDate.CurrentSpelMapType + "   GlobalCMapType  " + GlobalCMapType);
            if (GlobalCMapType == "dongnei")
            {
                _dixingType = GlobalMapDate.DONGNEI;
            }
            else if(GlobalCMapType == "boss"){
                //这里可以扩展 boss的 悬空地图  ***不能用 pingdi 只能用boss  平地要和 boss分开
                //boss 等特殊地图 要控制 延展长度
                //boss^bossPingdi^2^bossName
                //cundnag^2
                //jingying^JYPingdi(JYTiaoyue)^3

                _dixingType = GlobalMapDate.CurrentSpelMapType.Split('^')[1];
                _dixingkuozhanNums = int.Parse(GlobalMapDate.CurrentSpelMapType.Split('^')[2]);
            }else if (GlobalCMapType == "jingying")
            {
                //精英怪

                //地形类型
                _dixingType = GlobalMapDate.CurrentSpelMapType.Split('^')[1];
                //地形 长度 扩展
                _dixingkuozhanNums = int.Parse(GlobalMapDate.CurrentSpelMapType.Split('^')[2]);
                //怪物名字
                _JYGuaiName = GlobalMapDate.CurrentSpelMapType.Split('^')[3];
            }else if(GlobalCMapType == GlobalMapDate.CUNDANG_PINGDI)
            {
                //存档平地
                _dixingType = GlobalMapDate.CUNDANG_PINGDI;
                _dixingkuozhanNums = 1;
            }else if (GlobalCMapType == GlobalMapDate.DUOGUAI_JINGYING_PINGDI)
            {
                _dixingType = GlobalMapDate.DUOGUAI_JINGYING_PINGDI;
                if (GlobalMapDate.CurrentSpelMapType.Split('^').Length > 2)
                {
                    _dixingkuozhanNums = int.Parse(GlobalMapDate.CurrentSpelMapType.Split('^')[2]);
                }
            }
            else if (GlobalCMapType == GlobalMapDate.DUOGUAI_JSY_PINGDI)
            {
                _dixingType = GlobalMapDate.DUOGUAI_JSY_PINGDI;

            }
            else if (GlobalCMapType == GlobalMapDate.DONGNEI_TIAOYUE_1)
            {
                //洞内跳跃机关 1型
                _dixingType = GlobalMapDate.DONGNEI_TIAOYUE_1;
                if (GlobalMapDate.CurrentSpelMapType.Split('^').Length >= 2)
                {
                    _dixingkuozhanNums = int.Parse(GlobalMapDate.CurrentSpelMapType.Split('^')[1]);
                }
            }

        }
        else
        {
            //一般地形 也有可能会有 洞内 跳跃等
            _dixingType = GlobalMapDate.YIBAN;
        }

        //测试用 不用了就删掉
        //_dixingType = GlobalMapDate.DONGNEI_TIAOYUE_1;
        //_dixingType = GlobalMapDate.PINGDI;
        print(" ******************************************************************************************* _dixingType  地形类型   "+ _dixingType);

        //根据不同地形 生成的 中心连接点 也不一样  还要根据 坐标 和nums判断 地板和 景类型
        if (_dixingType == GlobalMapDate.YIBAN)
        {
            CreateLJByName("lr_"+ Globals.mapTypeNums,true);
        }else if (_dixingType == GlobalMapDate.DONGNEI)
        {
            CreateLJByName("lr_" + Globals.mapTypeNums, true);
        }
        else if (_dixingType == GlobalMapDate.TIAOYUE)
        {
            //跳跃  洞内跳跃  洞内倒挂  悬浮 +机关  这些都要在 跳跃地板里面去写 怎么获取 
            CreateLJByName("tiaoyue_" + Globals.mapTypeNums);
        }
        else if(_dixingType == "daogua")
        {
            //倒挂

        }else if (_dixingType == "xuanfushu")
        {
            //竖形长方形 的跳跃 悬浮地形

        }else if (_dixingType == GlobalMapDate.PINGDI || _dixingType == GlobalMapDate.BOSS_PINGDI|| _dixingType == GlobalMapDate.JINGYING_PINGDI|| _dixingType == GlobalMapDate.CUNDANG_PINGDI||
            _dixingType == GlobalMapDate.DUOGUAI_JINGYING_PINGDI)
        {
            CreateLJByName("lr_" + Globals.mapTypeNums, true);
        }
        else if (_dixingType == GlobalMapDate.CUNDANG_PINGDI)
        {
            //存档
        }
        else if (_dixingType == GlobalMapDate.DONGNEI_TIAOYUE_1)
        {
            //洞内跳跃机关 1型
            //_dixingType = GlobalMapDate.DONGNEI_TIAOYUE_1;
            CreateLJByName("lr_" + Globals.mapTypeNums);
            //CreateLJByName("lr_dnty_" + Globals.mapTypeNums);
        }


        //创建 连接点

        //创建 各个方向的 起始点地板

        char[] FXArr = _lianjieFX.ToCharArray();


       

        for (int i = 0; i < FXArr.Length; i++)
        {
            int xiaoDuanNums = 1 + GlobalTools.GetRandomNum(2);
            if (_dixingkuozhanNums != 0) xiaoDuanNums = _dixingkuozhanNums;
            string _fx = FXArr[i].ToString();

            string _toScreenName = ToScreenName(_fx, menFXList);


           

            //if(FXArr.Length == 1)
            //{
            //    //单门
            //    if (_fx == "u")
            //    {
            //        _fx = "r";
            //    }
            //    else if (_fx == "d")
            //    {
            //        _fx = "l";
            //    }

            //}

            CreateFZRoad(FXArr[i].ToString(), xiaoDuanNums, _toScreenName, _fx);

        }

        SpeMapSpeSet();
        //CreateFZRoad("u", 3, "test");


    }


    public string GetGuanKaType()
    {
        return _dixingType;
    }


    void SpeMapSpeSet()
    {
        float __x = lianjiedian.GetComponent<DBBase>().GetRightPos().x + lianjiedian.GetComponent<DBBase>().GetWidth() * 0.5f;
        float __y = lianjiedian.GetComponent<DBBase>().GetRightPos().y;
        if (_dixingType == GlobalMapDate.BOSS_PINGDI) {
            print("出现boss 做出现boss的相应设置");
            //找到中心地板
           
            //放入一个 随机boss  写一个 可以随机的 Boss表
            string bossName = "";

        }
        else if (_dixingType == GlobalMapDate.CUNDANG_PINGDI)
        {
            print("存档平地");
            GetCunDangDian(lianjiedian);

        }else if (_dixingType == GlobalMapDate.JUQING_PINGDI)
        {
            //剧情 平地
        }else if (_dixingType == GlobalMapDate.JINGYING_PINGDI)
        {
            //精英怪平地
            print("精英怪平地！！");
            GetJingYingGuai(lianjiedian);
        }else if (_dixingType == GlobalMapDate.DUOGUAI_JINGYING_PINGDI)
        {
            print("*************************************************************精英多怪平地！！");
            GetJingYingGuai(lianjiedian);
        }else if (_dixingType == GlobalMapDate.DUOGUAI_JSY_PINGDI)
        {
            print("多怪 警示鱼 地图");

        }
    }

    //出精英怪
    void GetJingYingGuai(GameObject diban,string GuaiNameList = "random")
    {
        float __x = diban.GetComponent<DBBase>().GetRightPos().x + lianjiedian.GetComponent<DBBase>().GetWidth() * 0.5f;
        float __y = diban.GetComponent<DBBase>().GetRightPos().y;
        string GuaiName = _JYGuaiName;
        if (GuaiName != "") {
            GuaiName = MapNames.GetInstance().GetCanRandomUSEJYGName("JingYingGuai");  //GlobalMapDate.GetCanRandomUSEJYGName();
            GameObject guai = GlobalTools.GetGameObjectByName(GuaiName);
            guai.transform.position = new Vector2(__x, __y);
            guai.transform.parent = maps.transform;
            GuaiList.Add(guai);
            return;
        }
        
        if (GuaiNameList != "")
        {
            //判断 数组 长度
            //在 循环中 判断 是否是 空怪
            //__x的位置 随机

            //随机 关卡 记录的 普通精英怪
            if (GuaiNameList == "random") {
                int nums = 1 + GlobalTools.GetRandomNum(2);
                for(int i = 0; i < nums; i++)
                {
                    GuaiName = MapNames.GetInstance().GetCanRandomUSEJYGName("YiBanJingYingGuai");
                    GameObject guai2 = GlobalTools.GetGameObjectByName(GuaiName);
                    print("   精英怪 数量   "+nums+"   精益怪名字  "+ GuaiName);
                    __x = GlobalTools.GetRandomNum() > 50 ? __x +3+ GlobalTools.GetRandomDistanceNums(2) : __x -3- GlobalTools.GetRandomDistanceNums(2);
                    guai2.transform.position = new Vector2(__x, __y);
                    guai2.transform.parent = maps.transform;
                    GuaiList.Add(guai2);
                }
            }

        }
        else
        {
            GameObject guai = GlobalTools.GetGameObjectByName(GuaiName);
            guai.transform.position = new Vector2(__x, __y);
            guai.transform.parent = maps.transform;
            GuaiList.Add(guai);
        }
        
    }


    void GetCunDangDian(GameObject diban)
    {
        float __x = diban.GetComponent<DBBase>().GetRightPos().x + lianjiedian.GetComponent<DBBase>().GetWidth() * 0.5f;
        float __y = diban.GetComponent<DBBase>().GetRightPos().y+2.4f;
        GameObject cundangdian = GlobalTools.GetGameObjectByName("C1_cundangdian");
        cundangdian.transform.position = new Vector2(__x, __y);
        cundangdian.transform.parent = maps.transform;
    }


    //出机关



    protected override void GetGuaiControlMen()
    {
        //print("有怪没啊    " + GuaiList.Count);
        //中心地板 不是左右连接 或者 怪数量小于4 不允许关门
        //如果是左右地图 但是 是跳跃型 也不能关门 后面要做判断


        //print("g怪兽数组   "+GuaiList.Count);
        foreach(GameObject guai in GuaiList)
        {
            //print("  怪物的 类型是什么  " + guai.GetComponent<RoleDate>().enemyType);
            if (guai == null||guai.GetComponent<RoleDate>()==null) continue;
            string guaiType = guai.GetComponent<RoleDate>().enemyType;
            if (guaiType == "jingying"||guaiType == "boss")
            {
                print("有精英怪  可以关门 ");
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "kyguanmen"), this);
                return;
            }
        }


        print("_dixingType   -------------- 地形  "+ _dixingType);
        if (_dixingType == GlobalMapDate.DUOGUAI_JINGYING_PINGDI)
        {
            print("*******************************************  小多精英怪! ");
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "kyguanmen"), this);
        } else if (_dixingType == GlobalMapDate.BOSS_PINGDI || _dixingType == GlobalMapDate.JINGYING_PINGDI) {
            print(" ***************************************************************************************** boss 或者精英怪 地形    可以 关门！！！！");
            
        } else if (_lianjiedibanType == "lr" && GuaiList.Count >= 4)
        {
            //print("  @@@@@@@@@--------------------------------- 是左右地图  有4个以上的怪 可以 关门！！！！");
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "kyguanmen"), this);
        }
        else
        {
            print(" @@@@@@@@@@@    ------------------------ iiiiiiiiiiiiiiiiiiiiii  不许关门  ");
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "meiguai"), this);
        }
        //print("---------------------->有怪没啊>    " + GuaiList.Count);
    }







    string ToScreenName(string fx,List<string> MenList)
    {
        string toMenName = "";
        for(int i = 0; i < MenList.Count; i++)
        {
            //print("MenList -------->???  " + MenList[i]);
            if (MenList[i].Split(':')[0] == fx) return MenList[i].Split(':')[1].ToString();
        }
        return toMenName;
    }



    bool IsHasFX(string lianjieFX,string fx)
    {
        return lianjieFX.Contains(fx);
    }

    bool _IsCanChuGuai = true;
    protected override void CreateFZRoad(string fx, int mapNums, string goScreenName, string danFX = "")
    {
        for (int i = 0; i <= mapNums; i++)
        {
            _IsCanChuGuai = true;
            if (i == mapNums - 1)
            {
                _isNearMenDuan = true;
            }
            else
            {
                _isNearMenDuan = false;
            }
            //这里要 小段 有几个地图组成
            int XiaoDiTuNums = 1 + GlobalTools.GetRandomNum(3);
            if (_dixingkuozhanNums != 0) XiaoDiTuNums = 1;
            if (fx == "l" || fx == "d")
            {
                if (i != mapNums)
                {
                    CreateRoadDuanByFX(fx, XiaoDiTuNums, i);

                    if(i == 0&&((fx == "l" && IsHasFX(_lianjieFX,"u"))||(fx == "d"&& IsHasFX(_lianjieFX, "r"))))
                    {
                        _IsCanChuGuai = false;
                    }
                }
                else
                {
                    //********************************@***********************门 地图生成 控制
                    string menNameL = "dt_men_l";
                    if(Globals.mapTypeNums!=1) menNameL = "dt_men_l_"+ mapTypeNums;
                    mapObj = GlobalTools.GetGameObjectByName(menNameL);
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
                    if (_dixingType == GlobalMapDate.DONGNEI_TIAOYUE_1) pos = new Vector2(LJDpos.x, LJDpos.y);
                    GetMapObjPos(0);

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
                    string menNameR = "dt_men_r";
                    if (Globals.mapTypeNums != 1) menNameR = "dt_men_r_" + mapTypeNums;
                    mapObj = GlobalTools.GetGameObjectByName(menNameR);
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
                    if (_dixingType == GlobalMapDate.DONGNEI_TIAOYUE_1) pos = new Vector2(LJDpos.x,LJDpos.y);
                    GetMapObjPos(0);
                }
            }

            //if (i != mapNums) ChuGuaiByMapNunms(i, mapNums, fx);

        }
    }

    //是否是临门段
    bool _isNearMenDuan = false;
    bool _isNearMen = false;
    void IsNearMen(int i,int DuanNums)
    {
        _isNearMen = i == DuanNums - 1 ? true : false;
    }

    void ChuGuaiByMapNunms(int i = 0,int mapNums = 0,string fx = "l")
    {
        //boss平地 剧情平地 存档平地
        if (_dixingType == GlobalMapDate.BOSS_PINGDI|| _dixingType == GlobalMapDate.JUQING_PINGDI|| _dixingType == GlobalMapDate.CUNDANG_PINGDI) return;

        //return;

        //判断 是否是临门的 最后的小段 地板

        //判断是否是 下路或者左路的 第一段
        if (!_IsCanChuGuai) return;


        //如果是 临近门的 地板段 判断 方向  和 是否是最后一段
        if(!_isNearMenDuan||(_isNearMenDuan && !_isNearMen))
        {
            ChuGuai();
        }


        //if (i != mapNums - 1)
        //{
        //    ChuGuai();
        //}
        //else
        //{
        //    //近门最后一个 地板 上面出怪   先取消 太近出门就被攻击
        //    ChuGuai(false, fx);
        //}
    }



    Vector2 LJDpos;
    Vector2 pos;
    int sd;
    GameObject mapObj;

    bool _isQishi0 = false;

    //生成小段的 地形     参数 isFirstNums 是指 是否是 中心连接点出来的 第一个地图 是的话就要取中心地图 连接点位置
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



        //print("_isQishi0  " + _isQishi0 + " isFirstNums  " + isFirstNums + "    ");

        



        if(_dixingType == GlobalMapDate.YIBAN)
        {
            //print("*******************************一般地形 生成");
            YiBanDiXing(FX,DuanNums,isFirstNums,IsToMen);
        }else if (_dixingType == GlobalMapDate.TIAOYUE)
        {
            //print("*******************************跳跃地形………… 生成");
            //跳跃地形
            TiaoYueDiBan(FX, DuanNums, isFirstNums, IsToMen);
        }
        else if(_dixingType == GlobalMapDate.DONGNEI)
        {
            YiBanDiXing(FX, DuanNums, isFirstNums, IsToMen);
        }
        else if (_dixingType == GlobalMapDate.PINGDI|| _dixingType == GlobalMapDate.BOSS_PINGDI ||_dixingType == GlobalMapDate.JINGYING_PINGDI|| _dixingType == GlobalMapDate.CUNDANG_PINGDI ||
            _dixingType == GlobalMapDate.DUOGUAI_JINGYING_PINGDI || _dixingType == GlobalMapDate.DUOGUAI_JSY_PINGDI)
        {
            YiBanDiXing(FX, DuanNums, isFirstNums, IsToMen);
        }else if (_dixingType == GlobalMapDate.DONGNEI_TIAOYUE_1)
        {
            TiaoYueDiBan(FX, DuanNums, isFirstNums, IsToMen, "db_dnty");
        }



        
    }





    //默认可以生成跳跃地形
    bool _morenKeyiShengChengTiaoyue = true;
    //一般地形
    void YiBanDiXing(string FX, int DuanNums, int isFirstNums, bool IsToMen = true)
    {
        int nums = GlobalTools.GetRandomNum();
        _morenKeyiShengChengTiaoyue = true;
        //这个是 生成 一般的地图
        for (int i = 0; i < DuanNums; i++)
        {
            IsNearMen(i,DuanNums);
            //mapObj = GetDiBanByName();
            if (FX == "l")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    LJDpos = lianjiedian.GetComponent<DBBase>().GetLeftPos();
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                }
                //判断是否是 对接连接点的 起始路
                if (isFirstNums == 0 && IsHasFX(_lianjieFX, "d"))
                {
                    mapObj = GetDiBanByName();
                    PuTongShangShengDX(i, FX, true);
                }
                else
                {
                    ShengChengDiBan(i, FX, nums,false,DuanNums);
                }

            }
            else if (FX == "d")
            {
                if (IsHasFX(_lianjieFX, "l")) _morenKeyiShengChengTiaoyue = false;
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    LJDpos = lianjiedian.GetComponent<DBBase>().GetLeftPos();
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                }

                if (isFirstNums == 0 && IsHasFX(_lianjieFX, "l"))
                {
                    mapObj = GetDiBanByName();
                    PuTongXiaJiangDX(i, FX, true);
                }
                else
                {
                    ShengChengDiBan(i, FX, nums,false,DuanNums);

                }
            }
            else if (FX == "r")
            {
                if(IsHasFX(_lianjieFX, "u")) _morenKeyiShengChengTiaoyue = false;
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    if (lianjiedianY)
                    {
                        LJDpos = lianjiedianY.GetComponent<DBBase>().GetRightPos();
                    }
                    else
                    {
                        LJDpos = lianjiedian.GetComponent<DBBase>().GetRightPos();
                    }

                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                }
                //判断是否是 对接连接点的 起始路
                if (isFirstNums == 0 && IsHasFX(_lianjieFX, "u"))
                {
                    mapObj = GetDiBanByName();
                    PuTongXiaJiangDX(i, FX, true);
                }
                else
                {
                    ShengChengDiBan(i, FX, nums,false,DuanNums);
                }
            }
            else if (FX == "u")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;

                    if (lianjiedianY)
                    {
                        LJDpos = lianjiedianY.GetComponent<DBBase>().GetRightPos();
                    }
                    else
                    {
                        LJDpos = lianjiedian.GetComponent<DBBase>().GetRightPos();
                    }
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                }

                if (isFirstNums == 0)
                {
                    mapObj = GetDiBanByName();
                    if (IsHasFX(_lianjieFX, "r"))
                    {
                        PuTongShangShengDX(i, FX, true);
                    }
                    else
                    {
                        PuTongShangShengDX(i, FX, false);
                    }

                }
                else
                {
                    ShengChengDiBan(i, FX, nums,false ,DuanNums);

                }
            }
            GetMapObjPos(i);
            ChuGuaiByMapNunms();
        }
        _isQishi0 = false;
        //洞内的 地形判断
        _IsGetDongNeiLuType = false;
        //小段 地形 是否有 洞内判断
        _isCanHasDongNei = false;
    }


    protected override GameObject GetDiBanByName(string mapObjTypeName = "db_pd")
    {
        //**********************@****************普通地板生成数据控制
        string mapArrName = mapObjTypeName;

        DibanType = GameMapDate.GetLianjiedianTypeByCName(CMapName);

        if (PingdiDibanType != "") mapArrName = PingdiDibanType;
        if (DibanType != "") mapArrName += "_" + Globals.mapTypeNums;
        int nums = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance()).Count;
        string mapName = GetDateByName.GetInstance().GetListByName(mapArrName, MapNames.GetInstance())[GlobalTools.GetRandomNum(nums)];
        return GlobalTools.GetGameObjectByName(mapName);
    }





    //跳跃地形
    void TiaoYueDiBan(string FX, int DuanNums, int isFirstNums, bool IsToMen = true,string SpeDiBanType = "tiaoyue")
    {

        int nums = GlobalTools.GetRandomNum();
        //这个是 生成 一般的地图
        for (int i = 0; i < DuanNums; i++)
        {
            IsNearMen(i, DuanNums);
            //创建地板
            mapObj = GetDiBanByName(SpeDiBanType);

            if (i != DuanNums - 1)
            {
                if(SpeDiBanType == "tiaoyue") mapObj.GetComponent<DB_TiaoYue>().JiGuan_PenSheZiDanJG();
            }

            if (FX == "l")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    LJDpos = lianjiedian.GetComponent<DBBase>().GetLeftPos();
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                }
                PuTongPingDiDX(i, FX, false);

            }
            else if (FX == "d")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    LJDpos = lianjiedian.GetComponent<DBBase>().GetLeftPos();
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetLeftPos();
                }

                PuTongPingDiDX(i, FX, false);
            }
            else if (FX == "r")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;
                    if (lianjiedianY)
                    {
                        LJDpos = lianjiedianY.GetComponent<DBBase>().GetRightPos();
                    }
                    else
                    {
                        LJDpos = lianjiedian.GetComponent<DBBase>().GetRightPos();
                    }

                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                }
                //判断是否是 对接连接点的 起始路
                PuTongPingDiDX(i, FX, false);
            }
            else if (FX == "u")
            {
                if (isFirstNums == 0 && !_isQishi0)
                {
                    _isQishi0 = true;

                    if (lianjiedianY)
                    {
                        LJDpos = lianjiedianY.GetComponent<DBBase>().GetRightPos();
                    }
                    else
                    {
                        LJDpos = lianjiedian.GetComponent<DBBase>().GetRightPos();
                    }
                }
                else
                {
                    LJDpos = _cMapObj.GetComponent<DBBase>().GetRightPos();
                }

                PuTongPingDiDX(i, FX, false);
            }
            //设置地图块的 位置 和深度
            GetMapObjPos(i);
            if(SpeDiBanType == "tiaoyue") ChuGuaiByMapNunms();
        }

        _isQishi0 = false;
    }












    //延伸的 地板
    void YanShengDiBan(string FX,int DuanNums,int isFirstNums, bool IsFenglu = false) {
        //生成什么 地形的 几率
        int nums = GlobalTools.GetRandomNum();
        for (int i = 0; i < DuanNums; i++)
        {
            
            if(FX== "l"||FX == "d")
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
            }
            else if (FX == "r" || FX == "u")
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
            }
          

            ShengChengDiBan(i, FX, nums,true, DuanNums);
            //放置和 记录地板 
            GetMapObjPos(i);

            if (i == DuanNums - 1)
            {
                if (IsFenglu)
                {
                    //封路

                }
                else
                {
                    lianjiedianY = mapObj;
                }
            }
        }
        _isQishi0 = false;
    }


    bool _IsGetDongNeiLuType = false;
    string _luType = "pingdi";
    //判断 小段路的 地形
    string GetDongNeiLuType()
    {
        if (!_IsGetDongNeiLuType)
        {
            _IsGetDongNeiLuType = true;
            int nums = GlobalTools.GetRandomNum();
            if (nums < 30)
            {
                if (_luType != "xiajiang")
                {
                    _luType = "shangsheng";
                }
                else
                {
                    _luType = "pingdi";
                }
            }
            else if (nums < 60)
            {
                if (_luType != "shangsheng")
                {
                    _luType = "xiajiang";
                }
                else
                {
                    _luType = "pingdi";
                }
                
            }
            else
            {
                _luType = "pingdi";
            }
        }
        return _luType;
    }


    bool _isCanHasDongNei = false;
    //判断 是否能有洞内
    bool IsCanHasDongNei()
    {
        if (!_isCanHasDongNei)
        {
            _isCanHasDongNei = true;
            //还要根据 全局关卡 信息判断  越往下 越多
            if (GlobalTools.GetRandomNum() < 90)
            {
                return false;
            }
        }
        return true;
    }


    //这个是 所有地形都包括了
    //nums 是几率 不是数组长度
    void ShengChengDiBan(int i, string FX, int ReNums, bool IsYanZhan = false,int duanNums = 0)
    {
        //print("  nums  "+nums+ "  IsYanZhan  "+ IsYanZhan);

        //这里根据几率 怎么算几率 来生成 不同地形

        float __XDistance = 0;
        float __YDistance = GlobalTools.GetRandomDistanceNums(2);
        //**洞内****
        if (_dixingType == "dongnei")
        {
            //全洞内 直线  洞内的时候 是先 有 _dixingType == lr作为前置判断的 所以这里只可能是lr
            mapObj = GetDiBanByName();

            //是否有 高密度机关图
            print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            print("fx   "+FX+"   >>>>>>>>>>>洞内图!!!!  duanNums是多少  "+ duanNums+"   i "+i);

            if (GetDongNeiLuType() == "shangsheng")
            {
                
                __XDistance = FX == "l" ? 4 : -4;
                PuTongShangShengDX(i, FX, false);
                mapObj.GetComponent<DBBase>().ShowDingDB(__YDistance, __XDistance);

                print("fx   " + FX + "   >>>>>>>>>>>洞内 上升!!!!duanNums    " + duanNums);
            }
            else if (GetDongNeiLuType() == "xiajiang")
            {
                __XDistance = FX == "l" ? -4 : 4;
                PuTongXiaJiangDX(i, FX, false);
                if (duanNums != 0 && i != duanNums - 1) mapObj.GetComponent<DBBase>().ShowDingDB(__YDistance, __XDistance);
                //mapObj.GetComponent<DBBase>().ShowDingDB(__YDistance, __XDistance);
                print("fx   " + FX + "   >>>>>>>>>>>洞内 下降@@@@@@!!!!duanNums    " + duanNums);
            }
            else
            {
                __XDistance = FX == "l" ? 4 : -4;
                PuTongPingDiDX(i, FX, false);
                mapObj.GetComponent<DBBase>().ShowDingDB(__YDistance, __XDistance);
                print("fx   " + FX + "   >>>>>>>>>>>洞内 平地=======@@@@@@!!!!duanNums    " + duanNums);
            }
            

            return;
        }




        if (_dixingType == GlobalMapDate.PINGDI|| _dixingType == GlobalMapDate.BOSS_PINGDI|| _dixingType == GlobalMapDate.JINGYING_PINGDI|| _dixingType == GlobalMapDate.CUNDANG_PINGDI||
            _dixingType == GlobalMapDate.DUOGUAI_JINGYING_PINGDI|| _dixingType == GlobalMapDate.DUOGUAI_JSY_PINGDI)
        {
            mapObj = GetDiBanByName();
            PuTongPingDiDX(i, FX, false);
            return;
        }



        if (Globals.mapType=="1")
        {
            //如果是第一关 在自动地图里面 都是 上下交错的 没有平地
            ReNums = 50 + GlobalTools.GetRandomNum(50);
        }



        if (ReNums < 30)
        {
            //平地
            mapObj = GetDiBanByName();
            PuTongPingDiDX(i, FX, false);
            //boss
            //精英图
            //剧情
            //存档点

        }
        else if (ReNums < 50)
        {
            //这个里面 都是平地型的 地形
            //跳跃
            if (_morenKeyiShengChengTiaoyue)
            {
                mapObj = GetDiBanByName("tiaoyue");
                if (i != duanNums - 1)
                {
                    mapObj.GetComponent<DB_TiaoYue>().JiGuan_PenSheZiDanJG();
                }
            }
            else
            {
                mapObj = GetDiBanByName();
            }
            //洞内***********************************************************************
            //mapObj = GetDiBanByName();
            PuTongPingDiDX(i, FX, false);

            //是否有 喷射的 子弹机关

            //是否有刺机关 隐刺机关

        }
        else if (ReNums < 80)
        {
            //普通下降地形
            mapObj = GetDiBanByName();
            if (IsYanZhan)
            {
                PuTongXiaJiangDX(i, FX, false);
            }else if ((FX == "l" && IsHasFX(_lianjieFX, "d"))|| (FX == "u" && IsHasFX(_lianjieFX, "r")))
            {
                //如果包含 D 下路 就不能有下降 下降改为平地  ***这里可以判断 是否是洞内
                PuTongPingDiDX(i, FX, false);

                if (i != 0 && IsCanHasDongNei()&&duanNums != 1)
                {
                    __XDistance = FX == "l" ? 4 : -4;
                    if(duanNums!=1)mapObj.GetComponent<DBBase>().ShowDingDB(__YDistance, __XDistance);
                }
            }
            else
            {
                PuTongXiaJiangDX(i, FX, false);

                if (i != duanNums-1 && IsCanHasDongNei() && duanNums != 1)
                {
                    __XDistance = (FX == "l"|| FX == "d") ? -4 : 4;
                    if ((FX == "d" && IsHasFX(_lianjieFX, "l")) || (FX == "r" && IsHasFX(_lianjieFX, "u"))) return;
                    mapObj.GetComponent<DBBase>().ShowDingDB(__YDistance, __XDistance);
                }
            }

            //是否有 隐刺 
            GetYinci();


        }
        else
        {
            //普通上升地形
            mapObj = GetDiBanByName();
            if (IsYanZhan)
            {
                PuTongShangShengDX(i, FX, false);
            }
            else if ((FX== "d" && IsHasFX(_lianjieFX, "l"))|| (FX == "r" && IsHasFX(_lianjieFX, "u")))
            {
                //如果包含 L 左路 就不能上升 上升改为平地
                PuTongPingDiDX(i, FX, false);
            }
            else
            {
                //上升 这里判断是否在洞内
                PuTongShangShengDX(i, FX, false);
                if (i!=0&&IsCanHasDongNei() && duanNums != 1)
                {
                    __XDistance = (FX == "d"|| FX == "l") ? 4 : -4;
                    //if ((FX == "l" && IsHasFX(_lianjieFX, "d")) || (FX == "u" && IsHasFX(_lianjieFX, "r"))) return;
                    mapObj.GetComponent<DBBase>().ShowDingDB(__YDistance, __XDistance);
                }
            }
            GetYinci();

        }
    }




    void GetYinci()
    {
        //这里要根据 关卡nums 和坐标吧
        if (GlobalTools.GetRandomNum() > 150)
        {
            mapObj.GetComponent<DBBase>().JG_GetYinci();
        }
    }



    void GetMapObjPos(int i)
    {
        sd = 19 + i % 7;
        if (mapObj.transform.Find("diban")) mapObj.GetComponent<DBBase>().SetSD(sd);
        mapObj.transform.position = pos;
        mapObj.transform.parent = maps.transform;
        if (mapObj.GetComponent<DBBase>().IsShowDingDB) mapObj.GetComponent<DBBase>().SetDingDBPos();
        _cMapObj = mapObj;
        //mapObj.GetComponent<DBBase>().GetJing(); 不能这样调用 这样调用 会导致 后面 按数据生成的时候 无法处理
        mapObjArr.Add(mapObj);
    }




    //普通跳跃地板
    void PuTongTiaoYueDB(int i, string fx, bool IsQishi = false)
    {

    }




    //普通 梯度上升 地形
    void PuTongShangShengDX(int i,string fx, bool IsQishi = false)
    {
        mapObj.GetComponent<DBBase>().GetDiBanYuanBeiJingUpOrDown("up");
        if (fx == "l"|| fx == "d")
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
        mapObj.GetComponent<DBBase>().GetDiBanYuanBeiJingUpOrDown("down");
        float xiajiang = 0;
        GameObject dangban ;
        if (IsQishi&&i==0) xiajiang = 7;
        if (fx == "l"|| fx == "d")
        {
            pos = new Vector2(LJDpos.x - mapObj.GetComponent<DBBase>().GetWidth(), LJDpos.y - mapObj.GetComponent<DBBase>().GetHight() * 0.5f - GlobalTools.GetRandomDistanceNums(1)- xiajiang);

        }
        else
        {
            pos = new Vector2(LJDpos.x , LJDpos.y - mapObj.GetComponent<DBBase>().GetHight() * 0.5f - GlobalTools.GetRandomDistanceNums(1)- xiajiang);
        }



        if (IsQishi && i == 0)
        {
            //下降位置的 挡板 防止穿帮
            dangban = GlobalTools.GetGameObjectByName("db_pd_db");
            Vector2 posDangBan = pos;
            if(fx == "l" || fx == "d")
            {
                posDangBan = new Vector2(pos.x + mapObj.GetComponent<DBBase>().GetWidth(), pos.y);
            }
            else
            {
                posDangBan = new Vector2(pos.x - mapObj.GetComponent<DBBase>().GetWidth(), pos.y);
            }

            sd = 18;
            if (dangban.transform.Find("diban")) mapObj.GetComponent<DBBase>().SetSD(sd);
            dangban.transform.position = posDangBan;
            dangban.transform.parent = maps.transform;
            mapObjArr.Add(dangban);
            //****** 这个地板 不能显示 景 注意

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
