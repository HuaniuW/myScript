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

    RoleDate roleDate;
    GameBody gameBody;


    void OnTriggerEnter2D(Collider2D Coll)
    {

        gameBody = Coll.GetComponent<GameBody>();
        roleDate = Coll.GetComponent<RoleDate>();
        if (!roleDate.isCanBeHit) return;

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
            
            if (Coll.GetComponent<RoleDate>().isDie) return;
            float _roleScaleX = gameObject.transform.parent.transform.localScale.x;


            //这个已经不需要了 
            //if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().GetBeHit(jn_date, _roleScaleX);
            GetBeHit(jn_date, _roleScaleX);
            //力作用  这个可以防止 推力重叠 导致任务飞出去
            Vector3 tempV3 = Coll.GetComponent<Rigidbody2D>().velocity;
            Coll.GetComponent<Rigidbody2D>().velocity = new Vector3(0,tempV3.y, tempV3.z);

            if (jn_date != null &&gameBody != null)
            {
                //判断是否破防   D 代办事项 
                if (jn_date.atkPower - roleDate.yingzhi > roleDate.yingzhi * 0.5)
                {
                    gameBody.HasBeHit();
                    Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(400 * _roleScaleX, 0));
                }
                else if (jn_date.atkPower - roleDate.yingzhi > 0)
                {
                    gameBody.HasBeHit();
                    Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(300 * _roleScaleX, 0));
                    if (atkObj&& jn_date._type == "1")
                    {
                        atkObjV3Zero();
                        atkObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300 * _roleScaleX, 0));
                    }
                }
                else
                {
                    if (atkObj && jn_date._type == "1")
                    {
                        atkObjV3Zero();
                        atkObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500 * _roleScaleX, 0));
                    }
                }

            }

          
            //判断作用力与反作用力  硬直判断

            //
            //Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            
            //如果是碰撞就消失 调用消失方法
            if (jn_date != null && jn_date._type == "3")
            {
                //if (gameObject) ObjectPools.GetInstance().DestoryObject2(gameObject);     
                transform.parent.GetComponent<JN_base>().DisObj();
            }

        }
    }

    //攻击者X方向速度清0
    void atkObjV3Zero()
    {
        Vector3 v3 = atkObj.GetComponent<Rigidbody2D>().velocity;
        atkObj.GetComponent<Rigidbody2D>().velocity = new Vector3(0, v3.y, v3.z);
    }

  

    public void GetBeHit(JN_Date jn_date, float sx)
    {
        //print("被击中 !! "+this.transform.name+"   攻击力 "+ jn_date.atkPower+"  我的防御力 "+this.GetComponent<RoleDate>().def);


       
        if (roleDate.isDie) return;

        float addxue = jn_date.atkPower - roleDate.def;
        addxue = addxue > 0 ? addxue : 1;
        roleDate.live -= addxue;
        if (roleDate.live < 0) roleDate.live = 0;
        //print("live "+ roleDate.live);
        if (!roleDate.isCanBeHit) return;

       




        //判断是否在躲避阶段  无法被攻击
        //判断击中特效播放位置
        //击退 判断方向
        float _psScaleX = sx;
        //判断是否在空中
        //挨打动作  判断是否破硬直
        //判断是否生命被打空
        Bloods(_psScaleX);
    }

    //击中特效
    void Bloods(float psScaleX)
    {
        //print("fx:   "+psScaleX);
        GameObject blood = Resources.Load("BloodSplatCritical2D1") as GameObject;
        blood = ObjectPools.GetInstance().SwpanObject2(blood);
        blood.transform.position = this.transform.position;
        blood.transform.localScale = new Vector3(1, 1, psScaleX);

        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(blood, 0.5f));

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
