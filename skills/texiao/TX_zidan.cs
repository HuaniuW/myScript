using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_zidan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected GameObject _player;
    protected bool isFaShe = false;
    public float speeds = 20;
    //Vector2 v2;


    private void OnEnable()
    {
        if (!_player) _player = GlobalTools.FindObjByName("player");
        //HitKuai
        //获取角速度
        isFaShe = true;
        testN = 0;
        //v2 = GlobalTools.GetVector2ByPostion(_player.transform.position,this.transform.position,speeds);
        /*  v2 = _player.transform.position - this.transform.position;
          print("_player.transform.position    " + _player.transform.position + "  this.transform.position   " + this.transform.position+  "-----------------------zd>  " +v2);
          GetComponent<Rigidbody2D>().velocity = v2;*/


        //isFire = false;
        //isShangshen = false;
    }


    public void SetZDSpeed(float theSpeed)
    {
        this.speeds = theSpeed;
    }



    ////上升的一定高度 速度下降的时候 开始进进攻 高速子弹 或者 有时间限制的  追踪子弹
    ////上升速度
    //public float UpSpeed = 15;
    ////设置重力系数

    //bool isShangshen = false;
    //bool isFire = false;

    //public bool IsGaosuZiDan = false;
    //void GaosuZiDan()
    //{
    //    if (IsGaosuZiDan)
    //    {
    //        if (!isShangshen)
    //        {
    //            isShangshen = true;
    //            Vector2 upPos = new Vector2();
    //            GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(_player.transform.position, this.transform.position, speeds);
    //        }
    //    }
    //}

  
    public virtual void GetZiDanStart()
    {

    }


    protected virtual void fire()
    {

        if (_isFSByFX)
        {
            GetComponent<Rigidbody2D>().velocity = FSFXV2;
            return;
        }


        if (_player&& isFaShe) {
            isFaShe = false;
            //print("----------------------------------------->>  fire!!!!! "+speeds);
            GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(_player.transform.position, this.transform.position, speeds);
            //print("   sudu   "+ GetComponent<Rigidbody2D>().velocity);
        } 
    }

    [Header("是否自行开启向 玩家的 攻击")]
    public bool IsAtkAuto = true;


    public void CloseAutoFire()
    {
        IsAtkAuto = false;
    }

    public void GetSpeedV2()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = FSFXV2;
    }

    public void SetV2Speed(Vector2 v2)
    {
        FSFXV2 = v2;
    }



    //指定的攻击目标
    public void GetTargetObj(GameObject targetObj)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(targetObj.transform.position, this.transform.position, speeds);
    }


    public void GetDiretionByV2(Vector2 v2,float theSpeed = 0)
    {
        if (theSpeed != 0) speeds = theSpeed;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(v2, this.transform.position, speeds);
    }

    bool _isFSByFX = false;
    public Vector2 FSFXV2 = new Vector2(1, 0);
    public void SetZiDanSpeedByFX(float scaleX)
    {
        _isFSByFX = true;
        //FSFXV2 = new Vector2(1, 0);
        FSFXV2 *= scaleX*speeds;
    }


    [Header("爆炸 特效 的类型  爆炸特效名字")]
    public int bzType = 1;


    protected int testN = 0;
    public bool IsCanHit = true;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag);
        if (Coll.tag == "Player"||Coll.tag == "diban"|| Coll.tag == "zidanDun")
        {
            if (!IsCanHit) return;
            //print(testN + "   Coll.tag  碰到了什么鬼：    " + Coll.tag);
            if (Coll.tag == "Player" && Coll.GetComponent<RoleDate>().isCanBeHit == false) return;
            //生成爆炸
            if(testN == 0)
            {
                testN++;
                string bzName = "TX_zidan" + bzType + "_bz";
                GameObject baozha = ObjectPools.GetInstance().SwpanObject2(Resources.Load(bzName) as GameObject);
                baozha.transform.position = this.transform.position;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;                
            }
            RemoveSelf();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAtkAuto) {
            fire();
        }
        //print("sudu   "+ GetComponent<Rigidbody2D>().velocity);
    }


    public virtual void ResetAll()
    {
        //testN = 0;
        _isFSByFX = false;
    }

    public virtual void RemoveSelf()
    {
        //移除自己 
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 0));
        ResetAll();
    }
}
