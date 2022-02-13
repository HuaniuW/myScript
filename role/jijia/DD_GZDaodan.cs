using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_GZDaodan : DD_Base
{

    protected float _YuanshiMaxSpeedX = 100;

    public ParticleSystem TX_ShangPenhuo;
    public ParticleSystem TX_XiaPenhuo;

    protected override void GetEnable()
    {
        _YuanshiMaxSpeedX = MaxSpeedX;
    }

    protected virtual void GenzongTiaozheng()
    {
        if (Mathf.Abs(this.transform.position.x - _player.transform.position.x) <= 0.7f)
        {
            //MaxSpeedX += (_targetGuai.GetComponent<Rigidbody2D>().velocity.x - this.GetComponent<Rigidbody2D>().velocity.x) * 0.2f;
            //MaxSpeedX += (_player.GetComponent<Rigidbody2D>().velocity.x + 10 - MaxSpeedX) * 0.6f;
            MaxSpeedX = _player.GetComponent<Rigidbody2D>().velocity.x + 3;
        }
        else
        {
            //MaxSpeedX += (_YuanshiMaxSpeedX-this.GetComponent<Rigidbody2D>().velocity.x)*0.2f;
            MaxSpeedX = _YuanshiMaxSpeedX;
        }




        if (Mathf.Abs(this.transform.position.y - _player.transform.position.y)<=0.2f)
        {
            SpeedY = 0;
            return;
        }



        if (_player.transform.position.y > this.transform.position.y)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5));
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5));
        }
        SpeedY = GetComponent<Rigidbody2D>().velocity.y;
    }


    protected override void GetStart()
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
        base.ZibaoJishi();
        //print("  _player  "+ _player);
        if (_player && this.transform.position.x > _player.transform.position.x + 16)
        {
            Boom();
            RemoveSelf();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);
        }
    }


}
