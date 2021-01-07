﻿using System.Collections;
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


    public RectTransform xuetiao;

    void HideXuetiao()
    {
        if (xuetiao)
        {
            xuetiao.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void ShowXuetiao()
    {
        if (xuetiao)
        {
            xuetiao.GetComponent<CanvasGroup>().alpha = 1;
        }
    }



    [Header("是否开启 被攻击 隐身")]
    public bool IsOpenBeHitHide = false;

    [Header("被攻击后 隐身 反击的 招式名字 ")]
    public string HideAtkName = "";

    [Header("被攻击 隐身 几率")]
    public int BeHitHideJL = 30;

    [Header("后退的力量 ")]
    public float houshanValue = 30;

    bool IsBeHitingHide = false;

    //float BeHitYingshenJiShi = 0;
    ////被攻击隐身 重置 时间上限
    //float YinShenjishiCZTime = 1;
    void YinshenXianzhiJishi()
    {
        if (IsBeHitingHide&&!GetComponent<RoleDate>().isBeHiting)
        {
            IsBeHitingHide = false;
        }
    }



    //被攻击后 隐身后退
    public void BeHitHide()
    {
        if (!IsOpenBeHitHide) return;
       
        if (IsBeHitingHide) return;
        if (GetComponent<RoleDate>().isDie) return;
        IsBeHitingHide = true;
        //if (IsBeHitingHide &&!GetComponent<RoleDate>().isBeHiting)
        //{
        //    IsBeHitingHide = false;
        //}
        //计算几率
        int jl = GlobalTools.GetRandomNum();
        print("/////////////////////////////@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@---------------------------------->   jl    " + jl + "        " + BeHitHideJL);
        if (jl<=BeHitHideJL)
        {
            if (YinShenType == 2)
            {
                ShowYinShenTX();
            }


            IsInBeHitFJ = true;
            //重置 被攻击的判断
            _gameBody.FanJiBeHitReSet();
            //清0速度
            _gameBody.SpeedXStop();
            //后退速度  用后闪的 接口
            _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
            _gameBody.BackJumpVX(houshanValue);
            GetComponent<RoleDate>().isCanBeHit = false;
            ShowXuetiao();
            //隐身 接口
            print("------------------被攻击后 隐身");
            GetComponent<AIAirBase>().GetAtkFSByName("yinshen");
            ReSetAll();
        }
        

    }

    bool IsInBeHitFJ = false;
    //被攻击后 反击 AC
    public void BeHitFanjiAC()
    {
        if (IsInBeHitFJ)
        {
            //print("  被攻击 反击   "+ HideAtkName);

            if (HideAtkName != "") {
                GetComponent<AIAirBase>().GetReSet();
                GetComponent<AIAirBase>().GetAtkFSByName(HideAtkName);
            }
            
        }
        else
        {
            //print("  自带的 反击 动作   " + ATKACName);
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
        ShowXuetiao();

        YinShenJiShi = 0;
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
        if(IsGetHide()) GetShow();
    }

    public void TempStart()
    {
        if (!IsHideing) IsHideing = true;
    }


    [Header("隐身类型")]
    public int YinShenType = 1;

    [Header("隐身时候播放的 粒子")]
    public string LiZi_yinshen_name = "TX_yinshenYM1";
    GameObject _yanmu;
    bool IsStartYinShen = false;


    [Header("隐身时候 播放的声音")]
    public AudioSource AudioYinShen;


    //隐身时长 计时
    float YinShenTimes = 1;
    float YinShenJiShi = 0;

    //隐身出现位置 类型

    void ShowYinShenTX()
    {
        if (_yanmu == null)
        {
            _yanmu = ObjectPools.GetInstance().SwpanObject2(Resources.Load(LiZi_yinshen_name) as GameObject);
            _yanmu.transform.position = this.transform.position;
        }
        else
        {
            _yanmu.transform.position = this.transform.position;
            _yanmu.GetComponent<ParticleSystem>().Play();
        }

        if (AudioYinShen != null) AudioYinShen.Play();
        //如果做 细节  还有怪物叫声
    }




    public bool IsGetHide()
    {
        if (!IsHideing) return false;

        //这里按类型 分类 如果是 替身术 有烟幕的 另外写 
        if (YinShenType == 2)
        {
            if (!IsStartYinShen)
            {
                IsStartYinShen = true;
                _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
                HideXuetiao();
                if (!IsCanBeHit)
                {
                    GetComponent<RoleDate>().isCanBeHit = false;
                }
                ShowYinShenTX();
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+1000);
            }

            YinShenJiShi += Time.deltaTime;
            if(YinShenJiShi>= YinShenTimes)
            {
                YinShenJiShi = 0;
                IsHideing = false;
                return true;
            }

            return false;
        }



        //print("  hide ????  ");
        if (_db.animation.lastAnimationName == HideACName && _db.animation.isCompleted)
        {
            IsHideing = false;
            _db.animation.Stop();
            //开始显示流程
            return true;
        }

        if (_db.animation.lastAnimationName != HideACName)
        {
            //print("yinshenAC!");
            //_db.animation.GotoAndPlayByFrame(HideACName, 0, 1);
            _gameBody.GetAcMsg(HideACName);
            _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
            _gameBody.SetACPlayScale(YsSpeed);
            HideXuetiao();
            if (!IsCanBeHit)
            {
                GetComponent<RoleDate>().isCanBeHit = false;
            }
        }

        return false;
    }


    public bool IsShowOut()
    {
        //if (!IsShowing) return false;


        if(YinShenType == 2)
        {
            ShowYinShenTX();



            _db.animation.Stop();
            _gameBody.GetStand();
            GetComponent<AIAirBase>().ZhuanXiang();
            IsShowing = false;
            GetComponent<RoleDate>().isCanBeHit = true;
            return true;
        }






        if (!_db.animation.HasAnimation(ShowACName))
        {
            IsShowing = false;
            GetComponent<RoleDate>().isCanBeHit = true;
            //print("  没有显示动作  隐身流程走完！！！！  ");
            return true;
        }

        //print(1);

        if (_db.animation.lastAnimationName == ShowACName && _db.animation.isCompleted)
        {
            IsShowing = false;

            //print(2);

            _db.animation.Stop();
            //防止闪烁
            _gameBody.GetStand();
            GetComponent<RoleDate>().isCanBeHit = true;
            GetComponent<AIAirBase>().ZhuanXiang();
            _gameBody.SetACPlayScale(1);
            //print("     ----->   显示? ????");
            ShowXuetiao();
            return true;
        }

        if (_db.animation.lastAnimationName != ShowACName)
        {
            _gameBody.GetAcMsg(ShowACName);
            //最好是在 动作里面做短
            _gameBody.SetACPlayScale(showACSpeed);
        }
        return false;
    }




    Vector2 showPos = new Vector2(1000,1000);
    float hitDiBanDistance = 0.4f;
    AIAirRunNear _airRun;
    //计数2次 多次调用自己
    int ChoseNums = 0;
    void GetShow()
    {
        //显示时候 可以释放特效 替身术 等

        //隐身 去哪  1.玩家头顶  2.玩家前后





        if (!_airRun) _airRun = GetComponent<AIAirRunNear>();
        //找点 先找身后 再找前面
        float __x;
        float __y;

        if (YinShenType == 2)
        {
            __x = GlobalTools.GetRandomNum()>50? thePlayer.transform.position.x - atkDistanceX: thePlayer.transform.position.x + atkDistanceX;
            __y = thePlayer.transform.position.y - 1 + GlobalTools.GetRandomDistanceNums(4);
        }
        else
        {
            if (this.transform.position.x > thePlayer.transform.position.x)
            {
                if (ChoseNums == 0)
                {
                    __x = thePlayer.transform.position.x - atkDistanceX;
                }
                else
                {
                    __x = thePlayer.transform.position.x + atkDistanceX;
                }

            }
            else
            {
                if (ChoseNums == 0)
                {
                    __x = thePlayer.transform.position.x + atkDistanceX;
                }
                else
                {
                    __x = thePlayer.transform.position.x - atkDistanceX;
                }

            }

            __y = thePlayer.transform.position.y;
        }


       

        showPos = new Vector2(__x, __y);

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
        if (IsShowOut()) IsOver = true;
    }

    [Header("显示时候 动作速度")]
    public float showACSpeed = 1;
    //int tn = 0;

    GameObject thePlayer;
    public void GetStart(GameObject obj)
    {
        //print("yinshen start!!!");
        thePlayer = obj;
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
        YinshenXianzhiJishi();
    }
}
