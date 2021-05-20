using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLziHit : MonoBehaviour
{
    GameObject atkObj;
    // Start is called before the first frame update
    void Start()
    {
        SetAtkObject();
    }

    private void Awake()
    {
       
    }


    [Header("过多久 消除自己 0是不消除")]
    public float DisSelfByTimes = 0;


    public void SetAtkObject()
    {
        atkObj = GetComponent<JN_base>().atkObj;
        if (atkObj == null && ParentLizi != null)
        {
            atkObj = ParentLizi.GetComponent<JN_base>().atkObj;
            GetComponent<JN_base>().atkObj = atkObj;
        }
        float _atkScaleX = 1;
        if (GetComponent<JN_base>().atkObj)
        {
            _atkScaleX = GetComponent<JN_base>().atkObj.transform.localScale.x;
        }

        GetComponent<HitKuai>().GetTXObj(this.gameObject, true, _atkScaleX, atkObj);

        if (DisSelfByTimes != 0)
        {
            StartCoroutine(IEDestory(DisSelfByTimes));
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    [Header("是否是 只会 攻击碰撞一次")]
    public bool IsHitOnce = false;


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





    //发生粒子碰撞的回调函数
    private void OnParticleCollision(GameObject other)
    {
        //print(other.name);
        if(other.tag == "Player")
        {
            //IsHitPlayer = true;
            //if (!HitOnceTest)
            //{
            //    HitOnceTest = true;
                
            //    
            //}


            if (IsHitOnce)
            {
                foreach (string _name in HitObjList)
                {
                    if(_name == other.name)
                    {
                        return;
                    }
                }
                HitObjList.Add(other.name);
                print("  -->>>> lizihit  beihitName  "+ other.name);
            }
            GetComponent<HitKuai>().LiziHit(other);


            //火焰燃烧 的 攻击 持续时间  特效里面 做 持续燃烧特效


            //还有解决方案 1.是 让 被攻击者 自己判断 ---- *****
            //2.是 这里  用list 记录 被攻击对象 名字 和 持续时间
        }

        //if (IsHitDisSelf) ObjectPools.GetInstance().DestoryObject2(this.gameObject);
        if (IsHitDisSelf)
        {
            if (DisShowZiLiziName != "")
            {
                GameObject DisShowZiLizi = ObjectPools.GetInstance().SwpanObject2(Resources.Load(DisShowZiLiziName) as GameObject);
                DisShowZiLizi.transform.position = this.transform.position;
                DisShowZiLizi.GetComponent<JN_base>().atkObj = atkObj;
                DisShowZiLizi.GetComponent<OnLziHit>().SetAtkObject();
            }
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
