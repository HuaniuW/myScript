using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_ZidanHengChong : TX_zidan
{
    [Header("横向发射 声音")]
    public AudioSource Audio_FasheHeng;


    // Start is called before the first frame update
    void Start()
    {
       
    }


    private void OnEnable()
    {
        if (!_player) _player = GlobalTools.FindObjByName("player");
        //HitKuai
        //获取角速度
        isFaShe = true;
        testN = 0;

        IsCanHit = false;
        ZidanUp();
        StartTimes = 0.5f + GlobalTools.GetRandomDistanceNums(0.5f);

       
    }




    // Update is called once per frame
    void Update()
    {
        if (IsStartTimes)
        {
            StartTimesJishi += Time.deltaTime;
            if(StartTimesJishi>= StartTimes)
            {
                StartTimesJishi = 0;
                IsStartTimes = false;
                fire();
            }
        }
    }

    public override void ResetAll()
    {
        //testN = 0;
        _isFSByFX = false;
        boomJishi = 0;
        StartTimesJishi = 0;
        IsStartTimes = false;
    }



    //bool IsGetUp = false;
    //升起
    void ZidanUp()
    {
        //给一个 上升的 速度
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1+GlobalTools.GetRandomDistanceNums(3));

        IsStartTimes = true;

    }


    bool IsStartTimes = false;
    float StartTimes = 0.5f;
    float StartTimesJishi = 0;

    protected override void fire()
    {
        if (_player && isFaShe)
        {
            isFaShe = false;
            IsCanHit = true;
            //print("----------------------------------------->>  fire!!!!! " + speeds);

            Vector2 _speed = Vector2.zero;

            if (_player.transform.position.x > this.transform.position.x)
            {
                _speed = new Vector2(speeds,0);
            }
            else
            {
                _speed = new Vector2(-speeds, 0);
            }


            GetComponent<Rigidbody2D>().velocity = _speed;


            if (Audio_FasheHeng)
            {
                Audio_FasheHeng.Play();
            }

            //print("   sudu   "+ GetComponent<Rigidbody2D>().velocity);
        }
    }

}
