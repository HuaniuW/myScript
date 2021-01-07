using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChongJiHX : AIChongji
{
    protected override void Starting()
    {
        if (GetComponent<RoleDate>().isDie || _targetObj == null || _targetObj.GetComponent<RoleDate>().isDie)
        {
            ReSetAll();
            return;
        }

        if (!isTanSheing && GetNearTarget()) IsTongYing = true;

        if (isTanSheing) {
            Tanshe();
            return;
        }

        if (IsTongYing && GetComponent<AIAirRunNear>().GetMoveNearY(0.2f, 5)) {
            isTanSheing = true;
            IsTongYing = false;
        }
        
        
    }


    public override void GetStart(GameObject targetObj)
    {
        if (_targetObj == null) _targetObj = targetObj.transform;

        if (GlobalTools.GetDistanceByTowPoint(this.transform.position, _targetObj.transform.position) < 2)
        {
            ReSetAll();
            ChongjiOver();
            return;
        }

        isGetOver = false;
        if (ChongjiYingZhi != 0) GetComponent<RoleDate>().addYZ(ChongjiYingZhi);
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

        return new Vector2(__x,__y);
    }


    protected override void Tanshe()
    {
        //print("   IsHitWall:     "+ GetComponent<GameBody>().IsHitWall+ "       @@@@@ IsGround        "+ GetComponent<GameBody>().IsGround);

        //print("@@@@@@@@@@@@@@@@@@@@@弹射中！！！！！！！");
        if (GetComponent<RoleDate>().isBeHiting || GetComponent<RoleDate>().isDie||GetComponent<GameBody>().IsHitWall)
        {
            print("撞墙了！！！！");
            ReSetAll();
            ChongjiOver();
            return;
        }


        //做起始动作
        //起始动作完成 弹射 做第二个动作
        //完成后 还原动作
        if (GetComponent<AirGameBody>().GetDB().animation.HasAnimation("chongji_begin") && GetComponent<AirGameBody>().GetDB().animation.HasAnimation("chongji_start"))
        {
            if (GetComponent<AirGameBody>().GetDB().animation.lastAnimationName != "chongji_begin" && GetComponent<AirGameBody>().GetDB().animation.lastAnimationName != "chongji_start")
            {
                //转向 朝向玩家
                GetComponent<AirGameBody>().GetAcMsg("chongji_begin");
                GetComponent<AirGameBody>().GetDB().animation.Stop();
                GetComponent<AirGameBody>().GetStop();
                deltaNums = 0;
                return;
            }


            if (GetComponent<AirGameBody>().GetDB().animation.lastAnimationName == "chongji_begin")
            {
                CJYanchiNums += Time.deltaTime;
                //print("  CJYanchiNums   " + CJYanchiNums + "   GetComponent<AirGameBody>().GetDB().animation  " + GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);
                if (CJYanchiNums >= CJYanchiTime)
                {
                    GetComponent<AirGameBody>().GetAcMsg("chongji_start");
                    if (!GetComponent<AirGameBody>().GetDB().animation.isPlaying) GetComponent<AirGameBody>().GetDB().animation.Play();
                }
                return;
            }
        }

        //print("ac   "+ GetComponent<AirGameBody>().GetDB().animation.lastAnimationName);


        if (!IsStartChongji && !IsGenZongType)
        {
            IsStartChongji = true;
            targetPos = _targetObj.position;
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

            float __x1 = this.transform.position.x>targetPos.x? this.transform.position.x- moveDistance : this.transform.position.x + moveDistance;
            float __y1 = this.transform.position.y;

            targetPos = new Vector2(__x1, __y1);
        }

        if (IsGenZongType) targetPos = _targetObj.position;


        //print(" 5  "+ this.transform.position);
        deltaNums += Time.deltaTime;

        if (deltaNums >= _tsTimes || GetComponent<AIAirRunNear>().ZhijieMoveToPoint(targetPos, 0.1f, chongjiSpeed, false, false))
        {
            //print(">>>>????????  冲击over  " + "  @@@@@@  deltaNums   " + deltaNums + "  _tsTimes  " + _tsTimes);
            ChongjiOver();
        }

        CJYanmu.Play();
    }
}
