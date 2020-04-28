using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAirGoToAndAC : MonoBehaviour
{

    //几种类型

    //2是用来 下压的    怎么过去 1.飞过去（控制 移动速度） 2.隐身过去 
    [Header("类型 1.固定位置点 2.在玩家上方多高")]
    public int TypeNum = 1;

    //eg    2_3|-3_4|7_9    
    [Header("类型1 用 固定点数组 字符串")]
    public string PosArr = "";


    GameObject _obj;
    public void GetStart(GameObject obj)
    {
        _obj = obj;
        IsOver = false;
        ChosePos();
    }


    Vector2 chosePos = new Vector2(1000,1000);
    //固定坐标 list
    List<Vector2> gudingZBList = new List<Vector2>();
    void ChosePos()
    {
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
        }
        //运动到目标点

    }

    bool IsStartMove = false;

    //选取一个 位置点
    Vector2 ChoseAndCheckPointInList()
    {
        //随机找一个点 如果这个点 碰撞了 直接over不重调了
        int n = GlobalTools.GetRandomNum(gudingZBList.Count);
        print("  随机数n：  "+n+ "  找点 啊      gudingZBList.Count  "+ gudingZBList.Count+"  ? 第一个点位置  "+ gudingZBList[0]);
        Vector2 _v2 = gudingZBList[n];
        if (!IsPointHitWall(_v2)) {
            return _v2;
        }
        
        return new Vector2(1000,1000);
    }

    //检测 选定点   是否撞到墙壁
    AIAirRunNear _aiRunNear;
    public float TcDistance = 2;
    bool IsPointHitWall(Vector2 pos) {
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
        //print("   IsStartMove "+ IsStartMove);

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
                if (_aiRunNear.ZhuijiPointZuoBiao(chosePos, 0.4f, MoveSpeed))
                {
                    StartAtkAC();
                }
            }
            else
            {
                //print("   zhijie yi dong!!! MoveSpeed     " + MoveSpeed);
                if (_aiRunNear.ZhijieMoveToPoint(chosePos, 0.4f, MoveSpeed))
                {
                    
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

                //GetComponent<AIAirBase>().GetReSet();
                //GetComponent<AIAirBase>().GetAtkFSByName(AtkACName);
                ReSetAll();
            }
            
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
