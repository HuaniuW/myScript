using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ShunxuSandan :AI_SkillBase
{
    //ZD_ShunxuSandan_5 /7/8
    //顺序发射 的 散弹
    // Start is called before the first frame update
    protected override void TheStart()
    {
        
        if (_player == null) _player = GlobalTools.FindObjByName("player");
        this.ZSName = "ShunxuSandan";
        IsSpeAISkill = true;

    }

    [Header("子弹发射 声音")]
    public AudioSource Audio_ZidanFashe;



    protected override void GetTheStart()
    {
        // ZDAIName=>    ZD_ShunxuSandan_5


        //开启 特殊 技能释放  这里 就进入持续技能里面  ChixuSkillStarting


        ZiDanTypeNum = int.Parse(ZDAIName.Split('_')[2]);
        qishiNums = (int)(ZidanNums * 0.5f);
        vToTarget = ZidanFX();
        TempMaxZidanNums = ZidanNums;

        ZiDanType();
        
    }

    private void ZiDanType()
    {
        //throw new NotImplementedException();
        if(ZiDanTypeNum == 1)
        {
            //5子弹
            TempMaxZidanNums = 5;
            ZiDanName = "TX_zidan21";
        }
        else if (ZiDanTypeNum == 2)
        {
            TempMaxZidanNums = 7;
            ZiDanName = "TX_zidan21";
        }
        else if (ZiDanTypeNum == 3)
        {
            TempMaxZidanNums = 8;
            ZiDanName = "TX_zidan21";
        }
    }

    int TempMaxZidanNums = 0;

    [Header("顺序发射的 子弹数量")]
    public int ZidanNums = 5;

    protected override void ACSkillShowOut()
    {

    }

    [Header("计时间隔")]
    public float jishiJiange = 0.3f;
    float jishi = 0;
    protected override void ChixuSkillStarting()
    {

        if (_roleDate.isDie || _roleDate.isBeHiting)
        {
            ReSetAll();
            TheSkillOver();
        }
        //print("  ----------------------------chixu!!!!! ");
        if(TempMaxZidanNums == 0)
        {
            ReSetAll();
            TheSkillOver();
            //_isGetOver = true;
            //IsGetOver();
            return;
        }
        jishi += Time.deltaTime;
        if (jishi >= jishiJiange)
        {
            jishi = 0;
            SanLianSanDan(vToTarget, TempMaxZidanNums);
            TempMaxZidanNums--;
        }
    }


    Vector2 vToTarget;
    protected virtual Vector2 ZidanFX()
    {
        return _player.transform.position - zidanDian1.position;
    }



    int qishiNums = 0;
    int ZiDanTypeNum = 1;
    public string ZiDanName = "TX_zidan21";
    void SanLianSanDan(Vector2 v1, int nums = 1, float hudu = 20)
    {
        GameObject zidan;
        float _hudu = hudu;

        //(qishiNums+1)-nums


        zidan = GetZiDan();

        if (Audio_ZidanFashe)
        {
            print(" -------------------------------------------  子弹发射 声音 ");
            Audio_ZidanFashe.Play();
        }

        _hudu = hudu * ( qishiNums+1-nums) * 3.14f / 180;
        Vector2 v2 = GlobalTools.GetNewV2ByHuDu(_hudu, v1);
        zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v2, 10);


        //for (int i = 0; i < nums; i++)
        //{
        //    zidan = GetZiDan();
        //    _hudu = hudu * (i + 1) * 3.14f / 180;
        //    Vector2 v2 = GlobalTools.GetNewV2ByHuDu(_hudu, v1);
        //    zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v2, 10);
        //}

        //for (int i = 0; i < nums; i++)
        //{
        //    zidan = GetZiDan();
        //    _hudu = -hudu * (i + 1) * 3.14f / 180;
        //    Vector2 v2 = GlobalTools.GetNewV2ByHuDu(_hudu, v1);
        //    zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v2, 10);
        //}

    }

    protected GameObject GetZiDan()
    {
        //print("************************************************ZiDanName   " + ZiDanName);
        GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(ZiDanName) as GameObject);
        //print("  zidan "+zidan);

        if (ZiDanTypeNum == 15 || ZiDanTypeNum == 16)
        {
            //火焰弹 
            zidan.GetComponent<OnLziHit>().SetCanHit();
        }
        zidan.transform.position = zidanDian1.position;
        zidan.GetComponent<TX_zidan>().CloseAutoFire();
        zidan.transform.localScale = this.transform.localScale;
        zidan.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
       
        
        return zidan;
    }



}
