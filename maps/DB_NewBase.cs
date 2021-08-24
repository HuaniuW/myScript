using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_NewBase : DBBase
{
    [Header("近背景1")]
    public Transform JinBeijings1;
    [Header("近背景2")]
    public Transform JinBeijings2;

    public override void GetJing()
    {
        print("New DB Base!! GetJing!");

        //Globals.mapType = GlobalMapDate.PINGDI;
        //Globals.mapType = GlobalMapDate.DONGNEI;

        //Transform[] allChildren = Jinjings.GetChildCount();

        if (maps == null) maps = GlobalTools.FindObjByName("maps");

        if (!IsShowDingDB && IsHasShu && Globals.mapType != GlobalMapDate.DONGNEI)
        {
            if (GlobalTools.GetRandomNum() > 0)
            {
                GetShu();
            }
        }

        //ShowDingDB();
        //SetDingDBPos();

        print("子对象数量   "+ JinBeijings1.childCount);

        //foreach (Transform child in Jinjings)
        //{
        //    string objName = child.gameObject.name;
        //    print(objName);
        //    JinBeijings.Add(objName);
        //}

        //**********************近景
        if (IsJinBeijings)
        {
            GetObjListNameList(JinBeijings1, _JinBeijings1);
            GetObjListNameList(JinBeijings2, _JinBeijings2);
            GetLRJinBG();
        }

        //雾
        if (IsSCWu) GetWus();

        //**********************前景
        GetObjListNameList(JinQianjings1, _JinQianjings1);
        GetQianJing();
        //大远  前景
        GetDaYuanQianJing();


        //*********************背景
        //近景？？


        //中 和 远背景
        GetObjListNameList(JinZhongBeijings1, _JinZhongBeijings1);
        GetObjListNameList(JinBeijings, _JinBeijings);
        GetObjListNameList(JinBeijings3, _JinBeijings3);
        GetYuanBeiJing();




        SetTongYongXiushiJing(TongYongXiushiJing,_TongYongXiushiJing);



        //*********************装饰物
        if (IsZhuangshiwu)
        {
            //栏杆 栅栏 等
            Zhuangshiwu();
            //print(" wcao QianZhuangshiwu_1  "+ QianZhuangshiwu_1);
            Zhuangshiwu(QianZhuangshiwu_1);
        }

        if (IsSCWu) GetWus();


        //**********************后加 的 平地前景
        if (Globals.mapType == GlobalMapDate.PINGDI)
        {
            //平地 景
            PingDiQJ1();
        }

    }


    




   
    




    protected override void Zhuangshiwu(string JName = "Zhuangshiwu_1")
    {

        //print("zsw  JName " + JName);
        string zswArrName = MapNames.GetInstance().GetJingArrNameByGKKey(JName);
        //print("zsw  zswArrName " + zswArrName);


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
                //__y = tl.position.y + jingH * 0.5f - GlobalTools.GetRandomDistanceNums(0.3f) + 0.2f;
                if (JName == "Zhuangshiwu_1")
                {
                    __y = tl.position.y + jingH * 0.5f - GlobalTools.GetRandomDistanceNums(0.3f) + 0.2f;
                }
                else
                {
                    __y = tl.position.y + jingH * 0.5f - GlobalTools.GetRandomDistanceNums(0.3f) - 0.7f;
                }

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
                //print(" zsw  1> maxNums   "+ maxNums);
                string JingName = GlobalTools.GetNewStrQuDiaoClone(Jobj.name);
                //print(" zsw  2> JingName=Jobj.name>   " + JingName);
                string jingNameTou = JingName.Split('-')[0];
                //print(" zsw  3> jingNameTou   " + jingNameTou);

                for (int i = 0; i < maxNums; i++)
                {
                    string LJJingName = jingNameTou + "-" + (i + 1);
                    //print(i+ "  zsw  4>    LJJingName   " + LJJingName);
                    GameObject ljJing;
                    ljJing = GlobalTools.GetGameObjectByName(LJJingName);
                    if (ljJing == null)
                    {
                        //continue;
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
            __y = tl.position.y + 2.2f;
        }

        if (JName == "QianZhuangshiwu_1") __y -= 0.9f;
        Jobj.transform.position = new Vector3(__x, __y, 0);

    }





    //获取 景 名字数组
    public List<string> GetObjListNameList(Transform ParentObj,List<string> NameList)
    {
        NameList.Clear();
        if (ParentObj == null||ParentObj.childCount == 0) return NameList;
        foreach (Transform child in ParentObj)
        {
            string objName = child.gameObject.name;
            print("景是什么 ？ "+objName);
            NameList.Add(objName);
        }
        return NameList;

    }


    [Header("近 中远 背景1")]
    public Transform JinZhongBeijings1;
    protected List<string> _JinZhongBeijings1 = new List<string>();


    [Header("远 背景1")]
    public Transform YuanBeijings1;
    protected List<string> _YuanBeijings1 = new List<string>();


    [Header("大远 背景1")]
    public Transform DaYuanBeijings1;
    protected List<string> _DaYuanBeijings1 = new List<string>();



    [Header("近 背景1")]
    public Transform JinBeijings;
    protected List<string> _JinBeijings = new List<string>();

    [Header("近 背景2")]
    public Transform JinBeijings3;
    protected List<string> _JinBeijings3 = new List<string>();





    //*********远背景***********
    protected override void GetYuanBeiJing()
    {
        int nums = 1;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        nums = 1 + GlobalTools.GetRandomNum(3);
        SetJingByDistanceU2(_JinBeijings, nums, pos1, pos2, pos1.y - 1.7f - GlobalTools.GetRandomDistanceNums(1), 0.4f, 0.2f, -30, "u");



        nums = 1+GlobalTools.GetRandomNum(2);
        if(nums!=0)SetJingByDistanceU2(_JinBeijings3, nums, pos1, pos2, pos1.y - 1.9f - GlobalTools.GetRandomDistanceNums(1f), 0.6f, 0.4f, -40, "u");



        nums = 1 + GlobalTools.GetRandomNum(3);
        SetJingByDistanceU2(_JinZhongBeijings1, nums, pos1, pos2, pos1.y - 1.7f - GlobalTools.GetRandomDistanceNums(1), 1.2f, 0.4f, -50, "u");



        //if (_UpOrDown == "up" || _UpOrDown == "down")
        //{

        //}


        if (GlobalTools.GetRandomNum() > 20)
        {
            GetObjListNameList(YuanBeijings1, _YuanBeijings1);
            //ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey(DaYuanBeijing_1);
            SetDaYuanBeijing(_YuanBeijings1, 1, pos1, pos2, pos1.y + 2.8f + _YJmoveY, 2.8f, 0.5f, -70, "u",6.4f);
        }

        ////return;

        //大远背景
        if (GlobalTools.GetRandomNum() > 90)
        {
            GetObjListNameList(DaYuanBeijings1, _DaYuanBeijings1);
            SetDaYuanBeijing(_DaYuanBeijings1, 1, pos1, pos2, pos1.y, 4.6f, 0.5f, -80, "u", -0.2f);
        }


    }








    [Header("近前景1")]
    public Transform JinQianjings1;
    protected List<string> _JinQianjings1 = new List<string>();


    [Header("大远 前景1")]
    public Transform DaYuanQianjings1;
    protected List<string> _DaYuanQianjings1 = new List<string>();

    //**********生成前景*********
    protected override void GetQianJing()
    {

        if (_JinQianjings1.Count == 0) return;

        int nums = 0;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        nums = 4 + GlobalTools.GetRandomNum(4);
        SetJingByDistanceU2(_JinQianjings1, nums, pos1, pos2, pos1.y + JQJ_Y_tiaozheng - 1f - GlobalTools.GetRandomDistanceNums(1), -0.6f - GlobalTools.GetRandomDistanceNums(1), 0.1f, 40, "u");

        return;
       
       
    }

    //*********大远 前景********
    protected virtual void GetDaYuanQianJing()
    {
        //if (Globals.mapType != GlobalMapDate.PINGDI) return;

        //int nums = 1 + GlobalTools.GetRandomNum(3);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        //print("大远景 hei!!!!!!!!!!!");

        if (GlobalTools.GetRandomNum() > 90)
        {
            GetObjListNameList(DaYuanQianjings1, _DaYuanQianjings1);
            //nums = 1;
            SetJingByDistanceU2(_DaYuanQianjings1, 1, pos1, pos2, pos1.y - 1.5f - GlobalTools.GetRandomDistanceNums(2), -3f, 0.5f, 55, "u");
        }
    }






    //List<string>[] JinBeijings = new List<string>;

    protected List<string> _JinBeijings1 = new List<string>();
    protected List<string> _JinBeijings2 = new List<string>();
    //*******近景**************
    protected override void GetLRJinBG()
    {
        if (_JinBeijings1.Count == 0) return;
        int nums = 2 + GlobalTools.GetRandomNum(3);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        SetJingByDistanceU2(_JinBeijings1, nums, pos1, pos2, pos1.y + 0.5f, 0, 0, -15, "u");

        if (_JinBeijings2.Count == 0) return;
        if (GlobalTools.GetRandomNum() > 90)
        {
            //大的 近景 最好只出现一次就好  太明显了  一看就知道 重复了的
            nums = 1;
            SetJingByDistanceU2(_JinBeijings2, nums, pos1, pos2, pos1.y, 0, 0, -14, "u", 2);
        }

      
    }


    //大远背景 从中间开始 两面距离 分散的 排列方法（注意 大平地 排列方法 按以前的来）
    protected void SetDaYuanBeijing(List<string> JingLists, int nums, Vector2 pos1, Vector2 pos2, float _y, float _z, float _dz, int sd, string _cx, float ZYXiuzheng)
    {
        if (JingLists.Count == 0) return;
        List<string> strArr = JingLists;//GetDateByName.GetInstance().GetListByName(jinglistName, MapNames.GetInstance());
        //string _jinglistNameTou = jinglistName.Split('_')[0];
        //print("    ----------------------------------------------------------------------------  "+ _jinglistNameTou);
        for (int i = 0; i < nums; i++)
        {
            string objName = strArr[GlobalTools.GetRandomNum(strArr.Count)];
            //print("  ----------------------------- 前景名字  "+ objName);
            GameObject jingObj = GlobalTools.GetGameObjectByName(objName);
            print("111111jingObj    "+ jingObj.name);
            if (jingObj == null) continue;
            jingObj.transform.parent = maps.transform;

            float __x = 0;
            float __y = 0;
            float __z = 0;
            float jingW = GlobalTools.GetJingW(jingObj);
            float jingH = GlobalTools.GetJingH(jingObj);
            float w = Mathf.Abs(pos1.x - pos2.x);

            float __dxXiuzheng = 1;
            float __dx = GlobalTools.GetRandomNum(100) > 50 ? GlobalTools.GetRandomDistanceNums(__dxXiuzheng) : -GlobalTools.GetRandomDistanceNums(__dxXiuzheng);
            __x = pos1.x + w * 0.5f+__dx;

            if (jingObj.GetComponent<JingBase>())
            {
                __y = _y + jingObj.GetComponent<JingBase>().GetCenterPosY() - ZYXiuzheng - GlobalTools.GetRandomDistanceNums(0.4f); //GlobalTools.GetRandomDistanceNums(1);
            }
            else
            {
                __y = _y + 3f;
            }
            __z = _z + GlobalTools.GetRandomDistanceNums(_dz);
            jingObj.transform.position = new Vector3(__x, __y, __z);

            int _sd = sd + i % 10;
            GlobalTools.SetMapObjOrder(jingObj, _sd);

            GlobalTools.SaveGameObj(jingObj);

        }
    }

    [Header("****顶地板景1")]
    public Transform DingDBJings;
    protected List<string> _DingDBJings = new List<string>();

    [Header("****顶地板景1-背景墙")]
    public Transform DingDBJingsQiang;
    protected List<string> _DingDBJingsQiang = new List<string>();


    //顶部地板的 倒挂景
    protected override void DingDBJing()
    {
        GetTopDingJing(DingDBJings, _DingDBJings,10);
        GetTopDingJing(DingDBJingsQiang, _DingDBJingsQiang,GlobalTools.GetRandomNum(2));
        if (IsShowDingDB)
        {
            //生成喷火机关
            //JiGuan_Penghuo();
        }

    }



    
    protected virtual void GetTopDingJing(Transform JingTrans, List<string> JingNameList, int JingNums = 0)
    {
        GetObjListNameList(JingTrans, JingNameList);
        if (JingNameList.Count == 0) return;

        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;


        float _l = DingDBPosL.transform.position.x;
        float _r = DingDBPosR.transform.position.x;
        float _topPosY = DingDBPosL.transform.position.y;
        float _w = Mathf.Abs(_l - _r);

        int nums = 0;
        if (JingNums != 0)
        {
            nums = JingNums;
        }
        else
        {
            nums = 5 + GlobalTools.GetRandomNum(JingNameList.Count);
        }

        for (int i = 0; i < nums; i++)
        {
            string objName = JingNameList[GlobalTools.GetRandomNum(JingNameList.Count)];
            print(" xs objName  " + objName);

            GameObject jingObj = GlobalTools.GetGameObjectByName(objName);
            if (jingObj == null) continue;
            jingObj.transform.parent = maps.transform;

            float JingW = 0;
            float JingH = 0;
            float _x = 0;
            float _y = 0;
            float _z = 0;
            int sd = 0;

            if (jingObj.GetComponent<JingBase>())
            {
                JingW = jingObj.GetComponent<JingBase>().GetWidth();
                JingH = jingObj.GetComponent<JingBase>().GetHeight();
            }
            else
            {
                JingW = GlobalTools.GetJingW(jingObj);
                JingH = GlobalTools.GetJingH(jingObj);
            }

            string touName = objName.Split('_')[0];
            string touNmae2 = "";//objName.Split('_')[1];
            if(objName.Split('_')[1]!=null) touNmae2 = objName.Split('_')[1];

            if (touName == "qj") {
                if (JingW >= 8f)
                {
                    print("XXXX1  jingW "+JingW+"  _w "+_w);
                    _x = _l + JingW * 0.5f + GlobalTools.GetRandomDistanceNums(_w - JingW - 0.3f);
                    _y = _topPosY + JingH * 0.5f - GlobalTools.GetRandomDistanceNums(0.5f);
                    _z = -GlobalTools.GetRandomDistanceNums(0.3f);
                    sd = 30 + i % 8;
                }else if (JingW <= 4.4f)
                {
                    print("XXXX2  jingW " + JingW + "  _w " + _w);
                    _x = _l + JingW * 0.5f + _w * 0.2f + GlobalTools.GetRandomDistanceNums(_w * 0.6f);
                    _y = _topPosY + JingH * 0.5f - GlobalTools.GetRandomDistanceNums(0.5f);
                    _z = -0.6f + -GlobalTools.GetRandomDistanceNums(0.6f);
                    sd = 50 + i % 8;
                }
                else
                {
                    _x = _l + JingW * 0.5f + _w * 0.2f + GlobalTools.GetRandomDistanceNums(_w * 0.6f);
                    _y = _topPosY + JingH * 0.5f - GlobalTools.GetRandomDistanceNums(0.3f);
                    _z =  -GlobalTools.GetRandomDistanceNums(0.3f);
                    sd = 40 + i % 8;
                }
            }
            else if (touName == "Qiang")
            {
                //墙 越宽 越靠前
                if (JingW >= 8f)
                {
                    _x = _l + JingW*0.5f + GlobalTools.GetRandomDistanceNums(_w - JingW - 0.3f);
                    _y = _topPosY + GlobalTools.GetRandomDistanceNums(JingH * 0.2f);
                    _z = -0.3f - GlobalTools.GetRandomDistanceNums(0.3f);
                    sd = -10 - i % 8;
                }
                else if (JingW  <= 4.4f)
                {
                    _x = _l + JingW * 0.5f + _w*0.2f + GlobalTools.GetRandomDistanceNums(_w*0.6f);
                    _y = _topPosY - GlobalTools.GetRandomDistanceNums(JingH * 0.5f);
                    _z = 0.8f + GlobalTools.GetRandomDistanceNums(0.6f);
                    sd = -70 - i % 8;
                }
                else
                {
                    _x = _l + JingW * 0.5f + _w * 0.2f + GlobalTools.GetRandomDistanceNums(_w * 0.6f);
                    _y = _topPosY - GlobalTools.GetRandomDistanceNums(JingH * 0.5f);
                    _z = 0.4f+ GlobalTools.GetRandomDistanceNums(0.6f);
                    sd = -60 - i % 8;
                }
            }
            else
            {
                //顶部的 零碎景
                _x = _l+JingW*0.5f +  GlobalTools.GetRandomDistanceNums(_w - JingW-0.3f);
                _y = _topPosY - JingH * 0.3f + GlobalTools.GetRandomDistanceNums(JingH*0.8f);
                _z = GlobalTools.GetRandomDistanceNums(0.3f);

                //前 还是后
                _z = GlobalTools.GetRandomNum() > 50 ? _z : -_z;
                if (_z >= 0)
                {
                    sd = 22 + i % 8;
                }
                else
                {
                    sd = -10 - i % 8;
                    //_z = -GlobalTools.GetRandomDistanceNums(0.3f);
                }

            }


            if (touName == "wy"|| touNmae2 == "wy")
            {
                JingNameList.Remove(objName);
            }

            if (jingObj.GetComponent<JingBase>())
            {
                jingObj.GetComponent<JingBase>().SetSD(sd);
            }
            else
            {
                GlobalTools.SetMapObjOrder(jingObj, sd);
            }
            
            jingObj.transform.position = new Vector3(_x,_y,_z);
        }

        

    }






    [Header("通用修饰景")]
    public Transform TongYongXiushiJing;
    protected List<string> _TongYongXiushiJing = new List<string>();



    protected virtual void SetTongYongXiushiJing(Transform JingTrans,List<string> JingNameList,int JingNums = 0)
    {
        print(" xs   width   "+GetWidth()*0.6f);
        GetObjListNameList(JingTrans, JingNameList);
        if (JingNameList.Count == 0) return;

        //有字节头的 特殊处理  eg:  qj_    yqj_    ybj_
        //其他根据 大小来 判断 位置

        float _l = tl.transform.position.x;
        float _r = rd.transform.position.x;
        float _topPosY = tl.transform.position.y;
        float _w = GetWidth();
        int nums = 0;
        if(JingNums!= 0)
        {
            nums = JingNums;
        }
        else
        {
            nums = 1 + GlobalTools.GetRandomNum(JingNameList.Count);
        }



        print(" xs  nums "+nums);

        for (int i=0;i<nums;i++)
        {
            string objName = JingNameList[GlobalTools.GetRandomNum(JingNameList.Count)];
            print(" xs objName  "+ objName);

            GameObject jingObj = GlobalTools.GetGameObjectByName(objName);
            if (jingObj == null) continue;
            jingObj.transform.parent = maps.transform;


            //无法判断大小的 都作为修饰景
            //和 雾一起配合  +一个 雾进去？？  是加 雾 还是 自带  ----- 先不加这个
            //深度 在 -10 --- -40
            //dY <= 2

            float JingW = 0;
            float JingH = 0;
            float _x = 0;
            float _y = 0;
            float _z = 0;

            if (jingObj.GetComponent<JingBase>())
            {
                JingW = jingObj.GetComponent<JingBase>().GetWidth();
                JingH = jingObj.GetComponent<JingBase>().GetHeight();
            }
            else
            {
                JingW = GlobalTools.GetJingW(jingObj);
                JingH = GlobalTools.GetJingH(jingObj);
            }
            
            float LeftPosX = tl.transform.position.x;
            float LeftPosY = tl.transform.position.y;

            float ___qishiX = LeftPosX + (GetWidth() - JingW) * 0.25f;
            float ___RandomDisX = (GetWidth() - JingW) * 0.5f;
            float _dy = 0;
            _dy = JingH <= 3 ? GlobalTools.GetRandomDistanceNums(0.5f) : 0.5f + GlobalTools.GetRandomDistanceNums(1f);
            if (JingH <= 1.4f) _dy = GlobalTools.GetRandomDistanceNums(0.2f) - GlobalTools.GetRandomDistanceNums(0.5f);
            //_dy = 0;
            int sd = 20;

            string[] strArr = jingObj.name.Split('_');
            string touName = strArr[0];
            string S2Name="";
            if (strArr[1] != null)
            {
                S2Name = strArr[1];
            }


           


            if (touName == "qj")
            {
                
            }
            else if (touName == "yqj")
            {
                _z = -1.2f - GlobalTools.GetRandomDistanceNums(0.5f);
                _x = LeftPosX+ GetWidth() * 0.4f+ GlobalTools.GetRandomDistanceNums(GetWidth() * 0.2f);
                _dy = JingH-GlobalTools.GetRandomDistanceNums(0.8f);
                sd = 50 - i % 8;
            }
            else if (touName == "ybj")
            {

            }
            else
            {
                if (JingW  >= 7.6f || JingH >= 4)
                {
                    //居中靠后型的
                    _z = 1 + GlobalTools.GetRandomDistanceNums(1);
                    _x = LeftPosX + GetWidth() * 0.2f + GlobalTools.GetRandomDistanceNums(GetWidth() * 0.6f);  // ___qishiX+___RandomDisX;
                    _dy = 0.5f + GlobalTools.GetRandomDistanceNums(2f);
                    sd = -60 - i % 8;
                }
                else if (JingW  <= 4f)
                {
                    _z = GlobalTools.GetRandomDistanceNums(0.4f);
                    _x = LeftPosX + JingW * 0.6f + GlobalTools.GetRandomDistanceNums(GetWidth() - JingW * 1.2f);
                    sd = -1 - i % 8;
                }
                else
                {
                    _z = 0.5f + GlobalTools.GetRandomDistanceNums(1);
                    _x = LeftPosX + JingW * 0.8f + GlobalTools.GetRandomDistanceNums(GetWidth() - JingW * 1.8f);
                    sd = -50 - i % 8;
                }

                



                //if (jingObj.GetComponent<JingBase>())
                //{
                   
                //}
                //else
                //{
                    

                   
                //}
            }

            if (touName == "wy"|| S2Name!="")
            {
                //唯一  直接在 数组内取出删除
                JingNameList.Remove(objName);

            }



            if (jingObj.GetComponent<JingBase>())
            {
                jingObj.GetComponent<JingBase>().SetSD(sd);
            }
            else
            {
                GlobalTools.SetMapObjOrder(jingObj, sd);
            }

            _y = LeftPosY + JingH * 0.5f - _dy;


            jingObj.transform.position = new Vector3(_x, _y, _z);
            GlobalTools.SaveGameObj(jingObj);

        }


    }





    //_cx 朝向  xzds 旋转度数
    protected void SetJingByDistanceU2(List<string> JingLists, int nums, Vector2 pos1, Vector2 pos2, float _y, float _z, float _dz, int sd, string _cx, float xzds = 0)
    {
        if (JingLists.Count == 0) return;
        List<string> strArr = JingLists;//GetDateByName.GetInstance().GetListByName(jinglistName, MapNames.GetInstance());
        //string _jinglistNameTou = jinglistName.Split('_')[0];
        //print("    ----------------------------------------------------------------------------  "+ _jinglistNameTou);
        for (int i = 0; i < nums; i++)
        {
            string objName = strArr[GlobalTools.GetRandomNum(strArr.Count)];
            //print("  ----------------------------- 前景名字  "+ objName);
            GameObject jingObj = GlobalTools.GetGameObjectByName(objName);
            if (jingObj == null) continue;
            jingObj.transform.parent = maps.transform;
            //大于宽度的景 直接删除了
            bool IsShu = false;
            //if ((_jinglistNameTou != "ybj" && _jinglistNameTou != "ybj2") && _jinglistNameTou != "Shu")
            //{
            //    if (IsDaYuDis(jingObj, pos1.x, pos2.x) && _jinglistNameTou != "zyj" && _jinglistNameTou != "dyj")
            //    {
            //        Destroy(jingObj);
            //        continue;
            //    }
            //}
            //else
            //{
            //    IsShu = true;
            //}


            bool IsLBSuoDuan = false;

            //if (_jinglistNameTou == "yqj")
            //{

            //    IsLBSuoDuan = true;
            //}


            //if (_jinglistNameTou == "ybj" || _jinglistNameTou == "ybj2")
            //{
            //    //print(" *************************************************************************** ------>>>???     "+jingObj.name);

            //    float ___y = _y;
            //    if (_UpOrDown != GlobalMapDate.YIBAN && _UpOrDown != GlobalMapDate.BOSS_PINGDI && _UpOrDown != GlobalMapDate.JINGYING_PINGDI && _UpOrDown != GlobalMapDate.JUQING_PINGDI)
            //    {
            //        if (i == 0 || i == nums - 1)
            //        {
            //            ___y -= 4;
            //        }
            //    }


            //    GlobalTools.SetDaBeiJingTY(jingObj, pos1.x, pos2.x, _y, _z, _dz, i, nums, xzds, sd, false, IsShu, IsLBSuoDuan);
            //    continue;
            //}

            //_y -= GlobalTools.GetRandomDistanceNums(0.2f);

            GlobalTools.SetJingTY(jingObj, pos1.x, pos2.x, _y, _z, _dz, i, nums, xzds, sd, false, IsShu, IsLBSuoDuan);
        }
    }

}
