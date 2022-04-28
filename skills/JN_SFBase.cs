using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class JN_SFBase : MonoBehaviour, ISkill
{
    //技能释放基础类
    [Header("释放特效的 动作 名字")]
    public string ACName = "";
    [Header("释放特效的 名字")]
    public string TXName = "";
    [Header("是否要取 gamebody")]
    public bool IsGetBody = true;

    [Header("修正 X")]
    public float XZX = 0;

    [Header("修正 Y")]
    public float XZY = 0;

    [Header("攻击距离")]
    public float AtkDistance = 0;

    [Header("攻击距离Y的方向  控制位置上下 ")]
    public float AtkDistanceY = 0;

    [Header("接近 速度")]
    public float NearSpeed = 0.9f;

    [Header("起手 延迟  技能动作的 延迟动作")]
    public float qishoudongzuoyanchi = 0;


    [Header(">收手 延迟  技能动作的 延迟动作")]
    public float ACOverYCTimes = 0;


    protected GameBody _gameBody;

    protected bool IsStarting = false;

    bool IsInAtkDistance = false;

    bool IsStopMove = true;

    [Header("提高硬直")]
    public float AddYZ = 0;

    [Header(">提高硬直 持续时间")]
    public float AddYZTimes = 0;



    //[Header("技能发动 的声音")]
    //public AudioSource AudioFadong;


    protected GameObject _player;

    // Start is called before the first frame update
    protected virtual  void Start()
    {
        if (IsGetBody) {
            //print("  释放技能 ??  start! ");
            _gameBody = GetComponent<GameBody>();
            _gameBody.GetDB().AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("?");
        GetUpDate();
    }

    [Header("直接释放技能")]
    public bool IsZhiJieShiFangJN = false;


    //bool IsStartAtkAC = false;

    protected virtual void GetUpDate()
    {
        if (!IsStarting) return;
        //print("---------->shi fang jineng !!!!!!!");
        


        if (GetComponent<AIBase>().IsTuihuiFangshouquing)
        {
            ReSetAll();
            print("  yueguangzhan tuihuifangshouqu!!!!!! ");
            return;
        }

        if (GetComponent<RoleDate>().isBeHiting|| GetComponent<RoleDate>().isDie)
        {
            print("释放技能 被攻击取消动作  ！！！！！！！！！！");
            ReSetAll();
            return;
        }


        //print("0   IsStarting "+ IsStarting);
        if (IsStarting)
        {
            //直接释放技能 不用走近
            if (IsZhiJieShiFangJN)
            {
                if (!IsInAtkDistance)
                {
                    IsInAtkDistance = true;
                    print("------->  直接释放 ");
                    //NearRoleInDistance(AtkDistance, AtkDistanceY);
                    if (IsStopMove) StopMove();
                    GetAC();
                    //_gameBody.isAcing = true;
                    //IsStartAtkAC = true;
                }
                //return;
            }

            //print("------------------>shifangjineng  start!!!!!");

            //if (TXName == "TX_LuanRen") print("  ???? gameBody speed   " + _gameBody.GetComponent<Rigidbody2D>().velocity+ "   IsInAtkDistance  "+ IsInAtkDistance);
            //if (IsInAtkDistance)
            //{
            //    print("  --------->  stop!!!!!!  ");
            //    StopMove();
                
            //}
            //print("2");
            if (!IsInAtkDistance)
            {
                //print("3");
                if (NearRoleInDistance(AtkDistance, AtkDistanceY))
                {
                    //print("4");
                    IsInAtkDistance = true;
                    //print("txname   "+TXName);
                    //停止速度 防止滑动
                    if(IsStopMove) StopMove();
                    GetAC();
                }
            }else
            {
                
                if (!_gameBody.isAcing)
                {
                    //角色动作走完
                    IsStarting = false;
                    testnums = 0;
                    //print("5 zhijiezouwan ac fang jineng");
                }
            }
           


        }

        //if(_gameBody) print("_gameBody.GetDB().animation.timeScale3 :       " + _gameBody.GetDB().animation.timeScale);
    }

    void StopMove()
    {
        //print("   ？？？？stopmove！！！！！  ");
        //_gameBody.SetXSpeedZero();
        _gameBody.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

    [Header("释放动作的 声音")]
    public AudioSource ACAudio;

    //bool isHasPlayerACAudio = false;

    protected virtual void GetAC()
    {
        //判断有没有 攻击动作 没有直接退出
        if (!_gameBody.GetDB().animation.HasAnimation(ACName))
        {
            print("没有攻击动作 直接退出！！");
            ReSetAll();
            return;
        }

        //防止 侦听 进入 gamebody
        _gameBody.IsSFSkill = true;
        
        //转向


        //收招延迟时间
        if (ACOverYCTimes != 0) _gameBody.GetACMsgOverYC(ACOverYCTimes);

        //角色 释放动作
        print(" ********************************************************** @@@@@@@@@@@@@@@   acName    "+ACName);
        _gameBody.GetAcMsg(ACName);
        //if (!isHasPlayerACAudio)
        //{
        //    isHasPlayerACAudio = true;
            
        //}
        if (ACAudio) ACAudio.Play();
        //print("  临时 提高硬直   "+AddYZ);
        if (AddYZ!=0) GetComponent<TempAddValues>().TempAddYZ(AddYZ, AddYZTimes);

        //起始效果 提示音 光 提示特效 等

        //这里是否有 什么释放动作  慢动作 减速 +硬直 等   地上的前置特效

        //起手慢动作

        if (qishoudongzuoyanchi!=0) _gameBody.GetPause(qishoudongzuoyanchi, 0);
        //print("_gameBody.GetDB().animation.timeScale:       "+ _gameBody.GetDB().animation.timeScale);
        //_gameBody.GetDB().animation.timeScale = 0;
        //print("_gameBody.GetDB().animation.timeScale2 :       " + _gameBody.GetDB().animation.timeScale);
    }



    public virtual void GetStart(GameObject obj)
    {
        _player = obj;
        //print("?????   " + obj);
        //进入 攻击距离
        IsStarting = true;
        if (SFTX) {
            //print(" 播放特效啊 ！！！！！！！！！！！！！！！！！！！！！！！！ ");
            SFTX.Play();
        }
        
    }

    public ParticleSystem SFTX;

    public virtual bool IsGetOver() {


        //print(">>>>>>>_gameBody.isAcing:     "+ _gameBody.isAcing);
        //if (IsStartAtkAC &&!_gameBody.isAcing)
        //{
        //    //角色动作走完
        //    IsStarting = false;
        //    testnums = 0;
        //    print("5 zhijiezouwan ac fang jineng");
        //}


        if (!IsStarting)
        {
            if (SFTX) SFTX.Stop();
            ReSetAll();
            //print("  IsStarting  over ////////////////    " + IsStarting);
        }
        return !IsStarting;
    }



    public virtual void ReSetAll()
    {
        print("技能释放 resetAll!!!!!!!!!!!");
        _gameBody.IsSFSkill = false;
        IsInAtkDistance = false;
        IsStarting = false;
        GetComponent<GameBody>().isAcing = false;
    }



    protected int testnums = 0;
    protected virtual void ShowACTX(string type, EventObject eventObject)
    {
        testnums++;
        //print("type:  "+type);
        //print("eventObject  ????  " + eventObject);
        if(IsStarting)print( testnums + "  ___________________________________________________________________________________________________________________________name    "+ eventObject.name);
       
        

        if (type == EventObject.FRAME_EVENT)
        {
            if (!IsStarting) return;
            if (eventObject.name == "ac")
            {
                print("?? 特效TXName " + TXName);
                if (TXName == "diandings")
                {
                    return;
                }

                GetComponent<ShowOutSkill>().ShowOutSkillByName(TXName, true);
                
                OtherTX();


            }
        }

    }


    //衍生效果 震动  时间线减慢 等等 可以继承到后面写
    protected virtual void OtherTX()
    {

    }


    private void OnDisable()
    {
        print("释放技能类 销毁");
        if(_gameBody) _gameBody.GetDB().RemoveDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
    }



    //上下 的时候 碰撞检测
    bool IsHitUpOrDown()
    {
      
        return GetComponent<AIAirRunAway>().IsHitDown;
    }


    public virtual bool NearRoleInDistance(float distance,float distanceY = 0)
    {
        if (_player == null) return true;

        if (_player.GetComponent<RoleDate>().isDie) {
            ReSetAll();
            return false;
        }
        



        float dyU = _player.transform.position.y + 2;
        float dyD = _player.transform.position.y - 2;

        if (distanceY != 0)
        {
            dyU = _player.transform.position.y + 2 + distanceY;
            dyD = _player.transform.position.y - 2 + distanceY;
        }

        if (GetComponent<RoleDate>().IsAirEnemy)
        {

            //先找点

            if (transform.position.y >= dyU)
            {
                
                GetComponent<AirGameBody>().RunY(-GetComponent<AirGameBody>().moveSpeedY);
                if (GetComponent<AIAirRunAway>().IsHitDown)
                {
                    //直接取消动作
                    GetComponent<AIAirBase>().QuXiaoAC();
                }
                return false;
            }
            else if (transform.position.y <= dyD)
            {
                
                GetComponent<AirGameBody>().RunY(GetComponent<AirGameBody>().moveSpeedY);
                if (GetComponent<AIAirRunAway>().IsHitTop)
                {
                    //直接取消动作
                    GetComponent<AIAirBase>().QuXiaoAC();
                }
                return false;
            }

        }
       
        //print("  --------->distance    " + distance+"       "+NearSpeed+"   ?????    "+(_player.transform.position.x - transform.position.x));
        if (_player.transform.position.x - transform.position.x >= distance)
        {
            //print("右跑！");
            //目标在右
            _gameBody.RunRight(NearSpeed);
            return false;
        }
        else if (_player.transform.position.x - transform.position.x <= -distance)
        {
            //print("左 跑！");
            //目标在左
            if (GetComponent<AirGameBody>())
            {
                _gameBody.RunLeft(NearSpeed);
            }
            else
            {
                _gameBody.RunLeft(-NearSpeed);
            }
            
            return false;
        }
        else
        {
            return true;
        }
    }

}
