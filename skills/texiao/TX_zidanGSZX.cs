using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_zidanGSZX : TX_zidan
{
    //高速直线子弹
    protected override void fire()
    {
        if (_player && isFaShe)
        {
            isFaShe = false;
            //print("----------------------------------------->>  fire!!!!! " + speeds);
            Vector2 v2 = Vector2.zero;
            if (GetComponent<JN_base>().atkObj.transform.localScale.x > 0)
            {
                v2 = new Vector2(-this.speeds, 0);
            }
            else
            {
                v2 = new Vector2(this.speeds, 0);
            }

            GetComponent<Rigidbody2D>().velocity = v2;
            //print("   sudu   " + GetComponent<Rigidbody2D>().velocity);
        }


        //base.fire();
      

    }
}
