using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Feixingqi : MonoBehaviour
{
    public ParticleSystem TX_GaosuFly;
    public AudioSource Audio_Penqi;

    [Header("是否被 跟踪导弹 标记**导弹爆炸后记得 移除")]
    public bool IsBeCheck = false;


    //最大飞行速度
    protected float MaxFlySpeedX = 70;
    protected float _YuanshiSpeedX = 0;
    protected float Tuili = 100;
    protected bool IsGaosuFly = false;
    protected float GaosiJishi = 0;
    protected float JishiTime = 4;
    void Fly()
    {
        if (IsGaosuFly)
        {
            GaosiJishi += Time.deltaTime;
            if (GaosiJishi >= JishiTime)
            {
                GaosiJishi = 0;
                IsGaosuFly = false;
            }
        }


        if(this.transform.position.x - _player.transform.position.x <= 20)
        {
            MaxFlySpeedX = 110;
            IsGaosuFly = true;
            if (TX_GaosuFly&&TX_GaosuFly.isStopped)
            {
                TX_GaosuFly.Play();
                Audio_Penqi.Play();
            }
        }
        else
        {
            if (!IsGaosuFly) {
                MaxFlySpeedX = _YuanshiSpeedX;
                if (TX_GaosuFly && TX_GaosuFly.isPlaying)
                {
                    TX_GaosuFly.Stop();
                    Audio_Penqi.Stop();
                }
            }
            
        }


        GetComponent<Rigidbody2D>().AddForce(new Vector2(Tuili, 0));
        //print("------------->  "+GetComponent<Rigidbody2D>().velocity);
        if (GetComponent<Rigidbody2D>().velocity.x > MaxFlySpeedX)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(MaxFlySpeedX, GetComponent<Rigidbody2D>().velocity.y);
        }
    }


    protected GameObject UpPos;
    protected GameObject DownPos;



    protected GameObject _player;
    protected virtual void GetPlayer()
    {
        if (!_player)
        {
            _player = GlobalTools.FindObjByName(GlobalTag.PlayerJijiaObj);
        }
        if (!_roleDate)
        {
            _roleDate = GetComponent<RoleDate>();
        }
        if (_roleDate) _roleDate.live = _roleDate.maxLive;

        UpPos = GlobalTools.FindObjByName("TopPos");
        DownPos = GlobalTools.FindObjByName("DownPos");


    }


    bool IsFire = false;
    float FireJishi = 0;
    float FireTimes = 5;
    bool IsCanFire()
    {
        if (!IsFire)
        {
            FireJishi += Time.deltaTime;
            if(FireJishi>= FireTimes)
            {
                IsFire = true;
                //FireJishi = 0;
                return true;
            }
        }
        return false;
    }

    void GetFire()
    {
        if (_player && this.transform.position.x > _player.transform.position.x && this.transform.position.x - _player.transform.position.x < 100)
        {
            if (IsFire)
            {
                IsFire = false;
                FireJishi = 0;
                //print("---------------------> 发射 子弹！！！！！！！！！！");
                //SanFaSandan();
                StartJinageZidan();
                
            }
        }



        //if (_player)
        //{
        //    if (IsFire)
        //    {
        //        IsFire = false;
        //        print("---------------------> 发射 子弹！！！！！！！！！！");
        //        //SanFaSandan();
        //        //SuijiZidan();
        //        StartJinageZidan();
        //        FireJishi = 0;
        //    }
        //}
    }


    string ZidanName = "TX_zidanJijia_1";
    //3发散弹
    void SanFaSandan()
    {
        GameObject zidan1 = GlobalTools.GetGameObjectInObjPoolByName(ZidanName);
        zidan1.transform.position = this.transform.position;
        zidan1.transform.parent = this.transform.parent;
        zidan1.GetComponent<ZIdan_Jijia>().SetSpeed(10,0);
        GameObject zidan2 = GlobalTools.GetGameObjectInObjPoolByName(ZidanName);
        zidan2.transform.position = this.transform.position;
        zidan2.transform.parent = this.transform.parent;
        zidan2.GetComponent<ZIdan_Jijia>().SetSpeed(10, 0.8F);
        GameObject zidan3 = GlobalTools.GetGameObjectInObjPoolByName(ZidanName);
        zidan3.transform.position = this.transform.position;
        zidan3.transform.parent = this.transform.parent;
        zidan3.GetComponent<ZIdan_Jijia>().SetSpeed(10, -0.8F);
    }


    void SuijiZidan()
    {
        for (int i=0;i<8;i++)
        {
            GameObject zidan1 = GlobalTools.GetGameObjectInObjPoolByName(ZidanName);
            zidan1.name = ZidanName;
            zidan1.transform.position = this.transform.position;
            zidan1.transform.parent = this.transform.parent;
            zidan1.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 + GlobalTools.GetRandomDistanceNums(100), -100 - GlobalTools.GetRandomDistanceNums(100)));
        }
    }

    void StartJinageZidan()
    {
        IsJiangePenZidan = true;
        jishi = 0;
        ZidanNums = 10;
    }


    bool IsJiangePenZidan = false;
    float jishi = 0;
    float jiange = 0.1f;
    int ZidanNums = 10;

    void JiangePenZidan()
    {
        if (IsJiangePenZidan)
        {
            jishi += Time.deltaTime;
            if(jishi>= jiange)
            {
                jishi = 0;
                ZidanNums--;
                ShowZidan();
                if (ZidanNums == 0)
                {
                    IsJiangePenZidan = false;
                    
                    ZidanNums = 10;
                }
                
            }
        }
    }


    void ShowZidan()
    {
        //if(!this.gameObject)return;
        GameObject zidan1 = GlobalTools.GetGameObjectInObjPoolByName(ZidanName);
        zidan1.name = ZidanName;
        zidan1.transform.position = this.transform.position;
        zidan1.transform.parent = this.transform.parent.transform.parent;
        float _speedY = -300 - GlobalTools.GetRandomDistanceNums(300);
        if (this.transform.position.y < _player.transform.position.y)
        {
            _speedY *= -1;
        }
        zidan1.GetComponent<Rigidbody2D>().AddForce(new Vector2(300 + GlobalTools.GetRandomDistanceNums(300), _speedY));
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GetPlayer();
        _YuanshiSpeedX = MaxFlySpeedX;
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
        IsCanFire();
        GetFire();
        JiangePenZidan();
        GetLive();
        GetUpdate();
    }

    protected virtual void GetUpdate()
    {

    }

    RoleDate _roleDate;
    void GetLive()
    {
        if (_roleDate)
        {
            if (_roleDate.live <= 0)
            {
                HitObj();
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D Coll)
    {
        print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag);
        if(Coll.tag == GlobalTag.JIGUANG)
        {
            HitObj();
            return;
        }

        if (Coll.tag == GlobalTag.Player || Coll.tag == GlobalTag.DIBAN)
        {
            //print(testN + "   Coll.tag  碰到了什么鬼：    " + Coll.tag);
            if (Coll.tag == GlobalTag.Player && Coll.GetComponent<RoleDate>().isCanBeHit == false) return;
            //生成爆炸
            HitObj();
            if (Coll.tag == GlobalTag.Player)
            {
                Coll.GetComponent<RoleDate>().live -= GetComponent<JN_Date>().atkPower;
                //Coll.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                float ForceY = 400;
                if (Coll.transform.position.y <= this.transform.position.y)
                {
                    ForceY = -400;
                }
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);
                Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(500, ForceY));
            }
        }
    }

    protected virtual void HitObj()
    {
        Boom();
        RemoveSelf();
    }

    public virtual void RemoveSelf()
    {

        //移除自己 
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 0));
        ResetAll();
    }

    private void ResetAll()
    {

    }

    protected virtual void Boom()
    {
        string bzName = "TX_HuoyanBaozha_2";
        GameObject baozha = ObjectPools.GetInstance().SwpanObject2(Resources.Load(bzName) as GameObject);
        baozha.transform.position = this.transform.position;
        baozha.name = bzName;
        if (baozha.GetComponent<JN_Date>()) baozha.GetComponent<JN_Date>().team = this.GetComponent<JN_Date>().team;
        baozha.GetComponent<ParticleSystem>().Play();
        if (GetComponent<Rigidbody2D>()) GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        string BZSanpianName = "TX_Sanpians_1";
        int nums = 10 + GlobalTools.GetRandomNum(14);
        for (int i = 0; i < nums; i++)
        {
            GameObject baozhaSanpian = GlobalTools.GetGameObjectInObjPoolByName(BZSanpianName);
            baozhaSanpian.name = BZSanpianName;
            Vector2 speed = new Vector2(300 - GlobalTools.GetRandomDistanceNums(600), 1000 - GlobalTools.GetRandomDistanceNums(2000));
            speed = new Vector2(speed.x + 200 + GlobalTools.GetRandomDistanceNums(400), speed.y);
            if (GlobalTools.GetRandomNum() > 70)
            {
                speed = new Vector2(-200 - GlobalTools.GetRandomDistanceNums(400), speed.y);
            }
            baozhaSanpian.transform.position = this.transform.position;
            baozhaSanpian.GetComponent<Rigidbody2D>().AddForce(speed);
        }
    }
}
