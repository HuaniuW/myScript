using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKuai2 : MonoBehaviour
{
    public bool IsPlayAudio = true;

    public int teamNum;

    [Header("是否能被躲避")]
    public bool IsCanBeDodge = false;
    // Use this for initialization
    void Start()
    {
        //print(this.transform);
        //print("？？？？？？？？？？？？？------------------" + this.transform.parent);
    }


    //GameObject的位置  攻击属性  相对位置x y  尺寸   是否立即消失

    private void OnEnable()
    {
        //print("???");
        //_isCanHit = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //GameObject atkObj;

    RoleDate roleDate;
    GameBody gameBody;
    Rigidbody2D _rigidbody2D;


    GameObject txObj;
    public void GetTXObj(GameObject txObj)
    {
        this.txObj = txObj;
        StartCoroutine(ObjectPools.GetInstance().IEDestory2(this.gameObject));
    }

    public float atkScaleX = 1;

    [Header("是否能 击中boss")]
    public bool IsCanHitBoss = true;


    void OnTriggerEnter2D(Collider2D Coll)
    {
        


        gameBody = Coll.GetComponent<GameBody>();
        roleDate = Coll.GetComponent<RoleDate>();
        _rigidbody2D = Coll.GetComponent<Rigidbody2D>();

        //atkObj = txObj.GetComponent<JN_base>().atkObj;
        //print(atkObj.name);

        print(" ---coll name "+Coll.name+"    roledate "+roleDate);

        if (roleDate&&!IsCanHitBoss&& roleDate.enemyType == "boss")
        {
            return;
        }

        JN_Date jn_date = GetComponent<JN_Date>();

        if (roleDate != null && roleDate.team != jn_date.team)
        {
            //print("击中的2Dbox  "+Coll.GetComponent<BoxCollider2D>().transform.position);

            if (roleDate.isDie) return;
            //if (!roleDate.isCanBeHit) return;
            //取到施展攻击角色的方向
            //float _roleScaleX = -atkObj.transform.localScale.x;


            if (IsCanBeDodge)
            {
                //print("roleDate  " + roleDate);
                //print(" isCanbeihit  " + roleDate.isCanBeHit);
                if (!roleDate.isCanBeHit) return;
            }

            //这个已经不需要了 
            //if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().GetBeHit(jn_date, _roleScaleX);


            //print("   这里是否进来  " + Coll.transform.position.x + "  -------    " + this.transform.parent.transform.transform);
            if (this.transform.parent) atkScaleX = Coll.transform.position.x < this.transform.parent.transform.position.x ? -1 : 1;
            GetBeHit(jn_date, atkScaleX);
            //力作用  这个可以防止 推力重叠 导致任务飞出去
            Vector3 tempV3 = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector3(0, tempV3.y, tempV3.z);

            if (jn_date != null && gameBody != null)
            {
                gameBody.HasBeHit();

                if (this.transform.parent == null) return;
                if (Coll.transform.position.x > this.transform.parent.transform.position.x)
                {
                    //Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(400 , 0));
                    Coll.GetComponent<GameBody>().GetZongTuili(new Vector2(400, 0));
                }
                else
                {
                    //Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400 , 0));
                    Coll.GetComponent<GameBody>().GetZongTuili(new Vector2(-400, 0));
                }
                
            }
            
        }
    }


    public void GetBeHit(JN_Date jn_date, float sx)
    {
        if (roleDate.isDie) return;
        //if (!roleDate.isCanBeHit) return;
        float addxue = jn_date.atkPower - roleDate.def;
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

        if (!IsPlayAudio) return;
        //HitTX(_psScaleX, "BloodSplatCritical2D1");
        //HitTX(_psScaleX, "jizhong", roleDate.beHitVudio);

        HitTX(_psScaleX, "BloodSplatCritical3", "", 2, false, false);
        if (jn_date.HitInSpecialEffectsType != 3) HitTX(_psScaleX, "jizhong", roleDate.beHitVudio, 4, true, true);
    }
    //JN_Date jn_date;

    void HitTX(float psScaleX, string txName, string hitVudio = "", float beishu = 3, bool isSJJD = false, bool isZX = true, float hy = 0)
    {
        //print("hy ------------------------------------------------------>     "+hy);

        GameObject hitTx = Resources.Load(txName) as GameObject;
        hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);

        //print(hitTx.name + "  1---------------------->>   " + hitTx.transform.localEulerAngles);
        //print("sudu-------------------------------------------   "+ gameBody.GetComponent<Rigidbody2D>().velocity.x);
        hitTx.transform.position = new Vector3(gameBody.transform.position.x - hy * psScaleX, gameBody.transform.position.y, gameBody.transform.position.z);
        //击中特效缩放
        hitTx.transform.localScale = new Vector3(beishu, beishu, beishu);

        //print(hitTx.transform.localEulerAngles + " 000000000000000000000000000000000 "+hitTx.transform.name);
        hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, hitTx.transform.localEulerAngles.y * psScaleX, hitTx.transform.localEulerAngles.z);

        //print("hitTx.transform.localEulerAngles    "+ hitTx.transform.localEulerAngles+ "   psScaleX  "+ psScaleX);
        if (IsPlayAudio && hitVudio != "")
        {
            hitTx.GetComponent<JZ_audio>().PlayAudio(hitVudio);
        }

        //if (!isZX)
        //{
        //    //if(atkObj.transform.position.y-)
        //    if (jn_date.isAtkZongXiang)
        //    {
        //        hitTx.transform.localEulerAngles = new Vector3(-90, hitTx.transform.localEulerAngles.y * psScaleX, hitTx.transform.localEulerAngles.z);
        //        hitTx.transform.position = new Vector3(gameBody.transform.position.x - hy * psScaleX - 0.5f, gameBody.transform.position.y, gameBody.transform.position.z);
        //    }
        //    else
        //    {
        //        hitTx.transform.localEulerAngles = new Vector3(hitTx.transform.localEulerAngles.x, hitTx.transform.localEulerAngles.y * psScaleX, hitTx.transform.localEulerAngles.z);
        //    }

        //}
    }


    //击中特效
    //void HitTX(float psScaleX, string txName, string hitVudio = "")
    //{
    //    GameObject hitTx = Resources.Load(txName) as GameObject;
    //    hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);
    //    hitTx.transform.position = gameBody.transform.position;
    //    hitTx.transform.localScale = new Vector3(1, 1, psScaleX);
    //    if (IsPlayAudio && hitVudio != "")
    //    {
    //        hitTx.GetComponent<JZ_audio>().PlayAudio(hitVudio);
    //    }
    //}



    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");
    }
}
