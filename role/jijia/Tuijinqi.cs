using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuijinqi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("推进器 编号")]
    public int TJQNums = 1;

    protected virtual void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag);
        if ( Coll.tag == "zidan" || Coll.tag == GlobalTag.JIGUANG)
        {
           
            if(Coll.tag == GlobalTag.JIGUANG)
            {
                //this.GetComponent<RoleDate>().live -= 400;
            }
            else
            {
                //this.GetComponent<RoleDate>().live -= 100;
            }

            print(" 推进器  被攻击 -----> " + Coll.tag+"   剩余血量  "+ this.GetComponent<RoleDate>().live);
            if (this.GetComponent<RoleDate>().live<=0)
            {
                //移除 自身
                //爆炸 震动
                this.transform.parent.GetComponent<B_sanjiaoYi>().TuijinqiBoomByNum(TJQNums);
                this.gameObject.SetActive(false);
            }



        }
    }


}
