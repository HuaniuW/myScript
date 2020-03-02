using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AIZiDan : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();
        _gameBody.GetDB().AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        _pointDistration = GetComponent<PointDistaction>();
        _runNear = GetComponent<AIAirRunNear>();
        _runAway = GetComponent<AIAirRunAway>();
        _behavior = GetComponent<AIAirBehavior>();
    }

    GameObject _targetObj;
    public UnityEngine.Transform zidanDian;
    GameBody _gameBody;
    string ACName = "atk_zd_1";
    bool _isBehaviorOver = false;
    bool _isStart = false;

    [Header("进入Y指定距离 才能射击")]
    public float InDistanceY = 3;
    [Header("追击进入的 攻击距离")]
    public float AtkDistance = 15;

    public float Radios = 1;

    PointDistaction _pointDistration;
    AIAirRunNear _runNear;
    AIAirRunAway _runAway;
    AIAirBehavior _behavior;


    bool IsFindRoading = false;
    bool IsGetFireTest = false;




    Vector2 newPoint = Vector2.zero;
    bool IsGoToNewPoint = false;

    [Header("是否在地面的怪")]
    public bool IsDimian = false;

    void ReSetAll()
    {
        _isBehaviorOver = false;
        IsFindRoading = false;
        IsGetFireTest = false;

        newPoint = Vector2.zero;
        IsGoToNewPoint = false;
        
        _isStart = false;

        if(_behavior) _behavior.StopJS();
    }

    //找到目标点  移动到攻击范围
    public void GetStart(GameObject targetObj)
    {
        _targetObj = targetObj;
        _isBehaviorOver = false;
        IsFindRoading = true;
        _isStart = true;
        //print("---------------------------------------------------> 子弹！！！！！！！");
    }

    public bool IsBehaviorOver()
    {
        return _isBehaviorOver;
    }

   
    void Starting()
    {

        if (IsDimian)
        {
            GetComponent<GameBody>().TurnDirection(_targetObj);
            GetFire();
            return;
        }

        if (IsGoToNewPoint)
        {
            print("-------------------------------> 对点");
            GoToNewPoint();
            return;
        }


        //选点 1.在射程内 2.y距离在一定范围内  如果Y超了怎么办 上下移动 如果碰到墙 退出   太近 又要远离
        //判断是否进入射程
        if (IsFindRoading)
        {
            //print("-------------------------------> 一般寻路！！！！！！！！！");
            GoToNewPoint();
            return;
        }
        if (IsGetFireTest)
        {
            IsGetFireTest = false;
            //判断 和目标间 是否 有障碍物
            if(_pointDistration.IsTwoPointLineHitObj(_targetObj.transform.position, this.transform.position))
            {
                //找新点
                print("找新点！！！");
                GetToNewPoint();
                return;
            }
            //print("开火的时候 速度是多少   "+_gameBody.GetPlayerRigidbody2D().velocity);
            //_gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
            //这里做一个缓动减速
            _behavior.StartJS();
            GetComponent<GameBody>().TurnDirection(_targetObj);
            GetFire();
        }
       
    }


    int _zdType = 1;
    int _acType = 1;
    string ZDName = "TX_zidan2";


    public void GetZidanType(string str)
    {
        //判断子弹类型 和 动作类型
        //zidan_1   zidan_2
        //zidan_1_1       第三个1 是动作  atk_zd_+ '1'   如果没有就是 1
        //1.普通子弹  2.高速子弹 3.跟踪子弹 4.3连弹  5.直线弹
        _zdType = int.Parse(str.Split('_')[1]);
        _acType = int.Parse(str.Split('_')[2]);

        ACName = "atk_zd_" + _acType;
        ZDName = "TX_zidan" + _zdType;
    }

    void GoToNewPoint()
    {
        //print("距离--------------------------->    "+ (_targetObj.transform.position - this.transform.position).sqrMagnitude);
        //进入2点攻击距离
        if (_targetObj == null) return;
        if (!_pointDistration.IsTwoPointInDistance(_targetObj.transform.position, this.transform.position, AtkDistance))
        {
            if (IsGoToNewPoint) {
                //print(" new Point 坐标寻路 ");
                _runNear.ZhuijiZuoBiao(newPoint, AtkDistance);
            }
            else
            {
                //print(" new Point  ");
                _runNear.Zhuiji(AtkDistance);
            }
            return;
        }

        _runNear.ZJStop();

        //判断Y距离 Y距离要达到攻击范围
        if (!_pointDistration.IsTwoPointInDistanceY(_targetObj.transform.position, this.transform.position, InDistanceY))
        {
            _runNear.GetMoveNearY(InDistanceY);
            //如果Y碰撞
            if (_runAway.IsHitTop || _runAway.IsHitDown)
            {
                print("》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》碰到墙了");
                _isBehaviorOver = true;
                _isStart = false;
            }

            return;
        }
        IsFindRoading = false;
        IsGoToNewPoint = false;
        IsGetFireTest = true;
    }


    void GetToNewPoint()
    {

        newPoint = this.transform.position.x < _targetObj.transform.position.x ? new Vector2(_targetObj.transform.position.x + Mathf.Abs(_targetObj.transform.position.x - this.transform.position.x), this.transform.position.y) : new Vector2(_targetObj.transform.position.x + Mathf.Abs(_targetObj.transform.position.x + this.transform.position.x), this.transform.position.y);

        if (_pointDistration.IsPointCanStay(newPoint, Radios))
        {
            GetFire();
        }
        else
        {
            _isBehaviorOver = true;
        }
        _isStart = false;

    }

    void GetFire()
    {
        _gameBody.GetAcMsg(ACName);
    }


    protected virtual void ShowACTX(string type, EventObject eventObject)
    {
        if (type == EventObject.FRAME_EVENT)
        {
            if (eventObject.name == "zd"|| eventObject.name == "zd2") {
                //闪一下点特效
                GameObject shanGuang = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TX_zidan1shan") as GameObject);  //GlobalTools.GetGameObjectByName("TX_zidan1shan");
                shanGuang.transform.position = zidanDian.position;

                //判断子弹类型 和 动作类型
                //zidan_1   zidan_2
                //zidan_1_1       第三个1 是动作  atk_zd_+ '1'   如果没有就是 1
                //1.普通子弹  2.高速子弹 3.跟踪子弹 4.3连弹  5.直线弹


                GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(ZDName) as GameObject);
                zidan.transform.position = zidanDian.position;
                zidan.transform.localScale = this.transform.localScale;
                //放出子弹 子弹方向   点位置 - 目标位置+ -Y
                //print("发射子弹！！！！！！！！！！！！！！！！！！");
            }
        }
    }

    private void OnDisable()
    {
        print("AIZidan   我被销毁了");
        _gameBody.GetDB().RemoveDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
    }

    //接受事件
    //释放点 
    //特效显示 丢出子弹  子弹碰撞墙和 角色爆炸

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RoleDate>().isBeHiting)
        {
            ReSetAll();
            return;
        }
        if (_isStart)
        {
            Starting();
        }

        if(_gameBody.GetDB().animation.lastAnimationName == ACName&& _gameBody.GetDB().animation.isCompleted)
        {
            ReSetAll();
            _isBehaviorOver = true;
        }
    }
}
