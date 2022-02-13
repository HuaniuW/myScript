using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_GZDaodanPlayer : DD_GZDaodan
{

    public AudioSource Audio_DDFashe;

    protected override void GetEnable()
    {
        //base.GetEnable();
        GetComponent<Rigidbody2D>().gravityScale = 1;
        DownTiming = 0;
        isDownOver = false;
        _targetGuai = null;
        _YuanshiMaxSpeedX = MaxSpeedX;
        MaxSpeedX = 140;
        Audio_DDFashe.Stop();
        TX_Tuijin1.gameObject.SetActive(false);
        TX_Tuijin1.Stop();
    }

    float _QSspeedX = 0;
    public void SetQSSpeedX(float speedX)
    {
        _QSspeedX = speedX;
    }


    float DownTiming = 0;
    float DownStratTime = 0.6f;
    bool isDownOver = false;
    bool GetDown()
    {
        if (!isDownOver)
        {
            DownTiming += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = new Vector2(_QSspeedX,this.GetComponent<Rigidbody2D>().velocity.y);
            if (DownTiming >= DownStratTime)
            {
                isDownOver = true;
                
                DownTiming = 0;
            }
        }
       
        return isDownOver;
    }

    GameObject _guais;
    GameObject _targetGuai;

    GameObject GetTarget()
    {
        if (!_guais)
        {
            _guais = GlobalTools.FindObjByName(GlobalTag.GUAIS);
        }

        if (_guais.transform.childCount == 0) return null;
        GameObject guai = null;
        for (int i=0;i< _guais.transform.childCount; i++)
        {
            guai = _guais.transform.GetChild(i).gameObject;
            print("guai:   "+guai.name);
            if (guai != null&& !guai.GetComponent<G_Feixingqi>().IsBeCheck&&this.transform.position.x<guai.transform.position.x)
            {
                guai.GetComponent<G_Feixingqi>().IsBeCheck = true;
                return guai;
            }
        }

        //随机 获取目标
        guai = _guais.transform.GetChild(GlobalTools.GetRandomNum(_guais.transform.childCount)).gameObject;
        if (guai.transform.position.x < this.transform.position.x) guai = null;
        return guai;
    }


    float tuiliY = 4;
    protected override void GenzongTiaozheng()
    {
        if (_targetGuai == null) return;


        if (Mathf.Abs(this.transform.position.x - _targetGuai.transform.position.x)<=0.7f)
        {
            //MaxSpeedX += (_targetGuai.GetComponent<Rigidbody2D>().velocity.x-this.GetComponent<Rigidbody2D>().velocity.x)*0.2f;
            MaxSpeedX = _targetGuai.GetComponent<Rigidbody2D>().velocity.x + 3;
        }
        else
        {
            //MaxSpeedX += (_YuanshiMaxSpeedX-this.GetComponent<Rigidbody2D>().velocity.x)*0.2f;
            MaxSpeedX = _YuanshiMaxSpeedX;
        }



        if (Mathf.Abs(this.transform.position.y - _targetGuai.transform.position.y) <= 0.2f)
        {
            SpeedY = 0;
            return;
        }





        SpeedY = (_targetGuai.transform.position.y - this.transform.position.y) * 0.94f;



        //if (_targetGuai.transform.position.y > this.transform.position.y)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, tuiliY));
        //}
        //else
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -tuiliY));
        //}
        //SpeedY = GetComponent<Rigidbody2D>().velocity.y;
    }

    protected override void GetStart()
    {
        if (!GetDown()) return;


        if (!IsStart)
        {
            IsStart = true;
            TX_Tuijin1.gameObject.SetActive(true);
            TX_Tuijin1.Simulate(0.0f);
            TX_Tuijin1.Stop();
            TX_Tuijin1.Play();
            Audio_DDFashe.Play();
            _targetGuai = GetTarget();
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else
        {
            ZibaoJishi();
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Tuili, 0));
            GenzongTiaozheng();
            //print("------------->  "+GetComponent<Rigidbody2D>().velocity);
            if (GetComponent<Rigidbody2D>().velocity.x > MaxSpeedX)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(MaxSpeedX, SpeedY);
            }
        }
    }


    protected override void ZibaoJishi()
    {
        //base.ZibaoJishi();
        //print("  _player  "+ _player);

        if (IsZibaoJishi())
        {
            Boom();
            RemoveSelf();
        }

        //print("_targetGuai     "+ _targetGuai.name);

        if(_targetGuai) print("  yyyy  "+this.transform.position.y+"   ----mubiaoY  "+ _targetGuai.transform.position.y);


        if (_targetGuai && this.transform.position.x > _targetGuai.transform.position.x + 16)
        {
            Boom();
            RemoveSelf();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag);
        if (Coll.tag == GlobalTag.Diren || Coll.tag == GlobalTag.DIBAN || Coll.tag == "zidan")
        {
            //生成爆炸
            HitObj();
            if (Coll.tag == GlobalTag.Diren)
            {
                print("击中 敌人   "+ Coll.GetComponent<RoleDate>().live);
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
}
