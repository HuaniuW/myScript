using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Du_wu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("毒雾 毒伤害")]
    public float DuShanghai = 20;
    float CXShijian = 6;


    private void OnTriggerStay2D(Collider2D Coll)
    {
        //print(" coll  "+Coll.name);
        if(Coll.tag == GlobalTag.Player)
        {
            if (!Coll.GetComponent<ChiXuShangHai>().IsDu)
            {
                Coll.GetComponent<ChiXuShangHai>().InDu(DuShanghai, CXShijian);
            }


                //if (beHitObj != null && BeHitRoleDate.team != jn_date.team) beHitObj.GetComponent<ChiXuShangHai>().InDu(jn_date.DuChixuShanghai, jn_date.DuChixuShanghaiTime);
        }
    }





}
