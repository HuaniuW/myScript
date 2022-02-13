using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIdan_Jijia : DD_Base
{

  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void GetEnable()
    {
        MaxSpeedX = 30;
    }


    public void SetSpeed(float _sX = 0,float _sY = 0)
    {
        _speedX = _sX;
        _speedY = _sY;
        GetComponent<Rigidbody2D>().velocity = new Vector2(_speedX, _speedY);
    }

    float _speedX = 0;
    float _speedY = 0;

    protected override void GetStart()
    {
        if (!IsStart)
        {
            IsStart = true;
            //JinGaoStart();
            //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.LAIXI_JINGGAO, this.GetJinggao);
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.LAIXI_JINGGAO, null), this);
        }
        else
        {
            ZibaoJishi();
            //LaixiJingao();
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(Tuili, 0));
            //print("------------->  "+GetComponent<Rigidbody2D>().velocity);
            if (GetComponent<Rigidbody2D>().velocity.x > MaxSpeedX)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(MaxSpeedX, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag);
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

    protected override void ZibaoJishi()
    {
        ZihuiJishi += Time.deltaTime;
        if (ZihuiJishi >= ZibaoTimes)
        {
            RemoveSelf();
        }
    }


    protected override void Boom()
    {
        string bzName = "TX_Zidan1_bz";
        GameObject baozha = ObjectPools.GetInstance().SwpanObject2(Resources.Load(bzName) as GameObject);
        baozha.transform.position = this.transform.position;
        baozha.transform.localScale = new Vector3(2, 2, 2);
        baozha.name = bzName;
        if (baozha.GetComponent<JN_Date>()) baozha.GetComponent<JN_Date>().team = this.GetComponent<JN_Date>().team;
        baozha.GetComponent<ParticleSystem>().Play();
        if (GetComponent<Rigidbody2D>()) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }



}
