using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_zidan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ZidanTest();
        TheStart();
    }

    protected virtual void TheStart()
    {

    }

    void ZidanTest()
    {
        SetV2Speed(new Vector2(0, -6));
        _isFSByFX = true;
        isFaShe = false;
        fire();
    }


    [Header("延迟 碰撞时间")]
    public float YanchiHitTimes = 0;

    bool IsYanchiJishiOver = false;
    float yanchiJishi = 0;
    protected bool YanchiHit()
    {
        if (IsYanchiJishiOver) return false;
        print("  yanchi pengzhuang  YanchiHitTimes  "+this.name+ "      YanchiHitTimes?  " + YanchiHitTimes);
        if (YanchiHitTimes!=0)
        {
            yanchiJishi += Time.deltaTime;
            print("  yanchi pengzhuang  panduan  " + yanchiJishi);
            if (yanchiJishi>= YanchiHitTimes)
            {
                print("yanchi  可以碰撞了------？？？YanchiHitTimes   " + YanchiHitTimes);
                yanchiJishi = 0;
                IsYanchiJishiOver = true;
                return false;
            }
            return true;
        }

        return false;
    }


    void YanchiHitReset()
    {
        IsYanchiJishiOver = false;
        yanchiJishi = 0;
    }


    protected GameObject _player;
    protected bool isFaShe = false;
    public float speeds = 20;
    //Vector2 v2;

    [Header("技能队伍")]
    public float team = 2;

    private void OnEnable()
    {
        if (!_player) {
            _player = GlobalTools.FindObjByName("player");
            if(_player == null)
            {
                _player = GlobalTools.FindObjByName(GlobalTag.PlayerJijiaObj);
            }
        }
        
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
        YanchiHitReset();
        OtherOnEnable();
    }

    protected virtual void OtherOnEnable()
    {

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
            print("----------------------------------------->>  fire!!!!! "+speeds);
            GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(_player.transform.position, this.transform.position, speeds);
            //print("   sudu   "+ GetComponent<Rigidbody2D>().velocity);
            FSFXV2 = GetComponent<Rigidbody2D>().velocity;

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


    public void Resetfsfxv2()
    {
        FSFXV2 = new Vector2(Mathf.Abs(FSFXV2.x), 0);
    }

    protected bool _isFSByFX = false;
    public Vector2 FSFXV2 = new Vector2(0, 0);
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

    public bool IsCanHitDiban = true;

    protected virtual void OnTriggerEnter2D(Collider2D Coll)
    {
        if (YanchiHitTimes != 0 && !IsYanchiJishiOver) {
            print("  yanchi--------ooooooooo-------------!!! "+this.name+ "  YanchiHitTimes??  "+ YanchiHitTimes);
            return;
        }
        
        //print("   Coll.tag  碰到什么鬼！！！：    " + Coll.tag);
        if (Coll.tag == "Player"||(IsCanHitDiban && Coll.tag == "diban")|| Coll.tag == "zidanDun"||Coll.tag == GlobalTag.JIGUANG)
        {
            if (!IsCanHit) return;
            //print(testN + "   Coll.tag  碰到了什么鬼：    " + Coll.tag);
            if (Coll.tag == "Player" && Coll.GetComponent<RoleDate>().isCanBeHit == false) return;
            //生成爆炸
            HitObj();

        }
    }


    protected virtual void HitObj()
    {
        Boom();
        RemoveSelf();
    }


    protected virtual void Boom()
    {
        
        if (testN == 0)
        {
            testN++;
            string bzName = "TX_zidan" + bzType + "_bz";
            GameObject baozha = ObjectPools.GetInstance().SwpanObject2(Resources.Load(bzName) as GameObject);
            baozha.transform.position = this.transform.position;
            if(baozha.GetComponent<JN_Date>()) baozha.GetComponent<JN_Date>().team = this.GetComponent<JN_Date>().team;
            baozha.GetComponent<ParticleSystem>().Play();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public float BoomByTimesNum = 0;
    protected float boomJishi = 0;
    void RemoveByTimes()
    {
        if (BoomByTimesNum != 0)
        {
            boomJishi += Time.deltaTime;
            if (boomJishi >= BoomByTimesNum) {
                boomJishi = 0;
                Boom();
                RemoveSelf();
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        YanchiHit();
        OtherUpdate();
        if (IsAtkAuto) {
            fire();
        }
        RemoveByTimes();
        //print("sudu   "+ GetComponent<Rigidbody2D>().velocity);
    }


    protected virtual void OtherUpdate()
    {

    }


    public virtual void ResetAll()
    {
        //testN = 0;
        _isFSByFX = false;
        boomJishi = 0;
    }

    public virtual void RemoveSelf()
    {
        //移除自己 
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 0));
        ResetAll();
    }
}
