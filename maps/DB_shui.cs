using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_shui : DBBase
{
    // Start is called before the first frame update

    [Header("水岸点1")]
    public Transform thePos1;
    [Header("水岸点2")]
    public Transform thePos2;
    [Header("是否是超长的 水面")]
    public bool IsChaoChang = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    //地板类型  悬空  会出机关刺的地板  踩上去 会下沉的地板  突出的地板（一般地板）
    //-- 长短 板  一般 付出面板   和踩上会下沉地板   悬浮地板（上面会出刺）  竖着的地板  上面有刺 


    [Header("水面出现 浮地板的个数")]
    public int ShuiMianDBNums = 0;

    void GetDBShuiMian()
    {
        
        if (ShuiMianDBNums == 0) return;
        List<string> db_shuimians = GetDateByName.GetInstance().GetListByName("db_shuimian_" + Globals.mapTypeNums.ToString(), MapNames.GetInstance());      //MapNames.GetInstance().GetCanRandomUSEJYGName("db_shuimian_"+ Globals.mapTypeNums.ToString());  //MapNames.GetInstance().db_shuimian+"_"+Globals.mapTypeNums.ToString();
        if (db_shuimians.Count == 0) return;

        int duans = ShuiMianDBNums;

        //GameObject maps = GlobalTools

        if (ShuiMianDBNums == 100)
        {
            //=100的时候 根据 距离来生成 地板个数
            int nums = (int)Mathf.Abs(thePos2.position.x - thePos1.position.x) / 10-2;
            if (nums < 0) return;
            duans = nums;
        }

      


        float _distances = Mathf.Abs(thePos2.position.x - thePos1.position.x) / duans;
        float __x = 0;
        float __y = 0;
        if (duans == 1) _distances *= 0.5f;
        for (int i = 0; i < duans; i++)
        {
            if (ShuiMianDBNums == 100 && i == duans - 1) continue;
            __x = thePos1.position.x + _distances*(i+1)-0.5f + GlobalTools.GetRandomDistanceNums(1);
            __y = thePos1.position.y-1.9f;
            GameObject db_shuimian = GlobalTools.GetGameObjectByName(db_shuimians[GlobalTools.GetRandomNum(db_shuimians.Count)]);
            db_shuimian.transform.position = new Vector2(__x, __y);

            int _order = 6 + i % 6;
            GlobalTools.SetMapObjOrder(db_shuimian, _order);

            db_shuimian.transform.parent = maps.transform;
            
        }
    }

    //水上出现的 地板 浮板

    //水上出现的怪   跳跃鱼  跳起来朝角色喷子弹的鱼   （黑色恶魔 横冲 竖冲 天上丢子弹 隐身去到要去的位置）





    //近背景
    protected override void GetLRJinBG()
    {
        //return;
        //怎么根据 关卡来判断出来的 景数量？？？之要判断数量？  还有位置  关于旋转？？树好像有旋转  看看怎么写进去
        string jjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("jjd");
        if (jjdArrName == "") return;
        int nums = 1 + GlobalTools.GetRandomNum(3);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        Vector2 pos1r = thePos1.position;
        Vector2 pos2l = thePos2.position;

        string jjdArrName2 = MapNames.GetInstance().GetJingArrNameByGKKey("jjd2");
        if (jjdArrName2 == "") return;


        if (Globals.mapTypeNums == 1)
        {
            //左岸景
            SetJingByDistanceU(jjdArrName, nums, pos1, pos1r, pos1r.y, 0, 0, -15, "u");
            //右岸景
            SetJingByDistanceU(jjdArrName, nums, pos2l, pos2, pos2l.y, 0, 0, -15, "u");



            nums = 1 + GlobalTools.GetRandomNum(1);
            //左岸景
            SetJingByDistanceU(jjdArrName2, nums, pos1, pos1r, pos1r.y, 0, 0, -14, "u", 2);
            //右岸景
            SetJingByDistanceU(jjdArrName2, nums, pos2l, pos2, pos2l.y, 0, 0, -14, "u", 2);


            //水中景


        }
        else if (Globals.mapTypeNums == 2)
        {
            nums = 1 + GlobalTools.GetRandomNum(3);
            //左岸景
            SetJingByDistanceU(jjdArrName, nums, pos1, pos1r, pos1r.y + 0.5f, 0, 0, -15, "u");
            //右岸景
            SetJingByDistanceU(jjdArrName, nums, pos2l, pos2, pos2.y + 0.5f, 0, 0, -15, "u");


            nums = 1 + GlobalTools.GetRandomNum(1);
            if (nums == 0) return;
            //左岸景
            SetJingByDistanceU(jjdArrName2, nums, pos1, pos1r, pos1.y, 0, 0, -14, "u", 2);
            //右岸景
            SetJingByDistanceU(jjdArrName2, nums, pos2l, pos2, pos2l.y, 0, 0, -14, "u", 2);

            //水中景

        }
    }


    //近前景
    protected override void GetJQJ()
    {
        int nums = 0;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        Vector2 pos1r = thePos1.position;
        Vector2 pos2l = thePos2.position;

        if (Globals.mapTypeNums == 1)
        {
            nums = 4 + GlobalTools.GetRandomNum(4);
            //float _y = pos1.y - 1.5f;
            string qjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd");
            if (qjdArrName != "") {
                SetJingByDistanceU(qjdArrName, nums, pos1, pos1r, pos1.y - 1f - GlobalTools.GetRandomDistanceNums(1), -0.2f, 0.1f, 30, "u");
                SetJingByDistanceU(qjdArrName, nums, pos2l, pos2, pos2.y - 1f - GlobalTools.GetRandomDistanceNums(1), -0.2f, 0.1f, 30, "u");
            }
            
            //SetJingByDistanceU("qjd_1", nums, pos1, pos2, pos1.y, -4f, -1f, 40, "u");
            string qjd2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd2");
            if (qjd2ArrName != "") {
                SetJingByDistanceU(qjd2ArrName, nums, pos1, pos1r, pos1r.y - 1.5f, -0.3f, 0, 30, "u");
                SetJingByDistanceU(qjd2ArrName, nums, pos2l, pos2, pos2.y - 1.5f, -0.3f, 0, 30, "u");
            }
            

            string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            if (qjd3ArrName != "") {
                SetJingByDistanceU(qjd3ArrName, nums, pos1, pos1r, pos1r.y - 1.6f, -0.4f, 0, 30, "u");
                SetJingByDistanceU(qjd3ArrName, nums, pos2l, pos2, pos2.y - 1.6f, -0.4f, 0, 30, "u");
            }
           

            string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            nums = 1 + GlobalTools.GetRandomNum(1);
            if (qyjdArrName != "") {
                SetJingByDistanceU(qyjdArrName, nums, pos1, pos1r, pos1r.y - 2.2f, -0.6f, 1, 40, "u", 2);
                SetJingByDistanceU(qyjdArrName, nums, pos2l, pos2, pos2.y - 2.2f, -0.6f, 1, 40, "u", 2);
            }
            
        }
        else if (Globals.mapTypeNums == 2)
        {
            nums = 1 + GlobalTools.GetRandomNum(1);
            string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            if (qjd3ArrName != "") {
                SetJingByDistanceU(qjd3ArrName, nums, pos1, pos1r, pos1r.y - 2f, -0.6f, 1.2f, 40, "u");
                SetJingByDistanceU(qjd3ArrName, nums, pos2l, pos2, pos2.y - 2f, -0.6f, 1.2f, 40, "u");
            }
            

            string qjd5ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd5");
            nums = 1 + GlobalTools.GetRandomNum(2);
            if (qjd5ArrName != "") {
                SetJingByDistanceU(qjd5ArrName, nums, pos1, pos1r, pos1r.y - 1f, -0.2f, 0.6f, 45, "u", 2);
                SetJingByDistanceU(qjd5ArrName, nums, pos2l, pos2, pos2.y - 1f, -0.2f, 0.6f, 45, "u", 2);
            } 

            string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd4");
            nums = 1 + GlobalTools.GetRandomNum(1);
            if (qyjdArrName != "") {
                SetJingByDistanceU(qyjdArrName, nums, pos1, pos1r, pos1r.y - 1.9f, -1f, 0.6f, 60, "u", 2);
                SetJingByDistanceU(qyjdArrName, nums, pos2l, pos2, pos2.y - 1.9f, -1f, 0.6f, 60, "u", 2);
            }
            
        }
    }


    protected override void GetWus()
    {

        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, rd.position.y+1);

        //float _y = pos1.y - 1.5f;
        //SetJingByDistanceU("liziWu_1", nums, pos1, pos2, pos1.y+1, 0, 0, 0, "u");
        //Color color1 = new Color(0.1f, 1f, 1f, 0.1f);
        //GetWu("", pos1, pos2, -30, color1);
        Color color2 = MapNames.GetInstance().GetColorByGKKey(); //new Color(0.1f, 1f, 1f, 0.3f);
        GetWu("", pos1, pos2, -70, color2);


        if (!IsLiziWu) return;

        string liziArrName = MapNames.GetInstance().GetJingArrNameByGKKey("liziWu");
        if (liziArrName == "") return;


        List<string> liziArr = GetDateByName.GetInstance().GetListByName(liziArrName, MapNames.GetInstance());

        if(ShuiMianDBNums == 1)
        {
            SetLiziByNums(2, liziArr, pos1.x, pos2.x, pos1.y - 1);
        }
        else if(ShuiMianDBNums == 100)
        {
            SetLiziByNums(10, liziArr, pos1.x, pos2.x, pos1.y - 1);
        }
        else
        {
            SetLiziByNums(1, liziArr, pos1.x, pos2.x, pos1.y - 1);
        }


       





        if (GlobalTools.GetRandomNum() > 0)
        {
            liziArrName = MapNames.GetInstance().GetJingArrNameByGKKey("liziWu2");
            if (liziArrName == "") return;
            liziArr = GetDateByName.GetInstance().GetListByName(liziArrName, MapNames.GetInstance());
            //SetLiziByNums(1, liziArr, pos1.x, pos2.x, pos1.y - 1);

            if (ShuiMianDBNums == 1)
            {
                SetLiziByNums(2, liziArr, pos1.x, pos2.x, pos1.y - 1);
            }
            else if (ShuiMianDBNums == 100)
            {
                SetLiziByNums(10, liziArr, pos1.x, pos2.x, pos1.y - 1);
            }
            else
            {
                SetLiziByNums(1, liziArr, pos1.x, pos2.x, pos1.y - 1);
            }
        }

    }



    protected override void GetYQJ()
    {
        return;
        int nums = 1 + GlobalTools.GetRandomNum(2);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //怎么区分 远前景
        string yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj");
        if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 2f, -1.4f, 0.4f, 40, "u");

        yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj2");
        //nums = 1+ GlobalTools.GetRandomNum(2);
        if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 2f, -1.6f, 0.5f, 45, "u");

        if (GlobalTools.GetRandomNum() > 90)
        {
            yqjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("yqj3");
            nums = 1;
            if (yqjdArrName != "") SetJingByDistanceU(yqjdArrName, nums, pos1, pos2, pos1.y - 2f, -3f, 0.5f, 55, "u");
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

        //if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums2, pos1, pos2, pos1.y - 0.5f + _YJmoveY, 1f, 0.8f, -50, "u");




        if (GlobalTools.GetRandomNum() > 60)
        {
            ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("ybj");
            if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums, pos1, pos2, pos1.y - 1f + _YJmoveY, 2.8f, 1.5f, -70, "u");
        }

        if (GlobalTools.GetRandomNum() > 60)
        {
            ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("ybj2");
            if (ybjArrName != "") SetJingByDistanceU(ybjArrName, nums, pos1, pos2, pos1.y - 1.2f + _YJmoveY, 4.6f, 1.5f, -80, "u");
        }

        if (ShuiMianDBNums == 100)
        {
            print("JINLAMEI************************!!!!!");
            ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("ybj");
            if (ybjArrName != "") SetJingByDistanceU(ybjArrName, 10, pos1, pos2, pos1.y - 1f + _YJmoveY, 2.8f, 1.5f, -70, "u");

            ybjArrName = MapNames.GetInstance().GetJingArrNameByGKKey("ybj2");
            if (ybjArrName != "") SetJingByDistanceU(ybjArrName, 10, pos1, pos2, pos1.y - 1.2f + _YJmoveY, 4.6f, 1.5f, -80, "u");
        }


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

        Vector2 pos1r = thePos1.position;
        Vector2 pos2l = thePos2.position;



        //float _y = pos1.y - 1.5f;

        SetJingByDistanceU(jyjArrName, nums, pos1r, pos2l, pos2l.y - 7.3f - GlobalTools.GetRandomDistanceNums(1), 0.7f, 0.3f, -30, "u", 1);

        //水下前景
        SetJingByDistanceU(jyjArrName, nums+2, pos1r, pos2l, pos2l.y - 6.9f - GlobalTools.GetRandomDistanceNums(1.2f), -0.9f, 0.3f, 30, "u", 1);


        //两岸的 景
        //左
        SetJingByDistanceU(jyjArrName, nums, pos1, pos1r, pos1r.y - 0.5f - GlobalTools.GetRandomDistanceNums(2), 0.7f, 0.3f, -30, "u", 1);
        //右
        SetJingByDistanceU(jyjArrName, nums, pos2l, pos2, pos2.y - 0.5f - GlobalTools.GetRandomDistanceNums(2), 0.7f, 0.3f, -30, "u", 1);

        if (Globals.mapTypeNums == 2)
        {


            print("    进来没 中远景！！！！！！！！！   ");
            nums = GlobalTools.GetRandomNum(2);
            if(ShuiMianDBNums==100) nums = 10+ GlobalTools.GetRandomNum(5);
            string jyjArrName2 = MapNames.GetInstance().GetJingArrNameByGKKey("zyj");
            SetJingByDistanceU(jyjArrName2, nums, pos1r, pos2l, pos2l.y - 4.3f - GlobalTools.GetRandomDistanceNums(1), 2f, 0.6f, -40, "u", 1);


            nums = 1 + GlobalTools.GetRandomNum(1);
            if (ShuiMianDBNums == 100) nums = 10 + GlobalTools.GetRandomNum(5);
            string jyjArrName3 = MapNames.GetInstance().GetJingArrNameByGKKey("dyj");
            SetJingByDistanceU(jyjArrName3, nums, pos1r, pos2l, pos2l.y - 3.5f - GlobalTools.GetRandomDistanceNums(1), 5f, 1.6f, -50, "u", 1);

        }

    }





    protected override void GetTopJ3()
    {
        string qjuArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju");
        if (qjuArrName == "") return;
        int nums = 8 + GlobalTools.GetRandomNum(4);
        if (IsChaoChang) nums += 10;
        //DingDBPosL.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //DingDBPosR.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //print("  ??>>>>>>>>>**qjuArrName   " + qjuArrName+"   pos  "+ DingDBPosL.transform.position);
        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;
        SetJingByDistanceU(qjuArrName, nums, pos1, pos2, pos1.y - 2, 0, 0, 50, "d");
    }

    protected override void GetTopJ4()
    {
        string qju2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju2");
        if (qju2ArrName == "") return;
        int nums = 8 + GlobalTools.GetRandomNum(2);
        if (IsChaoChang) nums += 10;
        //DingDBPosL.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //DingDBPosR.transform.parent = GlobalTools.FindObjByName("maps").transform;
        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;
        SetJingByDistanceU(qju2ArrName, nums, pos1, pos2, pos1.y - 1.5f, -0.3f, 0, 20, "d");
    }








    //机关喷火
    protected override void JiGuan_Penghuo()
    {
        return;
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
                __y = tl.transform.position.y + 1f;
            }


            JG_PenHuo.transform.position = new Vector2(__x, __y);
            JG_PenHuo.transform.parent = maps.transform;
            float jiangeshijian = 0.5f + GlobalTools.GetRandomDistanceNums(1);
            float penfashijian = 0.5f + GlobalTools.GetRandomDistanceNums(1);

            JG_PenHuo.GetComponent<JG_huoyan>().jiangeshijian = jiangeshijian;
            JG_PenHuo.GetComponent<JG_huoyan>().penfashijian = penfashijian;

        }
    }



    protected override void Zhuangshiwu(string JName = "zsw")
    {

        //水面地板
        GetDBShuiMian();
        //出水面怪
        ShuiMianChuGuai();

        string zswArrName = MapNames.GetInstance().GetJingArrNameByGKKey(JName);
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
            float _pianyi = GlobalTools.GetRandomNum() > 50 ? GlobalTools.GetRandomDistanceNums(0.2f) : -GlobalTools.GetRandomDistanceNums(0.2f);
            //__x = tl.position.x + GetWidth() * 0.5f + _pianyi;

            if (GlobalTools.GetRandomNum() > 50)
            {
                //左边
                __x = pos1.x + (this.thePos1.position.x - pos1.x) * 0.5f + _pianyi;
            }
            else
            {
                __x = thePos2.position.x + (pos2.x - thePos2.position.x) * 0.5f + _pianyi;
            }

            __y = tl.position.y + 2.6f;
        }
        Jobj.transform.position = new Vector3(__x, __y, 0);

        //水下 是否有
        if (GlobalTools.GetRandomNum() > 0)
        {
            float _pianyi = 0;
            GameObject Jobj2;
            if (ShuiMianDBNums == 100)
            {
                int zswNums = 5;
                float _distances = Mathf.Abs(thePos2.position.x - thePos1.position.x) / (zswNums+1);
                for (int i=0;i<zswNums;i++)
                {
                    Jobj2 = GetJObjByListName(zswArrName);
                    _pianyi = GlobalTools.GetRandomNum() > 50 ? GlobalTools.GetRandomDistanceNums(1f) : -GlobalTools.GetRandomDistanceNums(1f);
                    __x = pos1.x + (i + 1) * _distances+_pianyi;
                    __y = tl.position.y - 1.6f - GlobalTools.GetRandomDistanceNums(1f);
                    Jobj2.transform.position = new Vector2(__x, __y);
                }
            }
            else
            {
                Jobj2 = GetJObjByListName(zswArrName);
                _pianyi = GlobalTools.GetRandomNum() > 50 ? GlobalTools.GetRandomDistanceNums(1f) : -GlobalTools.GetRandomDistanceNums(1f);
                __x = pos1.x + (pos2.x - pos1.x) * 0.5f + _pianyi;
                __y = tl.position.y - 1.6f - GlobalTools.GetRandomDistanceNums(0.4f);
                Jobj2.transform.position = new Vector3(__x, __y, 0);
            }

        }


    }


    //水面出怪
    void ShuiMianChuGuai()
    {
        int GuaiNums = 0;
        if (ShuiMianDBNums == 100)
        {
            GuaiNums = 10 + GlobalTools.GetRandomNum(6);
            float _distande = Mathf.Abs(thePos2.position.x - thePos1.position.x)/(GuaiNums+2);
            float __x = 0;
            float __y = 0;
            for (int i = 0; i < GuaiNums; i++)
            {
                string GuaiName = MapNames.GetInstance().GetCanRandomUSEJYGName("kongZhongXiaoGuai");
                GameObject guai = GlobalTools.GetGameObjectByName(GuaiName);
                __x = thePos1.position.x + (i + 1) * _distande + GlobalTools.GetRandomDistanceNums(0.6f);
                __y = thePos1.position.y + 0.8f + GlobalTools.GetRandomDistanceNums(0.6f);
                guai.transform.position = new Vector2(__x,__y);
                guai.transform.parent = maps.transform;
                maps.GetComponent<GetReMap2>().GuaiList.Add(guai);
            }

        }
    }




}
