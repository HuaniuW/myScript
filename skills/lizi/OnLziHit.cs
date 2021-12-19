using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLziHit : MonoBehaviour
{
    GameObject atkObj;

    [Header("粒子特效1")]
    public ParticleSystem TX_lizi1;
    [Header("粒子特效2")]
    public ParticleSystem TX_lizi2;

    // Start is called before the first frame update
    void Start()
    {
        //print(" ************************************************ this.position " + this.transform.position);
        SetAtkObject();
    }


    public void GetStart()
    {
        print(" ----爆炸震动 ");
        ChuxianZhengdong();
        
    }



    //private void OnEnable()
    //{
        
    //}


    [Header("是否 附带 震动")]
    public bool IsShowShock = false;
    void ChuxianZhengdong()
    {
        //出现的时候 会震动
        if (IsShowShock)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.4"), this);
        }
    }

    float LiziJishi = 0;

    private void Awake()
    {
        IsCanHit = true;
        DisSelfJishi = 0;
        LiziJishi = 0;
        HitObjList.Clear();
    }


    [Header("过多久 消除自己 0是不消除")]
    public float DisSelfByTimes = 0;


    public void SetAtkObject()
    {
        atkObj = GetComponent<JN_base>().atkObj;
        if (atkObj == null && ParentLizi != null)
        {
            print("atkObj  "+atkObj+ "  ParentLizi "+ ParentLizi);
            atkObj = ParentLizi.GetComponent<JN_base>().atkObj;
            GetComponent<JN_base>().atkObj = atkObj;
        }
        float _atkScaleX = 1;
        if (GetComponent<JN_base>().atkObj)
        {
            _atkScaleX = GetComponent<JN_base>().atkObj.transform.localScale.x;
        }

        if(GetComponent<HitKuai>()) GetComponent<HitKuai>().GetTXObj(this.gameObject, true, _atkScaleX, atkObj);

        //if (DisSelfByTimes != 0)
        //{
        //    StartCoroutine(IEDestory(DisSelfByTimes));
        //}
    }


    // Update is called once per frame
    void Update()
    {
        LiziJishi += Time.deltaTime;
        JishiDisSelf();
    }



    float DisSelfJishi = 0;

    void JishiDisSelf()
    {
        if (DisSelfByTimes == 0) return;
        

        if (TX_lizi1 && TX_lizi1.isStopped)
        {
            if (TX_lizi1) TX_lizi1.loop = true;
            if (TX_lizi2) TX_lizi2.loop = true;
            ObjectPools.GetInstance().DestoryObject2(this.gameObject);
        }

        DisSelfJishi += Time.deltaTime;
        if (DisSelfJishi>= DisSelfByTimes)
        {
            DisSelfJishi = 0;
            print("removeSelf!!!!");
            //if (GetComponent<ParticleSystem>())
            //{
            //    GetComponent<ParticleSystem>().loop = false;
            //}
            //if (GetComponentInChildren<ParticleSystem>())
            //{
            //    GetComponentInChildren<ParticleSystem>().loop = false;
            //}


            if(TX_lizi1) TX_lizi1.loop = false;
            if (TX_lizi2) TX_lizi2.loop = false;

            if(TX_lizi1 == null)
            {
                ObjectPools.GetInstance().DestoryObject2(this.gameObject);
            }

            //ObjectPools.GetInstance().DestoryObject2(this.gameObject);
            //ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject,0.2f);
        }
    }




    List<string> HitObjList = new List<string>() { };


    //bool HitOnceTest = false;



    public GameObject ParentLizi;

    public void SetAtkObject(GameObject theAtkObj)
    {
        GetComponent<JN_base>().atkObj = theAtkObj;
    }

    //public ParticleSystem huoyan;

    [Header("是否 碰撞后 删除自己")]
    public bool IsHitDisSelf = false;


    bool IsCanHit = true;
    public void SetCanHit()
    {
        IsCanHit = true;
    }



    //发生粒子碰撞的回调函数
    private void OnParticleCollision(GameObject other)
    {

        //if (other.tag == "zidanDun")
        //{
        //    print("   粒子击中 防御盾  ");
        //}
        print(this.name + "  IsCanHit  " + IsCanHit+"   $$$$$  "+other.tag);


        //print(" this  "+this.tag+"    -----   "+ other.tag);
        if (this.tag == GlobalTag.HUOYAN)
        {
            if (other.tag == GlobalTag.ENEMY || other.tag == GlobalTag.BOSS || other.tag == GlobalTag.Diren)
            {
                if (other.GetComponent<RoleDate>().KangHuoJilv == 100)
                {
                    print("  this -----  100 抵抗火 ");
                    return;
                }

            }
        }

        if (this.tag == GlobalTag.DIAN)
        {
            if (other.tag == GlobalTag.ENEMY || other.tag == GlobalTag.BOSS || other.tag == GlobalTag.Diren)
            {
                if (other.GetComponent<RoleDate>().KangDuJilv == 100)
                {
                    print("  this -----  100 抵抗毒  ！！！！ ");
                    return;
                }

            }
        }


        if (!IsCanHit) return;
        //print(other.tag);
        if (other.tag == GlobalTag.Player|| other.tag == GlobalTag.JINGYING|| other.tag == GlobalTag.ENEMY|| other.tag == GlobalTag.AirEmeny|| other.tag == GlobalTag.BOSS|| other.tag == GlobalTag.Diren)
        {
           
            foreach (string str in HitObjList)
            {
                int id = int.Parse(str.Split('_')[0]);
                float times = float.Parse(str.Split('_')[1]);
                if (id == other.GetInstanceID()&&LiziJishi - times<=0.4f)
                {
                    return;
                }
            }
            print("  粒子击中敌人！！！！！ " + other.name);
            string o = other.GetInstanceID() + "_" + LiziJishi;
            HitObjList.Add(o);
            GetComponent<HitKuai>().LiziHit(other);




            //火焰燃烧 的 攻击 持续时间  特效里面 做 持续燃烧特效


            //还有解决方案 1.是 让 被攻击者 自己判断 ---- *****
            //2.是 这里  用list 记录 被攻击对象 名字 和 持续时间  *****改为被攻击对象的 id 取id
        }

        //if (IsHitDisSelf) ObjectPools.GetInstance().DestoryObject2(this.gameObject);
        if (IsHitDisSelf)
        {
            if (DisShowZiLiziName != "")
            {
                GameObject DisShowZiLizi = ObjectPools.GetInstance().SwpanObject2(Resources.Load(DisShowZiLiziName) as GameObject);
                DisShowZiLizi.transform.position = this.transform.position;
                DisShowZiLizi.GetComponent<JN_base>().atkObj = atkObj;
                if (DisShowZiLizi.GetComponent<OnLziHit>())
                {
                    DisShowZiLizi.GetComponent<OnLziHit>().SetAtkObject();
                    DisShowZiLizi.GetComponent<OnLziHit>().GetStart();
                }
            }
            IsCanHit = false;
            ObjectPools.GetInstance().DestoryObject2(this.gameObject);
        }


        //StartCoroutine(IEDestory(0.5f));



        //if(other.tag == "diban")
        //{
        //    huoyan.Play();
        //    huoyan.transform.position = this.transform.position;
        //}
    }

    public IEnumerator IEDestory(float time)
    {
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        print("  ---->xiecheng dis Self!!!  ");
        //this.gameObject.SetActive(false);
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);
    }

    [Header("自己消失 时候 出现的 子粒子")]
    public string DisShowZiLiziName = "";


    private void OnDisable()
    {
        ResetAll();
    }

    private void ResetAll()
    {
        HitObjList.Clear();
    }


    //private void OnParticleTrigger()
    //{
    //    //只要勾选了粒子系统的trigger，程序运行后会一直打印
    //    print("触发了");
    //}
}
