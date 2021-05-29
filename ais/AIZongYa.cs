using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZongYa : MonoBehaviour,ISkill
{
    [Header("是否需要判断 击碎地板")]
    public bool IsNeedCheckDiBan = false;
    [Header("下压 可以 压碎 的 地板")]
    public GameObject DiBan;
    [Header("血量 低于多少的时候 可以压碎地板")]
    public float ZhenSuiDiBanXueLiang = 0;

    bool IsNeedZhenSuiDiBan()
    {
        if (IsNeedCheckDiBan && DiBan && DiBan.activeSelf && _roleDate.live <= ZhenSuiDiBanXueLiang) return true;
        return false;
    }

    [Header("砸地面的 音效")]
    public AudioSource S_ZaDiMian;

    [Header("飞行冲刺 音效")]
    public AudioSource S_FeiXingChongCi;


    [Header("开始技能前的 叫声 音效")]
    public AudioSource S_Start;



    [Header("中压 的 冲击距离")]
    public float ZhongYaDistance = 0;
    //冲击 起始点
    Vector2 ZhongYaStartPos = new Vector2(1000,1000);
    bool IsOutZhongYaDistance()
    {
        if (ZhongYaDistance == 0) return false;

        if(ZhongYaStartPos == new Vector2(1000, 1000))
        {
            ZhongYaStartPos = this.transform.position;
            return false;
        }


        float YaDis  = Mathf.Abs(this.transform.position.y - ZhongYaStartPos.y);
        //print("压到的距离  "+YaDis);

        if (YaDis >= ZhongYaDistance) return true;
        return false;
    }


    [Header("是否需要 提前探测 能否下压  能压到地板就下压 不能就提前取消")]
    public bool IsNeedTest = false;

    bool IsTestHitGround(Vector2 startPst)
    {
        if (!IsNeedTest) return false;
        if (ZhongYaDistance == 0) return false;
        Vector2 end = new Vector2(startPst.x, startPst.y - ZhongYaDistance-3);
        Debug.DrawLine(startPst, end, Color.red);
        bool grounded = Physics2D.Linecast(startPst, end, GetComponent<GameBody>().groundLayer);
        //if (!grounded)
        //{
        //    print("   没有触碰到地板 直接取消 冲压 ");
        //}
        return !grounded;
    }


    GameObject _player;
    public void GetStart(GameObject gameObj)
    {
        if (IsNeedCheckDiBan)
        {
            if (!DiBan||!DiBan.activeSelf || DiBan.GetComponent<XSDiban>().IsHideFloor)
            {
                _isGetOver = true;
                return;
            }
            
        }
        _isGetOver = false;
        _player = gameObj;
        _jiadongzuoNums = JiaDongZuoNums;
        //_airGameBody = GetComponent<AirGameBody>();
        StartCS();
        //print(" 调用 纵压   "+ _isGoToStartPos);
    }

    [Header("高度限制点")]
    public Transform GaoXZPos;

    //1.获取初始点
    void StartCS()
    {
        //初始点 限制高度
        //判断 玩家y超过了限制高度 就 直接结束
        GoToPos = GetStartPos();
        if (GoToPos == new Vector2(1000, 1000)||IsTestHitGround(GoToPos))
        {
            _isGetOver = true;
            return;
        }
        zhuijiRun();
        _isGoToStartPos = true;
    }

    bool _isGetOver = false;

    public bool IsGetOver()
    {
        return _isGetOver;
    }

    public void ReSetAll()
    {
        _isGetOver = false;
        _isGoToStartPos = false;
        _isXiaYaQSACing = false;
        _startXiaYa = false;
        
        rongcuoNum = 0;
        _QSYCTime = 0;
        _airRunNear.ResetAll();

        _isHasYaXia = false;
        ZhongYaStartPos = new Vector2(1000, 1000);
        if (GetComponent<JN_Date>()) GetComponent<JN_Date>().HitInSpecialEffectsType = 5;

        IsHasZhengdongStop = false;
        xiaYaJS = 0;
    }


    [Header("是否 有 再次 下压瞄准")]
    public bool IsHasJiaDongZuo = false;
    [Header("是否 有 再次 下压瞄准 几率")]
    public float JiaDongZuoJL = 100;
    [Header("是否 有 再次 下压瞄准 次数")]
    public int JiaDongZuoNums = 1;
    int _jiadongzuoNums = 0;


    bool _isGoToStartPos = false;
    //2.移动到选取的 高空点
    void GoToStartPos()
    {
        //print(" zhongya 1  "+ _isGoToStartPos);
        if (_airGameBody.IsHitWall || GoToMoveToPoint(GoToPos, 0.5f, 8))
        {
            ReSetAll();
            GetXiaYaPos();
            _isXiaYaQSACing = true;
            //_isGetOver = true;
            //print(" ---------------------------------------------- 到达指定 的高空！！！！ ");
        }

        RongCuo();

    }

    [Header("如果没有 压到地板 下降的深度")]
    public float XiaYaY = 40;

    Vector2 _XiaYaPos;
    void GetXiaYaPos()
    {
        _XiaYaPos = new Vector2(_player.transform.position.x, this.transform.position.y - XiaYaY);
    }


    float rongcuoNum = 0;
    void RongCuo()
    {
        rongcuoNum += Time.deltaTime;
        if (rongcuoNum >= 2f)
        {
            rongcuoNum = 0;
            print("容错 重启");
            //容错 可以再重选点  可以限制次数
            ReSetAll();
            _isGetOver = true;
        }
    }



    bool _isXiaYaQSACing = false;
    float _QSYCTime = 0;
    [Header("开始下压的时候 的起手延迟时间")]
    public float _QSYCNums = 0.3f;
    [Header("开始下压的时候 的起手延迟动作名字")]
    string QiShouACName = "skillBegin_1";
    //下压的 起始动作
    void XiaYaQSAC()
    {
        _QSYCTime += Time.deltaTime;
        if (!_airGameBody.isAcing)
        {
            _airGameBody.GetAcMsg(QiShouACName);
            
        }


        if (_QSYCTime >= _QSYCNums)
        {
            _QSYCTime = 0;

            ReSetAll();
            //假动作
            if (IsHasJiaDongZuo && _jiadongzuoNums > 0)
            {
                if (GlobalTools.GetRandomNum() <= JiaDongZuoJL)
                {
                    print("假动作  再次瞄准");
                    _jiadongzuoNums--;
                    StartCS();
                    return;
                }
            }

            //_isXiaYaQSACing = false;

            if (!_startXiaYa)
            {
                _startXiaYa = true;
                if (S_Start) S_Start.Play();
                if (GetComponent<JN_Date>()) GetComponent<JN_Date>().HitInSpecialEffectsType = 1;
                if (S_FeiXingChongCi) S_FeiXingChongCi.Play();
            }
            
        }
    }


    [Header("在 角色 上方的 Y距离")]
    public float TheDistanceYToPlayer = 9;
    Vector2 GoToPos = new Vector2(1000, 1000);
    Vector2 GetStartPos()
    {
        if (_player == null) return GoToPos;
        float __y = 0;

        if (GaoXZPos)
        {
            __y = GaoXZPos.transform.position.y > _player.transform.position.y + TheDistanceYToPlayer ? _player.transform.position.y + TheDistanceYToPlayer : GaoXZPos.transform.position.y;
        }
        else
        {
            __y = _player.transform.position.y + TheDistanceYToPlayer;
        }
        
        return new Vector2(_player.transform.position.x, __y);
    }





    bool _startXiaYa = false;
    bool _isHasYaXia = false;

    [Header("下压特效名字")]
    public string TX_XiaLuoYanAME = "tx_xialuozhendongYC";

    [Header("下压后 停顿的时间")]
    public float hasXiaYaDJS = 0.3f;

    [Header("下压速度")]
    public float ZYSpeed = 4;

    [Header("下压到顶临界距离")]
    public float YInDiatance = 0.5f;


    bool IsHasZhengdongStop = false;
    float yazhuTime = 0;
    [Header("下压后 停顿时间")]
    public float YaZhuStopTime = 0;

    [Header("下压是否需要判断 终端速度 提前结束")]
    public bool IsNeedCheckSpeed = true;


    float xiaYaJS = 0f;
    void GetXiaYa()
    {
        if (IsOutZhongYaDistance())
        {
            xiaYaJS = 0;
            ReSetAll();
            _isGetOver = true;
            return;
        }



        if (_isHasYaXia)
        {

            xiaYaJS += Time.deltaTime;
            if (xiaYaJS>= hasXiaYaDJS)
            {
                xiaYaJS = 0;
                ReSetAll();
                _isGetOver = true;
            }
            return;
        }

        GameObject yanchenHitKuai;
        bool _IsHitDown = _airGameBody.IsHitDown;
        if (_IsHitDown  ||GoToMoveToPoint(_XiaYaPos, YInDiatance, ZYSpeed))
        {

            if (IsHasZhengdongStop)
            {
                yazhuTime += Time.deltaTime;
                if (yazhuTime >= YaZhuStopTime)
                {
                    ReSetAll();
                    _isGetOver = true;
                    yazhuTime = 0;
                }
                return;
            }


            if (_IsHitDown)
            {
                float _speedY = _airGameBody.GetPlayerRigidbody2D().velocity.y;
                //print(" @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ ysudu     " + _speedY);
               
               
                //如果下压 速度小于10  就不能 激起震动伤害
                if (IsNeedCheckSpeed&&_speedY > -10) {
                    ReSetAll();
                    _isGetOver = true;
                    return;
                }
                


                

                //烟尘
                GameObject yc;
                Vector2 _pos = this.transform.position;
                yc = Resources.Load(TX_XiaLuoYanAME) as GameObject;
                yanchenHitKuai = ObjectPools.GetInstance().SwpanObject2(yc);
                yanchenHitKuai.transform.parent = this.transform.parent;
                yanchenHitKuai.GetComponent<JN_base>().GetPositionAndTeam(_pos, _roleDate.team, 1, this.gameObject, true);
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);
                print("@@@@@@@@@@@ 进来几次 ");


                if (S_ZaDiMian) S_ZaDiMian.Play();

                _airGameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;

                if(IsNeedZhenSuiDiBan()){
                    //进入 击碎地板的  流程
                    if (DiBan) {
                        DiBan.GetComponent<XSDiban>().BeStart();
                    } 
                }

                IsHasZhengdongStop = true;
            }
        }
    }


    RoleDate _roleDate;
    AIAirRunNear _airRunNear;
    // Start is called before the first frame update
    void Start()
    {
        _airGameBody = GetComponent<AirGameBody>();
        _roleDate = GetComponent<RoleDate>();
        _airRunNear = GetComponent<AIAirRunNear>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_roleDate.isBeHiting || _roleDate.isDie || (_player && _player.GetComponent<RoleDate>().isDie))
        {
            ReSetAll();
            _isGetOver = true;
            return;
        }


        if (_isGoToStartPos) GoToStartPos();
        if (_isXiaYaQSACing) XiaYaQSAC();
        if (_startXiaYa) GetXiaYa();
    }

    [Header("下压的 最大Y速度")]
    public float XiaYaMaxSpeedY = 0;

    public bool GoToMoveToPoint(Vector2 point, float inDistance = 0, float TempSpeed = 0)
    {
        zhuijiRun();
        
        Vector2 thisV2 = this.transform.position;
        Vector2 v2 = (point - thisV2) * TempSpeed;

        if (XiaYaMaxSpeedY != 0)
        {
            if (Mathf.Abs(v2.y) > XiaYaMaxSpeedY)
            {
                v2.y = v2.y>0? XiaYaMaxSpeedY : -XiaYaMaxSpeedY;
            }
        }

        //print("??>>>>>>>>>>>>>>>>>      "+v2);

        _airGameBody.GetPlayerRigidbody2D().velocity = v2;
        _airGameBody.IsJiasu = true;

        if (_airGameBody.IsHitWall)
        {
            return true;
        }

        //距离小于 误差内 直接结束
        if ((thisV2 - point).sqrMagnitude < inDistance)
        {
            _airGameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
            return true;
        }
        return false;
    }

    bool _isStartChongJi = false;
    AirGameBody _airGameBody;
    void zhuijiRun()
    {
        if (!_isGoToStartPos&&!_isStartChongJi&& _player)
        {
            if (this.transform.position.x < _player.transform.position.x)
            {
                _airGameBody.TurnRight();
            }
            else
            {
                _airGameBody.TurnLeft();
            }
        }


        if (GetComponent<AIChongji>() && GetComponent<AIChongji>().isTanSheing) return;
        _airGameBody.isRunYing = true;
        _airGameBody.Run();

    }
}
