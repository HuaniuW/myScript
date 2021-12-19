using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_Diu : MonoBehaviour
{
    //丢出去的 物品  技能道具等


    // Start is called before the first frame update
    void Start()
    {
        
    }


    //获取 攻击方的 atkObj
    public GameObject AtkObj;
    public void GetAtkObj(GameObject obj)
    {
        AtkObj = obj;
    }


    private void OnEnable()
    {
        jishiNums = 0;
        IsHasBaozha = false;
    }

    [Header("***********敌我类型 分类")]
    public int IntType = 1;

    //碰撞 地面 敌人
    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("   碰撞》》》》》》》》 "+Coll.tag);
        if (IntType == 1)
        {
            if (!IsHasBaozha && (Coll.tag == "diban" || Coll.tag == "diren" || Coll.tag == "boss"))
            {
                IsHasBaozha = true;
                print("  发生碰撞 " + Coll.tag);
                Baozha();
                Removeself();
            }
        }
        else if(IntType == 2)
        {
            if(!IsHasBaozha && Coll.tag == GlobalTag.ZIDANDUAN)
            {
                Vector2 v2 = this.GetComponent<Rigidbody2D>().velocity;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-v2.x, v2.y);
                return;
            }


            if (!IsHasBaozha && (Coll.tag == "diban" || Coll.tag == GlobalTag.Player))
            {
                IsHasBaozha = true;
                print("  发生碰撞 " + Coll.tag);
                Baozha();
                Removeself();
            }
        }
       

    }

    [Header("主动计时爆炸 是0的话就不计时")]
    public float JishiBaozhaNums = 0;
    float jishiNums = 0;
    bool IsHasBaozha = false;
    protected void JishiBaozha()
    {
        if (IsHasBaozha||JishiBaozhaNums == 0) return;
        jishiNums += Time.deltaTime;
        if (jishiNums>= JishiBaozhaNums)
        {
            jishiNums = 0;
            IsHasBaozha = true;
            print("   计时爆炸！！！！！！！！  "+this.gameObject.name);
            Baozha();
            Removeself();
        }
    }

    [Header("丢出去爆炸物的 名字")]
    public string BaozhaName = "TX_zidan11_bz";
    //生成 爆炸特效
    protected void Baozha()
    {
        //GameObject obj = GlobalTools.GetGameObjectInObjPoolByName(BaozhaName);
        GameObject obj = GlobalTools.GetGameObjectByName(BaozhaName);
        //print("  obj "+obj.name);
        //注意 命名后 再存入 对象池 避免出现数字 id bug
        obj.name = BaozhaName;
        print(" name "+obj.name+"  pos  "+obj.transform.position+"  xianshi "+ obj.gameObject.activeSelf+"   --thisPos  "+this.transform.position);
        //obj.GetComponent<TX_removeSelf>().GetStart();
        obj.gameObject.SetActive(true);
        if (obj == null) return;
        //obj.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y+2);
        obj.transform.position = this.gameObject.transform.position;
        obj.transform.parent = this.gameObject.transform.parent;
        //obj.GetComponent<JN_base>().GetAtkObjIn(AtkObj);
        //obj.GetComponent<ParticleSystem>().Stop();
        obj.GetComponent<ParticleSystem>().Simulate(0,true);
        
        obj.GetComponent<ParticleSystem>().Play();
        //print("   ---->   粒子数  "+obj.GetComponent<ParticleSystem>().particleCount);
        jishiNums = 0;
        IsHasBaozha = true;
    }


    //移除自身
    public void Removeself()
    {
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 0));
    }




    // Update is called once per frame
    void Update()
    {
        JishiBaozha();
    }
}
