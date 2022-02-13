using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_SDaodan : DD_Base
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    

    protected override void GetEnable()
    {
        MaxSpeedX = 110;
        chushiY = this.transform.position.y;
    }

    float chushiY = 0;
    float _Fy = 6;
    float _pianyiY = 3;

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
            
            if(this.transform.position.y>= chushiY+ _pianyiY)
            {
                _Fy = -6;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y*0.95f);
            }
            else if (this.transform.position.y <= chushiY - _pianyiY)
            {
                _Fy = 6;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y * 0.95f);
            }
            

            GetComponent<Rigidbody2D>().AddForce(new Vector2(Tuili, _Fy));
            //print("------------->  "+GetComponent<Rigidbody2D>().velocity);
            if (GetComponent<Rigidbody2D>().velocity.x > MaxSpeedX)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(MaxSpeedX, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    protected override void ZibaoJishi()
    {
        base.ZibaoJishi();
        //print("  _player  "+ _player);
        if(_player &&this.transform.position.x> _player.transform.position.x + 16)
        {
            Boom();
            RemoveSelf();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);
        }
    }


}
