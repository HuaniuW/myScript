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

    GameObject atkObj;
   

    void OnTriggerEnter2D(Collider2D Coll)
    {
        atkObj = gameObject.transform.parent.GetComponent<JN_base>().atkObj;
        //print("w " + _atkVVo._scaleW);
        //print("name  " + gameObject.name);
        //this.transform.position = this.transform.parent.transform.position;
        //print(Coll.name);
        //print("   _atkVVo.team   " + (_atkVVo == null));
        //print(Coll.name+"   team  "+ Coll.GetComponent<RoleDate>().team+ "   _atkVVo.team   "+ _atkVVo.team);

        JN_Date jn_date = gameObject.transform.parent.GetComponent<JN_Date>();

        if (Coll.GetComponent<RoleDate>()!=null&& Coll.GetComponent<RoleDate>().team != jn_date.team)
        {
            //print("击中的2Dbox  "+Coll.GetComponent<BoxCollider2D>().transform.position);
            float _roleScaleX = gameObject.transform.parent.transform.localScale.x;
            if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().GetBeHit(jn_date, _roleScaleX);
            //力作用
            //Coll.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //判断作用力与反作用力  硬直判断

            //atkObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100 * _roleScaleX, 0));
            //Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(300* _roleScaleX, 0));
            //如果是碰撞就消失 调用消失方法
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
