using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuHitHuo : MonoBehaviour
{
    //毒遇到火
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    [Header("可点燃间隔时间")]
    public float CanFireJiangeTime = 0;
    float CanFireJishi = 0;
    void TheCanFireJishi()
    {
        if (CanFireJiangeTime == 0||!IsHasDianran) return;
        CanFireJishi += Time.deltaTime;
        if (CanFireJishi>= CanFireJiangeTime)
        {
            CanFireJishi = 0;
            IsHasDianran = false;
        }
    }
    

    void Update()
    {
        //if (IsYuhuo)
        //{
        //    jishi += Time.deltaTime;
        //    if (jishi >= times)
        //    {
        //        jishi = 0;
        //        IsYuhuo = false;
        //        ShowFire();
        //    }
        //}

        TheCanFireJishi();
        HitHuoChixuTime();
    }

    //bool IsYuhuo = false;
    //float jishi = 0;
    //float times = 0.05f;


    //private void OnParticleCollision(GameObject other)
    //{
    //    print("hy  hit "+other.tag);

    //    //print(this.name + "  IsCanHit  " + IsCanHit);
    //    if(other.tag == "Huoyan")
    //    {
    //        //1 生成 火焰  要根据 大小和 形状 来生成

    //        print(" hy-----------毒 碰到火焰了  ");

    //        //2 毒雾火焰 放在 子粒子---这个不行  没有火焰伤害了
    //    }
    //}


    private void OnEnable()
    {
        IsHasDianran = false;
        //jishi = 0;
        //IsYuhuo = false;


        if (HitHuoKuaiTimes != 0)
        {
            //print(" 11111  chuxian    " + GetComponent<CapsuleCollider2D>().enabled+ "  HitHuoKuaiJishiNums  "+ HitHuoKuaiJishiNums);
            if (QishiDuYanCanDianranYanchiTime == 0) {
                GetComponent<CapsuleCollider2D>().enabled = true;
            }
            else
            {
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
           
            HitHuoKuaiJishiNums = 0;
            IsQishiYanchiOver = false;
            qishiYanchiJishi = 0;
            //print("   chuxian    " + GetComponent<CapsuleCollider2D>().enabled + " IsHasDianran  " + IsHasDianran+ "   HitHuoKuaiJishiNums  "+ HitHuoKuaiJishiNums);
        }
    }



    [Header("起始 可以点燃 烟雾 延迟")]
    public float QishiDuYanCanDianranYanchiTime = 0;

    float qishiYanchiJishi = 0;
    bool IsQishiYanchiOver = false;




    [Header("火焰出现的 位置")]
    public List<Transform> HuoyanOutPosList;


    [Header("碰到火焰 块 持续时间")]
    public float HitHuoKuaiTimes = 0;

    
    float HitHuoKuaiJishiNums = -1;
    void HitHuoChixuTime()
    {
        if (!IsQishiYanchiOver&& QishiDuYanCanDianranYanchiTime!=0)
        {
            qishiYanchiJishi += Time.deltaTime;
            if(qishiYanchiJishi>= QishiDuYanCanDianranYanchiTime)
            {
                qishiYanchiJishi = 0;
                IsQishiYanchiOver = true;
                GetComponent<CapsuleCollider2D>().enabled = true;
            }
            return;
        }



        if (HitHuoKuaiTimes == 0|| HitHuoKuaiJishiNums ==-1) return;
        HitHuoKuaiJishiNums += Time.deltaTime;
        if(HitHuoKuaiJishiNums>= HitHuoKuaiTimes)
        {
            HitHuoKuaiJishiNums = -1;
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }


    [Header("被点燃 显示的 出现的火焰")]
    public string Huoyan = "TX_DianranHuoXiaoGuo";

    bool IsHasDianran = false;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print(" pengdao:  "+Coll.name+"  毒遇到火  碰撞    " + Coll.tag);
        if (Coll.tag == GlobalTag.HUOYAN|| Coll.tag == GlobalTag.DIAN)
        {
            //生成 火焰  火焰持续时间 和伤害？  如果没有打到敌人 和打到敌人
            //生成火效果 如果烧到敌人 就点燃  否则就不点燃 不显示持续火效果

            //IsYuhuo = true;

            //print("  dr------  "+Coll.gameObject.transform.parent.GetComponent<JN_base>().atkObj);
            Transform _hitKuaiObj = Coll.gameObject.transform.parent;
            if (!IsHasDianran && _hitKuaiObj && _hitKuaiObj.GetComponent<JN_Date>() && _hitKuaiObj.GetComponent<JN_Date>().fasntuili != 0)
            {
                //print("#########??????");

                GameObject atkObj = Coll.gameObject.transform.parent.GetComponent<JN_base>().atkObj;

                atkObj.GetComponent<GameBody>().SetYSpeedZero();
                //打到鱼 给反作用力16  不然很多怪一只在天上打 就能打死
                Rigidbody2D _atkRigidbody2D = atkObj.GetComponent<GameBody>().GetPlayerRigidbody2D();
                _atkRigidbody2D.velocity = new Vector2(_atkRigidbody2D.velocity.x, 18);


                atkObj.GetComponent<GameBody>().ResetShanJin();
            }
            


            //if (Coll.gameObject.transform.parent&& Coll.gameObject.transform.parent.GetComponent<JN_base>())
            //{
            //    GameObject obj = Coll.gameObject.transform.parent.GetComponent<JN_base>().atkObj;
            //    if (obj.tag == GlobalTag.Player&&obj.GetComponent<GameBody>().isInAiring&&obj.transform.position.y>=this.transform.position.y)
            //    {
            //        Rigidbody2D _atkRigidbody2D = obj.GetComponent<GameBody>().GetPlayerRigidbody2D();
            //        _atkRigidbody2D.velocity = new Vector2(_atkRigidbody2D.velocity.x, 18);
            //    }
            //}
            ShowFire();
        }
    }


    void ShowFire()
    {
        if (!IsHasDianran)
        {
            IsHasDianran = true;



            if (HuoyanOutPosList.Count != 0)
            {
                foreach(Transform pos in HuoyanOutPosList)
                {
                    CreateHuo(pos); 
                    //GameObject huoyan = CreateHuo(pos);
                    //huoyan.transform.position = pos.position;
                    //print("  火焰位置 " + pos.position + "   huoyan " + huoyan.activeSelf);
                }
            }
            else
            {
                CreateHuo();
                //GameObject huoyan = CreateHuo();
                //huoyan.transform.position = this.transform.position;
            }
        }



        //移除自身
        if (CanFireJiangeTime == 0) RemoveSelf();
    }


    GameObject CreateHuo(Transform pos = null)
    {
        GameObject huoyan = GlobalTools.GetGameObjectInObjPoolByName(Huoyan);
        //GameObject huoyan = GlobalTools.GetGameObjectByName(Huoyan);
        huoyan.transform.parent = this.transform.parent;
        
        huoyan.name = Huoyan;
        huoyan.gameObject.SetActive(true);

        if (pos)
        {
            float fanwei = 2;
            float ___x = pos.position.x - fanwei + GlobalTools.GetRandomDistanceNums(fanwei);
            float ___y = pos.position.y - fanwei + GlobalTools.GetRandomDistanceNums(fanwei);
            Vector2 newPos = new Vector2(___x,___y);
            huoyan.transform.position = newPos;
        }
        else
        {
            huoyan.transform.position = this.transform.position;
        }

        //****** 2021-11-08 注意 直接在这生成粒子 调用粒子系统-直接生成 直接生成 直接生成  不要return出去给变量 在外面调用return生成后 有概率不被摄像机渲染 注意 粒子系统 都直接生成     靠 *****************

        //print(huoyan.GetComponent<ParticleSystem>().IsAlive()+"   ?stop?  "+huoyan.GetComponent<ParticleSystem>().isStopped+"   --粒子数 "+ huoyan.GetComponent<ParticleSystem>().particleCount);
        //if (huoyan.GetComponent<ParticleSystem>().particleCount == 0)
        //{
        //    huoyan.gameObject.SetActive(false);
        //    huoyan = GlobalTools.GetGameObjectInObjPoolByName(Huoyan);
        //    huoyan.transform.parent = this.transform.parent;
        //    if (pos)
        //    {
        //        huoyan.transform.position = pos.position;
        //    }
        //    else
        //    {
        //        huoyan.transform.position = this.transform.position;
        //    }
            
        //    huoyan.name = Huoyan;
        //    huoyan.gameObject.SetActive(true);
        //}
        huoyan.GetComponent<ParticleSystem>().Simulate(0.0f);
        huoyan.GetComponent<ParticleSystem>().Play();
        return huoyan;
    }



    void RemoveSelf()
    {
        this.gameObject.GetComponent<ParticleSystem>().Stop();
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);
    }




    //protected void OnParticleTrigger()
    //{
    //    print("????????");
    //}
}
