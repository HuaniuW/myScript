using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_RenzheBiao : TX_zidan
{
    // Start is called before the first frame update
    [Header("丢飞刀 声音1")]
    public AudioSource FeibiaoDiu1;
    [Header("丢飞刀 声音2")]
    public AudioSource FeibiaoDiu2;

    [Header("飞刀被击中 声音1")]
    public AudioSource FeibiaoBeiJizhong1;
    [Header("飞刀被击中 声音2")]
    public AudioSource FeibiaoBeiJizhong2;

    [Header("飞刀旋转")]
    public AudioSource FeibiaoXuanzhuan;


    [Header("飞刀 击中地面1")]
    public AudioSource FeibiaoJizhongDimian1;




    RoleDate _roledate;
    protected override void TheStart()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _roledate = GetComponent<RoleDate>();
    }



    protected override void OtherOnEnable()
    {
        if (!_roledate)
        {
            _roledate = GetComponent<RoleDate>();
        }
        _roledate.live = 1000;
        if (!FeibiaoXuanzhuan.isPlaying) FeibiaoXuanzhuan.Play();
        if (GlobalTools.GetRandomNum() > 50)
        {
            FeibiaoDiu1.Play();
        }
        else
        {
            FeibiaoDiu2.Play();
        }

    }


    Rigidbody2D rb2d;


    [Header("忍者镖资源")]
    public GameObject Feibiao;
    float XuanzhuanDusu = 0;
    [Header("忍者镖 旋转速度")]
    public float SpeedXZ = 10;

    void Xuanzhuan()
    {
        if (IsHitDiban) return;
        XuanzhuanDusu += SpeedXZ;
        Feibiao.transform.localEulerAngles = new Vector3(0, 0, XuanzhuanDusu);
    }

    [Header("拖尾 特效")]
    public ParticleSystem Tuowei;
    [Header("击中地面 特效")]
    public ParticleSystem TX_jizhongDimian;

    protected override void OtherUpdate()
    {
        Xuanzhuan();
        HitDiban();

        //print("------rb2d.velocity    " + rb2d.velocity);
        //print("  FSFXV2  "+ FSFXV2+"    ----- "+ rb2d.velocity);
        if (IsBeHitChangeSpeed)
        {
            IsBeHitChangeSpeed = false;
            rb2d.velocity = FSFXV2;
        }

        if (_roledate)
        {
            if (_roledate.live <= 0)
            {
                ShowYanmu();
                RemoveSelf();
            }
        }

    }

    bool IsBeHitChangeSpeed = false;

    public void GetBehit(Vector3 atkObjv2)
    {
        if (!IsHitDiban)
        {
            Vector2 V2 = (this.transform.position - atkObjv2)*(4+GlobalTools.GetRandomDistanceNums(6));
            //print(this.transform.position+"     atkpos  "+atkObjv2+"    v2 "+V2);
            rb2d.gravityScale = 1f;
            //_isFSByFX = true;
            FSFXV2 = V2;
            //rb2d.velocity = V2;
            IsBeHitChangeSpeed = true;
            //print("rb2d.velocity    "+ rb2d.velocity);

            if (GlobalTools.GetRandomNum()>50)
            {
                FeibiaoBeiJizhong1.Play();
            }
            else
            {
                FeibiaoBeiJizhong2.Play();
            }

        }
    }



    protected override void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll.tag == "Player")
        {
            if (Coll.GetComponent<GameBody>().IsGedanging)
            {
                Tankai();
            }
            else
            {
                print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag+ "   FSFXV2   " + FSFXV2);
                print(rb2d.velocity);
                rb2d.velocity = FSFXV2;
            }
            return;
        }



        if ((IsCanHitDiban && Coll.tag == "diban") || Coll.tag == "zidanDun")
        {
            if (!IsCanHit) return;
            //print(testN + "   Coll.tag  碰到了什么鬼：    " + Coll.tag);
            //if (Coll.tag == "Player" && Coll.GetComponent<RoleDate>().isCanBeHit == false) return;

            if (Coll.tag == "zidanDun")
            {
                Tankai();
                return;
            }

            //生成爆炸
            HitObj();

        }
    }


    void Tankai()
    {
        float _x = -(0.2f + GlobalTools.GetRandomDistanceNums(0.5f))* FSFXV2.x;
        float _y = 6 + GlobalTools.GetRandomDistanceNums(8);
        rb2d.velocity = new Vector2( _x, _y);
        rb2d.gravityScale = 1f;
        print("   弹开????????????? ");
        //GetComponent<JN_Date>().team = 0;
    }


    protected override void HitObj()
    {
        //Boom();
        //RemoveSelf();
        rb2d.velocity = Vector2.zero;
        IsHitDiban = true;
        Tuowei.Stop();
        TX_jizhongDimian.Play();
        rb2d.gravityScale = 0f;


        FeibiaoJizhongDimian1.Play();
        FeibiaoXuanzhuan.Stop();
    }

    bool IsHitDiban = false;
    float jishi = 0;
    float jishiNums = 2;
    void HitDiban()
    {
        if (IsHitDiban)
        {
            rb2d.velocity = Vector2.zero;
            //jishi += Time.deltaTime;
            //if (jishi >= jishiNums)
            //{
            //    jishi = 0;
            //    ShowYanmu();
            //    RemoveSelf();
            //}
        }
    }

    string YanmuName = "TX_yinshenYM1";
    private void ShowYanmu()
    {
        //throw new NotImplementedException();
        GameObject yanmu = GlobalTools.GetGameObjectInObjPoolByName(YanmuName);
        yanmu.transform.position = this.transform.position;
        yanmu.transform.parent = this.transform.parent;
    }


    public override void ResetAll()
    {
        base.ResetAll();
        jishi = 0;
        IsHitDiban = false;
    }

   
}
