﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAirGoToAndAC : MonoBehaviour
{

    //几种类型

    //2是用来 下压的    怎么过去 1.飞过去（控制 移动速度） 2.隐身过去 
    [Header("类型 1.固定位置点 2.在玩家上方多高 3.同Y固定X 4.围绕一个点给出位置 5.在玩家前方固定距离")]
    public int TypeNum = 1;

    //eg    2_3|-3_4|7_9    
    [Header("类型1 用 固定点数组 字符串")]
    public string PosArr = "";



    [Header("类型2 用 固定点数组 字符串")]
    public List<GameObject> PointArr = new List<GameObject>() { };


    GameObject _obj;
    public void GetStart(GameObject obj)
    {
        _obj = obj;
        IsOver = false;
        ChosePos();
        if (!_aiRunNear) _aiRunNear = GetComponent<AIAirRunNear>();
    }


    [Header("是否需要判断 击碎地板")]
    public float ToDisX = 4;
    public float ToDisY = 4;


    [Header("是否需要判断 击碎地板")]
    public bool IsNeedCheckDiBan = false;
    [Header("下压 可以 压碎 的 地板")]
    public GameObject DiBan;
    [Header("判断 地板后 的 nums")]
    public int CheckDiBanHouNum = 3;
    [Header("中心点位置")]
    public Transform CenterPos;

    [Header("离 中心点位置 的随机距离最大值")]
    public float ToCenterPosAutoDistance = 1;



    Vector2 chosePos = new Vector2(1000,1000);
    //固定坐标 list
    List<Vector2> gudingZBList = new List<Vector2>();
    void ChosePos()
    {
        if (IsNeedCheckDiBan)
        {
            if (!DiBan||!DiBan.activeSelf|| DiBan.GetComponent<XSDiban>().IsHideFloor)
            {
                TypeNum = CheckDiBanHouNum;
            }
        }


        //print(" gt ----typeNum  "+ TypeNum);
        //typeNum = 1 就是4个随机点  2就是 在玩家的x 处 的固定高位 可以下压
        if(TypeNum == 1)
        {
            if(PosArr == "")
            {
               
                IsOver = true;

                return;
            }
            else
            {
                //==0 就是第一次 进来 找点 
                if(gudingZBList.Count == 0)
                {
                    string[] tempZT = PosArr.Split('|');
                    if (tempZT.Length == 1)
                    {
                        chosePos = new Vector2(float.Parse(tempZT[0].Split('_')[0]), float.Parse(tempZT[0].Split('_')[1]));
                        print(" --------> goto pos   "+ chosePos);
                        gudingZBList.Add(chosePos);
                    }
                    else
                    {
                        foreach (string str in tempZT)
                        {
                            Vector2 v2 = new Vector2(float.Parse(str.Split('_')[0]), float.Parse(str.Split('_')[1]));
                            gudingZBList.Add(v2);
                        }
                    }
                }

                //开始 找点
                chosePos = ChoseAndCheckPointInList();
                print(" goto寻找到的 点     "+ chosePos);
                if (chosePos==new Vector2(1000,1000))
                {
                    IsOver = true;
                    return;
                }
                IsStartMove = true;
            }
        }else if(TypeNum == 2)
        {
            //在玩家上方多高
        }else if (TypeNum == 3)
        {
            //与玩家 同Y 固定点 x
            gudingZBList.Clear();
            Vector2 v2 = new Vector2(CenterPos.position.x, _obj.transform.position.y);
            gudingZBList.Add(v2);
            chosePos = ChoseAndCheckPointInList();
            IsStartMove = true;
        }else if (TypeNum == 4)
        {
            //围绕给出 的一个 点 在随机距离内 生成 点 适合 空旷地带
            gudingZBList.Clear();

            float _dx = GlobalTools.GetRandomDistanceNums(ToCenterPosAutoDistance);    //GlobalTools.GetRandomNum()>50?GlobalTools.GetRandomDistanceNums(ToCenterPosAutoDistance):
            float _dy = GlobalTools.GetRandomDistanceNums(ToCenterPosAutoDistance);
            _dx = GlobalTools.GetRandomNum() > 50 ? _dx : -_dx;
            _dy = GlobalTools.GetRandomNum() > 50 ? _dy : -_dy;

            Vector2 v2 = new Vector2(CenterPos.position.x+_dx, CenterPos.position.y+_dy);
            gudingZBList.Add(v2);
            chosePos = ChoseAndCheckPointInList();
            IsStartMove = true;
        }else if (TypeNum == 5)
        {
            //直接飞到 玩家 最近的 攻击点  前上方 一点 固定X距离
            float _toX = 0;
            float _toY = 0;

            float _toPlayerDisX = ToDisX;
            float _toPlayerDisY = ToDisY;

            if(this.transform.position.x> _obj.transform.position.x)
            {
                _toX = _obj.transform.position.x + _toPlayerDisX;
                _toY = _obj.transform.position.y + _toPlayerDisY;
            }
            else
            {
                _toX = _obj.transform.position.x - _toPlayerDisX;
                _toY = _obj.transform.position.y + _toPlayerDisY;
            }

            chosePos = new Vector2(_toX, _toY);
            //print("  move     chosePos " + chosePos);
            IsStartMove = true;
            //if (!IsPointHitWall(v2))
            //{
            //    IsStartMove = true;
            //}

        }else if (TypeNum == 6)
        {
            //固定 点 数组
            if (PointArr.Count != 0)
            {
                foreach (GameObject o in PointArr)
                {
                    gudingZBList.Add(o.transform.position);
                }

                //开始 找点
                chosePos = ChoseAndCheckPointInList();
                print(" goto寻找到的 点     " + chosePos);
                if (chosePos == new Vector2(1000, 1000))
                {
                    IsOver = true;
                    return;
                }
                IsStartMove = true;
            }
            else
            {
                IsOver = true;
            }
        }
        //运动到目标点

    }

    bool IsStartMove = false;

    //选取一个 位置点
    Vector2 ChoseAndCheckPointInList()
    {
        //随机找一个点 如果这个点 碰撞了 直接over不重调了
        int n = GlobalTools.GetRandomNum(gudingZBList.Count);
        //print("  随机数n：  "+n+ "  找点 啊      gudingZBList.Count  "+ gudingZBList.Count+"  ? 第一个点位置  "+ gudingZBList[0]);
        Vector2 _v2 = gudingZBList[n];
        if (!IsPointHitWall(_v2)) {
            return _v2;
        }
        
        return new Vector2(1000,1000);
    }

    //检测 选定点   是否撞到墙壁
    AIAirRunNear _aiRunNear;
    //探测 点 4个方向距离 防止 有墙
    public float TcDistance = 2;
    protected bool IsPointHitWall(Vector2 pos) {
        if (!_aiRunNear) _aiRunNear = GetComponent<AIAirRunNear>();
        if (_aiRunNear == null) return true;
        //检测 点是否撞墙
        if(_aiRunNear.IsHitDiBan(pos, TcDistance) || _aiRunNear.IsHitDiBan(pos, -TcDistance)|| _aiRunNear.IsHitDiBan(pos, TcDistance,"")|| _aiRunNear.IsHitDiBan(pos, -TcDistance, "")){
            return true;
        }
        return false;
    }



    //移动类型  隐身  闪现  飞过去（飞的速度）
    [Header("移动类型 1.飞过去  2.闪现  3.隐身")]
    public int MoveType = 1;
    [Header("移动速度")]
    [Range(2, 6)]
    public float MoveSpeed = 5;

    //是否有 相应的 粒子要播放
    [Header("移动 触发的 粒子特效")]
    public ParticleSystem MoveLizi;

    [Header("默认是A*寻路 其次是直接 直接容易撞 要在简单地形用")]
    public bool IsAXunlu = true;

    AIYinshen _yinshen;
    void MoveToPos()
    {

        if (!IsStartMove) return;
        //print("  ---------------------------- IsStartMove "+ IsStartMove);

        if (GetComponent<RoleDate>().isBeHiting|| GetComponent<RoleDate>().isDie)
        {
            ReSetAll();
            return;
        }


        
        //移动过去 
        if (MoveType == 1)
        {
            //A*寻路
            if (IsAXunlu)
            {
                if (_aiRunNear.ZhuijiPointZuoBiao(chosePos, 1, MoveSpeed))
                {
                    StartAtkAC();
                }
            }
            else
            {
                //print("   zhijie yi dong!!! MoveSpeed     " + MoveSpeed);
                if (_aiRunNear.ZhijieMoveToPoint(chosePos, 1, MoveSpeed))
                {
                    print(" dao da mubiao dian weizhi !!! ");
                    StartAtkAC();
                }
            }



        }
        else if (MoveType == 2)
        {
            //闪现过去
            this.transform.position = chosePos;
            StartAtkAC();
        }
        else if (MoveType == 3)
        {
            //隐身过去
            if (!IsYinSheng)
            {
                IsYinSheng = true;
                _yinshen.TempStart();
            }
            if (IsShowing)
            {
                this.transform.position = chosePos;
                if (_yinshen.IsShowOut())
                {
                    StartAtkAC();
                    _yinshen.ReSetAll();
                }
                return;
            }
            if (_yinshen.IsGetHide()) IsShowing = true;
            
          
        }
    }

    bool IsYinSheng = false;
    bool IsShowing = false;

    bool IsStartAtkAC = false;
    void StartAtkAC()
    {
        IsStartMove = false;
        IsStartAtkAC = true;
        StartAtking();
    }

    public string AtkACName = "";
    void StartAtking()
    {
        if (!IsStartAtkAC) return;

        //不 揉近攻击了  攻击 在招式数组后面添加  这里只做移动处理


        if (AtkACName != "")
        {
            if(AtkACName.Split('_')[0] == "atk")
            {
                //普通攻击
                ReSetAll();
            }
            else
            {
                //技能攻击
                print("隐身 技能攻击");
                //GetComponent<AIAirBase>().GetReSet();
                //GetComponent<AIAirBase>().GetAtkFSByName(AtkACName);
                ReSetAll();
                //print(">>>GotoAndAC Over!!");
            }

        }
        else
        {
            print("GotoAndAC Over!!");
            ReSetAll();
        }
        
    }


    //到达目的地后 攻击动作
    //是否有攻击动作
    //攻击动作 是否 去掉 moveNear  到目标点 直接攻击  不再寻找位置




    public void ReSetAll()
    {
        chosePos = new Vector2(1000,1000);
        IsStartMove = false;
        IsOver = true;
        IsShowing = false;
        IsYinSheng = false;
        IsStartAtkAC = false;

        if (_yinshen) _yinshen.ReSetAll();
        if (_aiRunNear) _aiRunNear.ResetAll();
    }


    bool IsOver = false;
    public bool IsGetOver()
    {
        return IsOver;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (!_yinshen) _yinshen = GetComponent<AIYinshen>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPos();
        //StartAtking();
    }
}
