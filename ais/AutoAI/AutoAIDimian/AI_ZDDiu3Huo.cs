using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZDDiu3Huo : AI_SkillBase
{
    //ZD_ZDDiu3Huo
    protected override void TheStart()
    {
        base.TheStart();
        this.ZSName = "ZDDiu3Huo";
        _yuanshiOverDelayTimes = OverDelayTimes;
    }

    //控制 动作结束 延迟
    protected float _yuanshiOverDelayTimes = 0;
    protected override void GetTheStart()
    {
        base.GetTheStart();
        string type = ZDAIName.Split('_')[2];
        if (type == "2")
        {
            //电墙
            OverDelayTimes = 0.8f;

        }
        else
        {
            OverDelayTimes = _yuanshiOverDelayTimes;
        }

    }

    //丢出3个火炸弹 
    [Header("******丢 点*******************************")]
    public Transform PosDiu;

    [Header("*****丢物品的 名字")]
    public string DiuName = "";


    float vx = 18;
    float vy = 6;

    protected void Diu3huo()
    {
        DiuName = "jn_dihuo_3";

        vx = Mathf.Abs(vx);
        vx = this.transform.localScale.x > 0 ? vx*-1 : vx ;

        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o.transform.position = PosDiu.position;
        o.transform.parent = this.transform.parent;
        o.GetComponent<Rigidbody2D>().velocity = new Vector2(vx,vy);

        GameObject o2 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o2.transform.position = PosDiu.position;
        o2.transform.parent = this.transform.parent;
        o2.GetComponent<Rigidbody2D>().velocity = new Vector2(vx+4, vy+3);


        GameObject o3 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o3.transform.position = PosDiu.position;
        o3.transform.parent = this.transform.parent;
        o3.GetComponent<Rigidbody2D>().velocity = new Vector2(vx + 6, vy + 6);
    }

    protected override void ACSkillShowOut()
    {
        //base.ACSkillShowOut();
        print("  ZDAIName  "+ ZDAIName);
        string type = ZDAIName.Split('_')[2];
        if (type == "1")
        {
            //4毒
            Diu4Du();
        }else if (type == "2")
        {
            //电墙
            Dianqiang();
        }
        else
        {
            Diu3huo();
        }
       
    }

    private void Dianqiang()
    {
        //throw new NotImplementedException();
        DiuName = "TX_Dianqiang_2";
        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o.transform.position = PosDiu.position;
        o.transform.parent = this.transform.parent;
        float _vx = this.transform.localScale.x > 0 ? -1 : 1;
        o.GetComponent<TX_Dianqiang>().SetSpeedFX(_vx);
    }

    void Diu4Du()
    {
        vx = Mathf.Abs(vx);
        vx = this.transform.localScale.x > 0 ? vx * -1 : vx;
        vy = 10;
        DiuName = "jn_dihuo_4";
        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o.transform.position = PosDiu.position;
        o.transform.parent = this.transform.parent;
        o.GetComponent<Rigidbody2D>().velocity = new Vector2(vx, vy);

        GameObject o2 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o2.transform.position = PosDiu.position;
        o2.transform.parent = this.transform.parent;
        o2.GetComponent<Rigidbody2D>().velocity = new Vector2(vx + 4, vy + 3);


        GameObject o3 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o3.transform.position = PosDiu.position;
        o3.transform.parent = this.transform.parent;
        o3.GetComponent<Rigidbody2D>().velocity = new Vector2(vx + 6, vy + 6);

        GameObject o4 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o4.transform.position = PosDiu.position;
        o4.transform.parent = this.transform.parent;
        o4.GetComponent<Rigidbody2D>().velocity = new Vector2(vx + 6, vy - 6);
    }

}
