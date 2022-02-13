using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zidan_Player : ZIdan_Jijia
{


    protected override void GetEnable()
    {
        MaxSpeedX = 130;
    }


    protected override void ZibaoJishi()
    {
        ZihuiJishi += Time.deltaTime;
        if (ZihuiJishi >= ZibaoTimes)
        {
            RemoveSelf();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag);
        if (Coll.tag == GlobalTag.DIBAN|| Coll.tag == GlobalTag.Diren)
        {
            HitObj();
            if (Coll.tag == GlobalTag.Diren&& Coll.GetComponent<RoleDate>())
            {
                Coll.GetComponent<RoleDate>().live -= GetComponent<JN_Date>().atkPower;
               
            }
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
