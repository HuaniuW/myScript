using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Shitou_1 : DB_NewBase
{
    public override void GetJing()
    {
        print(" 石头  New DB Base!! GetJing!");


        //return;

        //Globals.mapType = GlobalMapDate.PINGDI;
        //Globals.mapType = GlobalMapDate.DONGNEI;

        //Transform[] allChildren = Jinjings.GetChildCount();

        if (maps == null) maps = GlobalTools.FindObjByName("maps");

        //if (!IsShowDingDB && IsHasShu && Globals.mapType != GlobalMapDate.DONGNEI)
        //{
        //    if (GlobalTools.GetRandomNum() > 0)
        //    {
        //        GetShu();
        //    }
        //}

        //ShowDingDB();
        //SetDingDBPos();

        //print("子对象数量   " + JinBeijings1.childCount);

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

        //if (IsSCWu) GetWus();


        //**********************后加 的 平地前景
        if (Globals.mapType == GlobalMapDate.PINGDI)
        {
            //平地 景
            PingDiQJ1();
        }

    }
}
