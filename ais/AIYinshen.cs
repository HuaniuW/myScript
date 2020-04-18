using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AIYinshen : MonoBehaviour
{
    GameBody _gameBody;
    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();
        if (!_db) _db = _gameBody.GetDB();
    }



    [Header("是否开启 被攻击 隐身")]
    public bool IsOpenBeHitHide = false;

    [Header("隐身后攻击招式名字")]
    public string HideAtkName = "";

    [Header("被攻击 隐身 几率")]
    public int BeHitHideJL = 30;

    [Header("后退的力量 ")]
    public float houshanValue = 30;

    bool IsBeHitingHide = false;
    //被攻击后 隐身后退
    public void BeHitHide()
    {
        if (!IsOpenBeHitHide) return;   
        if (IsBeHitingHide) return;
        //计算几率
        int jl = GlobalTools.GetRandomNum();
        if (jl<=BeHitHideJL)
        {
            IsBeHitingHide = true;
            IsInBeHitFJ = true;
            //重置 被攻击的判断
            _gameBody.FanJiBeHitReSet();
            //清0速度
            _gameBody.SpeedXStop();
            //后退速度  用后闪的 接口
            _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
            _gameBody.BackJumpVX(houshanValue);
            GetComponent<RoleDate>().isCanBeHit = false;
            //隐身 接口
            print("------------------被攻击后 隐身");
            GetComponent<AIAirBase>().GetAtkFSByName("yinshen");
            ReSetAll();
        }
    }

    [Header("是否 是在 被攻击的 时候反击")]
    public bool IsInBeHitFJ = false;
    //被攻击后 反击 AC
    public void BeHitFanjiAC()
    {
        if (IsInBeHitFJ)
        {
            print("  被攻击 反击   "+ HideAtkName);

            if (HideAtkName != "") {
                GetComponent<AIAirBase>().GetReSet();
                GetComponent<AIAirBase>().GetAtkFSByName(HideAtkName);
            }
            
        }
        else
        {
            print("  自带的 反击 动作   " + ATKACName);
            if (ATKACName != "") {
                GetComponent<AIAirBase>().GetReSet();
                GetComponent<AIAirBase>().GetAtkFSByName(ATKACName);
            }
            
        }
        IsInBeHitFJ = false;
    }


    [Header("自带 的 隐身完后 的 攻击招式")]
    public string ATKACName = "";

    public void ReSetAll()
    {
        ChoseNums = 0;
        showPos = new Vector2(1000, 1000);
        IsOver = true;
        IsHideing = false;
        IsShowing = false;
        IsBeHitingHide = false;
        _gameBody.SetACPlayScale(1);
    }


    public void ReSetDisXY()
    {
        atkDistanceX = 0;
        atkDistanceY = 0;
    }

    //攻击距离  给个可以写的 参数
    [Header("隐身显现后 离玩家 x 的距离")]
    public float atkDistanceX = 0;


    [Header("隐身显现后 离玩家 y 的距离")]
    public float atkDistanceY = 0;


    //隐身 不能被攻击  可不可以被  攻击 ？？？  给个参数 可以选
    [Header("隐身期间 是否能被攻击")]
    public bool IsCanBeHit = true;
    //寻找 主角 周围的 点   一般是前后    上方 按需求 不如 有下压的  可以有上方  上方的 有个 参数给出去

    Vector2 ChoseShowPos()
    {
        return Vector2.zero;
    }

    //找到点 周围没有碰撞 就  跳过去 播放显示 动画

    
    [Header("隐身动作")]
    public string HideACName = "yinshen_1";
    [Header("显身动作")]
    public string ShowACName = "xianshen_1";

    UnityArmatureComponent _db;

    [Header("隐身动作速度")]
    public float YsSpeed = 2;

    bool IsHideing = false;
    void GetHide()
    {

        if (!IsHideing) return;
        //print("  hide ????  ");
        if(_db.animation.lastAnimationName == HideACName && _db.animation.isCompleted)
        {
            IsHideing = false;
            _db.animation.Stop();
            //开始显示流程
            GetShow();
            return;
        }

        if(_db.animation.lastAnimationName!= HideACName)
        {
            //print("yinshenAC!");
            //_db.animation.GotoAndPlayByFrame(HideACName, 0, 1);
            _gameBody.GetAcMsg(HideACName);
            _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
            _gameBody.SetACPlayScale(YsSpeed);
            if (!IsCanBeHit)
            {
                GetComponent<RoleDate>().isCanBeHit = false;
            }
        }
        
    }

    Vector2 showPos = new Vector2(1000,1000);
    float hitDiBanDistance = 0.4f;
    AIAirRunNear _airRun;
    //计数2次 多次调用自己
    int ChoseNums = 0;
    void GetShow()
    {
        
        if (!_airRun) _airRun = GetComponent<AIAirRunNear>();
        //找点 先找身后 再找前面
        float __x;
        if(this.transform.position.x > _obj.transform.position.x)
        {
            if(ChoseNums == 0)
            {
                __x = _obj.transform.position.x - atkDistanceX;
            }
            else
            {
                __x = _obj.transform.position.x + atkDistanceX;
            }
            
        }
        else
        {
            if(ChoseNums == 0)
            {
                __x = _obj.transform.position.x + atkDistanceX;
            }
            else
            {
                __x = _obj.transform.position.x - atkDistanceX;
            }
            
        }

        showPos = new Vector2(__x, _obj.transform.position.y);

        //检测 点 是否 被碰撞 左右 上下 判断  
        if (_airRun.IsHitDiBan(showPos, hitDiBanDistance)|| _airRun.IsHitDiBan(showPos, -hitDiBanDistance)|| _airRun.IsHitDiBan(showPos, hitDiBanDistance,"")|| _airRun.IsHitDiBan(showPos, -hitDiBanDistance, ""))
        {
            ChoseNums++;
            if (ChoseNums == 1)
            {
                GetShow();
            }
            else
            {
                // 没有可以用的点
                ChoseNums = 0;
                //原地 显示
                showPos = this.transform.position;
                return;
            }
            
           
        }

        if (!IsShowing)
        {
            IsShowing = true;
            this.transform.position = showPos;
        }

        //print(" 找到点   "+showPos);

    }

    bool IsOver = false;
    bool IsShowing = false;
    void Showing()
    {
        if (!IsShowing|| IsOver) return;


        if (!_db.animation.HasAnimation(ShowACName))
        {
            IsShowing = false;
            IsOver = true;
            GetComponent<RoleDate>().isCanBeHit = true;
            //print("  没有显示动作  隐身流程走完！！！！  ");
            return;
        }

        //print(" 动作名  "+_db.animation.lastAnimationName);
        //if (_db.animation.lastAnimationName == ShowACName) print(" ??lastAnimationState  " + _db.animation.lastAnimationState+"  isOver? "+IsOver);
        if(_db.animation.lastAnimationName == ShowACName&& _db.animation.isCompleted)
        {
            IsShowing = false;
            
            _db.animation.Stop();
            //_db.animation.GotoAndPlayByFrame("stand_1",0,1);
            //防止闪烁
            _gameBody.GetStand();
            
            //_gameBody.GetStand();
            //_gameBody.GetStand();
            GetComponent<RoleDate>().isCanBeHit = true;
            //tn = 0;
            //print("  隐身流程走完！！！！  ");

            //_gameBody.TurnDirection(_obj);
            GetComponent<AIAirBase>().ZhuanXiang();
            _gameBody.SetACPlayScale(1);

            IsOver = true;

            //if (HideAtkName != "")
            //{
            //    print(" 隐身显示后的攻击  ！！！！！！！     "+ HideAtkName);
            //    GetComponent<AIAirBase>().GetAtkFSByName(HideAtkName);
            //    ReSetAll();
            //}
            
            return;
        }


        



        if (_db.animation.lastAnimationName != ShowACName)
        {
            //print("   显示出来啊1！！！！！！！！   "+_db.animation.lastAnimationName+"    isAcing  "+ _gameBody.isAcing);
            //_db.animation.GotoAndPlayByFrame(ShowACName,0,1);
            //if (_gameBody.isAcing) _gameBody.isAcing = false;
            //print("  test n 进来几次 "+tn);
            //tn++;
            _gameBody.GetAcMsg(ShowACName);
            //print(" 99  "+_db.animation.lastAnimationName+"  --  "+ _gameBody.isAcing);
        }
    }

    //int tn = 0;

    GameObject _obj;
    public void GetStart(GameObject obj)
    {
        //print("yinshen start!!!");
        _obj = obj;
        IsOver = false;
        IsHideing = true;
    }

    public bool IsGetOver()
    {
        //如果可以被攻击 并且 在隐身期间被攻击 的话 重置 跳出
        if (GetComponent<RoleDate>().isBeHiting) {
            IsInBeHitFJ = false;
            ReSetAll();
        }
        
        GetHide();
        Showing();
        
        return IsOver;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
