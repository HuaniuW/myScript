using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Senlin_1 : DBBase
{

    public void GetNoShu()
    {
        IsHasShu = false;
    }


    public override void GetJing()
    {
        print("森林图！！！！");


        //这里根据 当前 关卡  来判断  是否 生成 树 背景远景 等 复杂的 类型       ***** 判断  用哪个大关卡的 景


        //近前景  石头  草 啥的
        //前排的 近景 档在玩家前面  的栅栏 什么的  这个后面看  DD
        //背景  石头 草 树 花  栅栏 路灯 等   
        //背远景 树林影子 等
        //前远景 加一层 前景   石头 草树  黑色栅栏  模糊的 黑色景

        if (maps == null) maps = GlobalTools.FindObjByName("maps");

        //if (!IsShowDingDB && IsHasShu)
        //{
        //    if (GlobalTools.GetRandomNum() > 0)
        //    {
        //        GetShu();
        //    }
        //}


        if(IsHasShu||GlobalTools.GetRandomNum()>=90)GetShu();
        if (GlobalTools.GetRandomNum() > 0)
        {
            //从左到右 摆放
            //Zhuangshiwu();

            //中间开始两边摆放？

            //从中段往左一点 再开始摆放
            Zhuangshiwu2();
            Zhuangshiwu2("qZsw");
        }

        //雾
        if (IsSCWu) GetWus();

        //近背景
        if (IsPingDiJing) GetLRJinBG();


        //中远背景的 3个草皮
        GetZYBJCaopi();


        //近前景
        //if (IsPingDiJing) GetJQJ();


        //前远景 可以是前黑色 栅栏 的一个
        GetQianYuanjing();

        //大前远景 藤条
        //GetYQJ();


        if (IsEWaiZYBJ) GetEWaiZYBJ();


        //*************************************关键点************************************************
        //如果是 默认给出的 景  比如 远背景  要动的话（移动位置等）默认就不显示 并且 做好动态物品（可以动态取）

            //******************************************************************************************



        return;

     
        //远背景
        GetYBJ();



        //机关 隐刺
        //if (IsGetYinCi) JiGuan_YinCi();


    }



    [Header("额外的 中远背景")]
    public bool IsEWaiZYBJ = true;

    protected virtual void GetEWaiZYBJ()
    {
        //现在 只有一个 草皮

        string CaopiName = "ZYBJ_caopi_1";

        GameObject jing = GlobalTools.GetGameObjectByName(CaopiName);
        jing.transform.parent = maps.transform;


        float W = GlobalTools.GetJingW(jing);

        float __x = tl.position.x + W * 0.5f - 1 + GlobalTools.GetRandomDistanceNums((GetWidth() - W + 2));
        float __y = tl.position.y + GlobalTools.GetRandomDistanceNums(0.6f);
        float __z = 1.53f;

        jing.GetComponent<SpriteRenderer>().sortingOrder = -30;


        jing.transform.position = new Vector3(__x, __y, __z);
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
            if (qjdArrName != "") SetJingByDistanceU(qjdArrName, nums, pos1, pos2, pos1.y - 1f - GlobalTools.GetRandomDistanceNums(1), -1.2f - GlobalTools.GetRandomDistanceNums(1), 0.1f, 40, "u");
            //SetJingByDistanceU("qjd_1", nums, pos1, pos2, pos1.y, -4f, -1f, 40, "u");
            nums = 1 + GlobalTools.GetRandomNum(3);
            string qjd2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd2");
            if (qjd2ArrName != "") SetJingByDistanceU(qjd2ArrName, nums, pos1, pos2, pos1.y - 1f - GlobalTools.GetRandomDistanceNums(1f), -3f - GlobalTools.GetRandomDistanceNums(1), 0, 40, "u");
            nums = 4 + GlobalTools.GetRandomNum(4);
            string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            if (qjd3ArrName != "") SetJingByDistanceU(qjd3ArrName, nums, pos1, pos2, pos1.y - 0.4f - GlobalTools.GetRandomDistanceNums(0.6f), -4.2f, 0, 40, "u");

            string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            nums = 1 + GlobalTools.GetRandomNum(1);
            if (qyjdArrName != "") SetJingByDistanceU(qyjdArrName, nums, pos1, pos2, pos1.y - 1.6f - GlobalTools.GetRandomDistanceNums(0.6f), -5.6f, 1, 50, "u", 2);
        }
        else if (Globals.mapTypeNums == 2)
        {
            nums = 1 + GlobalTools.GetRandomNum(1);
            string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            if (qjd3ArrName != "") SetJingByDistanceU(qjd3ArrName, nums, pos1, pos2, pos1.y - 2f, -0.6f, 1.2f, 40, "u");

            string qjd5ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd5");
            nums = 1 + GlobalTools.GetRandomNum(2);
            if (qjd5ArrName != "") SetJingByDistanceU(qjd5ArrName, nums, pos1, pos2, pos1.y - 1f, -0.2f, 0.6f, 45, "u", 2);

            string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd4");
            nums = 1 + GlobalTools.GetRandomNum(1);
            if (qyjdArrName != "") SetJingByDistanceU(qyjdArrName, nums, pos1, pos2, pos1.y - 1.9f, -1.6f, 1, 50, "u", 2);
        }
    }

    private void GetZYBJCaopi()
    {
        //throw new NotImplementedException();
        int nums = 1 + GlobalTools.GetRandomNum(3);
        if (nums == 0) return;
        // 数组名字  ZYBJCaopi_1
        string ListName = "ZYBJCaopi_1";
        List<string> CaopiArr = GetDateByName.GetInstance().GetListByName(ListName, MapNames.GetInstance());
        for (int i = 0; i < nums; i++)
        {
            string jingName = CaopiArr[GlobalTools.GetRandomNum(CaopiArr.Count)];
            //print("-----------------> 啥啊  "+jingName);
            GameObject jing = GlobalTools.GetGameObjectByName(jingName);
            jing.transform.parent = maps.transform;


            float W = GlobalTools.GetJingW(jing);

            float __x = tl.position.x + W * 0.5f - 1 + GlobalTools.GetRandomDistanceNums((GetWidth() - W + 2));
            float __y = tl.position.y - 2.5f + GlobalTools.GetRandomDistanceNums(0.5f);
            float __z = 2.5f + 0.4f * i;

            jing.GetComponent<SpriteRenderer>().sortingOrder = -30 - i % 3;


            jing.transform.position = new Vector3(__x, __y, __z);
        }

    }

    protected override void GetWus()
    {

        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        //SetJingByDistanceU("liziWu_1", nums, pos1, pos2, pos1.y+1, 0, 0, 0, "u");
        Color color1 = new Color(0.1f, 1f, 1f, 0.1f);
        GetWu("", pos1, pos2, -30, color1);

        color1 = GlobalTools.RandomColor();
        if (GlobalTools.GetRandomNum() < 20) GetWu("qWu", pos1, pos2, 30, color1);

        //获取背景的 上升渐变的 雾
        //Color color2 = MapNames.GetInstance().GetColorByGKKey(); //new Color(0.1f, 1f, 1f, 0.3f);
        //GetWu("", pos1, pos2, -60, color2);


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




    protected virtual void GetQianYuanjing()
    {
        string zswArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qZsw");
        if (zswArrName == "") return;
        GameObject Jobj = GetJObjByListName(zswArrName);


        float __x = tl.position.x + GetWidth() * 0.5f - 1 + GlobalTools.GetRandomDistanceNums(2);
        float __y = tl.position.y + GlobalTools.GetRandomDistanceNums(1);
        float __z = -2.5f - GlobalTools.GetRandomDistanceNums(1.4f);

        Jobj.GetComponent<SpriteRenderer>().sortingOrder = 50;
        Jobj.transform.position = new Vector3(__x, __y, __z);




        //加一个 远景
        if (GlobalTools.GetRandomNum() > 0)
        {
            GameObject Jobj2 = GetJObjByListName(zswArrName);

            __y = tl.position.y - 0.5f + GlobalTools.GetRandomDistanceNums(1);
            __z = -0.5f - GlobalTools.GetRandomDistanceNums(1.4f);
            if (GlobalTools.GetRandomNum() > 50)
            {
                //zuo
                __x = tl.position.x + 1 + GlobalTools.GetRandomDistanceNums(1);
            }
            else
            {
                //you
                __x = tl.position.x + GetWidth() - 2 - GlobalTools.GetRandomDistanceNums(2);
            }
            Jobj.GetComponent<SpriteRenderer>().sortingOrder = 50;
            Jobj.transform.position = new Vector3(__x, __y, __z);
        }




        //生成 一个 藤条

        if (GlobalTools.GetRandomNum() > 90)
        {
            string yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj3");
            Vector2 pos1 = new Vector2(tl.transform.position.x, tl.transform.position.y);
            Vector2 pos2 = new Vector2(tl.transform.position.x + GetWidth(), tl.transform.position.y);
            if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, 1, pos1, pos2, pos1.y - 1.5f, -6f, 0.5f, 55, "u");
        }


    }



    //近背景
    protected override void GetLRJinBG()
    {
        //怎么根据 关卡来判断出来的 景数量？？？之要判断数量？  还有位置  关于旋转？？树好像有旋转  看看怎么写进去
        string jjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("jjd");
        if (jjdArrName == "") return;
        int nums = 7 + GlobalTools.GetRandomNum(8);
        print(">>>>>>>>>>>jing nums:   " + nums);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);





        string jjdArrName2 = MapNames.GetInstance().GetJingArrNameByGKKey("jjd2");
        if (jjdArrName2 == "") return;






        if (Globals.mapTypeNums == 1)
        {
            SetJingByDistanceU(jjdArrName, nums, pos1, pos2, pos1.y + 0.1f, 0, 0, -15, "u");


            //if (GlobalTools.GetRandomNum() > 80)
            //{
            //    nums = 1 + GlobalTools.GetRandomNum(1);
            //    SetJingByDistanceU(jjdArrName2, nums, pos1, pos2, pos1.y, 0, 0, -14, "u", 2);
            //}

        }
        else if (Globals.mapTypeNums == 2)
        {
            nums = 1 + GlobalTools.GetRandomNum(6);
            SetJingByDistanceU(jjdArrName, nums, pos1, pos2, pos1.y + 0.2f, 0, 0, -15, "u");

            nums = 1 + GlobalTools.GetRandomNum(1);
            SetJingByDistanceU(jjdArrName2, nums, pos1, pos2, pos1.y, 0, 0, -14, "u", 2);
        }
    }








    protected virtual void Zhuangshiwu2(string JName = "zsw")
    {
        string zswArrName = MapNames.GetInstance().GetJingArrNameByGKKey(JName);
        if (zswArrName == "") return;



        //中间 向两边扩散的 方法
        //靠内一点的 平铺


        //判断是否有装饰物  只有一个
        //if (GlobalTools.GetRandomNum() < 20) return;
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
        print("剩余的 w 是多少宽度     " + _w);
        float __x = tl.position.x + GlobalTools.GetRandomDistanceNums(_w * 0.5f);
        //__x = tl.position.x + _w * 0.5f;
        print("  __x  起始位置   " + __x);
        _w -= (__x - tl.position.x);
        print(" 除去 起始X后 剩余的 w 是多少宽度     " + _w);
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
                //print(" Jobj  "+ Jobj.name);

                //jingW = GlobalTools.GetJingW(Jobj);  //Jobj.GetComponent<SpriteRenderer>().bounds.;
                //jingH = GlobalTools.GetJingH(Jobj);

                //__x = tl.position.x + jingW * 0.5f;
                __y = tl.position.y + jingH * 0.5f - 0.2f;

            }
            Jobj.transform.position = new Vector3(__x, __y, 0);
            //if (GlobalTools.GetRandomNum() > 40)
            //{
            //    //限制 出的几率 免得 太多
            //    return;
            //}

            int nums = (int)(_w / jingW) - 1;
            print(" ------------------------------> _w " + _w + "  jingW   " + jingW + "   xxxxx " + Jobj.transform.position.x);
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
                    //记录上一个 景位置  来算下一个位置

                    float _ljx = tl.position.x + (i + 1) * jingW;


                    if (!Jobj.GetComponent<J_SPBase>())
                    {
                        //_ljx = tl.position.x + (i + 1) * jingW + jingW * 0.5f;
                        print("i " + i + "  --->_ljx: " + _ljx + "  jingW  " + jingW);
                        _ljx = Jobj.transform.position.x + (i + 1) * jingW;
                        __y = tl.position.y + jingH * 0.5f - GlobalTools.GetRandomDistanceNums(0.4f) - 0.15f;
                        print(" ----------> __y " + __y + "   tly " + tl.position.y);
                    }
                    else
                    {

                        _ljx = tl.position.x + (i + 1) * jingW;

                    }

                    if (JName == "qZsw")
                    {
                        ljJing.GetComponent<SpriteRenderer>().sortingOrder = 40 + i % 3;
                        __y -= (0.6f + GlobalTools.GetRandomDistanceNums(0.4f));
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
            Jobj.transform.position = new Vector3(__x, __y, 0);

        }
        else if (jingNameKey == "guangHua")
        {
            //发光的花
            float _pianyi = GlobalTools.GetRandomNum() > 50 ? GlobalTools.GetRandomDistanceNums(1f) : -GlobalTools.GetRandomDistanceNums(1f);
            __x = tl.position.x + GetWidth() * 0.5f + _pianyi;
            __y = tl.position.y + 2.2f;
            Jobj.transform.position = new Vector3(__x, __y, 0);
        }

        if (JName == "qZsw")
        {
            __y = tl.position.y - (0.4f + GlobalTools.GetRandomDistanceNums(0.4f));
            Jobj.GetComponent<SpriteRenderer>().sortingOrder = 40;
            Jobj.transform.position = new Vector3(__x, __y, 0);
        }



    }







}
