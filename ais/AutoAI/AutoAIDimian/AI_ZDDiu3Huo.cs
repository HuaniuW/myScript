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
    }

   
    protected override void GetTheStart()
    {
        base.GetTheStart();
        //string type = ZDAIName.Split('_')[2];
        if (SkillType == "2")
        {
            //电墙
            OverDelayTimes = 0.8f;

        }else if (SkillType == "rzb" || SkillType == "4")
        {
            //忍者镖
            //ACScaleStartTimes = 0.1f;
            _gameBody.GetPause(0.3f, 0.1f);
            AtkDistances = 30;
        }
        else if (SkillType == "3rzb" || SkillType == "5")
        {
            //向天上丢3发忍者镖
            _gameBody.GetPause(0.3f, 0.1f);
            AtkDistances = 30;
        }
        else
        {
            OverDelayTimes = _yuanshiOverDelayTimes;
            AtkDistances = _AtkDistances;
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
        print("diuhuo1   "+o.name);

        GameObject o2 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o2.transform.position = PosDiu.position;
        o2.transform.parent = this.transform.parent;
        o2.GetComponent<Rigidbody2D>().velocity = new Vector2(vx+4, vy+3);
        print("diuhuo2   " + o2.name);

        GameObject o3 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o3.transform.position = PosDiu.position;
        o3.transform.parent = this.transform.parent;
        o3.GetComponent<Rigidbody2D>().velocity = new Vector2(vx + 6, vy + 6);
        print("diuhuo3   " + o3.name);
    }


    [Header("临时提高硬直")]
    public float TempAddYingzhi = 800;


    protected override void ACSkillShowOut()
    {
        //base.ACSkillShowOut();
        print("  ZDAIName  "+ ZDAIName);
        //string type = ZDAIName.Split('_')[2];
        if (StartParticle)
        {
            StartParticle.Play();
        }
        if (SkillType == "1")
        {
            //4毒
            Diu4Du();
        }else if (SkillType == "2")
        {
            GetComponent<TempAddValues>().TempAddYZ(TempAddYingzhi,0.6f);
            //电墙
            Dianqiang();
        }else if (SkillType == "ywd" || SkillType == "3")
        {
            //烟雾弹
            Yanwudan();
        }else if (SkillType == "rzb"|| SkillType == "4")
        {
            RenzhebiaoHeng();
        }
        else if (SkillType == "3rzb" || SkillType == "5")
        {
            //向天上丢3发忍者镖
            Up3RenzheBiao();
        }
        else
        {
            print("--------------------------> 丢sanuhuo  ");
            Diu3huo();
            
        }
       
    }


    void ShowRenzhebiao(string ObjName, Vector2 v2, float g = 0)
    {
        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o.transform.position = PosDiu.position;
        o.transform.parent = this.transform.parent;
        o.name = DiuName;
        o.GetComponent<Rigidbody2D>().gravityScale = g;
        o.GetComponent<JN_Date>().team = this.GetComponent<RoleDate>().team;
        o.GetComponent<TX_RenzheBiao>().SetV2Speed(v2);
        o.GetComponent<TX_RenzheBiao>().GetSpeedV2();
    }


    private void Up3RenzheBiao()
    {
        //throw new NotImplementedException();
        DiuName = "AQ_renzhebiao";
        Vector2 v2 = new Vector2(-10 * this.transform.localScale.x, 10);
        ShowRenzhebiao(DiuName,v2,1.5f);

        v2 = new Vector2(-10 * this.transform.localScale.x, 15);
        ShowRenzhebiao(DiuName, v2, 1.5f);

        v2 = new Vector2(-5 * this.transform.localScale.x, 15);
        ShowRenzhebiao(DiuName, v2, 1.5f);



        //GameObject o = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        //o.transform.position = PosDiu.position;
        //o.transform.parent = this.transform.parent;
        //o.GetComponent<JN_Date>().team = this.GetComponent<RoleDate>().team;
        //o.GetComponent<TX_RenzheBiao>().SetV2Speed(new Vector2(-20 * this.transform.localScale.x, 0));
        ////print("忍者镖的 速度    " + o.GetComponent<Rigidbody2D>().velocity);
        //o.GetComponent<TX_RenzheBiao>().GetSpeedV2();

    }

    private void RenzhebiaoHeng()
    {
        //throw new NotImplementedException();
        DiuName = "AQ_renzhebiao";
        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o.transform.position = PosDiu.position;
        o.transform.parent = this.transform.parent;
        o.name = DiuName;

        //public void GetSpeedV2()
        //{
        //    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //    GetComponent<Rigidbody2D>().velocity = FSFXV2;
        //}

        //public void SetV2Speed(Vector2 v2)
        //{
        //    FSFXV2 = v2;
        //}


        o.GetComponent<JN_Date>().team = this.GetComponent<RoleDate>().team;
        o.GetComponent<TX_RenzheBiao>().SetV2Speed(new Vector2(-20 * this.transform.localScale.x, 0));
        print("忍者镖的 速度    "+o.GetComponent<Rigidbody2D>().velocity);
        o.GetComponent<TX_RenzheBiao>().GetSpeedV2();

        //o.GetComponent<Rigidbody2D>().velocity = o.GetComponent<TX_RenzheBiao>().FSFXV2;
    }

    private void Yanwudan()
    {
        //throw new NotImplementedException();
        DiuName = "TX_yinshenYM1";
        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o.transform.position = PosDiu.transform.position;
        o.transform.parent = this.transform.parent;
        o.name = DiuName;


        //o.GetComponent<TX_RenzheBiao>().SetZDSpeed(10);
        //o.GetComponent<TX_RenzheBiao>().SetZiDanSpeedByFX(this.transform.localScale.x);
        //public void SetZiDanSpeedByFX(float scaleX)
        //{
        //    _isFSByFX = true;
        //    //FSFXV2 = new Vector2(1, 0);
        //    FSFXV2 *= scaleX * speeds;
        //}


    }

    private void Dianqiang()
    {
        //throw new NotImplementedException();
        DiuName = "TX_Dianqiang_2";
        GameObject o = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o.transform.position = PosDiu.position;
        o.transform.parent = this.transform.parent;
        o.name = DiuName;
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
        o.name = DiuName;
        o.GetComponent<Rigidbody2D>().velocity = new Vector2(vx, vy);

        GameObject o2 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o2.transform.position = PosDiu.position;
        o2.transform.parent = this.transform.parent;
        o2.name = DiuName;
        o2.GetComponent<Rigidbody2D>().velocity = new Vector2(vx + 4, vy + 3);


        GameObject o3 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o3.transform.position = PosDiu.position;
        o3.transform.parent = this.transform.parent;
        o3.name = DiuName;
        o3.GetComponent<Rigidbody2D>().velocity = new Vector2(vx + 6, vy + 6);

        GameObject o4 = GlobalTools.GetGameObjectInObjPoolByName(DiuName);
        o4.transform.position = PosDiu.position;
        o4.transform.parent = this.transform.parent;
        o4.name = DiuName;
        o4.GetComponent<Rigidbody2D>().velocity = new Vector2(vx + 6, vy - 6);
    }

}
