using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_XiazaiZidan : TX_zidan
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    [Header("横向发射 声音")]
    public AudioSource Audio_Fashe;




    // Update is called once per frame
    void Update()
    {
        StartXiazaiTime();
    }


    float XiazaiTimeYanchi = 0;
    float XiazaiJishi = 0;
    public void SetZidanXiazaTime(float _xiazaiTimeYanchi)
    {
        XiazaiTimeYanchi = _xiazaiTimeYanchi;
        IsStartXiazai = true;
    }


    bool IsStartXiazai = false;

    

    void StartXiazaiTime()
    {
        if (!IsStartXiazai) return;

        XiazaiJishi += Time.deltaTime;
        if (XiazaiJishi>= XiazaiTimeYanchi)
        {
            XiazaiJishi = 0;
            IsStartXiazai = false;
            fire();
        }


    }

    public override void ResetAll()
    {
        //testN = 0;
        _isFSByFX = false;
        boomJishi = 0;
        XiazaiJishi = 0;
        IsStartXiazai = false;
    }

    protected override void fire()
    {
        if (_player && isFaShe)
        {
            isFaShe = false;
            IsCanHit = true;

            Vector2 _speed = new Vector2(0,-speeds);

            GetComponent<Rigidbody2D>().velocity = _speed;


            if (Audio_Fashe)
            {
                Audio_Fashe.Play();
            }

            //print("   sudu   "+ GetComponent<Rigidbody2D>().velocity);
        }
    }

}
