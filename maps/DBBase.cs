﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DBBase : MonoBehaviour
{
    [Header("左上点")]
    public Transform tl;

    [Header("右下点")]
    public Transform rd;

    public Light2D light2d;

    public GameObject diban1;
    public GameObject diban2;
    public GameObject diban3;
    public GameObject diban4;


    [Header("**顶部的 地板")]
    public GameObject dibanDing;

    //可以做梯形 洞内
    float _DingDBDistanceX = 0;
    //顶地板的 Y 距离
    float _DingDBDistanceY = 2;
    [Header("**顶部的地板 左边点")]
    public Transform DingDBPosL;

    [Header("**顶部的地板 右边点")]
    public Transform DingDBPosR;


    void HideDingDB()
    {
        if(dibanDing&& dibanDing.activeSelf)
        {
            dibanDing.SetActive(false);
        }
    }

    bool IsShowDingDB = false;
    public void ShowDingDB(float __posY= 0,float __posX = 0)
    {
        if (!dibanDing.activeSelf)
        {
            
            dibanDing.SetActive(true);
           
        }

        __dingDBPosX = __posX;
        float wuchaY = GlobalTools.GetRandomDistanceNums(2);
        __dingDBPosY = __posY-wuchaY;

        IsShowDingDB = true;
        //print("顶部景控制*****   " + dibanDing.activeSelf);
        //生成 顶部时候 不许出树 或者 大概率不许出树
        //dibanDing.SetActive(true);
        
    }


    public void JG_GetYinci()
    {
        IsGetYinCi = true;
    }

    bool IsGetYinCi = false;
    //机关 隐刺
    void JiGuan_YinCi()
    {
        GameObject JG_YinCi;
        float __x = tl.transform.position.x + GetWidth() * 0.5f;
        float __y = tl.transform.position.y;
        if (GlobalTools.GetRandomNum() > 90)
        {
            JG_YinCi = GlobalTools.GetGameObjectByName("JG_yinci");
            __y = tl.transform.position.y-0.3f;
        }
        else
        {
            JG_YinCi = GlobalTools.GetGameObjectByName("JG_ci1");
        }
        
       
        JG_YinCi.transform.position = new Vector2(__x,__y);
        JG_YinCi.transform.parent = maps.transform;
    }


    //机关喷火
    void JiGuan_Penghuo()
    {
        //生成喷火机关的 判断  根据坐标 关卡nums数 大关卡数来判断
        //生成喷火机关
        GameObject JG_PenHuo;
        int jilvs = GlobalTools.GetRandomNum();
        if (jilvs > 80)
        {
            JG_PenHuo = GlobalTools.GetGameObjectByName("JG_huoyan");

            //在上 还是在下
            float __x = 0;
            float __y = 0;

            
            if (GlobalTools.GetRandomNum() > 50)
            {
                //在顶部
                __x = DingDBPosL.transform.position.x + 1 + GlobalTools.GetRandomDistanceNums(GetWidth() - 2f);
                __y = DingDBPosL.transform.position.y - 1f;
                JG_PenHuo.transform.localScale = new Vector3(JG_PenHuo.transform.localScale.x, -JG_PenHuo.transform.localScale.y, JG_PenHuo.transform.localScale.z);
            }
            else
            {
                __x = tl.transform.position.x + 1 + GlobalTools.GetRandomDistanceNums(GetWidth() - 1);
                __y = tl.transform.position.y+1f;
            }
            

            JG_PenHuo.transform.position = new Vector2(__x,__y);
            JG_PenHuo.transform.parent = maps.transform;
            float jiangeshijian = 0.5f + GlobalTools.GetRandomDistanceNums(1);
            float penfashijian = 0.5f + GlobalTools.GetRandomDistanceNums(1);

            JG_PenHuo.GetComponent<JG_huoyan>().jiangeshijian = jiangeshijian;
            JG_PenHuo.GetComponent<JG_huoyan>().penfashijian = penfashijian;

        }
    }




    float __dingDBPosX = 0;
    float __dingDBPosY = 0;
    //设置顶地板 位置
    void SetDingDBPos()
    {
        dibanDing.transform.position = new Vector3(dibanDing.transform.position.x + __dingDBPosX, dibanDing.transform.position.y + __dingDBPosY, dibanDing.transform.position.z);
        dibanDing.transform.parent = GlobalTools.FindObjByName("maps").transform;
        DingDBJing();
    }


    //顶部地板的 倒挂景
    void DingDBJing()
    {
        //print("顶部景控制");
        //是否有什么背景？？？
        GetTopJ3();
        GetTopJ4();


        if (IsShowDingDB)
        {
            //生成喷火机关
            JiGuan_Penghuo();
        }

    }


    public bool IsTopJ3 = false;
    void GetTopJ3()
    {
        string qjuArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju");
        if (qjuArrName == "") return;
        int nums = 2 + GlobalTools.GetRandomNum(4);
        //DingDBPosL.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //DingDBPosR.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //print("  ??>>>>>>>>>**qjuArrName   " + qjuArrName+"   pos  "+ DingDBPosL.transform.position);
        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;
        SetJingByDistanceU(qjuArrName, nums, pos1, pos2, pos1.y-2 , 0, 0, 50, "d");
    }


    public bool IsTopJ4 = false;
    void GetTopJ4()
    {
        string qju2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju2");
        if (qju2ArrName == "") return;
        int nums = 3 + GlobalTools.GetRandomNum(2);
        //DingDBPosL.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //DingDBPosR.transform.parent = GlobalTools.FindObjByName("maps").transform;
        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;
        SetJingByDistanceU(qju2ArrName, nums, pos1, pos2, pos1.y - 1.5f, -0.3f, 0, 20, "d");
    }





    [Header("上面 自动景 左点")]
    public Transform topL;
    [Header("上面 自动景 右点")]
    public Transform topR;

    [Header("上面 自动景 左点2")]
    public Transform topL2;
    [Header("上面 自动景 右点2")]
    public Transform topR2;



    [Header("上面的 连接点")]
    public Transform lianjiedianU;

    [Header("下面的 连接点")]
    public Transform lianjiedianD;

    [Header("右边的 连接点")]
    public Transform lianjiedianR;


    [Header("左边的 连接点")]
    public Transform lianjiedianL;

    //根据这个类型来出怪
    [Header("地形type")]
    public string DXType = "";

    [Header("是否是挡板 在上下地形防止穿帮的地形 不生成景")]
    public bool IsDangBan = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalSetDate.instance.IsCMapHasCreated&&!IsDangBan) {
            GetJing();
            //随机灯光颜色
            //SetLightColor();
        }

        if (!IsShowDingDB) {
            //隐藏顶部地板
            HideDingDB();
        }
        else
        {
            SetDingDBPos();
        }
       


    }


    //关灯
    public void GuanDeng()
    {
        if (light2d) light2d.GetComponent<Light2D>().intensity = 0;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

   


    public float GetWidth()
    {
        return Mathf.Abs(tl.position.x - rd.position.x);
    }

    public float GetHight()
    {
        return Mathf.Abs(tl.position.y - rd.position.y);
    }



    public bool IsPingDiJing = false;

    

    protected GameObject maps;

    //左右景布置  这里 后面会根据全局 来判断和调整 景是哪些内容  区别后缀不要用数字
    public void GetJing()
    {
        //这里根据 当前 关卡  来判断  是否 生成 树 背景远景 等 复杂的 类型       ***** 判断  用哪个大关卡的 景


        //近前景  石头  草 啥的
        //前排的 近景 档在玩家前面  的栅栏 什么的  这个后面看  DD
        //背景  石头 草 树 花  栅栏 路灯 等   
        //背远景 树林影子 等
        //前远景 加一层 前景   石头 草树  黑色栅栏  模糊的 黑色景

        maps = GlobalTools.FindObjByName("maps");

        if (!IsShowDingDB && IsHasShu) {
            if (GlobalTools.GetRandomNum() > 0) {
                GetShu();
            }
        } 


      


        //if (IsZJY) GetZYJ();
        //近背景
        if (IsPingDiJing) GetLRJinBG();

        //近前景
        if (IsPingDiJing) GetJQJ();
        //雾
        if (IsSCWu) GetWus();
        //近远景
        if (IsJYJ) GetJYJ();
        //装饰物
        if (IsZhuangshiwu) Zhuanshiwu();

        //顶部的景
        if (IsTopJ) GetTopJ();
        if (IsTopJ2) GetTopJ2();

        if (IsCanGetYQJ) GetYQJ();

        GetYQJ();
        GetYBJ();



        //机关 隐刺
        if (IsGetYinCi) JiGuan_YinCi();


    }


    //近远背景
    void GetYBJ()
    {
        int nums = 1;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        string ybjArrName = "";

        ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("jybj");
        int nums2 = 1 + GlobalTools.GetRandomNum(3);
        if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums2, pos1, pos2, pos1.y - 0.5f, 1f, 0.8f, -50, "u");




        if (GlobalTools.GetRandomNum() > 60)
        {
            ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("ybj");
            if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums, pos1, pos2, pos1.y + 2.3f, 2.8f, 1.5f, -70, "u");
        }

        if (GlobalTools.GetRandomNum() > 60)
        {
            ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("ybj2");
            if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums, pos1, pos2, pos1.y + 3.2f, 4.6f, 1.5f, -80, "u");
        }


    }






    
    public bool IsCanGetYQJ = false;
    public void GetTheYQJ()
    {
        IsCanGetYQJ = true;
    }

    void GetYQJ()
    {
        int nums = 1 + GlobalTools.GetRandomNum(1);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //怎么区分 远前景
        string yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj");
        if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 0.5f, -1.4f, 0.4f, 40, "u");

        yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj2");
        //nums = 1+ GlobalTools.GetRandomNum(2);
        if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 0.5f, -1.6f, 0.5f, 45, "u");

        if (GlobalTools.GetRandomNum() > 90)
        {
            yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj3");
            nums = 1;
            if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 1.5f, -3f, 0.5f, 55, "u");
        }
       

    }


    public bool IsZhuangshiwu = false;
    //栅栏什么的 可以放进 近景
    //装饰物 是单个 等 什么的 只要单个的   一个底面只要一个 位置随机
    void Zhuanshiwu()
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
            if (Jobj.GetComponent<J_SPBase>().light2d != null) {
                Jobj.GetComponent<J_SPBase>().light2d.color = GlobalTools.RandomColor();
            }
            
            Jobj.GetComponent<J_SPBase>().SetSD(-10);
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
            __x = tl.position.x + GetWidth() * 0.5f+ _pianyi;

        }


        Jobj.transform.position = new Vector3(__x,__y,0);
        
    }


    public bool IsJYJ = false;
    //近远景
    void GetJYJ()
    {
        //控制数量  要不根据 宽来定数量
        string jyjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("jyj");
        if (jyjArrName == "") return;

        //string jyjArrName = "jyj_1";
        int nums = GlobalTools.GetRandomNum(3);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;

        SetJingByDistanceU(jyjArrName, nums, pos1, pos2, pos1.y - 0.5f - GlobalTools.GetRandomDistanceNums(2), 0.7f, 0.3f, -30, "u", 1);
    }




    public bool IsZJY = false;
    //中远景
    void GetZYJ()
    {
        int nums = 4 + GlobalTools.GetRandomNum(4);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        SetJingByDistanceU("zyj_1", nums, pos1, pos2, pos1.y - GlobalTools.GetRandomDistanceNums(3), 2, 1, -40, "u",1);
    }



    public bool IsHasShu = false;
    void GetShu()
    {
        string shuArrName = MapNames.GetInstance().GetJingArrNameByGKKey("shu");
        if (shuArrName == "") return;

        //print(" 树！！！！！！！！！！！！ ");
        int nums = 1+GlobalTools.GetRandomNum(2);
        //SetJingByDistanceU("shu_1", nums, pos1, pos2, pos1.y - 3f, 0, 0, -10, "d");

        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
       
        SetJingByDistanceU(shuArrName, nums, pos1, pos2, pos1.y - GlobalTools.GetRandomDistanceNums(1), 0, 0, -10, "u",1);


        //这里加 木栅栏  

        //路灯

        //铁栅栏  这种纯 排的 景
    }

    public bool IsTopJ = false;
    void GetTopJ()
    {
        string qjuArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju");
        if (qjuArrName == "") return;
        int nums = 2 + GlobalTools.GetRandomNum(4);
        Vector2 pos1 = topL.position;
        Vector2 pos2 = topR.position;
        SetJingByDistanceU(qjuArrName, nums, pos1, pos2, pos1.y-2.4f, 0, 0, 20, "d");
    }

    public bool IsTopJ2 = false;
    void GetTopJ2()
    {
        string qju2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju2");
        if (qju2ArrName == "") return;
        int nums = 3 + GlobalTools.GetRandomNum(2);
        Vector2 pos1 = topL2.position;
        Vector2 pos2 = topR2.position;
        SetJingByDistanceU(qju2ArrName, nums, pos1, pos2, pos1.y -1.5f, -0.3f, 0, 20, "d");
    }


    




    bool IsLiziWu = true;
    public void NoLiZiWu()
    {
        IsLiziWu = false;
    }
    
    public bool IsSCWu = false;
    //生成 粒子雾 
    void GetWus()
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
        SetLiziByNums(1, liziArr, pos1.x, pos2.x, pos1.y - 1);



        

        if (GlobalTools.GetRandomNum() > 0)
        {
            liziArrName = MapNames.GetInstance().GetJingArrNameByGKKey("liziWu2");
            if (liziArrName == "") return;
            liziArr = GetDateByName.GetInstance().GetListByName(liziArrName, MapNames.GetInstance());
            SetLiziByNums(1, liziArr, pos1.x, pos2.x, pos1.y - 1);
        }
        
    }


    void SetLiziByNums(int nums, List<string> liziList, float _x1, float _x2, float _y)
    {
        for (var i = 0; i < nums; i++)
        {
            //print("---------->  "+jingNameTou);
            string jingName = liziList[GlobalTools.GetRandomNum(liziList.Count)];
            //print("-----------------> 啥啊  "+jingName);
            GameObject jing = GlobalTools.GetGameObjectByName(jingName);
            //jing.GetComponent<ParticleSystem>().startColor  = GlobalTools.RandomColor();
            jing.transform.parent = GlobalTools.FindObjByName("maps").transform;
            //public static void SetLizi(GameObject jingObj, float _x1, float _x2, float _y, int i, int nums)
            GlobalTools.SetLizi(jing, _x1, _x2, _y, i, nums);
        }
    }





    void GetWu(string wuName, Vector2 qidian, Vector2 zhongdian, int SDOrder, Color color)
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


        _wu.transform.localScale = new Vector3(_w2 / (_w + 0.6f), _h2 / _h * 3,1);
        //print("  缩放 "+ _wu.transform.localScale);
        _wu.transform.position = new Vector2(qidian.x + _w2 * 0.5f + (_w2 - GlobalTools.GetJingW(_wu)) * 0.5f, zhongdian.y + GlobalTools.GetJingH(_wu) * 0.5f-0.3f);

        //print(">?????????????????????????????????????????????????????????????    "+ _w+"  >-缩放后  " +GlobalTools.GetJingW(_wu));

        GlobalTools.SetMapObjOrder(_wu, SDOrder);

        //print("雾的宽度  "+_w+"  _w2宽度  "+_w2+"   宽度缩放比例    "+_w2/_w+"   weizhi "+_wu.transform.position);
        //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  "+_wu.transform.position+"   起点  "+qidian+"   终点 "+zhongdian);


        //改变雾的颜色
        _wu.GetComponent<SpriteRenderer>().color = color;// new Color(0.1f,1f,1f,0.5f);//new Color((129 / 255)f, (69 / 255)f, (69 / 255)f, (255 / 255)f); //Color.red;
        
    }



    //近前景
    void GetJQJ()
    {
        int nums = 4 + GlobalTools.GetRandomNum(4);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        //float _y = pos1.y - 1.5f;
        string qjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd");
        if(qjdArrName!="") SetJingByDistanceU(qjdArrName, nums, pos1, pos2, pos1.y - 1f - GlobalTools.GetRandomDistanceNums(1), -0.2f, 0.1f, 30, "u");
        //SetJingByDistanceU("qjd_1", nums, pos1, pos2, pos1.y, -4f, -1f, 40, "u");
        string qjd2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd2");
        if (qjd2ArrName != "") SetJingByDistanceU(qjd2ArrName, nums, pos1, pos2, pos1.y - 1.5f, -0.3f,0, 30, "u");

        string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
        if (qjd3ArrName != "") SetJingByDistanceU(qjd3ArrName, nums, pos1, pos2, pos1.y - 1.6f, -0.4f, 0, 30, "u");

        string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
        nums = 1 + GlobalTools.GetRandomNum(1);
        if (qyjdArrName != "") SetJingByDistanceU(qyjdArrName, nums, pos1, pos2, pos1.y - 2.2f, -0.6f, 1, 40, "u",2);
    }

    //近背景
    void GetLRJinBG()
    {
        string jjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("jjd");
        if (jjdArrName == "") return;
        int nums = 2 + GlobalTools.GetRandomNum(6);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x,tl.position.y);
        
        SetJingByDistanceU(jjdArrName, nums,pos1,pos2, pos1.y, 0,0,-15,"u");



        string jjdArrName2 = MapNames.GetInstance().GetJingArrNameByGKKey("jjd2");
        if (jjdArrName2 == "") return;
        nums = 1 + GlobalTools.GetRandomNum(1);
        SetJingByDistanceU(jjdArrName2, nums, pos1, pos2, pos1.y, 0, 0, -14, "u",2);


    }

    //_cx 朝向  xzds 旋转度数
    void SetJingByDistanceU(string jinglistName,int nums,Vector2 pos1,Vector2 pos2,float _y,float _z,float _dz, int sd, string _cx,float xzds = 0)
    {
        List<string> strArr = GetDateByName.GetInstance().GetListByName(jinglistName, MapNames.GetInstance());
        string _jinglistNameTou = jinglistName.Split('_')[0];
        //print("    ----------------------------------------------------------------------------  "+ _jinglistNameTou);
        for (int i = 0; i < nums; i++)
        {
            string objName = strArr[GlobalTools.GetRandomNum(strArr.Count)];
            GameObject jingObj = GlobalTools.GetGameObjectByName(objName);
            jingObj.transform.parent = maps.transform;
            //大于宽度的景 直接删除了
            bool IsShu = false;
            if((_jinglistNameTou != "ybj"&& _jinglistNameTou != "ybj2") && _jinglistNameTou != "shu")
            {
                if (IsDaYuDis(jingObj, pos1.x, pos2.x))
                {
                    Destroy(jingObj);
                    continue;
                }
            }
            else
            {
                IsShu = true;
            }
           

            bool IsLBSuoDuan = false;

            if(_jinglistNameTou == "yqj")
            {
               
                IsLBSuoDuan = true;
            }


            if ( _jinglistNameTou == "ybj" || _jinglistNameTou == "ybj2")
            {
                //print(" *************************************************************************** ------>>>???     "+jingObj.name);

                float ___y = _y;
                if (i == 0 || i == nums - 1)
                {
                    ___y -= 4;
                }

                GlobalTools.SetDaBeiJingTY(jingObj, pos1.x, pos2.x, _y, _z, _dz, i, nums, xzds, sd, false, IsShu, IsLBSuoDuan);
                continue;
            }
           
            GlobalTools.SetJingTY(jingObj, pos1.x, pos2.x, _y, _z, _dz, i, nums, xzds, sd,false, IsShu, IsLBSuoDuan);
        }
    }



    GameObject GetJObjByListName(string jinglistName)
    {
        List<string> strArr = GetDateByName.GetInstance().GetListByName(jinglistName, MapNames.GetInstance());
        string objName = strArr[GlobalTools.GetRandomNum(strArr.Count)];
        GameObject jingObj = GlobalTools.GetGameObjectByName(objName);
        jingObj.transform.parent = maps.transform;
        return jingObj;
    }
    


    bool IsDaYuDis(GameObject obj,float _x1,float _x2) {
        if (GlobalTools.GetJingW(obj) > Mathf.Abs(_x2 - _x1))
        {
            return true;
        }
        return false;
    }

    //顶部景布置





    public virtual Vector2 GetRightPos()
    {
        return lianjiedianR.position;
    }

    public virtual Vector2 GetLeftPos()
    {
        return lianjiedianL.position;
    }


    public Vector2 GetUpPos()
    {
        return lianjiedianU.position;
    }


    public Vector2 GetDownPos()
    {
        return lianjiedianD.position;
    }


    //设置深度
    public virtual void SetSD(int sd)
    {
        if (diban1) diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd;
        if (diban2) diban2.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd+1;
        if (diban3) diban3.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd + 2;
        if (diban4) diban4.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd + 3;
    }


    public virtual int GetSD()
    {
        return diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder;
    }

    public virtual void SetLightColor()
    {
        if (light2d) {
            light2d.GetComponent<Light2D>().color = GlobalTools.RandomColor();
            light2d.GetComponent<Light2D>().intensity = 0.6f + GlobalTools.GetRandomDistanceNums(0.6f);
        }
        
    }

    public void SetLightColorByValue(Color _color)
    {
        if (light2d) light2d.GetComponent<Light2D>().color = _color;
    }


    public Color GetLightColor()
    {
        if (!light2d) return Color.white;
        return light2d.GetComponent<Light2D>().color;
    }
   

}
