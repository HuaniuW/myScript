using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChongJiHX : AIChongji
{
    [Header("冲击碰撞块")]
    public GameObject CJhitKuai;
    [Header("怪 自身的 碰撞块")]
    public GameObject ZiShenhitKuai;

    [Header("碰撞块的伤害值")]
    public float SkillPower = 500;

    float _ysAtkPower = 200;



    [Header("横向冲击 距离")]
    public float ChongjiDistance = 0;

    //冲击 起始点
    Vector2 ChongJiStartPos;

    bool IsOutChongjiDistance()
    {
        if (ChongjiDistance == 0) return false;
        if (Mathf.Abs(this.transform.position.x - ChongJiStartPos.x) >= ChongjiDistance) return true;
        return false;
    }



    [Header("音效 横向冲击")]
    public AudioSource S_HXChongJi;


    JN_Date _jnDate;

    protected override void GetInit()
    {
        _jnDate = GetComponent<JN_Date>();
        _ysAtkPower = _jnDate.atkPower;
        if (CJhitKuai.activeSelf) CJhitKuai.SetActive(false);
    }



    bool startTanShe = false;


    protected override void Starting()
    {
        //print("  chongji  starting!!!! ");

        if (_roleDate.isBeHiting)
        {
            print("被攻击了 ！！！！！！！！！！！！   ");
        }
        


        if (_roleDate.isDie || _roleDate.isBeHiting || _targetObj == null || _targetObj.GetComponent<RoleDate>().isDie)
        {
            //print(" over!!! ");
            //ChongjiOver();
            ReSetAll();
            return;
        }

        if (isTanSheing)
        {
            if (!startTanShe && ChongjiYingZhi != 0)
            {
                startTanShe = true;
                if (S_HXChongJi) S_HXChongJi.Play();
                _roleDate.addYZ(ChongjiYingZhi);

            }

            HXTanShe2();
            return;
        }



        if (!IsTongYing&&!isTanSheing && GetNearTarget()) {
            //IsTongYing = true;
            print("   靠近目标！ ");
            isTanSheing = true;
        }




        //if (IsTongYing && GetComponent<AIAirRunNear>().GetMoveNearY(1.6f, 8))
        //{
        //    IsTongYing = false;
        //    if (!isTanSheing)
        //    {
        //        isTanSheing = true;
        //        print("开始 弹射了！！！！！");
        //    }
        //}


    }


    [Header("横向冲击的 距离")]
    public float HXChongjiDis = 20;

    [Header("最大 冲击 速度")]
    public float MaxChongjiSpeed = 20;

    protected virtual void HXTanShe2()
    {
        //print("  HXTanShe2 ");
        if (GetComponent<RoleDate>().isBeHiting || GetComponent<RoleDate>().isDie || GetComponent<GameBody>().IsHitWall)
        {
            //print("  IsHitWall " + GetComponent<GameBody>().IsHitWall + "  ------IsGround  " + GetComponent<GameBody>().IsGround);
            //ChongjiOver();
            ReSetAll();
            return;
        }


        if (!IsStartChongji &&GetComponent<AirGameBody>().GetDB().animation.HasAnimation(ACName_ChongjiBegin) && GetComponent<AirGameBody>().GetDB().animation.HasAnimation(ACName_ChongjiStart))
        {

            //if (!IsStartChongji && !IsGenZongType)
            //{
            //    IsStartChongji = true;
            //}
            float __x = 0;
            if (GetComponent<AirGameBody>().GetDB().animation.lastAnimationName != ACName_ChongjiBegin && GetComponent<AirGameBody>().GetDB().animation.lastAnimationName != ACName_ChongjiStart)
            {

                __x = this.transform.position.x > _targetObj.position.x ? this.transform.position.x - HXChongjiDis : this.transform.position.x + HXChongjiDis;

                targetPos = new Vector2(__x, this.transform.position.y);
                //转向
                //朝向

                _airGameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
                if (this.transform.position.x < targetPos.x)
                {
                    _airGameBody.TurnRight();
                }
                else
                {
                    _airGameBody.TurnLeft();
                }

                if (!CJhitKuai.activeSelf)
                {
                    CJhitKuai.SetActive(true);
                    _jnDate.atkPower = SkillPower;
                }

                if (ZiShenhitKuai.activeSelf) ZiShenhitKuai.SetActive(false);

                //print("wo kao!!!!!!!!!");
                if (GetComponent<JN_Date>()) GetComponent<JN_Date>().HitInSpecialEffectsType = 1;



                if (StartSound) StartSound.Play();
                _airGameBody.isAcing = true;
                //_airGameBody.GetDB().animation.GotoAndStopByFrame(ACName_ChongjiBegin);
                _airGameBody.GetDB().animation.FadeIn(ACName_ChongjiBegin, 0.1f);
                print("start!!!");
                deltaNums = 0;
                return;
            }


            if (GetComponent<AirGameBody>().GetDB().animation.lastAnimationName == ACName_ChongjiBegin)
            {
                CJYanchiNums += Time.deltaTime;
                //print("  CJYanchiNums   " + CJYanchiNums + "   GetComponent<AirGameBody>().GetDB().animation  " + GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);
                if (CJYanchiNums >= CJYanchiTime)
                {
                    _airGameBody.isAcing = true;
                    _airGameBody.GetDB().animation.FadeIn(ACName_ChongjiStart);
                    IsStartChongji = true;

                    if (_targetObj.position.y > this.transform.position.y)
                    {
                        targetPos = new Vector2(targetPos.x, this.transform.position.y + 2);
                    }
                    else if (_targetObj.position.y < this.transform.position.y)
                    {
                        targetPos = new Vector2(targetPos.x, this.transform.position.y - 2);
                    }
                }
                return;
            }

          
        }

        //print("lastAnimationName   " + _airGameBody.GetDB().animation.lastAnimationName+ "   isAcing    "+ _airGameBody.isAcing);
        //print("  是否撞墙   "+ _airGameBody.IsHitWall+"  持续时间      "+ deltaNums+ "  _tsTimes   "+ _tsTimes);
        deltaNums += Time.deltaTime;
        if (deltaNums >= _tsTimes || IsOutChongjiDistance() || GetComponent<AIAirRunNear>().MoveToPoint(targetPos, 0.1f, chongjiSpeed, false, false, MaxChongjiSpeed) || _airGameBody.IsHitWall)
        {
            //if (deltaNums >= _tsTimes) print("  超出 冲击 时间   ");
            //if (IsOutChongjiDistance()) print("超出 冲击 距离结束");
            //if (_airGameBody.IsHitWall) print("撞墙 结束！！！");
            //print("**************************************************到达目标Ian附近 结束！！！");

            //print(">>>>????????  冲击over  " + "  @@@@@@  deltaNums   " + deltaNums + "  _tsTimes  " + _tsTimes);
            if (CJhitKuai) CJhitKuai.SetActive(false);
            ChongjiOver();
            if (!ZiShenhitKuai.activeSelf) ZiShenhitKuai.SetActive(true);
            //ReSetAll();
        }

        CJYanmu.Play();

    }



    public override void GetStart(GameObject targetObj)
    {
        if (_targetObj == null) _targetObj = targetObj.transform;

        if (GlobalTools.GetDistanceByTowPoint(this.transform.position, _targetObj.transform.position) < 2)
        {
            ReSetAll();
            //ChongjiOver();
            return;
        }

        isGetOver = false;
        
        isStarting = true;
        //print("this.transform.position：   " + this.transform.position);
    }


    protected override bool GetNearTarget()
    {
        _targetPos = GetTongYZuoBiao();
        //print("  _atkDistance@@@@@@@@@   "+ _atkDistance);
        return runNear.ZhuijiZuoBiao(_targetPos,_atkDistance);
    }


    float __x = 0;
    float __y = 0;
    Vector2 _targetPos = Vector2.zero;
    private Vector2 GetTongYZuoBiao()
    {
        //在左还是右
        if (this.transform.position.x> _targetObj.position.x)
        {
            //我在右
            __x = _targetObj.position.x + _atkDistance-0.8f;
            
        }
        else
        {
            //我在左
            __x = _targetObj.position.x - _atkDistance + 0.8f;
        }
        __y = _targetObj.position.y+0.4f;

        if(this.transform.position.y< _targetObj.position.y)
        {
            __y = _targetObj.position.y + 0.8f;
        }
        else
        {
            __y = _targetObj.position.y - 0.8f;
        }


        return new Vector2(__x,__y);
    }

    public override void ReSetAll()
    {
        ChongjiOver();
        if (CJhitKuai) CJhitKuai.SetActive(false);
        if (!ZiShenhitKuai.activeSelf) ZiShenhitKuai.SetActive(true);
        _jnDate.atkPower = _ysAtkPower;
        IsTongYing = false;
        deltaNums = 0;
        CJYanmu.Stop();
        if (ChongjiYingZhi != 0) _roleDate.hfYZ(ChongjiYingZhi);
        isStarting = false;
        isGetOver = true;
        isTanSheing = false;
        IsStartChongji = false;
        CJYanchiNums = 0;
        runNear.ResetAll();
        _airGameBody.SetACingfalse();

        _airGameBody.GetDB().animation.timeScale = 1f;
        startTanShe = false;
        if (GetComponent<JN_Date>()) GetComponent<JN_Date>().HitInSpecialEffectsType = 5;

    }


    //protected override void ChongjiOver()
    //{
    //    if (CJhitKuai.activeSelf) CJhitKuai.SetActive(false);
    //    if (!ZiShenhitKuai.activeSelf) ZiShenhitKuai.SetActive(true);
    //    _jnDate.atkPower = _ysAtkPower;
    //    IsTongYing = false;
    //    deltaNums = 0;
    //    CJYanmu.Stop();
    //    if (ChongjiYingZhi != 0) _roleDate.hfYZ(ChongjiYingZhi);
    //    //CJYanmu.loop = false;
    //    isStarting = false;
    //    isGetOver = true;
    //    isTanSheing = false;


    //    IsStartChongji = false;
    //    CJYanchiNums = 0;
    //    runNear.ResetAll();



    //    startTanShe = false;
    //    //print("*************************************************************冲击 结束！！！！！");
    //}


   

    protected override void Tanshe()
    {
        //print("   IsHitWall:     "+ GetComponent<GameBody>().IsHitWall+ "       @@@@@ IsGround        "+ GetComponent<GameBody>().IsGround);

        //print("@@@@@@@@@@@@@@@@@@@@@弹射中！！！！！！！");
        if (_roleDate.isBeHiting || _roleDate.isDie|| _airGameBody.IsHitWall)
        {
            print("撞墙了！！！！");
            ReSetAll();
            //ChongjiOver();
            return;
        }


        //做起始动作
        //起始动作完成 弹射 做第二个动作
        //完成后 还原动作
        if (_airGameBody.GetDB().animation.HasAnimation(ACName_ChongjiBegin) && _airGameBody.GetDB().animation.HasAnimation(ACName_ChongjiStart))
        {
            if (_airGameBody.GetDB().animation.lastAnimationName != ACName_ChongjiBegin && _airGameBody.GetDB().animation.lastAnimationName != ACName_ChongjiStart)
            {
                //转向 朝向玩家
                _airGameBody.GetAcMsg(ACName_ChongjiBegin);
                _airGameBody.GetDB().animation.Stop();
                _airGameBody.GetStop();
                deltaNums = 0;
                return;
            }


            if (_airGameBody.GetDB().animation.lastAnimationName == ACName_ChongjiBegin)
            {
                CJYanchiNums += Time.deltaTime;
                //print("  CJYanchiNums   " + CJYanchiNums + "   GetComponent<AirGameBody>().GetDB().animation  " + GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);
                if (CJYanchiNums >= CJYanchiTime)
                {
                    _airGameBody.GetAcMsg(ACName_ChongjiStart);
                    if (!_airGameBody.GetDB().animation.isPlaying) _airGameBody.GetDB().animation.Play();
                }
                return;
            }
        }

        //print("ac   "+ GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);


        if (!IsStartChongji && !IsGenZongType)
        {
            IsStartChongji = true;
            targetPos = _targetObj.position;
            ChongJiStartPos = this.transform.position;
            //这个点 要在靠后一点
            float dis = GlobalTools.GetDistanceByTowPoint(targetPos, transform.position);
            //float _d = 4;
            //float __x1 = targetPos.x - _d * (this.transform.position.x - targetPos.x) / dis;
            //float __y1 = targetPos.y - _d * (this.transform.position.y - targetPos.y) / dis;

            float moveDistance = 0;
            if(_atkDistance + 3> dis)
            {
                moveDistance = dis + 5;
            }
            else
            {
                moveDistance = _atkDistance + 5;
            }

            float __x1 = this.transform.position.x>targetPos.x? this.transform.position.x- moveDistance-5 : this.transform.position.x + moveDistance+5;
            float __y1 = this.transform.position.y;

            targetPos = new Vector2(__x1, __y1);

            //朝向
            if (this.transform.position.x < targetPos.x)
            {
                _airGameBody.TurnRight();
            }
            else
            {
                _airGameBody.TurnLeft();
            }

            if (!CJhitKuai.activeSelf) {
                CJhitKuai.SetActive(true);
                _jnDate.atkPower = SkillPower;
            }
            
            if (ZiShenhitKuai.activeSelf) ZiShenhitKuai.SetActive(false);

            //print("wo kao!!!!!!!!!");
            if (GetComponent<JN_Date>()) GetComponent<JN_Date>().HitInSpecialEffectsType = 1;
        }

        if (IsGenZongType) targetPos = _targetObj.position;


        //print(" 5  "+ this.transform.position);
        deltaNums += Time.deltaTime;


       

        if (deltaNums >= _tsTimes|| IsOutChongjiDistance()||GetComponent<AIAirRunNear>().ZhijieMoveToPoint(targetPos, 0.1f, chongjiSpeed, false, false)||_airGameBody.IsHitWall)
        {
            //print(">>>>????????  冲击over  " + "  @@@@@@  deltaNums   " + deltaNums + "  _tsTimes  " + _tsTimes);
            if(CJhitKuai) CJhitKuai.SetActive(false);
            ChongjiOver();
            if (!ZiShenhitKuai.activeSelf) ZiShenhitKuai.SetActive(true);
            //ReSetAll();
        }

        CJYanmu.Play();
    }
}
