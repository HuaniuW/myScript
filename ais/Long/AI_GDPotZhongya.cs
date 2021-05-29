using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_GDPotZhongya : MonoBehaviour,ISkill
{
    //固定点 重压

    [Header("飞到的 准备下压的 固定点附近")]
    public Transform FlyToPos;

    [Header("下压 位置 检测点1")]
    public Transform HitDownPot1;

    [Header("下压 要去的点")]
    public Vector2 XiaYaTot;




    [Header("下压动作名字")]
    public string XiayaACName = "";

    [Header("撞击地面 动作名字")]
    public string HitGroundACName = "";


    [Header("下压烟尘的 技能名字")]
    public string XiayaYanchengJinengName = "tx_xialuozhendongYC";

    [Header("龙吼声！")]
    public AudioSource LongHou;

    bool _isGetOver = false;



    GameObject _player;
    GameBody _gameBody;
    RoleDate _roleDate;
    AIAirRunNear _runNear;

    //飞到 开始 下压高点
    bool IsStartFlyToPot = false;
    void FlyToPot()
    {
        if (IsStartFlyToPot&&_runNear.ZhijieMoveToPoint(FlyToPos.position,1,4,false))
        {
            IsStartFlyToPot = false;
            IsStartXiaya = true;
            print("到达 准备 下压高点！");

            XiaYaTot = new Vector2(this.transform.position.x, this.transform.position.y - 20);

            //龙吼声
            LongHou.Play();
        }
    }

    //开始 下压
    bool IsStartXiaya = false;
    void StartXiaya()
    {
        if (!IsStartXiaya) return;

       



        if (IsHitGround)
        {
            print("撞到地面！！");
            IsStartXiaya = false;
            //出下压的 烟尘技能
            _runNear.ZJStop();
            _gameBody.StopVSpeed();

            ShowHitSkillObj();
            _gameBody.isAcing = false;

            //需不需要 延迟 几秒 结束？  动作做完啊

            IsHitDownOnGround = true;
            

            return;
        }

        if (_runNear.ZhijieMoveToPoint(XiaYaTot, 0.5f, 4, false, true, 20))
        {
            ReSetAll();
            print("到达点！！！！！");
            _isGetOver = true;
        }


        if (_gameBody.GetDB().animation.lastAnimationName != XiayaACName)
        {
            _gameBody.isAcing = true;
            _gameBody.GetDB().animation.GotoAndStopByFrame(XiayaACName);
        }
    }


    public virtual bool IsHitGround
    {
        get
        {
            //print("groundCheck 是否有这个 变量   "+ groundCheck);
            if (!HitDownPot1) return false;
            Vector2 start = HitDownPot1.position;
            Vector2 end = new Vector2(start.x, start.y - 0.8f);
            Debug.DrawLine(start, end, Color.blue);
            return Physics2D.Linecast(start, end, _gameBody.groundLayer);
        }
    }


    bool IsHuanYuanStand = false;

    bool IsHitDownOnGround = false;
    void HitDownOnGround()
    {
        if (!IsHitDownOnGround) return;

        if (IsHuanYuanStand)
        {
            if (_gameBody.GetAcMsg(_gameBody.GetStandACName(),2) == "completed")
            {
                IsHitDownOnGround = false;
                ReSetAll();
                _isGetOver = true;
            }
            return;
        }




        if(_gameBody.GetAcMsg(HitGroundACName) == "completed")
        {
            IsHuanYuanStand = true;
        }


        //if (_gameBody.GetDB().animation.lastAnimationName == HitGroundACName && _gameBody.GetDB().animation.isCompleted)
        //{
        //    IsHitDownOnGround = false;
        //    ReSetAll();
        //    _isGetOver = true;
        //    return;
        //}

        //if (_gameBody.GetDB().animation.lastAnimationName != HitGroundACName)
        //{
        //    print("落地动作  " + _gameBody.GetDB().animation.lastAnimationName);
        //    _gameBody.isAcing = true;
        //    _gameBody.GetDB().animation.GotoAndPlayByFrame(HitGroundACName);
        //    print("落地动作  " + _gameBody.GetDB().animation.lastAnimationName);
        //}

    }



    void ShowHitSkillObj()
    {
        GameObject yc;
        Vector2 _pos = this.transform.position;
        yc = Resources.Load(XiayaYanchengJinengName) as GameObject;
        GameObject yanchenHitKuai = ObjectPools.GetInstance().SwpanObject2(yc);
        yanchenHitKuai.transform.parent = this.transform.parent;
        yanchenHitKuai.GetComponent<JN_base>().GetPositionAndTeam(_pos, _roleDate.team, 1, this.gameObject, true);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);
        print("@@@@@@@@@@@ 进来几次 ");
    }



    public void GetStart(GameObject gameObj)
    {
        ReSetAll();
        _player = gameObj;
        IsStartFlyToPot = true;

    }

    public bool IsGetOver()
    {
        return _isGetOver;
    }

    public void ReSetAll()
    {
        IsStartFlyToPot = false;
        IsStartXiaya = false;
        _gameBody.isAcing = false;
        IsHitDownOnGround = false;
        IsHuanYuanStand = false;
        _isGetOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();
        _roleDate = GetComponent<RoleDate>();
        _runNear = GetComponent<AIAirRunNear>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_roleDate.isBeHiting || _roleDate.isDie)
        {
            ReSetAll();
            _isGetOver = true;
            return;
        }

        HitDownOnGround();
        StartXiaya();
        FlyToPot();
       
    }
}
