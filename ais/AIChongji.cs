using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChongji : MonoBehaviour, ISkill
{
    // Start is called before the first frame update
    void Start()
    {
        GetInit();
        runNear = GetComponent<AIAirRunNear>();
        runAway = GetComponent<AIAirRunAway>();
        _roleDate = GetComponent<RoleDate>();
        _airGameBody = GetComponent<AirGameBody>();
        CJYanmu.Stop();
    }


    protected virtual void GetInit()
    {

    }

    [Header("冲击开始时候的 喊声")]
    public AudioSource StartSound;

    protected RoleDate _roleDate;
    protected AirGameBody _airGameBody;

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

    public virtual void ReSetAll()
    {
        IsTongYing = false;
        isStarting = false;
        isGetOver = false;
        isTanSheing = false;
        isGetOver = true;
        deltaNums = 0;
        CJYanmu.Stop();
        GetComponent<AirGameBody>().GetDB().animation.timeScale = 1f;
        if (GetComponent<JN_Date>()) GetComponent<JN_Date>().HitInSpecialEffectsType = 5;
        _IsHitGroundUp = false;
    }


    Vector2 upPos;
    bool _IsHitGroundUp = false;
    void GetFlyUp()
    {
        if(GetComponent<AIAirRunNear>().ZhijieMoveToPoint(upPos , 0.5f, 1.5f, false, false, 0))
        {
            _IsHitGroundUp = false;
        }

        print("upPos   "+ upPos);
    }

    protected virtual void Starting()
    {
        
        if (GetComponent<RoleDate>().isDie|| _targetObj==null||_targetObj.GetComponent<RoleDate>().isDie)
        {
            ReSetAll();
            return;
        }
        //print("_IsHitGroundUp    "+ _IsHitGroundUp);

        //if (!isTanSheing &&runNear.IsHitDiBanByFX(this.transform.position,new Vector2(this.transform.position.x,this.transform.position.y-1.5f)))
        //{
        //    //启动时候 碰到地面
        //    //移动上升一点
        //    //print("  上升中！！ ");
        //    //GetFlyUp();
        //    if (!_IsHitGroundUp)
        //    {
        //        _IsHitGroundUp = true;
        //        runNear.ResetAll();
        //        upPos = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        //    }
            
        //}

        //if (_IsHitGroundUp) {
        //    GetFlyUp();
        //    return;
        //}

        //print("isTanSheing    "+ isTanSheing);
        if (!isTanSheing && GetNearTarget()) {
            isTanSheing = true;
            if (GetComponent<JN_Date>()) GetComponent<JN_Date>().HitInSpecialEffectsType = 1;
        }
        
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
        if (GetComponent<RoleDate>().isBeHiting|| GetComponent<RoleDate>().isDie || GetComponent<GameBody>().IsHitWall) {
            print("  IsHitWall "+ GetComponent<GameBody>().IsHitWall+ "  ------IsGround  "+ GetComponent<GameBody>().IsGround);


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
                //开始冲击时候的 怪物叫声
                if (StartSound) StartSound.Play();
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

            if (ChongJiShenHouDis != 0)
            {
                //这个点 要在靠后一点
                float dis = GlobalTools.GetDistanceByTowPoint(targetPos, transform.position);
                float _d = ChongJiShenHouDis;
                float __x1 = targetPos.x - _d * (this.transform.position.x - targetPos.x) / dis;
                float __y1 = targetPos.y - _d * (this.transform.position.y - targetPos.y) / dis;
                targetPos = new Vector2(__x1, __y1);
            }
          


            //转向
            //朝向
            if (this.transform.position.x < targetPos.x)
            {
                _airGameBody.TurnRight();
            }
            else
            {
                _airGameBody.TurnLeft();
            }

        }

        if(IsGenZongType) targetPos = _targetObj.position;


        //print(" 5  "+ this.transform.position);
        deltaNums += Time.deltaTime;

        if (deltaNums >= _tsTimes|| GetComponent<AIAirRunNear>().ZhijieMoveToPoint(targetPos, 0.1f, chongjiSpeed,false,false,0))
        {
            //print("自身的位置   "+this.transform.position+ "  冲击到了位置 停止targetPos  " + targetPos);
            ChongjiOver();
        }

        CJYanmu.Play();
    }

    [Header("冲击到身后的 距离 是0的话就不计算")]
    public float ChongJiShenHouDis = 0;

    protected virtual void ChongjiOver()
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
        if (GetComponent<JN_Date>()) GetComponent<JN_Date>().HitInSpecialEffectsType = 5;
        _IsHitGroundUp = false;
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
        print("鱼 冲击 接近目标----------------  接近 ");
        if (this.transform.position.x > _targetObj.position.x)
        {
            print("目标在左侧！！！  "+this.transform.position.x+"    -------x    "+ _targetObj.position.x);
            _airGameBody.TurnLeft();
        }
        else
        {
            print("目标在---右侧！！！");
            _airGameBody.TurnRight();
        }
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
