using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHengXiangChongZhuang : MonoBehaviour,ISkill
{

    protected GameObject _player;
    protected AirGameBody _airGameBody;

    public void GetStart(GameObject gameObj)
    {
        ReSetAll();
        _player = gameObj;
        _airGameBody = GetComponent<AirGameBody>();
        StartCZ();
    }


    public bool _isGetOver = false;
    public bool IsGetOver()
    {
        //throw new System.NotImplementedException();
        return _isGetOver;
    }

    public void ReSetAll()
    {
        _isGetOver = false;
        _isGoToPos = false;
        _isStartChongJi = false;
        _isStartChongJi = false;
        _isChongJiQiShou = false;
        _isHitWall = false;
        _isHitWallOver = false;
        _rongCuoNums = 0;
        _jishiTime = 0;
        JSNums = 0;

        //GetComponent<AIAirBase>().IsCanZhuangXiang = true;
        GetComponent<GameBody>().RunACChange("stand_1");
    }


    protected Vector2 startPos = Vector2.zero;


    //1.取到 移动过去准备冲撞的 点
    void StartCZ()
    {
        startPos = GetCanGoToPos();
        //print(" 获取的 初始 位置  "+startPos);
        if (startPos == new Vector2(1000, 1000))
        {
            ReSetAll();
            _isGetOver = true;
        }
        _isGoToPos = true;
    }

    protected bool _isGoToPos = false;
    //移动到 起始 点
    protected virtual void GoToPos()
    {

        if (_isChongJiQiShou)
        {
            ChongJiQiShou();
            return;
        }


        //这里要加判断 是否 碰到 地面 或者机关
        if (GetComponent<AirGameBody>().IsHitDown || GoToMoveToPoint(startPos, 0.5f, 2))
        {
            //print("我到达目的地了!!!startPos    "+ startPos+"  我的 位置是？ "+ this.transform.position+"  是否撞墙  "+ GetComponent<AirGameBody>().IsHitDown);
            //_isGetOver = true;
            
            //获取 冲击的 点
            if (this.transform.position.x > CenterPos.position.x)
            {
                if (this.transform.position.x< _player.transform.position.x)
                {
                    ReSetAll();
                    _isGetOver = true;
                    return;
                }

                ChongJiPos = new Vector2(this.transform.position.x - CJDistance, this.transform.position.y);
                _airGameBody.TurnLeft();
            }
            else {
                if (this.transform.position.x > _player.transform.position.x)
                {
                    ReSetAll();
                    _isGetOver = true;
                    return;
                }

                ChongJiPos = new Vector2(this.transform.position.x + CJDistance, this.transform.position.y);
                _airGameBody.TurnRight();
            }


            _isChongJiQiShou = true;
            //_isStartChongJi = true;
        }
    }


    //起手动作 和 延迟时间
    protected bool _isChongJiQiShou = false;
    [Header("起手延迟时间 时间过了 才能冲出去")]
    public float QiShouYCTime = 0.5f;
    float _jishiTime = 0;
    [Header("起手动作 名字")]
    public string QiShiACName = "run_3";
    protected void ChongJiQiShou()
    {

        _jishiTime += Time.deltaTime;
        if (!GetComponent<GameBody>().isAcing)
        {
            GetComponent<GameBody>().RunACChange(QiShiACName);
            GetComponent<GameBody>().GetAcMsg(QiShiACName);
        }
        
        if (_jishiTime>= QiShouYCTime)
        {
            GetComponent<GameBody>().isAcing = false;
            _isChongJiQiShou = false;
            _isGoToPos = false;
            _isStartChongJi = true;
        }
    }



    protected Vector2 ChongJiPos;
    [Header("横向 冲击的 距离 ")]
    public float CJDistance = 10;
    [Header("横向 冲击的 速度 : ")]
    public float CJSpeedX = 3;
    bool _isStartChongJi = false;

    bool _isHitWall = false;
    GameObject yanchenHitKuai;


    float _rongCuoNums = 0;

    void RongCuo()
    {
        _rongCuoNums += Time.deltaTime;
        if (_rongCuoNums >= 5)
        {
            ReSetAll();
            _isGetOver = true;
        }
    }


    float JSNums = 0;
    bool _isHitWallOver = false;
    void HengXiangChongJi()
    {
        //print("shifou jinru chongji !  CJSpeedX？  " + CJSpeedX);
        RongCuo();

        if (_isHitWallOver)
        {
            JSNums += Time.deltaTime;
            print(1);
            if (JSNums > 0.3f)
            {
                JSNums = 0;
                _isStartChongJi = false;
                ReSetAll();
                _isGetOver = true;
                //GetComponent<AIAirBase>().IsCanZhuangXiang = true;
                GetComponent<GameBody>().RunACChange("stand_1");
            }

            return;
        }



        //print("开始 横向 冲击  ！！！！！！！！！");
        if (GetComponent<AirGameBody>().IsHitWall || GoToMoveToPoint(ChongJiPos, 1f, CJSpeedX)) {

            print(2);

            if (GetComponent<AirGameBody>().IsHitWall)
            {
                print(3);
                _isHitWallOver = true;
            }
            else
            {
                //print("4   冲击 没有 撞墙 提前结束！！！！");
                _isStartChongJi = false;
                ReSetAll();
                _isGetOver = true;
                //GetComponent<AIAirBase>().IsCanZhuangXiang = true;
                GetComponent<GameBody>().RunACChange("stand_1");
            }
        }

        if (GetComponent<AirGameBody>().IsHitWall)
        {
            //print("冲击 撞墙了！！！！！");
            print(5);
            //震动的 烟尘
            //是否 有 两边扩展
            if (!_isHitWall)
            {
                //print("     --------------------------------------------------------------------------   ????? 没有进来这里     ");
                _isHitWall = true;
                //出烟尘 特效  震动
                GameObject yc;
                Vector2 _pos = this.transform.position;
                if (this.transform.localScale.x > 0)
                {
                    _pos = new Vector2(this.transform.position.x -0.4f, this.transform.position.y);
                    yc = Resources.Load("tx_ChongJiZDYC") as GameObject;
                }
                else
                {
                    _pos = new Vector2(this.transform.position.x+0.6f,this.transform.position.y);
                    yc = Resources.Load("tx_ChongJiZDYCY") as GameObject;
                }
                yanchenHitKuai = ObjectPools.GetInstance().SwpanObject2(yc);
                //yanchenHitKuai.transform.position = this.transform.position;
                //yanchenHitKuai.transform.parent = this.transform.parent.transform.parent;
                yanchenHitKuai.transform.parent = this.transform.parent;

                //print("不停 在进入这个地方吗 ？？？    "+yanchenHitKuai.transform.position);



                yanchenHitKuai.GetComponent<JN_base>().GetPositionAndTeam(_pos, this.GetComponent<RoleDate>().team, 1, this.gameObject,true);
                //print("烟尘特效Y----》@@@@@@@@    "+ yanchenHitKuai.transform.position.y);
                //Time.timeScale = 0.1f;
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.6"), this); // removeEventListener(EventTypeName.CAMERA_SHOCK, this.GetShock);
            }

        }
    }




    [Header("中心点位置")]
    public Transform CenterPos;

    [Header("离中心点的X方向 距离")]
    public float ToCenterPosXDistance = 0;

    //移动到 横向可以冲击额地方
    protected virtual Vector2 GetCanGoToPos()
    {
        //GetComponent<AIAirRunNear>()

        //判断左右 角色是在 中心点 左边 还是我右边  然后 找到同Y点 如果过去 底部碰撞 就算到达位置
        if (!_player)
        {
            return new Vector2(1000, 1000);
        }


        if (_player.transform.position.x > CenterPos.position.x)
        {
            return new Vector2(CenterPos.position.x - ToCenterPosXDistance, CenterPos.position.y);
        }
        else
        {
            return new Vector2(CenterPos.position.x + ToCenterPosXDistance, CenterPos.position.y);
        }

        
    }




    public bool GoToMoveToPoint(Vector2 point, float inDistance = 0, float TempSpeed = 0)
    {
        zhuijiRun();

        Vector2 thisV2 = this.transform.position;
        Vector2 v2 = (point - thisV2) * TempSpeed;

        this.GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = v2;
        this.GetComponent<AirGameBody>().IsJiasu = true;

        if (GetComponent<AirGameBody>().IsHitWall)
        {
            return true;
        }

        //这里要做预判


        //距离小于 误差内 直接结束
        if ((thisV2 - point).sqrMagnitude < inDistance)
        {
            //print(" thisV2  " + thisV2 + "    toPos   " + point);
            //print(" inDistance "+ inDistance);
            this.GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            return true;
        }
        return false;
    }


    void zhuijiRun()
    {
        if (!_isStartChongJi && _player)
        {
            if (this.transform.position.x < _player.transform.position.x)
            {
                //print("turn right");
                _airGameBody.TurnRight();
            }
            else
            {
                //print("turn left");
                _airGameBody.TurnLeft();
            }
        }
     

        if (GetComponent<AIChongji>() && GetComponent<AIChongji>().isTanSheing) return;
        GetComponent<AirGameBody>().isRunYing = true;
        _airGameBody.Run();

    }



    //冲击

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RoleDate>().isBeHiting|| GetComponent<RoleDate>().isDie)
        {
            _isGetOver = true;
            return;
        }


        if(_isGoToPos)GoToPos();
        if (_isStartChongJi) HengXiangChongJi();
    }
}
