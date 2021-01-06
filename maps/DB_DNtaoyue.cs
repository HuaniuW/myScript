using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_DNtaoyue : DBBase
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    [Header("是否是长隧道")]
    public bool IsChangSuiDao = false;

    protected override void InitStart()
    {
        //每次都会 调用
        print("@@@@@  每次都会调用  ininstart!!! ");
        //ShowTheDingDB = false;
        HideDingDB();
        if (!ShowTheDingDB)
        {
            print("  隐藏 顶地板！！！  ");
            HideDingDB();
            return;
        }
        else
        {
            ShowDingDB();
        }
    }


    void GetMapSet()
    {
        //获取地形 设置
        //1.纯地板
        //2.有浮动地板
        //3.倒挂
        //4.有下落地板
        //5.有变窄
        //6.有弹射



        //1.出怪几率  机关几率
        //2.有顶 无顶
    }


    protected override void OtherStart()
    {
        
        GetTYDiBan();
        BianZhai();//注意 这里不能放在 GetTYDiBan之前执行
        if (IsChangSuiDao)
        {

        }
        else
        {
            JiGuanAndGuai();
        }
    }


    void ChangSuiDaoJiGuanAndGuai()
    {
        //出弹射地板
    }


    public override void ShowDingDB(float __posY = 0, float __posX = 0)
    {
        if (!ShowTheDingDB) {
            dibanDing.SetActive(false);
            return;
        } 
        if (!dibanDing.activeSelf)
        {
            dibanDing.SetActive(true);
        }

        if (dibanDing.activeSelf) IsShowDingDB = true;

        __dingDBPosX = __posX;
        float wuchaY = GlobalTools.GetRandomDistanceNums(2);
        __dingDBPosY = __posY - wuchaY;


        //print("顶部景控制*****   " + dibanDing.activeSelf);
        //生成 顶部时候 不许出树 或者 大概率不许出树
        //dibanDing.SetActive(true);

    }

    //可变窄
    bool IsCanBianZhai = true;
    //变窄几率
    int BianZhaiJL = 60;

    //显示顶
    bool ShowTheDingDB = true;

    void BianZhai()
    {
        
        if (!IsCanBianZhai) return;
        //print("--------------------------------------------------->  变窄！！！！ ");

        if (GlobalTools.GetRandomNum() > 60) return;

        float zhudibanBianZhaiNums = 0.5f+GlobalTools.GetRandomDistanceNums(1);

        if (diban1 != null)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + zhudibanBianZhaiNums); 
        }

        if (ShowTheDingDB)
        {
            dibanDing.transform.position = new Vector2(dibanDing.transform.position.x, dibanDing.transform.position.y- zhudibanBianZhaiNums);
        }
    }


    //去顶的 
    bool IsQuDing = false;
    //景什么的 Y位置 修正
    float xiajiangNums = 3.5f;

    protected override void GetTopJ3()
    {
        if (!ShowTheDingDB) return;
        string qjuArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju");
        if (qjuArrName == "") return;
        int nums = 2 + GlobalTools.GetRandomNum(4);
        //DingDBPosL.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //DingDBPosR.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //print("  ??>>>>>>>>>**qjuArrName   " + qjuArrName+"   pos  "+ DingDBPosL.transform.position);
        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;
        SetJingByDistanceU(qjuArrName, nums, pos1, pos2, pos1.y - 2, 0, 0, 50, "d");
    }

    protected override void GetTopJ4()
    {
        if (!ShowTheDingDB) return;
        string qju2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju2");
        if (qju2ArrName == "") return;
        int nums = 3 + GlobalTools.GetRandomNum(2);
        //DingDBPosL.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //DingDBPosR.transform.parent = GlobalTools.FindObjByName("maps").transform;
        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;
        SetJingByDistanceU(qju2ArrName, nums, pos1, pos2, pos1.y - 1.5f, -0.3f, 0, 20, "d");
    }


    protected override void GetLRJinBG()
    {
        //怎么根据 关卡来判断出来的 景数量？？？之要判断数量？  还有位置  关于旋转？？树好像有旋转  看看怎么写进去
        string jjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("jjd");
        if (jjdArrName == "") return;
        int nums = 1 + GlobalTools.GetRandomNum(6);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        string jjdArrName2 = MapNames.GetInstance().GetJingArrNameByGKKey("jjd2");
        if (jjdArrName2 == "") return;

        if (Globals.mapTypeNums == 1)
        {
            SetJingByDistanceU(jjdArrName, nums, pos1, pos2, pos1.y-xiajiangNums, 0, 0, -15, "u");

            nums = 1 + GlobalTools.GetRandomNum(1);
            SetJingByDistanceU(jjdArrName2, nums, pos1, pos2, pos1.y-xiajiangNums, 0, 0, -14, "u", 2);
        }
        else if (Globals.mapTypeNums == 2)
        {
            nums = 1 + GlobalTools.GetRandomNum(6);
            SetJingByDistanceU(jjdArrName, nums, pos1, pos2, pos1.y + 0.5f-xiajiangNums, 0, 0, -15, "u");

            nums = 1 + GlobalTools.GetRandomNum(1);
            SetJingByDistanceU(jjdArrName2, nums, pos1, pos2, pos1.y-xiajiangNums, 0, 0, -14, "u", 2);
        }
    }



    //近前景
    protected override void GetJQJ()
    {
        int nums = 0;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        if (Globals.mapTypeNums == 1)
        {
            nums = 4 + GlobalTools.GetRandomNum(4);
            //float _y = pos1.y - 1.5f;
            string qjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd");
            if (qjdArrName != "") SetJingByDistanceU(qjdArrName, nums, pos1, pos2, pos1.y - 1f - GlobalTools.GetRandomDistanceNums(1)-xiajiangNums, -0.2f, 0.1f, 30, "u");
            //SetJingByDistanceU("qjd_1", nums, pos1, pos2, pos1.y, -4f, -1f, 40, "u");
            string qjd2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd2");
            if (qjd2ArrName != "") SetJingByDistanceU(qjd2ArrName, nums, pos1, pos2, pos1.y - 1.5f - xiajiangNums, -0.3f, 0, 30, "u");

            string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            if (qjd3ArrName != "") SetJingByDistanceU(qjd3ArrName, nums, pos1, pos2, pos1.y - 1.6f - xiajiangNums, -0.4f, 0, 30, "u");

            string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            nums = 1 + GlobalTools.GetRandomNum(1);
            if (qyjdArrName != "") SetJingByDistanceU(qyjdArrName, nums, pos1, pos2, pos1.y - 2.2f - xiajiangNums, -0.6f, 1, 40, "u", 2);
        }
        else if (Globals.mapTypeNums == 2)
        {
            nums = 1 + GlobalTools.GetRandomNum(1);
            string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            if (qjd3ArrName != "") SetJingByDistanceU(qjd3ArrName, nums, pos1, pos2, pos1.y - 2f - xiajiangNums, -0.6f, 1.2f, 30, "u");

            string qjd5ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd5");
            nums = 1 + GlobalTools.GetRandomNum(2);
            if (qjd5ArrName != "") SetJingByDistanceU(qjd5ArrName, nums, pos1, pos2, pos1.y - 1f - xiajiangNums, -0.2f, 0.6f, 35, "u", 2);

            string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd4");
            nums = 1 + GlobalTools.GetRandomNum(1);
            if (qyjdArrName != "") SetJingByDistanceU(qyjdArrName, nums, pos1, pos2, pos1.y - 1.9f - xiajiangNums, -1.6f, 1, 50, "u", 2);
        }
    }



    //生成 粒子雾 
    protected override void GetWus()
    {

        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        //SetJingByDistanceU("liziWu_1", nums, pos1, pos2, pos1.y+1, 0, 0, 0, "u");
        //Color color1 = new Color(0.1f, 1f, 1f, 0.1f);
        //GetWu("", pos1, pos2, -30, color1);
        Color color2 = MapNames.GetInstance().GetColorByGKKey(); //new Color(0.1f, 1f, 1f, 0.3f);
        GetWu("", pos1, pos2, -60, color2);


        if (!IsLiziWu) return;

        string liziArrName = MapNames.GetInstance().GetJingArrNameByGKKey("liziWu");
        if (liziArrName == "") return;


        List<string> liziArr = GetDateByName.GetInstance().GetListByName(liziArrName, MapNames.GetInstance());
        SetLiziByNums(1, liziArr, pos1.x, pos2.x, pos1.y - 1-xiajiangNums);





        if (GlobalTools.GetRandomNum() > 0)
        {
            liziArrName = MapNames.GetInstance().GetJingArrNameByGKKey("liziWu2");
            if (liziArrName == "") return;
            liziArr = GetDateByName.GetInstance().GetListByName(liziArrName, MapNames.GetInstance());
            SetLiziByNums(1, liziArr, pos1.x, pos2.x, pos1.y - 1-xiajiangNums);
        }

    }

    protected override void GetWu(string wuName, Vector2 qidian, Vector2 zhongdian, int SDOrder, Color color)
    {
        string _wuName = "wu_1_1";
        GameObject _wu = GlobalTools.GetGameObjectByName(_wuName);
        //补的雾 看后面的需求
        //GameObject _wu2 = GlobalTools.GetGameObjectByName(_wuName);
        _wu.transform.parent = GlobalTools.FindObjByName("maps").transform;
        float _w = GlobalTools.GetJingW(_wu);
        float _h = GlobalTools.GetJingH(_wu);
        float _w2 = Mathf.Abs(zhongdian.x - qidian.x);
        float _h2 = Mathf.Abs(zhongdian.y - qidian.y) + 5;

        //print(" 地板宽度     "+_w2);


        _wu.transform.localScale = new Vector3(_w2 / (_w + 0.6f), _h2 / _h * 3, 1);
        //print("  缩放 "+ _wu.transform.localScale);
        _wu.transform.position = new Vector2(qidian.x + _w2 * 0.5f + (_w2 - GlobalTools.GetJingW(_wu)) * 0.5f, zhongdian.y + GlobalTools.GetJingH(_wu) * 0.5f - 0.3f-xiajiangNums);

        //print(">?????????????????????????????????????????????????????????????    "+ _w+"  >-缩放后  " +GlobalTools.GetJingW(_wu));

        GlobalTools.SetMapObjOrder(_wu, SDOrder);

        //print("雾的宽度  "+_w+"  _w2宽度  "+_w2+"   宽度缩放比例    "+_w2/_w+"   weizhi "+_wu.transform.position);
        //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  "+_wu.transform.position+"   起点  "+qidian+"   终点 "+zhongdian);


        //改变雾的颜色
        _wu.GetComponent<SpriteRenderer>().color = color;// new Color(0.1f,1f,1f,0.5f);//new Color((129 / 255)f, (69 / 255)f, (69 / 255)f, (255 / 255)f); //Color.red;

    }


    //近远景
    protected override void GetJYJ()
    {
        //控制数量  要不根据 宽来定数量
        string jyjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("jyj");
        if (jyjArrName == "") return;

        //string jyjArrName = "jyj_1";
        int nums = 5 + GlobalTools.GetRandomNum(3);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;

        SetJingByDistanceU(jyjArrName, nums, pos1, pos2, pos1.y - 0.5f - GlobalTools.GetRandomDistanceNums(2)-xiajiangNums, 0.7f, 0.3f, -30, "u", 1);



        if (Globals.mapTypeNums == 2)
        {
            print("    进来没 中远景！！！！！！！！！   ");
            nums = GlobalTools.GetRandomNum(2);
            string jyjArrName2 = MapNames.GetInstance().GetJingArrNameByGKKey("zyj");
            SetJingByDistanceU(jyjArrName2, nums, pos1, pos2, pos1.y - 0.5f - GlobalTools.GetRandomDistanceNums(2)-xiajiangNums, 2f, 0.6f, -40, "u", 1);


            nums = 1 + GlobalTools.GetRandomNum(1);
            string jyjArrName3 = MapNames.GetInstance().GetJingArrNameByGKKey("dyj");
            SetJingByDistanceU(jyjArrName3, nums, pos1, pos2, pos1.y - 0.5f - GlobalTools.GetRandomDistanceNums(2)-xiajiangNums, 5f, 1.6f, -50, "u", 1);

        }

    }

    //装饰物 是单个 等 什么的 只要单个的   一个底面只要一个 位置随机
    protected override void Zhuanshiwu()
    {
        string zswArrName = MapNames.GetInstance().GetJingArrNameByGKKey("zsw");
        if (zswArrName == "") return;

        //判断是否有装饰物  只有一个
        if (GlobalTools.GetRandomNum() < 20) return;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        //int nums = 1 + GlobalTools.GetRandomNum(2);
        //SetJingByDistanceU("jyj_1", nums, pos1, pos2, pos1.y - GlobalTools.GetRandomDistanceNums(2), 0, 0, -10, "u");



        GameObject Jobj = GetJObjByListName(zswArrName);

        string jingNameKey = Jobj.name.Split('_')[0];


        float jingW = 0;
        float jingH = 0;

        if (Jobj.GetComponent<J_SPBase>())
        {
            jingW = Jobj.GetComponent<J_SPBase>().GetWidth();
            if (Jobj.GetComponent<J_SPBase>().light2d != null)
            {
                Jobj.GetComponent<J_SPBase>().light2d.color = GlobalTools.RandomColor();
            }

            Jobj.GetComponent<J_SPBase>().SetSD(-10);
        }
        else
        {
            jingW = GlobalTools.GetJingW(Jobj);
            jingH = GlobalTools.GetJingH(Jobj);
        }



        float _w = GetWidth() - jingW;
        float __x = tl.position.x + GlobalTools.GetRandomDistanceNums(_w);
        float __y = tl.position.y - GlobalTools.GetRandomDistanceNums(1);



        if (jingNameKey == "lj")
        {
            if (Jobj.GetComponent<J_SPBase>())
            {
                __x = tl.position.x;
                __y = tl.position.y - GlobalTools.GetRandomDistanceNums(0.3f);
            }
            else
            {
                jingW = GlobalTools.GetJingW(Jobj);  //Jobj.GetComponent<SpriteRenderer>().bounds.;
                jingH = GlobalTools.GetJingH(Jobj);

                __x = tl.position.x + jingW * 0.5f;
                __y = tl.position.y + jingH * 0.5f + GlobalTools.GetRandomDistanceNums(0.5f);
            }

            if (GlobalTools.GetRandomNum() > 40)
            {
                //限制 出的几率 免得 太多
                Jobj.transform.position = new Vector3(__x, __y, 0);
                return;
            }

            int nums = (int)(_w / jingW) - 1;


            if (nums > 1)
            {
                int maxNums = 1 + GlobalTools.GetRandomNum(nums);
                string JingName = GlobalTools.GetNewStrQuDiaoClone(Jobj.name);
                string jingNameTou = JingName.Split('-')[0];
                for (int i = 0; i < maxNums; i++)
                {
                    string LJJingName = jingNameTou + "-" + (i + 1);
                    GameObject ljJing;
                    ljJing = GlobalTools.GetGameObjectByName(LJJingName);
                    if (ljJing == null)
                    {
                        LJJingName = jingNameTou + "-1";
                        ljJing = GlobalTools.GetGameObjectByName(LJJingName);
                    }
                    //print("LJJingName      " + LJJingName);
                    float _ljx = tl.position.x + (i + 1) * jingW;
                    if (!Jobj.GetComponent<J_SPBase>())
                    {
                        _ljx = tl.position.x + (i + 1) * jingW + jingW * 0.5f;
                    }
                    ljJing.transform.position = new Vector3(_ljx, __y, 0);
                    ljJing.transform.parent = maps.transform;
                }

            }


        }
        else if (jingNameKey == "deng")
        {
            //灯 放路中间 左右偏移一点的位置
            float _pianyi = GlobalTools.GetRandomNum() > 50 ? GlobalTools.GetRandomDistanceNums(1f) : -GlobalTools.GetRandomDistanceNums(1f);
            __x = tl.position.x + GetWidth() * 0.5f + _pianyi;

        }
        else if (jingNameKey == "guangHua")
        {
            //发光的花
            float _pianyi = GlobalTools.GetRandomNum() > 50 ? GlobalTools.GetRandomDistanceNums(1f) : -GlobalTools.GetRandomDistanceNums(1f);
            __x = tl.position.x + GetWidth() * 0.5f + _pianyi;
            __y = tl.position.y + 2.6f;
        }


        Jobj.transform.position = new Vector3(__x, __y-xiajiangNums, 0);

    }


    protected override void GetYQJ()
    {
        int nums = 1 + GlobalTools.GetRandomNum(1);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //怎么区分 远前景
        string yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj");
        if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 0.5f-xiajiangNums, -1.4f, 0.4f, 40, "u");

        yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj2");
        //nums = 1+ GlobalTools.GetRandomNum(2);
        if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 0.5f-xiajiangNums, -1.6f, 0.5f, 45, "u");

        if (GlobalTools.GetRandomNum() > 90)
        {
            yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj3");
            nums = 1;
            if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 1.5f-xiajiangNums, -3f, 0.5f, 55, "u");
        }


    }

    //近远背景
    protected override void GetYBJ()
    {
        int nums = 1;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        string ybjArrName = "";

        ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("jybj");
        int nums2 = 1 + GlobalTools.GetRandomNum(3);


        if (_UpOrDown == "up" || _UpOrDown == "down")
        {

        }

        if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums2, pos1, pos2, pos1.y - 0.5f + _YJmoveY-xiajiangNums, 1f, 0.8f, -50, "u");




        if (GlobalTools.GetRandomNum() > 60)
        {
            ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("ybj");
            if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums, pos1, pos2, pos1.y + 2.3f + _YJmoveY-xiajiangNums, 2.8f, 1.5f, -70, "u");
        }

        if (GlobalTools.GetRandomNum() > 60)
        {
            ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("ybj2");
            if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums, pos1, pos2, pos1.y + 3.2f + _YJmoveY-xiajiangNums, 4.6f, 1.5f, -80, "u");
        }


    }





    //生成 中间 跳跃地板 等
    protected void GetTYDiBan()
    {
        string _dbName = MapNames.GetInstance().GetCanRandomUSEJYGName("db_zjdnty");
        if (!ShowTheDingDB||IsCanBianZhai)
        {
            //没有顶地板的 时候 没有倒挂地板
            if (_dbName == "db_tyd2_6") _dbName = "db_tyd2_2";
        }
        GameObject _db = GlobalTools.GetGameObjectByName(_dbName);
        if (_db == null) return;
        float __x = tl.position.x + 8.4f + GlobalTools.GetRandomDistanceNums(2);
        float __y = tl.position.y + 4.4f + GlobalTools.GetRandomDistanceNums(1);
        _db.transform.position = new Vector2(__x,__y-xiajiangNums);
        _db.transform.parent = maps.transform;
    }


    protected override void DingDBJing()
    {
        //print("顶部景控制");
        //是否有什么背景？？？
        GetTopJ3();
        GetTopJ4();
    }


    //出现 机关或者 怪物的 几率 
    float PenghuoJGJL = 20;
    //出现 怪物的几率
    float GuaiwuJL = 90;
   

    protected virtual void JiGuanAndGuai()
    {
        //生成喷火机关的 判断  根据坐标 关卡nums数 大关卡数来判断
        //生成喷火机关
        GameObject JG_PenHuo;
        int jilvs = GlobalTools.GetRandomNum();

        int jiguanNums = GlobalTools.GetRandomNum() > 50 ? 2 : 1;


        if (jilvs > PenghuoJGJL)
        {
            for(int i = 0; i < jiguanNums; i++)
            {
                bool IsGuaiwu = false;
                //判断是 怪物 还是 机关
                if(GlobalTools.GetRandomNum()> GuaiwuJL)
                {
                    //出怪物
                    IsGuaiwu = true;
                    //kongZhongXiaoGuai
                    string Guai_name = MapNames.GetInstance().GetCanRandomUSEJYGName("kongZhongXiaoGuai");
                    JG_PenHuo = GlobalTools.GetGameObjectByName(Guai_name);
                }
                else
                {
                    JG_PenHuo = GlobalTools.GetGameObjectByName("JG_huoyan");
                }
                

                //在上 还是在下
                float __x = 0;
                float __y = 0;

                if (IsGuaiwu)
                {
                    if (i == 0)
                    {
                        __x = DingDBPosL.transform.position.x + 2.6f + GlobalTools.GetRandomDistanceNums(1);
                    }
                    else
                    {
                        __x = DingDBPosL.transform.position.x + GetWidth() - 3.5f + GlobalTools.GetRandomDistanceNums(1);

                    }
                    __y = tl.transform.position.y + 2f  + GlobalTools.GetRandomDistanceNums(2);
                }
                else
                {
                    if (GlobalTools.GetRandomNum() > 30)
                    {
                        if (!ShowTheDingDB) {
                            JG_PenHuo.SetActive(false);
                            continue;
                        }
                        
                        //在顶部
                        if (i == 0)
                        {
                            __x = DingDBPosL.transform.position.x + 3f + GlobalTools.GetRandomDistanceNums(1);
                        }
                        else
                        {
                            __x = DingDBPosL.transform.position.x + GetWidth() - 3.5f + GlobalTools.GetRandomDistanceNums(1);

                        }

                        //__x = DingDBPosL.transform.position.x + 1 + GlobalTools.GetRandomDistanceNums(GetWidth() - 2f);
                        __y = DingDBPosL.transform.position.y - 1f;
                        JG_PenHuo.transform.localScale = new Vector3(JG_PenHuo.transform.localScale.x, -JG_PenHuo.transform.localScale.y, JG_PenHuo.transform.localScale.z);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            __x = DingDBPosL.transform.position.x + 2.6f + GlobalTools.GetRandomDistanceNums(1);
                        }
                        else
                        {
                            __x = DingDBPosL.transform.position.x + GetWidth() - 3.5f + GlobalTools.GetRandomDistanceNums(1);

                        }
                        //__x = tl.transform.position.x + 1 + GlobalTools.GetRandomDistanceNums(GetWidth() - 1);
                        __y = tl.transform.position.y + 1f - xiajiangNums - 1.5f;
                    }
                }


                if (!IsGuaiwu)
                {
                    float jiangeshijian = 0.5f + GlobalTools.GetRandomDistanceNums(1);
                    float penfashijian = 0.5f + GlobalTools.GetRandomDistanceNums(1);

                    JG_PenHuo.GetComponent<JG_huoyan>().jiangeshijian = jiangeshijian;
                    JG_PenHuo.GetComponent<JG_huoyan>().penfashijian = penfashijian;
                }

                JG_PenHuo.transform.position = new Vector2(__x, __y);
                JG_PenHuo.transform.parent = maps.transform;
               
            }


        }
    }

}
