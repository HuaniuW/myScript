using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Base : MonoBehaviour
{
    //导弹 基础类
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected GameObject _player;

    private void OnEnable()
    {
        GetEnable();
        if (!_player) _player = GlobalTools.FindObjByName(GlobalTag.PlayerJijiaObj);
        IsStart = false;
        ZihuiJishi = 0;
    }

    protected virtual void GetEnable()
    {

    }

    [Header("火箭 主推进器")]
    public ParticleSystem TX_Tuijin1;

    [Header("火箭 转向推进器1")]
    public ParticleSystem TX_Tuijin2;
    [Header("火箭 转向推进器2")]
    public ParticleSystem TX_Tuijin3;



    protected float SpeedX = 0;
    protected float SpeedY = 0;

    public float Tuili = 40;
    public float MaxSpeedX = 120;


    protected bool IsStart = false;
    protected virtual void GetStart()
    {
        if (!IsStart)
        {
            IsStart = true;
            TX_Tuijin1.Play();
            JinGaoStart();
            //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.LAIXI_JINGGAO, this.GetJinggao);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.LAIXI_JINGGAO, null), this);
        }
        else
        {
            ZibaoJishi();
            LaixiJingao();
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Tuili,0));
            //print("------------->  "+GetComponent<Rigidbody2D>().velocity);
            if (GetComponent<Rigidbody2D>().velocity.x > MaxSpeedX)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(MaxSpeedX, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }


    //来袭警告
    //string TX_LaixiJinggao = "";
    protected GameObject TX_LaixiJinggao;
    protected Transform JinggaoXObj;
    protected GameObject player;
    protected void JinGaoStart()
    {
        string TX_name = "TX_LaixiJinggao";
        TX_LaixiJinggao = GlobalTools.GetGameObjectInObjPoolByName(TX_name);
        TX_LaixiJinggao.name = TX_name;
        JinggaoXObj = GlobalTools.FindObjByName(GlobalTag.MAINCAMERA).GetComponent<CameraController>().CameraAirPosLeftX;
        TX_LaixiJinggao.transform.parent = this.transform.parent;
        TX_LaixiJinggao.transform.position = new Vector2(JinggaoXObj.position.x, this.transform.position.y);
        TX_LaixiJinggao.transform.localScale = new Vector3(1.6f,1.6f,1.6f);
        player = GlobalTools.FindObjByName(GlobalTag.PlayerJijiaObj);
    }

    float _dxjingshi = 23;
    //来袭警告
    protected virtual void LaixiJingao()
    {
        if (TX_LaixiJinggao)
        {
            TX_LaixiJinggao.transform.position = new Vector3(player.transform.position.x- _dxjingshi, this.transform.position.y,this.transform.position.z);
            if(TX_LaixiJinggao.transform.position.x< JinggaoXObj.position.x- _dxjingshi)
            {
                TX_LaixiJinggao.transform.position = new Vector3(JinggaoXObj.position.x- _dxjingshi, this.transform.position.y, this.transform.position.z);
            }
            if(this.transform.position.x>= TX_LaixiJinggao.transform.position.x)
            {
                ObjectPools.GetInstance().DestoryObject2(TX_LaixiJinggao);
            }
        }
    }




    protected float ZihuiJishi = 0;
    protected float ZibaoTimes = 20;
    protected virtual void ZibaoJishi()
    {
        if (IsZibaoJishi())
        {
            Boom();
            RemoveSelf();
        }
    }

    protected bool IsZibaoJishi()
    {
        ZihuiJishi += Time.deltaTime;
        if (ZihuiJishi >= ZibaoTimes)
        {
            return true;
        }
        return false;
    }



    protected virtual void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag);
        if (Coll.tag == GlobalTag.Player ||  Coll.tag == GlobalTag.DIBAN|| Coll.tag == "zidan"||Coll.tag == GlobalTag.JIGUANG)
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
        if(TX_LaixiJinggao) ObjectPools.GetInstance().DestoryObject2(TX_LaixiJinggao);

        //移除自己 
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 0));
        ResetAll();
    }

    private void ResetAll()
    {
        //IsStart = false;
        ZihuiJishi = 0;
    }

    protected virtual void Boom()
    {
        string bzName = "TX_HuoyanBaozha_2";
        GameObject baozha = ObjectPools.GetInstance().SwpanObject2(Resources.Load(bzName) as GameObject);
        baozha.transform.position = this.transform.position;
        baozha.name = bzName;
        if (baozha.GetComponent<JN_Date>()) baozha.GetComponent<JN_Date>().team = this.GetComponent<JN_Date>().team;
        baozha.GetComponent<ParticleSystem>().Play();
        if(GetComponent<Rigidbody2D>())GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        string BZSanpianName = "TX_Sanpians_1";
        int nums = 10 + GlobalTools.GetRandomNum(14);
        for(int i = 0; i < nums; i++)
        {
            GameObject baozhaSanpian = GlobalTools.GetGameObjectInObjPoolByName(BZSanpianName);
            baozhaSanpian.name = BZSanpianName;
            Vector2 speed = new Vector2(300-GlobalTools.GetRandomDistanceNums(600), 1000-GlobalTools.GetRandomDistanceNums(2000));
            speed = new Vector2(speed.x+ 200+GlobalTools.GetRandomDistanceNums(400),speed.y);
            if (GlobalTools.GetRandomNum() > 70)
            {
                speed = new Vector2(-200 -GlobalTools.GetRandomDistanceNums(400), speed.y);
            }
            baozhaSanpian.transform.position = this.transform.position;
            baozhaSanpian.GetComponent<Rigidbody2D>().AddForce(speed);
        }
    }



    // Update is called once per frame
    void Update()
    {
        GetStart();
    }
}
