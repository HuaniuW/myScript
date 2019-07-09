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
            //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"+this.name+"     "+this.txObj);
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
            Vector2 tempV3 = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector3(0,tempV3.y);
            if (jn_date != null &&gameBody != null)
            {
                ObjV3Zero(Coll.gameObject);
                //gameBody.GetPause(0.2f);
                //判断是否破防   D 代办事项 
                if (jn_date.atkPower - roleDate.yingzhi > roleDate.yingzhi * 0.5)
                {
                    //atkObjV3Zero(Coll.gameObject);
                    
                    Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(jn_date.chongjili * _roleScaleX, 0));
                    //if(Coll.tag!="Player") print(Coll.GetComponent<Rigidbody2D>().velocity.x);
                    //print(Coll.tag);
                    gameBody.HasBeHit(jn_date.chongjili);
                }
                else if (jn_date.atkPower - roleDate.yingzhi > 0)
                {
                    //atkObjV3Zero(Coll.gameObject);
                   
                    Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(jn_date.chongjili * _roleScaleX-100, 0));
                    gameBody.HasBeHit(jn_date.chongjili);
                    if (atkObj&& jn_date._type == "1")
                    {
                        ObjV3Zero(atkObj);
                        atkObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200 * _roleScaleX, 0));
                    }
                }
                else
                {
                    if (atkObj && jn_date._type == "1")
                    {
                        ObjV3Zero(atkObj);
                        atkObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300 * _roleScaleX, 0));
                    }
                }

				gameBody.GetPause(jn_date.yingzhishijian);

                //if (Coll.tag != "Player")

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

    //X方向速度清0
    void ObjV3Zero(GameObject obj)
    {
        Vector3 v3 = obj.GetComponent<Rigidbody2D>().velocity;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, v3.y);
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
        HitTX(_psScaleX, "BloodSplatCritical2D1","",2,false,false);
        HitTX(_psScaleX,"jizhong",roleDate.beHitVudio,3,true);
    }

    /// <summary>
    /// 特效
    /// </summary>
    /// <param name="psScaleX">角色的方向</param>
    /// <param name="txName">特效名字（动态取预制资源）</param>
    /// <param name="hitVudio">播放特效声音</param>
    /// <param name="isSJJD">是否随机角度</param>
    /// <param name="isZX">是否需要转向</param>
    void HitTX(float psScaleX,string txName,string hitVudio = "",float beishu = 3,bool isSJJD = false,bool isZX = true)
    {
        GameObject hitTx = Resources.Load(txName) as GameObject;
        hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);
        hitTx.transform.position = gameBody.transform.position;
        //击中特效缩放
        hitTx.transform.localScale = new Vector3(beishu, beishu, 1);

        float jd = 0;
       
        if (hitVudio != "")
        {
            hitTx.GetComponent<JZ_audio>().PlayAudio(hitVudio);
        }
        //特效方向 
        if (!isZX) return;
        if (psScaleX>0)
        {
            if(isSJJD)jd = Random.Range(-10, -15);
            hitTx.transform.localEulerAngles = new Vector3(0, 0, jd);
        }
        else
        {
            if (isSJJD) jd = Random.Range(0, -5);
            hitTx.transform.localEulerAngles = new Vector3(0, 144, jd);
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
