using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKuai : MonoBehaviour {

    public int teamNum;
    // Use this for initialization
    void Start() {
        //print(this.transform);
    }


    //GameObject的位置  攻击属性  相对位置x y  尺寸   是否立即消失

    private void OnEnable()
    {
        //print("???");
        //_isCanHit = true;
    }

    // Update is called once per frame
    void Update() {

    }

    GameObject atkObj;

    RoleDate roleDate;
    GameBody gameBody;
    Rigidbody2D _rigidbody2D;
    

    GameObject txObj;
    public void GetTXObj(GameObject txObj,bool isSkill = false){
        if (txObj != null)
        {
            this.txObj = txObj;
        }
        else
        {
            this.txObj = this.transform.parent.gameObject;
            print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"+this.name+"     "+this.txObj);
        }
        if(!isSkill)StartCoroutine(ObjectPools.GetInstance().IEDestory2(this.gameObject));
    }



    void OnTriggerEnter2D(Collider2D Coll)
    {

        gameBody = Coll.GetComponent<GameBody>();
        roleDate = Coll.GetComponent<RoleDate>();
        _rigidbody2D = Coll.GetComponent<Rigidbody2D>();
        if(this.txObj == null) this.txObj = this.transform.parent.gameObject;
        atkObj = txObj.GetComponent<JN_base>().atkObj;
        //print(atkObj.name);
        

        JN_Date jn_date = txObj.GetComponent<JN_Date>();

        if (roleDate != null&& roleDate.team != jn_date.team)
        {
            //print("击中的2Dbox  "+Coll.GetComponent<BoxCollider2D>().transform.position);
            
            if (roleDate.isDie) return;
            if (!roleDate.isCanBeHit) return;
            //取到施展攻击角色的方向
            float _roleScaleX = -atkObj.transform.localScale.x;


            //这个已经不需要了 
            //if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().GetBeHit(jn_date, _roleScaleX);
            GetBeHit(jn_date, _roleScaleX);
            //力作用  这个可以防止 推力重叠 导致人物飞出去
            Vector3 tempV3 = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector3(0,tempV3.y, tempV3.z);

            if (jn_date != null &&gameBody != null)
            {

                //gameBody.GetPause(0.2f);
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

            atkObj.GetComponent<GameBody>().GetPause();


            //判断作用力与反作用力  硬直判断

            //
            //Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));

            //如果是碰撞就消失 调用消失方法
            if (jn_date != null && jn_date._type == "3")
            {
                //if (gameObject) ObjectPools.GetInstance().DestoryObject2(gameObject);     
                txObj.GetComponent<JN_base>().DisObj();
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
        if (!roleDate.isCanBeHit) return;
        float addxue = atkObj.GetComponent<RoleDate>().atk+ jn_date.atkPower - roleDate.def;
        addxue = addxue > 0 ? addxue : 1;
        roleDate.live -= addxue;
        if (roleDate.live < 0) roleDate.live = 0;
        //print("live "+ roleDate.live);
        
        //判断是否在躲避阶段  无法被攻击
        //判断击中特效播放位置
        //击退 判断方向
        float _psScaleX = sx;
        //判断是否在空中
        //挨打动作  判断是否破硬直
        //判断是否生命被打空
        HitTX(_psScaleX, "BloodSplatCritical2D1");
        HitTX(_psScaleX,"jizhong",roleDate.beHitVudio);
    }

    //击中特效
    void HitTX(float psScaleX,string txName,string hitVudio = "")
    {
        GameObject hitTx = Resources.Load(txName) as GameObject;
        hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);
        hitTx.transform.position = gameBody.transform.position;
        hitTx.transform.localScale = new Vector3(1, 1, psScaleX);
        if (hitVudio != "")
        {
            hitTx.GetComponent<JZ_audio>().PlayAudio(hitVudio);
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
