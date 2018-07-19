using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKuai : MonoBehaviour {

    public int teamNum;
	// Use this for initialization
	void Start () {
        //print(this.transform);
	}


    //GameObject的位置  攻击属性  相对位置x y  尺寸   是否立即消失
   
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("w " + _atkVVo._scaleW);
        //print("name  " + gameObject.name);
        //this.transform.position = this.transform.parent.transform.position;
        //print(Coll.name);
        //print("   _atkVVo.team   " + (_atkVVo == null));
        //print(Coll.name+"   team  "+ Coll.GetComponent<RoleDate>().team+ "   _atkVVo.team   "+ _atkVVo.team);

        JN_Date jn_date = gameObject.transform.parent.GetComponent<JN_Date>();

        if (Coll.GetComponent<RoleDate>()!=null&& Coll.GetComponent<RoleDate>().team != jn_date.team)
        {
            if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().GetBeHit(jn_date);
            if (jn_date != null && jn_date._type == "3")
            {
                //if (gameObject) ObjectPools.GetInstance().DestoryObject2(gameObject);     
                transform.parent.GetComponent<JN_base>().DisObj();
            }
        }

       
        
        
        
    }
    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");
    }
}
