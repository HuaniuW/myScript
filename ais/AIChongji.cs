using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChongji : MonoBehaviour, ISkill
{
    // Start is called before the first frame update
    void Start()
    {
        runNear = GetComponent<AIAirRunNear>();
        runAway = GetComponent<AIAirRunAway>();
        CJYanmu.Stop();
    }

    protected AIAirRunNear runNear;
    protected AIAirRunAway runAway;
    // Update is called once per frame
    void Update()
    {
        if (isStarting) Starting();
    }

    protected Transform _targetObj;
    protected bool isGetOver = false;

    public bool IsChongjiOver()
    {
        return isGetOver;
    }

    public float chongjiSpeed = 20;

    public virtual void GetStart(GameObject targetObj)
    {
        if (_targetObj == null) _targetObj = targetObj.transform;

        //if (GlobalTools.GetDistanceByTowPoint(this.transform.position, _targetObj.transform.position) < 2)
        //{

        //}

        isGetOver = false;
        if(ChongjiYingZhi!=0)GetComponent<RoleDate>().addYZ(ChongjiYingZhi);
        isStarting = true;
        //print("this.transform.position：   " + this.transform.position);
    }

    protected bool IsTongYing = false;

    public void ReSetAll()
    {
        IsTongYing = false;
        isStarting = false;
        isGetOver = false;
        isTanSheing = false;
        isGetOver = true;
        deltaNums = 0;
        CJYanmu.Stop();
        GetComponent<AirGameBody>().GetDB().animation.timeScale = 1f;
    }


    protected virtual void Starting()
    {
        
        if (GetComponent<RoleDate>().isDie|| _targetObj==null||_targetObj.GetComponent<RoleDate>().isDie)
        {
            ReSetAll();
            return;
        }
        if (!isTanSheing && GetNearTarget())isTanSheing = true;
        if (isTanSheing) Tanshe();
    }

    float fantanNums = 0.1f;
    protected bool isStarting = false;
    public bool isTanSheing = false;
    protected float deltaNums = 0;
    [Header("冲击方式 是否是跟踪模式 ")]
    public bool IsGenZongType = false;

    protected bool IsStartChongji = false;

    protected Vector2 targetPos = Vector2.zero;


    [Header("准备动作做完后 开始 冲击的 延迟时间")]
    public float CJYanchiTime = 0.5f;
    protected float CJYanchiNums = 0;

    [Header("冲击时的硬直")]
    public float ChongjiYingZhi = 1000;

    protected virtual void Tanshe()
    {
        if (GetComponent<RoleDate>().isBeHiting|| GetComponent<RoleDate>().isDie || GetComponent<GameBody>().IsHitWall || GetComponent<GameBody>().IsGround) {

            ReSetAll();
            ChongjiOver();
            return;
        }


        //做起始动作
        //起始动作完成 弹射 做第二个动作
        //完成后 还原动作
        if (GetComponent<AirGameBody>().GetDB().animation.HasAnimation("chongji_begin")&& GetComponent<AirGameBody>().GetDB().animation.HasAnimation("chongji_start"))
        {
            if (GetComponent<AirGameBody>().GetDB().animation.lastAnimationName != "chongji_begin" && GetComponent<AirGameBody>().GetDB().animation.lastAnimationName != "chongji_start")
            {
                //转向 朝向玩家
                GetComponent<AirGameBody>().GetAcMsg("chongji_begin");
                GetComponent<AirGameBody>().GetDB().animation.Stop();
                GetComponent<AirGameBody>().GetStop();
                deltaNums = 0;
                return;
            }


            if (GetComponent<AirGameBody>().GetDB().animation.lastAnimationName == "chongji_begin")
            {
                CJYanchiNums += Time.deltaTime;
                //print("  CJYanchiNums   " + CJYanchiNums + "   GetComponent<AirGameBody>().GetDB().animation  " + GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);
                if (CJYanchiNums >= CJYanchiTime)
                {
                    GetComponent<AirGameBody>().GetAcMsg("chongji_start");
                    if(!GetComponent<AirGameBody>().GetDB().animation.isPlaying) GetComponent<AirGameBody>().GetDB().animation.Play();
                }
                return;
            }
        }

        //print("ac   "+ GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);


        if (!IsStartChongji&&!IsGenZongType) {
            IsStartChongji = true;
            targetPos = _targetObj.position;
            //这个点 要在靠后一点
            float dis = GlobalTools.GetDistanceByTowPoint(targetPos,transform.position);
            float _d = 4;
            float __x1 = targetPos.x - _d * (this.transform.position.x - targetPos.x) / dis;
            float __y1 = targetPos.y - _d * (this.transform.position.y - targetPos.y) / dis;
            targetPos = new Vector2(__x1,__y1);
        }

        if(IsGenZongType) targetPos = _targetObj.position;


        //print(" 5  "+ this.transform.position);
        deltaNums += Time.deltaTime;

        if (deltaNums >= _tsTimes|| GetComponent<AIAirRunNear>().ZhijieMoveToPoint(targetPos, 0.1f, chongjiSpeed))
        {
            ChongjiOver();
        }

        CJYanmu.Play();
    }


    protected void ChongjiOver()
    {
        IsTongYing = false;
        deltaNums = 0;
        CJYanmu.Stop();
        if (ChongjiYingZhi != 0) GetComponent<RoleDate>().hfYZ(ChongjiYingZhi);
        //CJYanmu.loop = false;
        isStarting = false;
        isGetOver = true;
        isTanSheing = false;
        IsStartChongji = false;
        CJYanchiNums = 0;
        GetComponent<AIAirRunNear>().ResetAll();
        GetComponent<AirGameBody>().SetACingfalse();
        //print("*************************************************************冲击 结束！！！！！");
    }


    [Header("冲击时间 注意 冲击速度越小 这个时间要越长 不然 就冲不到位置")]
    public float _tsTimes = 0.5f;

    [Header("冲击烟幕")]
    public ParticleSystem CJYanmu;

    [Header("鱼进入 冲击弹射 范围")]
    public float _atkDistance = 8;
    //定位目标
    protected virtual bool GetNearTarget()
    {
        return runNear.Zhuiji(_atkDistance,false);
    }

    bool ISkill.IsGetOver()
    {
        return IsChongjiOver();
    }

    //算xy速度
    //角色攻击动作
    //弹射
    //碰到墙壁 速度翻转
    //距离喷射拖尾特效
}
