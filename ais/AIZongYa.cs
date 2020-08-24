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
        if (IsNeedCheckDiBan && DiBan && DiBan.activeSelf && GetComponent<RoleDate>().live <= ZhenSuiDiBanXueLiang) return true;
        return false;
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
        _airGameBody = GetComponent<AirGameBody>();
        StartCS();
        print(" 调用 纵压   "+ _isGoToStartPos);
    }

    [Header("高度限制点")]
    public Transform GaoXZPos;

    //1.获取初始点
    void StartCS()
    {
        //初始点 限制高度
        //判断 玩家y超过了限制高度 就 直接结束
        GoToPos = GetStartPos();
        if (GoToPos == new Vector2(1000, 1000))
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

        _isHasYaXia = false;
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
        print(" zhongya 1  "+ _isGoToStartPos);
        if (GetComponent<AirGameBody>().IsHitWall || GoToMoveToPoint(GoToPos, 0.5f, 8))
        {
           



            ReSetAll();
            GetXiaYaPos();
            _isXiaYaQSACing = true;
            //_isGetOver = true;
            print(" ---------------------------------------------- 到达指定 的高空！！！！ ");
        }

        RongCuo();

    }

    Vector2 _XiaYaPos;
    void GetXiaYaPos()
    {
        _XiaYaPos = new Vector2(_player.transform.position.x, this.transform.position.y - 40);
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
        if (!GetComponent<AirGameBody>().isAcing)
        {
            GetComponent<AirGameBody>().GetAcMsg(QiShouACName);
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
            
            _startXiaYa = true;
        }
    }



    [Header("在 角色 上方的 Y距离")]
    public float TheDistanceYToPlayer = 9;
    Vector2 GoToPos = new Vector2(1000, 1000);
    Vector2 GetStartPos()
    {
        if (_player == null) return GoToPos;
        float __y = GaoXZPos.transform.position.y > _player.transform.position.y + TheDistanceYToPlayer ? _player.transform.position.y + TheDistanceYToPlayer : GaoXZPos.transform.position.y;
        return new Vector2(_player.transform.position.x, __y);
    }





    bool _startXiaYa = false;
    bool _isHasYaXia = false;

    [Header("下压特效名字")]
    public string TX_XiaLuoYanAME = "tx_xialuozhendongYC";

    [Header("下压后 停顿的时间")]
    public float hasXiaYaDJS = 0.3f;
    float xiaYaJS = 0f;
    void GetXiaYa()
    {
        if (_isHasYaXia)
        {

            xiaYaJS += Time.deltaTime;
            if(xiaYaJS>= hasXiaYaDJS)
            {
                xiaYaJS = 0;
                ReSetAll();
                _isGetOver = true;
            }
            return;
        }

        GameObject yanchenHitKuai;
        if (GetComponent<AirGameBody>().IsHitDown|| GoToMoveToPoint(_XiaYaPos,0.5f,4))
        {
           
            _isHasYaXia = true;
            if (GetComponent<AirGameBody>().IsHitDown)
            {
                GameObject yc;
                Vector2 _pos = this.transform.position;
                yc = Resources.Load(TX_XiaLuoYanAME) as GameObject;
                yanchenHitKuai = ObjectPools.GetInstance().SwpanObject2(yc);
                yanchenHitKuai.transform.parent = this.transform.parent;
                yanchenHitKuai.GetComponent<JN_base>().GetPositionAndTeam(_pos, this.GetComponent<RoleDate>().team, 1, this.gameObject, true);
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);
                print("@@@@@@@@@@@ 进来几次 ");

                GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;

                if(IsNeedZhenSuiDiBan()){
                    //进入 击碎地板的  流程
                    if (DiBan) {
                        DiBan.GetComponent<XSDiban>().BeStart();
                    } 
                }


            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RoleDate>().isBeHiting || GetComponent<RoleDate>().isDie || (_player && _player.GetComponent<RoleDate>().isDie))
        {
            ReSetAll();
            _isGetOver = true;
            return;
        }


        if (_isGoToStartPos) GoToStartPos();
        if (_isXiaYaQSACing) XiaYaQSAC();
        if (_startXiaYa) GetXiaYa();
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

        //距离小于 误差内 直接结束
        if ((thisV2 - point).sqrMagnitude < inDistance)
        {
            this.GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
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
