using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKuai2 : MonoBehaviour
{
    public bool IsPlayAudio = true;

    public int teamNum;
    // Use this for initialization
    void Start()
    {
        //print(this.transform);
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


    void OnTriggerEnter2D(Collider2D Coll)
    {

        gameBody = Coll.GetComponent<GameBody>();
        roleDate = Coll.GetComponent<RoleDate>();
        _rigidbody2D = Coll.GetComponent<Rigidbody2D>();

        //atkObj = txObj.GetComponent<JN_base>().atkObj;
        //print(atkObj.name);


        JN_Date jn_date = GetComponent<JN_Date>();

        if (roleDate != null && roleDate.team != jn_date.team)
        {
            //print("击中的2Dbox  "+Coll.GetComponent<BoxCollider2D>().transform.position);

            if (roleDate.isDie) return;
            //if (!roleDate.isCanBeHit) return;
            //取到施展攻击角色的方向
            //float _roleScaleX = -atkObj.transform.localScale.x;


            //这个已经不需要了 
            //if (Coll.GetComponent<BeHit>()) Coll.GetComponent<BeHit>().GetBeHit(jn_date, _roleScaleX);

            if(atkScaleX == 0)
            {
                //if (Coll.transform.position.x < this.transform.position.x)
                //print(Coll.transform.position.x + "  ---  " + this.transform.parent.transform.position.x);
                if(this.transform.parent)atkScaleX = Coll.transform.position.x < this.transform.parent.transform.position.x ? -1 : 1;
            }


            GetBeHit(jn_date, atkScaleX);
            //力作用  这个可以防止 推力重叠 导致任务飞出去
            Vector3 tempV3 = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector3(0, tempV3.y, tempV3.z);

            if (jn_date != null && gameBody != null)
            {
                gameBody.HasBeHit();
                Coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(400 * atkScaleX, 0));
            }
            
        }
    }


    public void GetBeHit(JN_Date jn_date, float sx)
    {
        if (roleDate.isDie) return;
        //if (!roleDate.isCanBeHit) return;
        print("hei woshi ci!!!!!!!!!!!!!!!!!!!!!!!!!   ");
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
        HitTX(_psScaleX, "BloodSplatCritical2D1");
        HitTX(_psScaleX, "jizhong", roleDate.beHitVudio);
    }

    //击中特效
    void HitTX(float psScaleX, string txName, string hitVudio = "")
    {
        GameObject hitTx = Resources.Load(txName) as GameObject;
        hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);
        hitTx.transform.position = gameBody.transform.position;
        hitTx.transform.localScale = new Vector3(1, 1, psScaleX);
        if (IsPlayAudio && hitVudio != "")
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
